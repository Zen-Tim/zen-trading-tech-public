# Data

Price data for futures instruments. Each instrument folder holds its own README with a table of the files in it: timeframe, session, row count and date range.

- [ES](./ES) — E-mini S&P 500. Nine files. 5-minute, 15-minute, 60-minute, daily, weekly and monthly for the day session, plus daily, weekly and monthly for the full electronic session.
- [FDAX](./FDAX) — DAX Futures (EUREX). Coming.
- [HSI](./HSI) — Hang Seng Index Futures. Coming.
- [Nikkei](./Nikkei) — Nikkei 225 Futures. Coming.

Every ES file holds five columns: `time`, `open`, `high`, `low`, `close`. Read [the ES README](./ES) before using them — it explains the timestamps, the timezone, and what back-adjusted prices can and cannot be used for.
