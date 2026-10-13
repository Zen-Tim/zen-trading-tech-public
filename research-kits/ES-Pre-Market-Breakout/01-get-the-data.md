# Step 1 — Get the Data

## The file in `data/`

| File | Market | Session | Dates | Rows |
|---|---|---|---|---|
| `ES_15m_24hour_v1.0_20261013.csv` | ES, E-mini S&P 500 futures, continuous contract | Full 24-hour trading (overnight and day session) | 2019-06-02 to 2026-09-18 | 172,005 |

Columns:

| Column | Meaning |
|---|---|
| `time_chicago` | The bar's start time, in Chicago local time (America/Chicago). Daylight saving is already applied. |
| `open`, `high`, `low`, `close` | Prices |

The bars are 15 minutes long. The file needs the overnight bars, so it is not a day-session-only file.

Prices are back-adjusted for contract rolls. Old prices will not match a chart of that date. That does not change the result, because each day is compared only with its own overnight session.

## Export your own data instead

1. In TradingView, open `ES1!` on the 15-minute chart.
2. Set the session to extended trading hours, so the overnight bars show.
3. Use Export chart data and save the CSV. TradingView limits how many bars one export holds, so a long history needs several exports joined together.
