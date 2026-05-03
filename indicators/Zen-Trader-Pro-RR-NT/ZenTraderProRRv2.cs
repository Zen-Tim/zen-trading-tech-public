#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
	/// <summary>
	/// ZenTraderProRRv2 v1.6 · 20260503
	///
	/// Middle-click: R-multiples from Close (bar colour = direction).
	/// Shift+middle-click: R-multiples from High/Low +/-1 tick.
	/// Ctrl+middle-click: fixed tick distances from Close.
	/// Click same bar again (same mode) to remove.
	/// Multiple rulers and modes can coexist on the same bar.
	///
	/// Version History:
	///   v1.6   20260503  Added Shift+middle-click mode: entry at bar
	///                    High+1tick (bull) / Low-1tick (bear), stop at
	///                    opposite extreme +/-1tick. R-multiples projected
	///                    from that wider range. All three modes coexist.
	///   v1.5.1 20260503  Reverted R-multiple direction to bar colour.
	///   v1.5   20260503  Ctrl+middle-click fixed distance mode.
	///   v1.4   20260418  Visual overhaul.
	///   v1.3   20260418  GetBarIdxByX + DPI + TriggerCustomEvent.
	///   v1.2   20260407  Bar colour direction (working).
	///   v1.1   20260407  Click-price direction (broken).
	///   v1.0   20260407  Initial build.
	/// </summary>
	public class ZenTraderProRRv2 : Indicator
	{
		#region Private types

		private enum RulerMode
		{
			RMultiple,		// Middle-click: entry at Close, R-multiple targets
			RHighLow,		// Shift+middle-click: entry at High/Low +1 tick, R-multiple targets
			FixedDistance	// Ctrl+middle-click: fixed tick distances from Close
		}

		private class RulerInfo
		{
			public int			BarIndex;
			public bool			IsLong;
			public RulerMode	Mode;
			public double		EntryPrice;
			public double		StopPrice;
			public double		HalfStopPrice;
			public double		R1Price;
			public double		R2Price;
			public double		R3Price;
			public double		R4Price;
			public double		StopDistance;
			// Fixed distance mode — up to 3 levels
			public double		FD1Price;
			public double		FD2Price;
			public double		FD3Price;
		}

		#endregion

		#region Private fields

		private readonly List<RulerInfo>	rulers		= new List<RulerInfo>();

		// SharpDX DirectWrite
		private TextFormat					textFormat;

		// SharpDX Direct2D brushes
		private SharpDX.Direct2D1.Brush		dxStopBrush;
		private SharpDX.Direct2D1.Brush		dxEntryBrush;
		private SharpDX.Direct2D1.Brush		dxR1Brush;
		private SharpDX.Direct2D1.Brush		dxR2Brush;
		private SharpDX.Direct2D1.Brush		dxR3Brush;
		private SharpDX.Direct2D1.Brush		dxR4Brush;
		private SharpDX.Direct2D1.Brush		dxTextBrush;
		private SharpDX.Direct2D1.Brush		dxWarningBrush;
		private SharpDX.Direct2D1.Brush		dxLabelBgBrush;
		private SharpDX.Direct2D1.Brush		dxFDBrush;

		private bool						mouseSubscribed;

		#endregion

		#region Properties — 1. Entry / Stop Level

		[NinjaScriptProperty]
		[Display(Name = "Use Close/Open for Stop", Description = "Use bar Close/Open instead of High/Low for stop placement",
			Order = 1, GroupName = "1. Entry / Stop Level")]
		public bool UseCloseOpen { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Use Close/Open for Entry", Description = "Use bar Close/Open instead of High/Low for entry placement",
			Order = 2, GroupName = "1. Entry / Stop Level")]
		public bool UseCloseOpenEntry { get; set; }

		#endregion

		#region Properties — 2. Display

		[NinjaScriptProperty]
		[Display(Name = "Font", Description = "Label font face and size",
			Order = 1, GroupName = "2. Display")]
		public SimpleFont LabelFont { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Convert Ticks to Points", Description = "Show distances as points instead of ticks (useful for instruments where 1 tick != 1 point)",
			Order = 2, GroupName = "2. Display")]
		public bool ConvertTicksToPoints { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show Currency Value", Description = "Append currency P&L to labels (e.g. +EUR 26.00)",
			Order = 3, GroupName = "2. Display")]
		public bool ShowCurrency { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show Distance", Description = "Show distance value on labels (ticks or points)",
			Order = 4, GroupName = "2. Display")]
		public bool ShowTicks { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show Price", Description = "Show price level on labels",
			Order = 5, GroupName = "2. Display")]
		public bool ShowPrice { get; set; }

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name = "Amount of Contracts", Description = "Contract count for currency P&L calculation",
			Order = 6, GroupName = "2. Display")]
		public int ContractAmount { get; set; }

		#endregion

		#region Properties — 3. Ruler

		[NinjaScriptProperty]
		[Range(1, 100)]
		[Display(Name = "Ruler Width #Bars", Description = "Lines extend this many bars to the right of the clicked bar",
			Order = 1, GroupName = "3. Ruler")]
		public int RulerWidth { get; set; }

		[NinjaScriptProperty]
		[Range(0, 100)]
		[Display(Name = "Stop Adjust Ticks", Description = "Tick offset added beyond stop level (pushes stop further from entry)",
			Order = 2, GroupName = "3. Ruler")]
		public int StopAdjustTicks { get; set; }

		[NinjaScriptProperty]
		[Range(0, 100)]
		[Display(Name = "Entry Adjust Ticks", Description = "Tick offset added beyond entry level (pushes entry further from stop)",
			Order = 3, GroupName = "3. Ruler")]
		public int EntryAdjustTicks { get; set; }

		[NinjaScriptProperty]
		[Range(0, 10000)]
		[Display(Name = "Max. Risk Ticks", Description = "Show warning if stop distance exceeds this many ticks (0 = disabled)",
			Order = 4, GroupName = "3. Ruler")]
		public int MaxRiskTicks { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show #1 Line", Description = "Show R-multiple target line #1",
			Order = 5, GroupName = "3. Ruler")]
		public bool ShowR1 { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show #2 Line", Description = "Show R-multiple target line #2",
			Order = 6, GroupName = "3. Ruler")]
		public bool ShowR2 { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show #3 Line", Description = "Show R-multiple target line #3",
			Order = 7, GroupName = "3. Ruler")]
		public bool ShowR3 { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show #4 Line", Description = "Show R-multiple target line #4",
			Order = 8, GroupName = "3. Ruler")]
		public bool ShowR4 { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show Stop", Description = "Show stop line",
			Order = 9, GroupName = "3. Ruler")]
		public bool ShowStop { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Show Stop #2 (Half)", Description = "Show half-stop line between entry and stop",
			Order = 10, GroupName = "3. Ruler")]
		public bool ShowHalfStop { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "Allow Multiple Rulers", Description = "When OFF, each new click replaces the previous ruler. When ON, rulers stack and you click the same bar to remove.",
			Order = 11, GroupName = "3. Ruler")]
		public bool AllowMultipleRulers { get; set; }

		#endregion

		#region Properties — 4. Ruler Configuration

		[NinjaScriptProperty]
		[Display(Name = "#1 Multiplier", Description = "R-multiple for line #1",
			Order = 1, GroupName = "4. Ruler Configuration")]
		public double R1Multiplier { get; set; }

		[NinjaScriptProperty]
		[Range(0, 1000)]
		[Display(Name = "#1 Adjustment (Ticks)", Description = "Tick adjustment for line #1",
			Order = 2, GroupName = "4. Ruler Configuration")]
		public int R1Adjust { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "#2 Multiplier", Description = "R-multiple for line #2",
			Order = 3, GroupName = "4. Ruler Configuration")]
		public double R2Multiplier { get; set; }

		[NinjaScriptProperty]
		[Range(0, 1000)]
		[Display(Name = "#2 Adjustment (Ticks)", Description = "Tick adjustment for line #2",
			Order = 4, GroupName = "4. Ruler Configuration")]
		public int R2Adjust { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "#3 Multiplier", Description = "R-multiple for line #3",
			Order = 5, GroupName = "4. Ruler Configuration")]
		public double R3Multiplier { get; set; }

		[NinjaScriptProperty]
		[Range(0, 1000)]
		[Display(Name = "#3 Adjustment (Ticks)", Description = "Tick adjustment for line #3",
			Order = 6, GroupName = "4. Ruler Configuration")]
		public int R3Adjust { get; set; }

		[NinjaScriptProperty]
		[Display(Name = "#4 Multiplier", Description = "R-multiple for line #4",
			Order = 7, GroupName = "4. Ruler Configuration")]
		public double R4Multiplier { get; set; }

		[NinjaScriptProperty]
		[Range(0, 1000)]
		[Display(Name = "#4 Adjustment (Ticks)", Description = "Tick adjustment for line #4",
			Order = 8, GroupName = "4. Ruler Configuration")]
		public int R4Adjust { get; set; }

		#endregion

		#region Properties — 5. Lines

		[XmlIgnore]
		[Display(Name = "Color Stop", Description = "Stop line and label background colour",
			Order = 1, GroupName = "5. Lines")]
		public System.Windows.Media.Brush StopBrush { get; set; }

		[Browsable(false)]
		public string StopBrushSerialize
		{
			get { return Serialize.BrushToString(StopBrush); }
			set { StopBrush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Entry", Description = "Entry line and label background colour",
			Order = 2, GroupName = "5. Lines")]
		public System.Windows.Media.Brush EntryBrush { get; set; }

		[Browsable(false)]
		public string EntryBrushSerialize
		{
			get { return Serialize.BrushToString(EntryBrush); }
			set { EntryBrush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Line #1", Description = "Line #1 colour",
			Order = 3, GroupName = "5. Lines")]
		public System.Windows.Media.Brush R1Brush { get; set; }

		[Browsable(false)]
		public string R1BrushSerialize
		{
			get { return Serialize.BrushToString(R1Brush); }
			set { R1Brush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Line #2", Description = "Line #2 colour",
			Order = 4, GroupName = "5. Lines")]
		public System.Windows.Media.Brush R2Brush { get; set; }

		[Browsable(false)]
		public string R2BrushSerialize
		{
			get { return Serialize.BrushToString(R2Brush); }
			set { R2Brush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Line #3", Description = "Line #3 colour",
			Order = 5, GroupName = "5. Lines")]
		public System.Windows.Media.Brush R3Brush { get; set; }

		[Browsable(false)]
		public string R3BrushSerialize
		{
			get { return Serialize.BrushToString(R3Brush); }
			set { R3Brush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Line #4", Description = "Line #4 colour",
			Order = 6, GroupName = "5. Lines")]
		public System.Windows.Media.Brush R4Brush { get; set; }

		[Browsable(false)]
		public string R4BrushSerialize
		{
			get { return Serialize.BrushToString(R4Brush); }
			set { R4Brush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Text Color", Description = "Label text colour (used inside label boxes)",
			Order = 7, GroupName = "5. Lines")]
		public System.Windows.Media.Brush TextBrush { get; set; }

		[Browsable(false)]
		public string TextBrushSerialize
		{
			get { return Serialize.BrushToString(TextBrush); }
			set { TextBrush = Serialize.StringToBrush(value); }
		}

		[XmlIgnore]
		[Display(Name = "Color Fixed Distance", Description = "Fixed distance lines colour (Ctrl+click mode)",
			Order = 8, GroupName = "5. Lines")]
		public System.Windows.Media.Brush FDBrush { get; set; }

		[Browsable(false)]
		public string FDBrushSerialize
		{
			get { return Serialize.BrushToString(FDBrush); }
			set { FDBrush = Serialize.StringToBrush(value); }
		}

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width Stop", Order = 10, GroupName = "5. Lines")]
		public int StopWidth { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width Entry", Order = 11, GroupName = "5. Lines")]
		public int EntryWidth { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width #1 Line", Order = 12, GroupName = "5. Lines")]
		public int R1Width { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width #2 Line", Order = 13, GroupName = "5. Lines")]
		public int R2Width { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width #3 Line", Order = 14, GroupName = "5. Lines")]
		public int R3Width { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10)]
		[Display(Name = "Width #4 Line", Order = 15, GroupName = "5. Lines")]
		public int R4Width { get; set; }

		#endregion

		#region Properties — 6. Label Style

		[NinjaScriptProperty]
		[Display(Name = "Show Label Background", Description = "Draw a filled rectangle behind each label",
			Order = 1, GroupName = "6. Label Style")]
		public bool ShowLabelBackground { get; set; }

		[NinjaScriptProperty]
		[Range(0.05, 1.0)]
		[Display(Name = "Label Background Opacity", Description = "Opacity of label background boxes (0.05 = nearly invisible, 1.0 = solid)",
			Order = 2, GroupName = "6. Label Style")]
		public double LabelBackgroundOpacity { get; set; }

		[NinjaScriptProperty]
		[Range(0.05, 1.0)]
		[Display(Name = "Label Text Opacity", Description = "Opacity of label text (0.05 = nearly invisible, 1.0 = solid)",
			Order = 3, GroupName = "6. Label Style")]
		public double LabelTextOpacity { get; set; }

		[NinjaScriptProperty]
		[Range(0.05, 1.0)]
		[Display(Name = "Line Opacity", Description = "Opacity of ruler lines (0.05 = nearly invisible, 1.0 = solid)",
			Order = 4, GroupName = "6. Label Style")]
		public double LineOpacity { get; set; }

		[NinjaScriptProperty]
		[Range(0, 20)]
		[Display(Name = "Label Padding", Description = "Padding inside label background in pixels",
			Order = 5, GroupName = "6. Label Style")]
		public int LabelPadding { get; set; }

		[NinjaScriptProperty]
		[Range(0, 10)]
		[Display(Name = "Corner Radius", Description = "Rounded corner radius of label background (0 = square)",
			Order = 6, GroupName = "6. Label Style")]
		public int LabelCornerRadius { get; set; }

		[NinjaScriptProperty]
		[Range(0, 200)]
		[Display(Name = "Label Offset", Description = "Gap in pixels between the label box and the bar (higher = further left from bar)",
			Order = 7, GroupName = "6. Label Style")]
		public int LabelOffset { get; set; }

		#endregion

		#region Properties — 7. Fixed Distance (Ctrl+Click)

		[NinjaScriptProperty]
		[Range(1, 10000)]
		[Display(Name = "Distance #1 (Ticks)", Description = "First fixed distance in ticks from Close (Ctrl+middle-click mode)",
			Order = 1, GroupName = "7. Fixed Distance (Ctrl+Click)")]
		public int FD1Ticks { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10000)]
		[Display(Name = "Distance #2 (Ticks)", Description = "Second fixed distance in ticks from Close",
			Order = 2, GroupName = "7. Fixed Distance (Ctrl+Click)")]
		public int FD2Ticks { get; set; }

		[NinjaScriptProperty]
		[Range(1, 10000)]
		[Display(Name = "Distance #3 (Ticks)", Description = "Third fixed distance in ticks from Close",
			Order = 3, GroupName = "7. Fixed Distance (Ctrl+Click)")]
		public int FD3Ticks { get; set; }

		#endregion

		#region State management

		protected override void OnStateChange()
		{
			switch (State)
			{
				case State.SetDefaults:
					Description		= "Middle-click: R-multiples from Close. Shift+middle-click: R-multiples from High/Low. Ctrl+middle-click: fixed distances from Close. Direction from bar colour. Click again to remove.";
					Name			= "ZenTraderProRRv2";
					IsOverlay		= true;
					IsSuspendedWhileInactive = true;
					DrawOnPricePanel = true;

					// 1. Entry / Stop Level
					UseCloseOpen		= false;
					UseCloseOpenEntry	= true;

					// 2. Display
					LabelFont			= new SimpleFont("Arial", 12);
					ConvertTicksToPoints = true;
					ShowCurrency		= false;
					ShowTicks			= true;
					ShowPrice			= false;
					ContractAmount		= 1;

					// 3. Ruler
					RulerWidth			= 4;
					StopAdjustTicks		= 1;
					EntryAdjustTicks	= 0;
					MaxRiskTicks		= 0;
					ShowR1				= true;
					ShowR2				= true;
					ShowR3				= true;
					ShowR4				= false;
					ShowStop			= true;
					ShowHalfStop		= false;
					AllowMultipleRulers	= false;

					// 4. Ruler Configuration
					R1Multiplier	= 0.5;
					R1Adjust		= 0;
					R2Multiplier	= 1.0;
					R2Adjust		= 0;
					R3Multiplier	= 2.0;
					R3Adjust		= 0;
					R4Multiplier	= 3.0;
					R4Adjust		= 0;

					// 5. Lines
					StopBrush	= Brushes.Red;
					EntryBrush	= Brushes.SlateGray;
					R1Brush		= Brushes.SlateGray;
					R2Brush		= Brushes.SlateGray;
					R3Brush		= Brushes.SlateGray;
					R4Brush		= Brushes.SlateGray;
					TextBrush	= Brushes.White;
					FDBrush		= Brushes.DodgerBlue;

					StopWidth	= 1;
					EntryWidth	= 1;
					R1Width		= 1;
					R2Width		= 1;
					R3Width		= 1;
					R4Width		= 1;

					// 6. Label Style
					ShowLabelBackground		= true;
					LabelBackgroundOpacity	= 0.85;
					LabelTextOpacity		= 1.0;
					LineOpacity				= 1.0;
					LabelPadding			= 4;
					LabelCornerRadius		= 2;
					LabelOffset				= 20;

					// 7. Fixed Distance
					FD1Ticks	= 20;
					FD2Ticks	= 40;
					FD3Ticks	= 80;
					break;

				case State.Historical:
					if (ChartControl != null && !mouseSubscribed)
					{
						ChartControl.Dispatcher.InvokeAsync(() =>
						{
							if (ChartControl != null)
							{
								ChartControl.MouseDown += OnChartMouseDown;
								mouseSubscribed = true;
							}
						});
					}
					break;

				case State.Terminated:
					if (ChartControl != null && mouseSubscribed)
					{
						ChartControl.Dispatcher.InvokeAsync(() =>
						{
							if (ChartControl != null)
							{
								ChartControl.MouseDown -= OnChartMouseDown;
								mouseSubscribed = false;
							}
						});
					}

					DisposeDxResources();

					if (textFormat != null)
					{
						textFormat.Dispose();
						textFormat = null;
					}
					break;
			}
		}

		protected override void OnBarUpdate() { }

		#endregion

		#region Mouse handling

		private void OnChartMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton != MouseButton.Middle)
				return;

			if (ChartControl == null || ChartBars == null || Bars == null)
				return;

			ChartPanel panel = ChartPanel;
			if (panel == null)
				return;

			System.Windows.Point clickPoint = e.GetPosition(ChartControl as IInputElement);

			if (clickPoint.Y < panel.Y || clickPoint.Y > panel.Y + panel.H)
				return;

			// DPI-aware bar index lookup
			int dpX = (int)ChartingExtensions.ConvertToHorizontalPixels(clickPoint.X,
				ChartControl.PresentationSource);

			int barIdx = ChartBars.GetBarIdxByX(ChartControl, dpX);

			if (barIdx < ChartBars.FromIndex)
				barIdx = ChartBars.FromIndex;
			else if (barIdx > ChartBars.ToIndex)
				barIdx = ChartBars.ToIndex;

			// DPI-aware Y for direction detection
			int dpY = (int)ChartingExtensions.ConvertToVerticalPixels(clickPoint.Y,
				ChartControl.PresentationSource);

			// Detect modifier keys
			bool isCtrl  = (Keyboard.Modifiers & ModifierKeys.Control) != 0;
			bool isShift = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;

			int capturedBarIdx = barIdx;
			int capturedDpY = dpY;

			// Determine which mode to use
			RulerMode mode;
			if (isCtrl)
				mode = RulerMode.FixedDistance;
			else if (isShift)
				mode = RulerMode.RHighLow;
			else
				mode = RulerMode.RMultiple;

			TriggerCustomEvent(o =>
			{
				ProcessClick(capturedBarIdx, capturedDpY, mode);
			}, null);

			e.Handled = true;
		}

		/// <summary>
		/// Determine direction from click position relative to bar midpoint.
		/// Uses DPI-converted device pixel Y coordinate.
		/// </summary>
		private bool IsClickAboveMidpoint(int barIdx, int devicePixelY)
		{
			double barHigh = Bars.GetHigh(barIdx);
			double barLow  = Bars.GetLow(barIdx);
			double barMid  = (barHigh + barLow) / 2.0;

			ChartPanel panel = ChartPanel;
			if (panel == null || panel.H <= 0)
				return true;

			// Convert bar midpoint price to device pixel Y
			// Panel maps: top = MaxValue, bottom = MinValue
			double priceRange = panel.MaxValue - panel.MinValue;
			if (priceRange <= 0)
				return true;

			// Convert panel Y to device pixels for comparison
			int panelTopDp = (int)ChartingExtensions.ConvertToVerticalPixels(panel.Y,
				ChartControl.PresentationSource);
			int panelHDp = (int)ChartingExtensions.ConvertToVerticalPixels(panel.H,
				ChartControl.PresentationSource);

			double midFraction = (panel.MaxValue - barMid) / priceRange;
			int midPixelY = panelTopDp + (int)(panelHDp * midFraction);

			// Lower device pixel Y = higher on screen = higher price
			return devicePixelY < midPixelY;
		}

		private void ProcessClick(int barIdx, int devicePixelY, RulerMode mode)
		{
			// Toggle: if clicking the same bar with the same mode, remove it
			int existingIdx = rulers.FindIndex(r => r.BarIndex == barIdx && r.Mode == mode);
			if (existingIdx >= 0)
			{
				rulers.RemoveAt(existingIdx);
				ChartControl.InvalidateVisual();
				ForceRefresh();
				return;
			}

			// Single-ruler mode: clear all existing rulers before adding new one
			if (!AllowMultipleRulers && rulers.Count > 0)
				rulers.Clear();

			if (mode == RulerMode.FixedDistance)
				ProcessFixedDistanceClick(barIdx, devicePixelY);
			else if (mode == RulerMode.RHighLow)
				ProcessRHighLowClick(barIdx);
			else
				ProcessRMultipleClick(barIdx, devicePixelY);
		}

		private void ProcessRMultipleClick(int barIdx, int devicePixelY)
		{
			double barHigh	= Bars.GetHigh(barIdx);
			double barLow	= Bars.GetLow(barIdx);
			double barOpen	= Bars.GetOpen(barIdx);
			double barClose	= Bars.GetClose(barIdx);
			double tickSize = Instrument.MasterInstrument.TickSize;

			// Direction from bar colour — the proven reliable method.
			// Click-position pixel math failed in v1.0, v1.1, and v1.5.
			// Bull bar (Close >= Open) = long, bear bar = short.
			bool isLong = barClose >= barOpen;

			double entryPrice, stopPrice;

			if (isLong)
			{
				entryPrice = UseCloseOpenEntry ? Math.Max(barOpen, barClose) : barHigh;
				stopPrice  = UseCloseOpen      ? Math.Min(barOpen, barClose) : barLow;
				entryPrice += EntryAdjustTicks * tickSize;
				stopPrice  -= StopAdjustTicks * tickSize;
			}
			else
			{
				entryPrice = UseCloseOpenEntry ? Math.Min(barOpen, barClose) : barLow;
				stopPrice  = UseCloseOpen      ? Math.Max(barOpen, barClose) : barHigh;
				entryPrice -= EntryAdjustTicks * tickSize;
				stopPrice  += StopAdjustTicks * tickSize;
			}

			entryPrice = Instrument.MasterInstrument.RoundToTickSize(entryPrice);
			stopPrice  = Instrument.MasterInstrument.RoundToTickSize(stopPrice);

			double stopDistance = Math.Abs(entryPrice - stopPrice);
			int direction = isLong ? 1 : -1;

			var ruler = new RulerInfo
			{
				BarIndex		= barIdx,
				IsLong			= isLong,
				Mode			= RulerMode.RMultiple,
				EntryPrice		= entryPrice,
				StopPrice		= stopPrice,
				HalfStopPrice	= Instrument.MasterInstrument.RoundToTickSize(entryPrice - direction * stopDistance * 0.5),
				StopDistance	= stopDistance,
				R1Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R1Multiplier + R1Adjust * tickSize)),
				R2Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R2Multiplier + R2Adjust * tickSize)),
				R3Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R3Multiplier + R3Adjust * tickSize)),
				R4Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R4Multiplier + R4Adjust * tickSize)),
			};

			rulers.Add(ruler);
			ChartControl.InvalidateVisual();
			ForceRefresh();
		}

		/// <summary>
		/// Shift+middle-click: Entry at bar extreme (High+1tick for bull, Low-1tick for bear),
		/// stop at opposite extreme (Low-1tick for bull, High+1tick for bear).
		/// Direction from bar colour. R-multiples projected from the wider range.
		/// </summary>
		private void ProcessRHighLowClick(int barIdx)
		{
			double barHigh	= Bars.GetHigh(barIdx);
			double barLow	= Bars.GetLow(barIdx);
			double barOpen	= Bars.GetOpen(barIdx);
			double barClose	= Bars.GetClose(barIdx);
			double tickSize = Instrument.MasterInstrument.TickSize;

			// Direction from bar colour
			bool isLong = barClose >= barOpen;

			double entryPrice, stopPrice;

			if (isLong)
			{
				// Long from High + 1 tick, stop at Low - 1 tick
				entryPrice = barHigh + tickSize;
				stopPrice  = barLow  - tickSize;
			}
			else
			{
				// Short from Low - 1 tick, stop at High + 1 tick
				entryPrice = barLow  - tickSize;
				stopPrice  = barHigh + tickSize;
			}

			entryPrice = Instrument.MasterInstrument.RoundToTickSize(entryPrice);
			stopPrice  = Instrument.MasterInstrument.RoundToTickSize(stopPrice);

			double stopDistance = Math.Abs(entryPrice - stopPrice);
			int direction = isLong ? 1 : -1;

			var ruler = new RulerInfo
			{
				BarIndex		= barIdx,
				IsLong			= isLong,
				Mode			= RulerMode.RHighLow,
				EntryPrice		= entryPrice,
				StopPrice		= stopPrice,
				HalfStopPrice	= Instrument.MasterInstrument.RoundToTickSize(entryPrice - direction * stopDistance * 0.5),
				StopDistance	= stopDistance,
				R1Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R1Multiplier + R1Adjust * tickSize)),
				R2Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R2Multiplier + R2Adjust * tickSize)),
				R3Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R3Multiplier + R3Adjust * tickSize)),
				R4Price			= Instrument.MasterInstrument.RoundToTickSize(entryPrice + direction * (stopDistance * R4Multiplier + R4Adjust * tickSize)),
			};

			rulers.Add(ruler);
			ChartControl.InvalidateVisual();
			ForceRefresh();
		}

		private void ProcessFixedDistanceClick(int barIdx, int devicePixelY)
		{
			double barOpen  = Bars.GetOpen(barIdx);
			double barClose = Bars.GetClose(barIdx);
			double tickSize = Instrument.MasterInstrument.TickSize;

			// Direction from bar colour — same as the other two modes
			bool isLong = barClose >= barOpen;
			int direction = isLong ? 1 : -1;

			var ruler = new RulerInfo
			{
				BarIndex	= barIdx,
				IsLong		= isLong,
				Mode		= RulerMode.FixedDistance,
				EntryPrice	= barClose,
				StopPrice	= barClose,	// not used, but keep consistent
				StopDistance = 0,
				FD1Price	= Instrument.MasterInstrument.RoundToTickSize(barClose + direction * FD1Ticks * tickSize),
				FD2Price	= Instrument.MasterInstrument.RoundToTickSize(barClose + direction * FD2Ticks * tickSize),
				FD3Price	= Instrument.MasterInstrument.RoundToTickSize(barClose + direction * FD3Ticks * tickSize),
			};

			rulers.Add(ruler);
			ChartControl.InvalidateVisual();
			ForceRefresh();
		}

		#endregion

		#region SharpDX rendering

		public override void OnRenderTargetChanged()
		{
			DisposeDxResources();

			if (RenderTarget == null)
				return;

			dxStopBrush		= StopBrush.ToDxBrush(RenderTarget);
			dxEntryBrush	= EntryBrush.ToDxBrush(RenderTarget);
			dxR1Brush		= R1Brush.ToDxBrush(RenderTarget);
			dxR2Brush		= R2Brush.ToDxBrush(RenderTarget);
			dxR3Brush		= R3Brush.ToDxBrush(RenderTarget);
			dxR4Brush		= R4Brush.ToDxBrush(RenderTarget);
			dxTextBrush		= TextBrush.ToDxBrush(RenderTarget);
			dxWarningBrush	= Brushes.OrangeRed.ToDxBrush(RenderTarget);
			dxFDBrush		= FDBrush.ToDxBrush(RenderTarget);

			if (textFormat != null)
				textFormat.Dispose();

			textFormat = LabelFont.ToDirectWriteTextFormat();
		}

		protected override void OnRender(ChartControl chartControl, ChartScale chartScale)
		{
			base.OnRender(chartControl, chartScale);

			if (RenderTarget == null || chartControl == null || chartScale == null)
				return;
			if (rulers.Count == 0)
				return;
			if (textFormat == null || dxTextBrush == null)
				return;

			RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.PerPrimitive;

			double tickSize		= Instrument.MasterInstrument.TickSize;
			double pointValue	= Instrument.MasterInstrument.PointValue;
			string currency		= Instrument.MasterInstrument.Currency.ToString();

			foreach (var ruler in rulers)
			{
				if (ruler.BarIndex < ChartBars.FromIndex || ruler.BarIndex > ChartBars.ToIndex)
					continue;

				float barX			= (float)chartControl.GetXByBarIndex(ChartBars, ruler.BarIndex);
				int rightBarIdx		= Math.Min(ChartBars.ToIndex, ruler.BarIndex + RulerWidth);
				float lineStartX	= barX;
				float lineEndX		= (float)chartControl.GetXByBarIndex(ChartBars, rightBarIdx);

				float panelRight = ChartPanel.X + ChartPanel.W;
				if (lineEndX > panelRight)
					lineEndX = panelRight;

				if (ruler.Mode == RulerMode.FixedDistance)
				{
					// --- Fixed Distance mode ---
					// Entry line (Close)
					{
						float y = (float)chartScale.GetYByValue(ruler.EntryPrice);
						string label = ShowPrice ? Instrument.MasterInstrument.FormatPrice(ruler.EntryPrice) : "";
						DrawRulerLine(lineStartX, lineEndX, y, dxEntryBrush, EntryWidth, label, dxEntryBrush);
					}

					// FD1
					{
						float y = (float)chartScale.GetYByValue(ruler.FD1Price);
						double dist = FD1Ticks * tickSize;
						string label = FormatLabel(ruler.FD1Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxFDBrush, 1, label, dxFDBrush);
					}

					// FD2
					{
						float y = (float)chartScale.GetYByValue(ruler.FD2Price);
						double dist = FD2Ticks * tickSize;
						string label = FormatLabel(ruler.FD2Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxFDBrush, 1, label, dxFDBrush);
					}

					// FD3
					{
						float y = (float)chartScale.GetYByValue(ruler.FD3Price);
						double dist = FD3Ticks * tickSize;
						string label = FormatLabel(ruler.FD3Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxFDBrush, 1, label, dxFDBrush);
					}
				}
				else
				{
					// --- R-Multiple mode ---
					if (ShowStop)
					{
						float y = (float)chartScale.GetYByValue(ruler.StopPrice);
						string label = FormatLabel(ruler.StopPrice, ruler.StopDistance, tickSize, pointValue, currency, true);
						DrawRulerLine(lineStartX, lineEndX, y, dxStopBrush, StopWidth, label, dxStopBrush);
					}

					if (ShowHalfStop)
					{
						float y = (float)chartScale.GetYByValue(ruler.HalfStopPrice);
						string label = FormatLabel(ruler.HalfStopPrice, ruler.StopDistance * 0.5, tickSize, pointValue, currency, true);
						DrawRulerLine(lineStartX, lineEndX, y, dxStopBrush, 1, label, dxStopBrush);
					}

					{
						float y = (float)chartScale.GetYByValue(ruler.EntryPrice);
						string label = ShowPrice ? Instrument.MasterInstrument.FormatPrice(ruler.EntryPrice) : "";
						DrawRulerLine(lineStartX, lineEndX, y, dxEntryBrush, EntryWidth, label, dxEntryBrush);
					}

					if (ShowR1)
					{
						float y = (float)chartScale.GetYByValue(ruler.R1Price);
						double dist = Math.Abs(ruler.R1Price - ruler.EntryPrice);
						string label = FormatLabel(ruler.R1Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxR1Brush, R1Width, label, dxR1Brush);
					}

					if (ShowR2)
					{
						float y = (float)chartScale.GetYByValue(ruler.R2Price);
						double dist = Math.Abs(ruler.R2Price - ruler.EntryPrice);
						string label = FormatLabel(ruler.R2Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxR2Brush, R2Width, label, dxR2Brush);
					}

					if (ShowR3)
					{
						float y = (float)chartScale.GetYByValue(ruler.R3Price);
						double dist = Math.Abs(ruler.R3Price - ruler.EntryPrice);
						string label = FormatLabel(ruler.R3Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxR3Brush, R3Width, label, dxR3Brush);
					}

					if (ShowR4 && R4Multiplier > 0)
					{
						float y = (float)chartScale.GetYByValue(ruler.R4Price);
						double dist = Math.Abs(ruler.R4Price - ruler.EntryPrice);
						string label = FormatLabel(ruler.R4Price, dist, tickSize, pointValue, currency, false);
						DrawRulerLine(lineStartX, lineEndX, y, dxR4Brush, R4Width, label, dxR4Brush);
					}

					if (MaxRiskTicks > 0)
					{
						double stopDistTicks = ruler.StopDistance / tickSize;
						if (stopDistTicks > MaxRiskTicks)
						{
							float warningY = (float)chartScale.GetYByValue(ruler.EntryPrice);
							string warningText = string.Format("! Stop {0:F0}t > Max {1}", stopDistTicks, MaxRiskTicks);

							using (var layout = new TextLayout(Core.Globals.DirectWriteFactory, warningText, textFormat, 300, 20))
							{
								RenderTarget.DrawTextLayout(
									new Vector2(lineEndX + 5, warningY - 20),
									layout, dxWarningBrush, DrawTextOptions.NoSnap);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Draw a horizontal ruler line extending right, with a boxed label to the LEFT.
		/// Layout: [label box] ---bar---line extends right--->
		/// </summary>
		private void DrawRulerLine(float x1, float x2, float y,
			SharpDX.Direct2D1.Brush lineBrush, int lineWidth,
			string label, SharpDX.Direct2D1.Brush labelBgBrush)
		{
			if (lineBrush == null || textFormat == null || dxTextBrush == null)
				return;

			float oldLineOpacity = lineBrush.Opacity;
			lineBrush.Opacity = (float)LineOpacity;

			RenderTarget.DrawLine(
				new Vector2(x1, y),
				new Vector2(x2, y),
				lineBrush, lineWidth);

			lineBrush.Opacity = oldLineOpacity;

			if (!string.IsNullOrEmpty(label))
			{
				using (var layout = new TextLayout(Core.Globals.DirectWriteFactory, label, textFormat, 300, textFormat.FontSize + 4))
				{
					float textW = layout.Metrics.Width;
					float textH = layout.Metrics.Height;
					float pad   = LabelPadding;
					float boxW  = textW + pad * 2;
					float boxH  = textH + pad * 2;

					float boxX  = x1 - boxW - LabelOffset;
					float boxY  = y - boxH / 2f;

					if (boxX < ChartPanel.X)
						boxX = x1 + 2;

					if (ShowLabelBackground && labelBgBrush != null)
					{
						float oldBgOpacity = labelBgBrush.Opacity;
						labelBgBrush.Opacity = (float)LabelBackgroundOpacity;

						if (LabelCornerRadius > 0)
						{
							var roundedRect = new RoundedRectangle
							{
								Rect = new SharpDX.RectangleF(boxX, boxY, boxW, boxH),
								RadiusX = LabelCornerRadius,
								RadiusY = LabelCornerRadius,
							};
							RenderTarget.FillRoundedRectangle(roundedRect, labelBgBrush);
						}
						else
						{
							RenderTarget.FillRectangle(
								new SharpDX.RectangleF(boxX, boxY, boxW, boxH),
								labelBgBrush);
						}

						labelBgBrush.Opacity = oldBgOpacity;
					}

					float oldTextOpacity = dxTextBrush.Opacity;
					dxTextBrush.Opacity = (float)LabelTextOpacity;

					RenderTarget.DrawTextLayout(
						new Vector2(boxX + pad, boxY + pad),
						layout, dxTextBrush, DrawTextOptions.NoSnap);

					dxTextBrush.Opacity = oldTextOpacity;
				}
			}
		}

		#endregion

		#region Label formatting

		private string FormatLabel(double price, double distance, double tickSize, double pointValue, string currency, bool isRisk)
		{
			var parts = new List<string>();

			if (ShowPrice)
				parts.Add(Instrument.MasterInstrument.FormatPrice(price));

			if (ShowTicks && distance > 0)
			{
				string sign = isRisk ? "-" : "+";
				if (ConvertTicksToPoints)
					parts.Add(sign + distance.ToString("F0"));
				else
				{
					double ticks = distance / tickSize;
					parts.Add(sign + ticks.ToString("F0"));
				}
			}

			if (ShowCurrency && distance > 0)
			{
				double pnl = (distance / tickSize) * (tickSize * pointValue) * ContractAmount;
				string sign = isRisk ? "-" : "+";
				parts.Add(sign + currency + " " + pnl.ToString("F2"));
			}

			if (parts.Count == 0 && distance > 0)
			{
				string sign = isRisk ? "-" : "+";
				parts.Add(sign + distance.ToString("F0"));
			}

			return string.Join(" | ", parts);
		}

		#endregion

		#region DX resource management

		private void DisposeDxResources()
		{
			if (dxStopBrush		!= null) { dxStopBrush.Dispose();	dxStopBrush		= null; }
			if (dxEntryBrush	!= null) { dxEntryBrush.Dispose();	dxEntryBrush	= null; }
			if (dxR1Brush		!= null) { dxR1Brush.Dispose();		dxR1Brush		= null; }
			if (dxR2Brush		!= null) { dxR2Brush.Dispose();		dxR2Brush		= null; }
			if (dxR3Brush		!= null) { dxR3Brush.Dispose();		dxR3Brush		= null; }
			if (dxR4Brush		!= null) { dxR4Brush.Dispose();		dxR4Brush		= null; }
			if (dxTextBrush		!= null) { dxTextBrush.Dispose();	dxTextBrush		= null; }
			if (dxWarningBrush	!= null) { dxWarningBrush.Dispose(); dxWarningBrush	= null; }
			if (dxLabelBgBrush	!= null) { dxLabelBgBrush.Dispose(); dxLabelBgBrush	= null; }
			if (dxFDBrush		!= null) { dxFDBrush.Dispose();		dxFDBrush		= null; }
		}

		#endregion
	}
}
