# 20 EMA Daily Touch — Research Kit

A self-contained kit to test one commonly quoted trading statistic on ES and FDAX:

> Price touches the 20 EMA on the 5-minute chart at some point in almost every day session.

This kit measures how often price comes back to the 20 EMA during the day, and how often bar 1 alone does it.

Part of the "My First Trading Edges" series on [Zen Trading Tech](https://zentradingtech.com).

---

## The headline numbers

- ES: price touches the 20 EMA on **99.96%** of 2,683 full sessions (2016-01-04 to 2026-10-09). One day missed: 2024-01-08, when the nearest low stayed above the EMA by less than one tick.
- FDAX: price touches the 20 EMA on **100%** of 1,300 full sessions (2021-09-01 to 2026-10-09). No day missed.
- Bar 1 is the first touch on 16.7% of ES days and 20.1% of FDAX days. The rest touch later in the day, so the result does not come from bar 1 alone.

Every percentage is a share of all full sessions.

If you run the kit on the provided data, you should get these exact numbers.

---

## How to use this kit

1. **[Get the data](./01-get-the-data.md)** — use the files in `data/`, or export your own from TradingView
2. **[Run the study](./02-run-the-study.md)** — the rules, a prompt to paste into an AI chat, and a Python script that does the same job
3. **Reference output** — `reference-output/` holds the result for each market, to check yours against
4. **Script** — `script/ema20_touch_study.py` reproduces every number in the reference output

---

## What you will need

- An AI chat that can read a CSV and run code. Claude is the recommended choice. Or Python 3 with pandas.
- A TradingView account only if you want to export another market

No indicator is needed. The data holds only the columns this test uses.

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
