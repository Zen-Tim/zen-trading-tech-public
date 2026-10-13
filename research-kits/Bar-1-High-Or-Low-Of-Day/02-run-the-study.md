# Step 2 — Run the Study

## The rules

- Bar 1 is the first 5-minute bar of the day session.
- Only full sessions count: 81 bars for ES, 102 bars for FDAX. Short holiday sessions are left out.
- Bar 1 makes the high of the day when bar 1's high equals the highest high of the session. A later bar that only matches that price does not take it away, because bar 1 printed it first.
- The same rule applies to the low.
- A day counts once if bar 1 makes the high, the low, or both.

## Option A — Ask an AI chat

Attach the CSV, then paste this:

```
You are checking a commonly quoted trading statistic with data. The attached CSV holds 5-minute bars for one futures market.
Columns: date, time_chicago or time_berlin (bar start, exchange local time), bar (1 = first bar of the day session), open, high, low, close.

Statistic to test: bar 1 is the high or the low of the day on about 20% of days.

Rules:
1. Group the bars by date. A full session has [81 for ES / 102 for FDAX] bars. Use full sessions only. Count the short sessions you leave out.
2. For each full session, find the highest high and the lowest low of the whole session.
3. Bar 1 makes the high of the day when bar 1's high equals the session's highest high. Ties count for bar 1, because it printed that price first.
4. Bar 1 makes the low of the day when bar 1's low equals the session's lowest low.
5. Count the days where bar 1 makes the high, the low, both, either one, and neither.

Report:
- The date range, the number of full sessions used, and the number of short sessions left out.
- A table: outcome, days, share of all full sessions. One decimal place.
- The same "either one" share for each calendar year.
- One sentence: does the 20% figure hold?
Write the code and run it. Do not estimate.
```

Replace the part in square brackets with the bar count for your market.

## Option B — Run the script

```
python script/bar1_high_low_study.py data/ES_5m_RTH_v1.0_20261013.csv ES
python script/bar1_high_low_study.py data/FDAX_5m_Xetra_v1.0_20261013.csv FDAX
```

Add a file name as a third argument to save the result as Markdown. Compare your result with `reference-output/`.
