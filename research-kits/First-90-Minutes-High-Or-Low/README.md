# Research Kit #6: First 90 Minutes High or Low

A self-contained kit to test one commonly quoted trading statistic on ES and FDAX:

> The high or the low of the day is in by the end of the first 18 bars on the 5-minute chart on about 75% of days.

This kit measures how often one end of the day is already in place after the first 90 minutes.

Part of the "My First Trading Edges" series on [Zen Trading Tech](https://zentradingtech.com).

---

## The headline numbers

- ES: the high or the low of the day is in by the end of bar 18 (10:00 Chicago) on **80.7%** of 2,683 full sessions (2016-01-04 to 2026-10-09).
- FDAX: the high or the low of the day is in by the end of bar 18 (10:30 Berlin) on **75.5%** of 1,300 full sessions (2021-09-01 to 2026-10-09).

Every percentage is a share of all full sessions. The 75% figure holds on both markets. The reference output also gives bars 6, 12, 24 and 36.

If you run the kit on the provided data, you should get these exact numbers.

---

## How to use this kit

1. **[Get the data](./01-get-the-data.md)** — use the files in `data/`, or export your own from TradingView
2. **[Run the study](./02-run-the-study.md)** — the rules, a prompt to paste into an AI chat, and a Python script that does the same job
3. **Reference output** — `reference-output/` holds the result for each market, to check yours against
4. **Script** — `script/first_90_minutes_study.py` reproduces every number in the reference output

---

## What you will need

- An AI chat that can read a CSV and run code. Claude is the recommended choice. Or Python 3 with pandas.
- A TradingView account only if you want to export another market

No indicator is needed. The data holds only the columns this test uses.

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
