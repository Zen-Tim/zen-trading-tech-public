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

---

## How to use

Each indicator is provided as a `.txt` file containing Pine Script source. To install:

1. Open the indicator's `.txt` file on GitHub and copy the entire contents
2. In TradingView, open the **Pine Editor**, paste the code into a new script
3. Save and add to chart

See each indicator's own README for inputs, configuration, and reading guidance.

---
