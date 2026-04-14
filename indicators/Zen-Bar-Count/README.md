# Zen Bar Count — Intraday Bar Counter

A TradingView indicator that counts bars from the start of each new trading day and labels them on the chart at a configurable interval, with a highlight colour at a second larger interval. A simple tool for seeing where you are inside the day at a glance — useful for time-of-day awareness, matching up to opening-range and measured-move studies, and teaching bar sequencing.

---

## What it does

On every new trading day the bar counter resets to 1, then increments by 1 on each subsequent bar. The indicator:

- **Plots a number** below the bar every N bars (default every 3rd bar)
- **Switches to a highlight colour** every M bars (default every 12th bar) — useful as an hourly marker on a 5-minute chart, a 2-hour marker on a 10-minute chart, etc.
- **Exports the bar count** to the Data Window and CSV download so you can use it in downstream analysis

The count runs on whatever timeframe the chart is on — it does not enforce a session. A new day is detected via `ta.change(time("D"))`, so the reset aligns with the instrument's own daily session roll.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open the `zen-bar-count-v5.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure inputs via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it renders cleanly on GitHub and copy-pastes straight into the Pine Editor without a download step.

---

## Inputs

| Group | Input | Default | Notes |
|---|---|---|---|
| Display | Show Bar Count | true | Master toggle for labels |
| Display | Label Size | small | auto / tiny / small / normal / large / huge |
| Intervals | Label Every N Bars | 3 | Show a label every N bars |
| Intervals | Highlight Every N Bars | 12 | Use highlight colour at this interval |
| Colours | Default Colour | green | Regular label colour |
| Colours | Highlight Colour | red | Colour used at the highlight interval |

---

## How to read it

- **The number** below each labelled bar is its sequence from the start of the day (1 = first bar).
- **The colour switch** marks every Nth bar at the highlight interval. With defaults (3 / 12) on a 5-minute chart, green labels appear every 15 minutes and red labels appear every hour — giving you an instant "what time is it inside the day" read without looking at the X-axis.
- **Reset happens at the daily session boundary** for the chart's instrument, so it works correctly on ES, FDAX, HSI, Nikkei and equities on whatever timeframe you're viewing.

Tune the two intervals to match your timeframe. A common setup on 10-minute charts is 3 / 6 (label every 30 min, highlight every hour); on 15-minute charts use 2 / 4 for the same effect.

---

## Version

**v5** — current. Configurable label and highlight intervals, size control, and Data Window export for downstream use.

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
