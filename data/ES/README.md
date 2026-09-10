# ES — E-mini S&P 500

Open price data for ES futures (CME), across six regular-session timeframes and three full-session timeframes.

Every file holds five columns and nothing else: `time`, `open`, `high`, `low`, `close`.

## Files

### RTH — the day session

Regular Trading Hours. 08:30 to 15:15, America/Chicago.

| File | Bar | Rows | First bar | Last bar | Size |
|------|-----|------|-----------|----------|------|
| ES_5m_RTH_v1.0_20260910.csv | 5 minutes | 105,750 | 2021-06-22 | 2026-09-09 | 5.4 MB |
| ES_15m_RTH_v1.0_20260910.csv | 15 minutes | 39,114 | 2020-11-23 | 2026-09-09 | 2.0 MB |
| ES_60m_RTH_v1.0_20260910.csv | 60 minutes | 29,659 | 2009-10-12 | 2026-09-09 | 1.5 MB |
| ES_daily_RTH_v1.0_20260910.csv | 1 day | 2,747 | 2015-10-06 | 2026-09-09 | 0.1 MB |
| ES_weekly_RTH_v1.0_20260910.csv | 1 week | 571 | 2015-10-06 | 2026-09-08 | 0.02 MB |
| ES_monthly_RTH_v1.0_20260910.csv | 1 month | 132 | 2015-10-06 | 2026-09-01 | 0.005 MB |

### ETH — the full session

Electronic Trading Hours. Opens 17:00 America/Chicago the previous calendar day, closes 16:00 the next.

| File | Bar | Rows | First bar | Last bar | Size |
|------|-----|------|-----------|----------|------|
| ES_daily_ETH_v1.0_20260910.csv | 1 day | 7,281 | 1997-09-09 | 2026-06-24 | 0.3 MB |
| ES_weekly_ETH_v1.0_20260910.csv | 1 week | 1,503 | 1997-09-08 | 2026-06-22 | 0.06 MB |
| ES_monthly_ETH_v1.0_20260910.csv | 1 month | 346 | 1997-09-01 | 2026-06-01 | 0.01 MB |

The version and date in each filename are the build's, not the data's. The First bar and Last bar columns above say what each file actually covers.

## How to read the time column

**Every timestamp is the time the bar OPENED.** A 5-minute bar stamped 08:30 covers 08:30:00 to 08:34:59.

**The three intraday files carry a full timestamp with its offset**, like `2021-06-22T08:30:00-05:00`. The offset is America/Chicago. It reads -05:00 during US daylight saving and -06:00 the rest of the year. Most tools parse this directly. The times are exchange local time and are never converted to a shared timezone.

**The six daily, weekly and monthly files carry a plain date**, like `2026-09-09`.

- On a daily file, the date is the trading date.
- On a weekly file, the date is the first trading day of that week.
- On a monthly file, the date is the first trading day of that month.

## What the prices are

**Prices are index points.** The smallest move ES makes is 0.25 points, so every price ends in .00, .25, .50 or .75.

**This is a continuous contract, back-adjusted.** ES trades in quarterly contracts. Each one expires and the next takes over. Stitching them end to end leaves a jump at every roll, so the older part of the series is shifted to close each jump. That is back-adjustment.

Two things follow from back-adjustment, and both matter:

1. **A historic price here is not the price that contract traded at on that day.** A daily bar from 2015 shows a level near 2,700 because the whole early series has been lifted to meet today's. The real front-month contract traded near 2,000 that day.
2. **Every distance is correct.** The size of a bar, the gap between two days, the move from a swing low to a swing high, the range of a week. Back-adjustment shifts the whole series by a constant at each roll, so it never changes a distance. Use these files for ranges, moves, gaps and patterns. Do not use them to look up what the market printed on a given date.

**The RTH files and the ETH files come from the same back-adjusted series**, so a price in one is comparable to a price in the other.

## Bars per session

A normal ES day session holds 81 five-minute bars, 27 fifteen-minute bars, and 7 sixty-minute bars. The last 60-minute bar is a 45-minute stub, because the session ends at 15:15.

Early-close days hold fewer. The 5-minute file has 10 such days, each with 45 bars. They are the day after Thanksgiving, Christmas Eve, and July 3rd. Count the bars in a session rather than assuming 81.

## Source and checks

Exported from TradingView, symbol `CME_MINI_ES1!`.

Each timeframe was checked against the one below it. Roll the 5-minute bars up into days and every open, high, low and close matches the daily file. The same holds for 60-minute into daily, daily into weekly, and daily into monthly, on both the RTH and the ETH sets. 187,103 rows checked, zero mismatches.
