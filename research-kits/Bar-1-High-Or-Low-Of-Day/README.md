# Bar 1 High or Low of the Day — Research Kit

A self-contained kit to test one commonly quoted trading statistic on ES and FDAX:

> Bar 1 on the 5-minute chart is the high or the low of the day on about 20% of days.

Bar 1 is the first bar every trader sees. This kit measures how often it ends up as one end of the whole day.

Part of the "My First Trading Edges" series on [Zen Trading Tech](https://zentradingtech.com).

---

## The headline numbers

- ES: bar 1 makes the high or the low of the day on **26.9%** of 2,683 full sessions (2016-01-04 to 2026-10-09). High 12.5%, low 14.4%, both 0%.
- FDAX: bar 1 makes the high or the low of the day on **29.0%** of 1,300 full sessions (2021-09-01 to 2026-10-09). High 13.9%, low 15.1%, both 0%.

Every percentage is a share of all full sessions. The figure of about 20% is too low on both markets: 26.9% of full sessions on ES and 29.0% on FDAX.

If you run the kit on the provided data, you should get these exact numbers.

---

## How to use this kit

1. **[Get the data](./01-get-the-data.md)** — use the files in `data/`, or export your own from TradingView
2. **[Run the study](./02-run-the-study.md)** — the rules, a prompt to paste into an AI chat, and a Python script that does the same job
3. **Reference output** — `reference-output/` holds the result for each market, to check yours against
4. **Script** — `script/bar1_high_low_study.py` reproduces every number in the reference output

---

## What you will need

- An AI chat that can read a CSV and run code. Claude is the recommended choice. Or Python 3 with pandas.
- A TradingView account only if you want to export another market

No indicator is needed. The data holds only the columns this test uses.

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
