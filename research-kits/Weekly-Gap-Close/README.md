# Weekly Gap Close — Research Kit

A self-contained kit for measuring how often ES trades back to last week's closing price, which day it happens, and how many weeks it takes when it does not happen straight away.

Every Monday most futures open away from Friday's close. That gap is the weekly gap. Last week's close is a level you can mark before the week starts. This kit shows you how to test how often price returns to it, using 17 years of ES data.

The same kit works on any instrument with daily bars — NQ, YM, FDAX, Hang Seng, Nikkei, gold, oil or a stock.

---

## What you end up with

An HTML report and a markdown summary containing:

- How many weeks opened with a gap, split into gaps up and gaps down
- How many gaps closed the same week, and how many closed on the first trading day
- Which trading day of the week closed the gap
- Closure rate by gap size, measured against the average weekly range
- How many weeks the unclosed gaps took to close, and which are still open
- A worked example from the latest week

See `reference-output/ES_Weekly_Gap_Analysis_v2.0_20260917.html` for the ES result built on Dec 2009 to Sep 2026.

---

## The headline ES numbers (Dec 2009 to 11 Sep 2026, 867 weekly gaps)

- 78.9% of gaps closed the same week.
- 54.8% of gaps closed on the first trading day (Monday, or Tuesday after a holiday).
- 89.6% of gaps closed within 4 weeks.
- 95.1% of gaps smaller than 5% of the weekly average range closed the same week.

If you run the kit on the provided data, you should get these exact numbers.

---

## How to use this kit

1. **[Get the data](./01-get-the-data.md)** — use the ES file in `data/`, or export your own daily bars from TradingView
2. **[Run the study](./02-run-the-study.md)** — the rules, the prompt to paste into an AI chat, and a Python script that does the same job
3. **Reference output** — `reference-output/` holds the ES HTML report and the markdown summary
4. **Script** — `script/weekly_gap_study.py` reproduces every number in the reference report

---

## How the gap is measured

The gap is sized against the **weekly ABR** — the average bar range. That is the average high-to-low range of the last 8 weekly bars.

A gap of 10% means the week opened one tenth of an average week's range away from last week's close.

Points or dollars make a 2012 gap look tiny next to a 2026 gap, because the index price has more than quadrupled. A percentage of the recent weekly range compares every week fairly.

---

## What you will need

- An AI chat that can read a CSV and write HTML — Claude is the recommended choice. Or Python 3 with pandas.
- A TradingView account only if you want to export another instrument

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
