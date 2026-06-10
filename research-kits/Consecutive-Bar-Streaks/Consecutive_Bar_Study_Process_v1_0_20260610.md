# Consecutive Bar Study — Process Document v1.0 · 20260610

**Purpose:** Step-by-step process for running the consecutive bar streak study on any instrument and timeframe.

---

## What This Study Produces

Three tables per instrument/timeframe:
1. **Overall Population** — bull/bear/doji bar counts and max streak lengths
2. **Bull Continuation Probability** — after N consecutive bull bars, probability next bar is also bull
3. **Bear Continuation Probability** — after N consecutive bear bars, probability next bar is also bear
4. **Population Funnel** — how many runs reached each depth (bull and bear)

---

## Step 1 — TradingView Export

1. Open the chart for the target instrument and timeframe
2. Add the indicator: `Zen Consecutive Bar Streak v1.0` (Pinescript in `Indicators/` folder)
3. Export OHLC data with the two indicator columns:
   - `Bar Direction` (+1 bull, -1 bear, 0 doji)
   - `Streak` (signed running count)
4. Export as CSV. Name format: `{Instrument}_{Timeframe}_{hash}.csv`
5. Save to `D:\Claude`

---

## Step 2 — Run the Analysis

Start a new Claude session in this project and provide:
- The CSV file
- This process document
- The design-system.md (already in project)

Prompt Claude with:
> "Run the consecutive bar streak study on this data. Produce the markdown report and HTML report following the design system. Use the existing ES Weekly study as the reference for structure and table format."

The analysis Claude needs to perform:
- Count bull bars, bear bars, doji bars
- For each streak depth N (starting at 2), count occurrences and how many continued vs reversed
- Fade rows with n < 10 (show as opacity 0.5)
- Population funnel: occurrences at each depth as % of total bars

---

## Step 3 — Review and Version

- Check tables match raw data
- Confirm faded rows are correctly identified (n < 10)
- Version as v1.0 on first release

---

## Step 4 — Publish to Local GitHub

Use the `research-publish-to-github` skill:

| Step | Action |
|---|---|
| Step 1 | Markdown to `Markdown-Summaries/` |
| Step 2 | HTML to `{Instrument}-Weekly-Consecutive-Bar-Streaks/index.html` (new folder) |
| Step 3 | Brief — skip (no brief for this study type) |
| Step 4 | Front page — Weekly category, correct instrument column |

**Folder naming pattern:** `{INSTRUMENT}-{Timeframe}-Consecutive-Bar-Streaks`
Examples:
- `ES-Weekly-Consecutive-Bar-Streaks`
- `FDAX-Weekly-Consecutive-Bar-Streaks`
- `ES-Daily-Consecutive-Bar-Streaks`

**Front page placement:**
- Weekly bars → Weekly category
- Daily bars → Daily Bar category
- Intraday bars → Intraday category

**Index.html note:** The index.html is large and will time out if Claude tries to write it directly. Use the Python patch script approach — Claude writes a small `patch_index.py` to `D:\Claude`, you run it locally.

---

## Step 5 — Push via GitHub Desktop

Commit message format:
> `Add {Instrument} {Timeframe} Consecutive Bar Streaks v1.0`

---

## Naming Conventions

| Item | Convention | Example |
|---|---|---|
| CSV source | `{Instrument}_{TF}_{hash}.csv` | `CME_MINI_ES1!, 1W_3390c.csv` |
| MD report | `{Inst}_{Name}_v{ver}_{date}.md` | `ES_Weekly_Consecutive_Bar_Streaks_v1.0_20260610.md` |
| HTML report | same as MD but `.html` | `ES_Weekly_Consecutive_Bar_Streaks_v1.0_20260610.html` |
| Repo folder | `{INST}-{TF}-Consecutive-Bar-Streaks` | `ES-Weekly-Consecutive-Bar-Streaks` |
| Indicator file | `{Inst} {TF} Consecutive Bar Streaks - Pinescript.txt` | `ES Weekly Consecutive Bar Streaks - Pinescript.txt` |

---

## Completed Studies

| Instrument | Timeframe | Version | Date | Repo Folder |
|---|---|---|---|---|
| ES | Weekly | v1.2 | 20260610 | `ES-Weekly-Consecutive-Bar-Streaks` |

---

## Version History

| Version | Date | Changes |
|---|---|---|
| v1.0 | 20260610 | Initial release |
