# Step 1 — Get the Data

The study needs **daily bars from the day session only**, with five columns: `time`, `open`, `high`, `low`, `close`.

---

## Option A — use the ES file provided

`data/ES_daily_RTH_v1.0_20260917.csv`

| Item | Value |
|---|---|
| Instrument | CME E-Mini S&P 500 futures, continuous back-adjusted contract |
| Session | RTH — 08:30 to 15:15 America/Chicago |
| Bars | 4,254 daily bars |
| First bar | 2009-10-12 |
| Last bar | 2026-09-11 |
| Columns | time, open, high, low, close |

The file is built from the 60-minute RTH file in this repo's [`data/ES`](../../data/ES) folder, rolled up to one bar per day. The last two days come from the same TradingView source.

Two storm-closure days, 29 and 30 Oct 2012, are left out. Each held one partial bar.

Use this file and you will match the reference report exactly.

---

## Option B — export your own from TradingView

1. Open the continuous contract (`ES1!`, `NQ1!`, `FDAX1!` and so on).
2. In symbol settings, turn on **Adjust data for contract changes**. Without this, contract rolls create false gaps.
3. **Use the day session only (RTH).** Switch the chart session from electronic trading hours to regular trading hours. A 24-hour session gives almost no gap, because Sunday night's trading starts close to Friday's settlement.
4. Set the timeframe to **1 day**.
5. Set the chart time zone to the exchange's local time zone.
6. Scroll back to load as much history as you want, then use **Export chart data**.
7. Keep the `time`, `open`, `high`, `low` and `close` columns. Any other columns are ignored.

---

## Why the day session matters

ES trades almost 24 hours. On a 24-hour chart, Monday's open sits a few ticks from Friday's close, so the weekly gap nearly disappears.

The gap traders watch is Friday's **day-session close** against Monday's **day-session open**. That needs RTH bars.
