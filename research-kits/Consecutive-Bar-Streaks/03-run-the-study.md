# Step 3 — Run the Study

Paste the prompt below into Claude (or any capable AI) along with these attachments:

1. Your CSV export from TradingView (step 2) — or one of the ready-made CSVs from the `data/` folder
2. The reference HTML report: `reference-output/ES_Weekly_Consecutive_Bar_Streaks_v1_2_20260610.html`
3. The reference markdown: `reference-output/ES_Weekly_Consecutive_Bar_Streaks_v1_2_20260610.md`

---PROMPT START---

I want you to run a Consecutive Bar Streak study on the attached CSV and produce two deliverables: an HTML report and a markdown summary. Mimic the style and structure of the attached ES Weekly reference report exactly.

## The data

The CSV is a bar export from TradingView with these relevant columns:

- `time` — bar timestamp
- `open`, `high`, `low`, `close` — OHLC
- `Bar Direction` — `+1` (bull bar, close > open), `-1` (bear bar, close < open), `0` (doji, close == open)
- `Streak` — signed running count of consecutive same-direction bars; resets to +1/-1 on direction change, 0 on doji

## What to compute

**1. Overall Population**
- Count of total bars, bull bars (+1), bear bars (-1), doji bars (0)
- Max consecutive bull bars (highest positive Streak value)
- Max consecutive bear bars (most negative Streak value, expressed as a positive number)
- Bull % and bear % of total bars

**2. Bull Continuation Table**
For each streak depth N starting at 2 (i.e. bars where Streak == N):
- Occurrences — count of bars at this depth
- Occ% of Total — occurrences as % of all bars
- Continue — count where the NEXT bar has Bar Direction == +1
- Cont% — Continue / Occurrences

Stop at the depth where occurrences drop to 0.

**3. Bear Continuation Table**
Same as above but for Streak == -N (bear bars at depth N).
- Continue — count where the NEXT bar has Bar Direction == -1
- Cont% below 50% means reversal is more likely than continuation

**4. Population Funnel**
Combined table showing Bull Runs and Bear Runs side by side at each depth, with % of total bars.

**Fading rule:** Rows where occurrences < 10 should be shown at opacity 0.5 in the HTML table. They are still included in the table but marked as unreliable.

**Print all four tables as plain text in chat before generating any files.** Wait for my confirmation if any numbers look off.

## The HTML report

Mimic the attached ES Weekly HTML reference exactly — same layout, same components, same colour system, same split-grid for bull and bear tables side by side, same insight cards. Substitute instrument name, timeframe, date range, and numbers throughout. Single self-contained HTML file, UTF-8, no external dependencies except Google Fonts.

## The markdown summary

Mimic the attached ES Weekly markdown reference structure. Include the same sections: Data Universe, Research (Overall Population, Bull Continuation, Bear Continuation, Population Funnel), Vocabulary, Version History. Write fresh notes and observations from the actual data — do not copy the ES weekly text.

## Filenames

- HTML: `{INSTRUMENT}_{Timeframe}_Consecutive_Bar_Streaks_v1_0_{YYYYMMDD}.html`
- Markdown: `{INSTRUMENT}_{Timeframe}_Consecutive_Bar_Streaks_v1_0_{YYYYMMDD}.md`

Replace `{INSTRUMENT}` with the short instrument code (ES, FDAX, HSI, NK, etc.), `{Timeframe}` with the bar timeframe (Weekly, Daily, 60min, 5min, etc.), and `{YYYYMMDD}` with today's date.

## One more rule

Confirm you understand the brief, show me the four tables as plain text first, then build the files.

---PROMPT END---
