# Step 2 — Download the Data from TradingView

This step gets you a CSV containing OHLC bars plus the two computed columns the study needs: **Bar Direction** and **Streak**.

If you want to run the study on ES without exporting anything yourself, the `data/` folder already has four ready-to-use CSVs (weekly, daily, 60-minute, 5-minute). Skip to [step 3](./03-run-the-study.md).

---

## Setup

Open the chart of your instrument in TradingView.

**Use the continuous futures contract**, not the front month. On TradingView these are labelled with a `1!` suffix (e.g. `ES1!`, `FDAX1!`, `NK1!`, `HSI1!`). The continuous contract gives you the longest unbroken history.

In the symbol settings (gear icon → Symbol tab), enable **"Adjust data for contract changes"** to back-adjust price across contract rolls.

For cash instruments or equities, use the normal symbol — no roll adjustment needed.

---

## Set the timeframe

Switch to your target timeframe. This study works on any timeframe:

- **Weekly / Daily** — longest history, most statistically robust results
- **60-minute / 15-minute / 5-minute** — useful for intraday streak behaviour, but requires more data to reach reliable sample sizes at deeper streak depths

The timeframe you pick is the timeframe the study analyses. A weekly bar streak is a completely different thing from a 5-minute bar streak.

---

## Set the time zone

Click the **time displayed at the bottom right of the chart** and pick the exchange's local time zone:

| Instrument | Time zone |
|---|---|
| ES, NQ, RTY, YM (CME) | UTC-6 (Chicago / America/Chicago) |
| FDAX (EUREX) | UTC+1 (Berlin / Europe/Berlin) |
| HSI (HKEx) | UTC+8 (Hong Kong / Asia/Hong_Kong) |
| Nikkei (CME or OSE) | UTC+9 (Tokyo / Asia/Tokyo) |

For intraday timeframes, the time zone determines which session a bar belongs to. For daily and weekly bars it has no practical effect on the data values.

---

## Apply the indicator

Make sure the Zen Consecutive Bar Streak indicator is on the chart (see [step 1](./01-install-indicator.md)).

Open the **Data Window** and confirm you can see `Bar Direction` and `Streak` values when you hover over bars.

---

## Export the CSV

Click the **down-arrow at the top of the chart** (or right-click the chart background) → **Export chart data**.

Settings to use:

- **Time format:** ISO
- **All exporters:** export everything available

Click **Export**. A CSV file downloads.

Sanity-check the file:
- Columns should include `time`, `open`, `high`, `low`, `close`, `Bar Direction`, `Streak`
- `Bar Direction` should be `+1`, `-1`, or `0` on every row
- `Streak` should increment in the same direction on consecutive same-direction bars and reset on a direction change

Save the file — the TradingView-generated filename is fine.

---

Once you have the CSV, go to [step 3 — run the study](./03-run-the-study.md).
