# Indicators

Trading indicators and tools for traders to use in their own research and execution. Each indicator lives in its own folder with the source file and a README explaining what it does, how to install it, and how to read its output.

All indicators are released under the MIT License.

---

## Available indicators

### [Zen OOH / OOL — Open on High / Open on Low](./Zen-OOH-OOL)

A TradingView Pine Script v5 indicator that flags sessions where the **first bar of the day** prints its open exactly at the high (bearish shave) or exactly at the low (bullish shave) of that opening bar. Plots arrows, highlights the opening bar, runs a stats table of occurrences, and fires optional alerts. Note this is about the opening bar itself — not the high or low of the whole day.

**Read the write-up:** [Shaved Opens Revisited — Open on High, Open on Low](https://zentradingtech.com/2026/04/11/shaved-opens-revisited-open-on-high-open-on-low/)

### [Zen Bar Count — Intraday Bar Counter](./Zen-Bar-Count)

A TradingView Pine Script v5 indicator that counts bars from the start of each new trading day and labels them on the chart at a configurable interval, with a second larger interval drawn in a highlight colour (e.g. every hour on a 5-minute chart). A simple tool for time-of-day awareness — see where you are inside the day at a glance, without squinting at the X-axis. Bar count is also exported to the Data Window for downstream use.

### [Zen Big Bar Midpoints](./Zen-Big-Bar-Midpoints)

A TradingView Pine Script v5 indicator that detects big bars (range greater than a configurable multiple of the 8-bar ABR, default 1.6x) and draws two midpoints on them — the high-low mid and the extreme-close mid — plus optional 0.33 / 0.66 fractional levels. Each set can be displayed as dotted lines or a translucent box, with independent bull/bear colours and separate toggles for each direction.

### [Zen Trading Toolkit](./Zen-Trading-Toolkit)

A TradingView Pine Script v6 multi-module indicator combining three research tools in one: ABR Measured Moves (from yesterday's close), Opening Range (N-bar box via calendar day detection), and a Volatility Stats Table (ABR plus ADR for both RTH and ETH). Instrument presets for ES, FDAX, HSI, and Nikkei with timezone-aware session detection built in. Every module and metric is independently toggleable.

### [Zen ATH Bands — All-Time High Pullback Levels](./Zen-ATH-Bands)

A TradingView Pine Script v5 indicator that plots the all-time high and a configurable ladder of percentage pullback levels below it. Seven independently-toggleable levels with customisable offsets and colours. Useful for cash indices and other instruments where ATH and its higher-timeframe pullback zones matter for context.

**Read the write-up:** [Cash Indices and Higher Time Frame Pullbacks](https://zentradingtech.com/2025/12/19/cash-indices-and-higher-time-frame-pullbacks/)

### [Zen Trader Pro RR — R/R Ruler (NinjaTrader 8)](./Zen-Trader-Pro-RR-NT)

A NinjaTrader 8 risk/reward ruler with three click modes: middle-click measures R-multiples from the close (Buy/Sell the Close), Shift+middle-click measures R-multiples from a breakout entry beyond the bar's High or Low (Buy Stop / Sell Stop), and Ctrl+middle-click projects fixed tick distances for quick measured move checks. Direction is automatic — reads bar colour. All three modes can coexist on the same bar simultaneously.

---

## How to use

Indicators are provided either as `.txt` files containing Pine Script source (TradingView) or as `.cs` files (NinjaTrader 8). See each indicator's own README for platform-specific install instructions, inputs, and configuration.

**TradingView (Pine Script):**
1. Open the indicator's `.txt` file on GitHub and copy the entire contents
2. In TradingView, open the **Pine Editor**, paste the code into a new script
3. Save and add to chart

**NinjaTrader 8:**
1. Download the `.cs` file
2. Copy it into `...\Documents\NinjaTrader 8\bin\Custom\Indicators\`
3. Open NT8, go to **Tools → Edit NinjaScript → Indicators**, find the file and compile

---
