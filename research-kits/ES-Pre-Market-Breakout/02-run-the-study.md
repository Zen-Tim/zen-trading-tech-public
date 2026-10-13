# Step 2 — Run the Study

## The rules

- Times are Chicago time and are bar start times.
- Globex session for a trading day: bars starting 17:00 the evening before, through the bar starting 08:15. Sunday evening belongs to Monday.
- Day session: bars starting 08:30 through the bar starting 15:00. That is 27 bars, ending at 15:15.
- Bars starting 15:15 to 16:45 belong to neither session.
- Only full day sessions count (27 bars) that also have a Globex session. Short holiday sessions are left out.
- Broke the Globex high: a day-session high above the Globex high. Equal does not count.
- Broke the Globex low: a day-session low below the Globex low. Equal does not count.

## Option A — Ask an AI chat

Attach the CSV, then paste this:

```
You are checking a trading claim with data. The attached CSV holds 15-minute ES futures bars covering the full 24-hour session.
Columns: time_chicago (bar start time, Chicago local time), open, high, low, close.

Claim to test: the ES day session trades above the Globex high or below the Globex low on about 90% of days.

Rules:
1. Each trading day has a Globex session and a day session.
2. Globex session: bars starting at 17:00 or later belong to the NEXT calendar day's Globex session. Bars starting before 08:30 belong to the same day's Globex session. Sunday evening bars belong to Monday.
3. Day session: bars starting 08:30 through 15:00 (27 bars). Ignore bars starting 15:15 to 16:45.
4. Use only days with all 27 day-session bars and at least one Globex bar. Count the short days you leave out.
5. Broke the high: the day-session high is ABOVE the Globex high. Broke the low: the day-session low is BELOW the Globex low. Equal does not count.

Report:
- The date range, the number of full sessions used, and the number left out.
- A table: broke the high only, broke the low only, broke both, broke neither, broke at least one side. Days and share of full sessions, one decimal place.
- The "at least one side" share for each calendar year.
- One sentence: does the 90% claim hold?
Write the code and run it. Do not estimate.
```

## Option B — Run the script

```
python script/es_globex_breakout_study.py data/ES_15m_24hour_v1.0_20261013.csv
```

Add a file name as a second argument to save the result as Markdown. Compare your result with `reference-output/`.
