# ES Pre-Market Breakout — Research Kit

A self-contained kit to test one claim from the Brooks Trading Course on ES:

> The day session trades above the Globex high or below the Globex low on about 90% of days.

Globex is the overnight session. It runs from 17:00 Chicago time the evening before until the day session opens at 08:30. Its high and its low are two levels every trader can mark before the open.

Part of the "My First Trading Edges" series on [Zen Trading Tech](https://zentradingtech.com). The FDAX version of this test has its own kit: [FDAX Pre-Market Breakout](../FDAX-Pre-Market-Breakout).

---

## The headline numbers

- ES: the day session breaks at least one side of the Globex range on **94.6%** of 1,820 full sessions (2019-06-03 to 2026-09-18).
- High only 40.2%. Low only 33.0%. Both 21.4%. Neither 5.4%.
- 2019 and 2020 run lower, at about 89% of days. 2021 to 2026 run between 94.7% and 97.6% of days.

Every percentage is a share of all full sessions. The 90% claim holds.

If you run the kit on the provided data, you should get these exact numbers.

---

## How to use this kit

1. **[Get the data](./01-get-the-data.md)** — use the file in `data/`, or export your own from TradingView
2. **[Run the study](./02-run-the-study.md)** — the rules, a prompt to paste into an AI chat, and a Python script that does the same job
3. **Reference output** — `reference-output/` holds the ES result to check yours against
4. **Script** — `script/es_globex_breakout_study.py` reproduces every number in the reference output

---

## What you will need

- An AI chat that can read a CSV and run code. Claude is the recommended choice. Or Python 3 with pandas.
- A TradingView account only if you want to export another market

No indicator is needed. The data holds only time and price.

---

## License

MIT. Free to use, modify, share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
