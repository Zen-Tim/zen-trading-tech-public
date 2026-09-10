# Indicators

Trading indicators and tools for traders to use in their own research and execution. Each indicator lives in its own folder with the source file and a README explaining what it does, how to install it, and how to read its output.

Fifteen indicators, listed A to Z below.

---

## Available indicators

### [Zen ABR Against](./Zen-ABR-Against)

A TradingView Pine Script indicator that marks Average Bar Range (ABR) levels as short tick marks above and below price, then colours any bar that breaks a prior bar's ABR level against the direction that break implies. Inspired by the "boxes against you" breakout-failure idea in Perry Kaufman's *Trading Systems and Methods*, where one box is one ABR. Two settings control the test: how many bars back to measure from, and how many ABR boxes away counts as a break. A diagnostic tool for testing that question visually, not an answer to it.

### [Zen ABR Range Bands](./Zen-ABR-Range-Bands)

A TradingView Pine Script indicator that plots a 20-bar EMA with optional bands at 1x, 2x, 3x and 4x the 8-bar ABR above and below it. Each band pair toggles independently and carries its own colour. Eight further checkboxes recolour any bar whose wick touches a chosen level — red for a hit above the EMA, blue for a hit below.

### [Zen ATH Bands — All-Time High Pullback Levels](./Zen-ATH-Bands)

A TradingView Pine Script v5 indicator that plots the all-time high and a configurable ladder of percentage pullback levels below it. Seven independently-toggleable levels with customisable offsets and colours. Useful for cash indices and other instruments where ATH and its higher-timeframe pullback zones matter for context.

