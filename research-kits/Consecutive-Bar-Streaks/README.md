# Consecutive Bar Streaks — Research Kit

A self-contained kit for measuring how often consecutive bull or bear bars continue vs reverse at each streak depth. Apply the indicator to any instrument and timeframe in TradingView, export the CSV, and drop it into an AI chat with the execution prompt. Out comes an HTML report and markdown summary showing continuation probabilities for every streak depth with a meaningful sample size.

The same kit works on any futures or cash instrument and any timeframe — weekly, daily, hourly, or intraday. The ES weekly study included here is the reference output.

---

## What you end up with

An HTML report containing:

- Overall population counts — bull bars, bear bars, doji bars, max streak lengths
- Bull continuation probability table — after N consecutive bull bars, probability the next bar is also bull
- Bear continuation probability table — after N consecutive bear bars, probability the next bar continues or reverses
- Population funnel — how many distinct runs reached each streak depth
- Insight cards summarising the key asymmetry between bull and bear behaviour

See `reference-output/ES_Weekly_Consecutive_Bar_Streaks_v1_2_20260610.html` for a worked example built on 29 years of ES weekly data.

---

## How to use this kit

1. **[Install the indicator](./01-install-indicator.md)** — Pine Script v6 source ready to paste into TradingView's Pine Editor
2. **[Download the data](./02-download-data.md)** — set the timeframe, apply the indicator, export to CSV
3. **[Run the study](./03-run-the-study.md)** — the prompt to paste into Claude along with your CSV
4. **Reference output** — `reference-output/` folder containing the ES weekly HTML and markdown reports
5. **Raw data** — `data/` folder with ES CSVs across four timeframes ready to use without exporting anything

Work through steps 1–3 in order. If you want to use the provided ES data directly, skip to step 3.

---

## What you'll need

- A TradingView account (free tier works for the data export)
- An AI chat that can read CSVs and write HTML — Claude is the recommended choice
- Around 5 minutes of AI processing time once the prompt is pasted

---

## Provided ES data

Four ready-to-use ES CSVs are in the `data/` folder:

| File | Timeframe | Bars | Date range |
|---|---|---|---|
| `CME_MINI_ES1_1W_3390c.csv` | Weekly | 1,501 | 1997–2026 |
| `CME_MINI_ES1_1440_04659.csv` | Daily | 6,706 | 1997–2026 |
| `CME_MINI_ES1_60_4c23e.csv` | 60-minute | 17,535 | ~2019–2026 |
| `ES_5min_combined.csv` | 5-minute | ~100k | 2022–2026 |

Each file already has the `Bar Direction` and `Streak` columns pre-computed by the indicator — you can drop any of them straight into the AI prompt without re-exporting from TradingView.

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
