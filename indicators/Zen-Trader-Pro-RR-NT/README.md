# Zen Trader Pro RR — R/R Ruler (NinjaTrader 8)

A free, open-source risk/reward ruler for NinjaTrader 8. Single `.cs` file — drop it into your indicators folder, compile, and use.

Three click modes for measuring a bar's trade potential. Direction is automatic — the indicator reads bar colour so you don't have to think about it. All three modes can coexist on the same bar simultaneously.

---

## What It Does

### Mode 1: Middle-Click — Buy/Sell the Close

Middle-click any bar. Bull bar = long, bear bar = short. Entry at the Close, stop at the opposite extreme (Low for longs, High for shorts) plus 1 tick adjustment. Projects R0.5, R1, and R2 targets from that entry/stop range.

This is the BTC (Buy the Close) / STC (Sell the Close) scenario. You're asking: if I got long at the close of that bar with my stop at the low, what did R1 and R2 look like?

### Mode 2: Shift+Middle-Click — Buy Stop / Sell Stop

Hold Shift, then middle-click. Direction still from bar colour. Entry goes to High + 1 tick (bull bar) or Low − 1 tick (bear bar). Stop at the opposite extreme + 1 tick. R-multiples project from the full bar range.

This is the breakout entry scenario — wider stop, wider range, same R-multiple logic. Useful for seeing where R1 and R2 sit before a breakout move happens.

### Mode 3: Ctrl+Middle-Click — Fixed Distance Targets

Hold Ctrl, then middle-click. No entry/stop logic. Projects three fixed tick distances from the Close in the direction of bar colour. Defaults: 20, 40, and 80 ticks — all configurable.

Quick measured move check. Set the distances once and get a one-click answer on any bar.

---

## Stacking and Removing Rulers

**Allow Multiple Rulers** is OFF by default — each new click replaces the previous ruler, keeping the chart clean.

Turn it ON and rulers stack. You can have all three modes on the same bar at once. Click the same bar with the same mode to toggle it off.

---

## Settings

| Group | Setting | Default | Notes |
|---|---|---|---|
| Display | Convert Ticks to Points | ON | Shows clean point distances. Turn OFF for instruments where 1 tick ≠ 1 point (e.g. CL, ZB) |
| Display | Show Currency Value | OFF | Appends currency P&L to labels using instrument point value × contract count |
| Display | Show Distance | ON | Shows tick or point distance on each label |
| Display | Show Price | OFF | Shows price level on each label |
| Display | Amount of Contracts | 1 | Contract count for currency P&L calculation |
| Ruler | Ruler Width #Bars | 4 | How far lines extend to the right of the clicked bar |
| Ruler | Stop Adjust Ticks | 1 | Tick buffer beyond stop level |
| Ruler | Entry Adjust Ticks | 0 | Tick buffer beyond entry level |
| Ruler | Max Risk Ticks | 0 | Shows a warning if stop distance exceeds this value (0 = disabled) |
| Ruler | Allow Multiple Rulers | OFF | Stack rulers or replace-on-click |
| Ruler Config | #1 Multiplier | 0.5 | R0.5 target |
| Ruler Config | #2 Multiplier | 1.0 | R1 target |
| Ruler Config | #3 Multiplier | 2.0 | R2 target |
| Ruler Config | #4 Multiplier | 3.0 | R4 (off by default) |
| Lines | Color Stop | Red | |
| Lines | Color Entry | Slate Gray | |
| Lines | Color #1–#4 | Slate Gray | Each independently configurable |
| Lines | Color Fixed Distance | Dodger Blue | |
| Label Style | Background Opacity | 0.85 | |
| Label Style | Text Opacity | 1.0 | |
| Label Style | Line Opacity | 1.0 | |
| Label Style | Corner Radius | 2 | |
| Label Style | Label Offset | 20 | Gap in pixels between label and bar |
| Fixed Distance | Distance #1 | 20 ticks | |
| Fixed Distance | Distance #2 | 40 ticks | |
| Fixed Distance | Distance #3 | 80 ticks | |

---

## How to Install

No import wizard, no licence file. Plain NinjaScript — copy, compile, done.

**Step 1:** Download `ZenTraderProRRv2.cs` from this folder.

**Step 2:** Copy it into your NinjaTrader indicators folder:
```
...\Documents\NinjaTrader 8\bin\Custom\Indicators\
```

**Step 3:** Open NinjaTrader 8.

**Step 4:** Go to **Tools → Edit NinjaScript → Indicators** (or **Ctrl+Shift+N**).

**Step 5:** Find `ZenTraderProRRv2` in the file list on the left. Double-click to open.

**Step 6:** Click **Compile**. You should see "Compiled successfully" in the output panel. No errors = good.

**Step 7:** On any chart, right-click → **Indicators**. Type **Zen** in the search box. Select **ZenTraderProRRv2**, click Add, then OK.

**Step 8:** Middle-click any bar to place a ruler. Shift+middle-click for breakout mode. Ctrl+middle-click for fixed distances. Click the same bar with the same mode to remove it.

---

## Version

**v1.6 · 20260503** — Three modes: close entry (middle-click), breakout entry (Shift+middle-click), fixed distance (Ctrl+middle-click). All three can coexist on the same bar.

| Version | Date | Changes |
|---|---|---|
| v1.6 | 20260503 | Added Shift+middle-click mode: entry at High+1tick (bull) / Low-1tick (bear), stop at opposite extreme. All three modes coexist. |
| v1.5.1 | 20260503 | Reverted R-multiple direction to bar colour |
| v1.5 | 20260503 | Ctrl+middle-click fixed distance mode |
| v1.4 | 20260418 | Visual overhaul |
| v1.3 | 20260418 | GetBarIdxByX + DPI + TriggerCustomEvent |
| v1.2 | 20260407 | Bar colour direction |
| v1.1 | 20260407 | Click-price direction |
| v1.0 | 20260407 | Initial build |

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
