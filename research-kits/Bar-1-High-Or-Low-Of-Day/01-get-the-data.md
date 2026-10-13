# Step 1 — Get the Data

## The files in `data/`

| File | Market | Session | Dates | Rows |
|---|---|---|---|---|
| `ES_5m_RTH_v1.0_20261013.csv` | ES, E-mini S&P 500 futures, continuous contract | Day session, 08:30 to 15:15 Chicago time (81 bars) | 2016-01-04 to 2026-10-09 | 218,585 |
| `FDAX_5m_Xetra_v1.0_20261013.csv` | FDAX, DAX futures, continuous contract | Xetra day session, 09:00 to 17:30 Berlin time (102 bars) | 2021-09-01 to 2026-10-09 | 132,879 |

Columns:

| Column | Meaning |
|---|---|
| `date` | The trading date |
| `time_chicago` / `time_berlin` | The bar's start time, in the exchange's local time (America/Chicago for ES, Europe/Berlin for FDAX). Daylight saving is already applied. |
| `bar` | Bar number in the day. 1 is the first 5-minute bar of the day session. |
| `open`, `high`, `low`, `close` | Prices |

Prices are back-adjusted for contract rolls. Old prices will not match a chart of that date. That does not change any result here, because every test compares bars inside the same day.

Short holiday sessions are in the file. The study leaves them out and counts them.

## Export your own data instead

1. In TradingView, open `ES1!` (or `FDAX1!`, or any market) on the 5-minute chart.
2. Set the session to regular trading hours only, so the chart shows the day session.
3. Use Export chart data and save the CSV.
4. Add a `bar` column: number the bars 1, 2, 3 within each day.
5. Tell the AI, or the script, how many bars a full session has on your market.