**Read the write-up:** [Cash Indices and Higher Time Frame Pullbacks](https://zentradingtech.com/2025/12/19/cash-indices-and-higher-time-frame-pullbacks/)

### [Zen Average Bar Range Stats](./Zen-Average-Bar-Range-Stats)

A minimal TradingView Pine Script v5 indicator that computes the three values a daily range distribution study needs — Range, Average Bar Range, and Range divided by ABR — and exposes them in the Data Window only. Nothing is drawn on the chart. Apply it to a daily chart, then use TradingView's "Export chart data" to get those three columns alongside the OHLC.

### [Zen Bar Count — Intraday Bar Counter](./Zen-Bar-Count)

A TradingView Pine Script v5 indicator that counts bars from the start of each new trading day and labels them on the chart at a configurable interval, with a second larger interval drawn in a highlight colour (e.g. every hour on a 5-minute chart). A simple tool for time-of-day awareness — see where you are inside the day at a glance, without squinting at the X-axis. Bar count is also exported to the Data Window for downstream use.

### [Zen Bar Range Projections](./Zen-Bar-Range-Projections)

A TradingView Pine Script v6 indicator that projects 0.5x and 1x ABR levels above and below the close of the prior bar - either on the chart timeframe or on a higher timeframe. Useful for seeing where a measured move from the last completed bar lands relative to current price. Higher-TF mode pulls values from the last completed HTF bar so the lines don't drift while the live HTF bar is forming. Bars-back input fans out up to 12 historical projection sets for visual auditing.

### [Zen Big Bar Midpoints](./Zen-Big-Bar-Midpoints)

A TradingView Pine Script v5 indicator that detects big bars (range greater than a configurable multiple of the 8-bar ABR, default 1.6x) and draws two midpoints on them — the high-low mid and the extreme-close mid — plus optional 0.33 / 0.66 fractional levels. Each set can be displayed as dotted lines or a translucent box, with independent bull/bear colours and separate toggles for each direction.

### [Zen Inside Bar — Inside Bar Highlighter](./Zen-Inside-Bar)

A TradingView Pine Script v6 indicator that highlights inside bars — bars whose high and low both sit within the prior bar's range — with a configurable colour, plus a built-in alert condition for when one prints.

### [Zen OOH / OOL — Open on High / Open on Low](./Zen-OOH-OOL)

A TradingView Pine Script v5 indicator that flags sessions where the **first bar of the day** prints its open exactly at the high (bearish shave) or exactly at the low (bullish shave) of that opening bar. Plots arrows, highlights the opening bar, runs a stats table of occurrences, and fires optional alerts. Note this is about the opening bar itself — not the high or low of the whole day.

**Read the write-up:** [Shaved Opens Revisited — Open on High, Open on Low](https://zentradingtech.com/2026/04/11/shaved-opens-revisited-open-on-high-open-on-low/)

### [Zen Open CSC Bars — Consecutive Same-Colour Bars](./Zen-Open-CSC-Bars)

A TradingView Pine Script v5 indicator that detects a run of N same-direction bars from the session open and measures the resulting spike two ways: the Consecutive Spike, the range of the first two bars, computed every day whether or not a run qualifies; and the Total Spike, the full range of the whole run, printed once the streak breaks. Labels, bar colouring and alerts on qualifying runs. Both spike sizes reach the Data Window for external logging.

### [Zen TSaM Swing Chart · Multi-Mode 2/3/4](./Zen-TSaM-Swing-Chart)

A TradingView Pine Script v5 indicator that draws price-based swing charts using the classic 2-bar reversal rule from Perry Kaufman's *Trading Systems and Methods* (Fig 5.3, p.184) — an event-driven trend method with no time component, only price against price. Runs three reversal thresholds (2, 3, and 4 bars) simultaneously on the same chart at cascading opacity, plus an optional background zone shading bars where all three agree on direction.

### [Zen Trader Pro RR — R/R Ruler (NinjaTrader 8)](./Zen-Trader-Pro-RR-NT)

A NinjaTrader 8 risk/reward ruler with three click modes: middle-click measures R-multiples from the close (Buy/Sell the Close), Shift+middle-click measures R-multiples from a breakout entry beyond the bar's High or Low (Buy Stop / Sell Stop), and Ctrl+middle-click projects fixed tick distances for quick measured move checks. Direction is automatic — reads bar colour. All three modes can coexist on the same bar simultaneously.

### [Zen Trading Toolkit](./Zen-Trading-Toolkit)

A TradingView Pine Script v6 multi-module indicator combining three research tools in one: ABR Measured Moves (from yesterday's close), Opening Range (N-bar box via calendar day detection), and a Volatility Stats Table (ABR plus ADR for both RTH and ETH). Instrument presets for ES, FDAX, HSI, and Nikkei with timezone-aware session detection built in. Every module and metric is independently toggleable.

### [Zen Trading Toolkit — ETH Edition](./Zen-Trading-Toolkit-ETH)

Same three modules as Zen Trading Toolkit, rebuilt for continuous/near-24hr instruments (e.g. GC/GOLD) with no RTH/ETH split — one session, one column, no instrument presets.

### [Zen Z Bars](./Zen-Z-Bars)

A TradingView Pine Script v5 indicator that flags Z Bars — bars whose range is a large multiple of the 8-bar ABR — and colour-codes them across four threshold tiers (1.1x, 1.5x, 2x, 3x). Draws a midpoint reference line on each Z Bar and an arrow on the following bar when the move continues in the same direction (follow-through). Each tier has an independent On/Off toggle, so individual size bands can be isolated or hidden.

---

## How to use

Source is provided as `.txt` or `.pine` files for TradingView, and `.cs` for NinjaTrader 8. Where an indicator ships as `.txt`, that is so it renders on GitHub and copy-pastes straight into the Pine Editor with no download step. See each indicator's own README for its inputs and configuration.

**TradingView (Pine Script) — `.txt` and `.pine`:**

1. Open the indicator's source file on GitHub and copy the entire contents
2. In TradingView, open the **Pine Editor**, paste the code into a new script
3. Save and add to chart

**NinjaTrader 8 — `.cs`:**

1. Download the `.cs` file
2. Copy it into `...\Documents\NinjaTrader 8\bin\Custom\Indicators\`
3. Open NT8, go to **Tools → Edit NinjaScript → Indicators**, find the file and compile

---

## Licence

MIT — see the [LICENSE](../LICENSE) file. Free to use, modify and share, with attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.

A few Pine Script files still carry TradingView's default Mozilla Public License 2.0 header. Where that header is present it governs that file.
