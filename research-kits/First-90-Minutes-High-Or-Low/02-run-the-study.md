# Step 2 — Run the Study

## The rules

- The first 90 minutes is bars 1 to 18 on the 5-minute chart.
- Only full sessions count: 81 bars for ES, 102 bars for FDAX. Short holiday sessions are left out.
- The bar that makes the high of the day is the first bar whose high equals the session's highest high. The same rule applies to the low.
- A day counts when the high, the low, or both were made in bars 1 to 18.

## Option A — Ask an AI chat

Attach the CSV, then paste this:

```
You are checking a commonly quoted trading statistic with data. The attached CSV holds 5-minute bars for one futures market.
Columns: date, time_chicago or time_berlin (bar start, exchange local time), bar (1 = first bar of the day session), open, high, low, close.

Statistic to test: the high or the low of the day is in by the end of bar 18 on about 75% of days.

Rules:
1. Group the bars by date. A full session has [81 for ES / 102 for FDAX] bars. Use full sessions only. Count the short sessions you leave out.
2. For each full session, find the highest high and the lowest low.
3. The high of the day was made on the FIRST bar whose high equals the highest high. The same for the low.
4. A day counts when the high-of-day bar, or the low-of-day bar, is bar 18 or earlier.

Report:
- The date range, the number of full sessions used, and the number of short sessions left out.
- A table for the end of bars 6, 12, 18, 24 and 36, with the clock time each bar ends: share of full sessions with the high in, the low in, and either one in. One decimal place.
- The bar 18 share for each calendar year.
- One sentence: does the 75% figure hold?
Write the code and run it. Do not estimate.
```

Replace the part in square brackets with the bar count for your market.

## Option B — Run the script

```
python script/first_90_minutes_study.py data/ES_5m_RTH_v1.0_20261013.csv ES
python script/first_90_minutes_study.py data/FDAX_5m_Xetra_v1.0_20261013.csv FDAX
```

Add a file name as a third argument to save the result as Markdown. Compare your result with `reference-output/`.
