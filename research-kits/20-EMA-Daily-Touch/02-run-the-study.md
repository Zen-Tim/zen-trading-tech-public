# Step 2 — Run the Study

## The rules

- A bar touches the 20 EMA when the EMA sits inside the bar's range: low at or below the EMA, and high at or above it.
- Only full sessions count: 81 bars for ES, 102 bars for FDAX. Short holiday sessions are left out.
- A day counts when at least one bar touches.

## Option A — Ask an AI chat

Attach the CSV, then paste this:

```
You are checking a commonly quoted trading statistic with data. The attached CSV holds 5-minute bars for one futures market.
Columns: date, time_chicago or time_berlin (bar start, exchange local time), bar (1 = first bar of the day session), open, high, low, close, ema20.
ema20 is a 20-bar exponential moving average of the close, already computed. Use it as given.

Statistic to test: price touches the 20 EMA at some point in almost every day session.

Rules:
1. Group the bars by date. A full session has [81 for ES / 102 for FDAX] bars. Use full sessions only. Count the short sessions you leave out.
2. A bar touches when low <= ema20 <= high.
3. A day counts when at least one bar touches. Also record the bar number of the first touch.

Report:
- The date range, the number of full sessions used, and the number of short sessions left out.
- A table: touched at least once, never touched, first touch on bar 1, first touch after bar 1. Days and share of full sessions, two decimal places.
- The dates of any days that never touched.
- One sentence: does the figure hold?
Write the code and run it. Do not estimate.
```

Replace the part in square brackets with the bar count for your market.

## Option B — Run the script

```
python script/ema20_touch_study.py data/ES_5m_RTH_v1.0_20261013.csv ES
python script/ema20_touch_study.py data/FDAX_5m_Xetra_v1.0_20261013.csv FDAX
```

Add a file name as a third argument to save the result as Markdown. Compare your result with `reference-output/`.
