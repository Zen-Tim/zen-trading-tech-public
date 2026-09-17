# Step 2 — Run the Study

Two ways to run it. Both follow the same rules and give the same numbers.

- **A — AI chat.** Paste the prompt below into Claude with the CSV and the two reference files attached.
- **B — Python.** Run `script/weekly_gap_study.py` on the CSV.

---

## The rules

These rules define every number in the report. Keep them the same if you want your result to compare with the ES reference.

1. **Week.** A calendar week, Monday to Sunday. Holiday weeks have fewer trading days.
2. **Week open** = the first trading day's open. **Week close** = the last trading day's close.
3. **Weekly gap** = this week's open minus last week's close. Positive is a gap up. Negative is a gap down. Zero is a flat open with no gap.
4. **Weekly ABR** (average bar range) = the average high-to-low range of the **previous 8 weekly bars**. It does not include the current week.
5. **Gap size** = the gap, ignoring its sign, divided by weekly ABR, times 100. A gap of 10% is one tenth of an average week's range.
6. **Closed** = on some later day, the low is at or below last week's close and the high is at or above it. Price has traded back to the level.
7. **First trading day** = Monday, or Tuesday when Monday is a holiday. Count closes by trading day of the week, not by weekday name, so holiday weeks are handled correctly.
8. **Weeks to close** = 0 if the gap closed in its own week, 1 if it closed the next week, and so on. A gap still open at the end of the data is recorded as not closed.
9. **Start** at the first week that has 8 prior weekly bars for the ABR.
10. **Gap size groups:** 0–5%, 5–10%, 10–15%, 15–20%, 20–30%, 30–50%, 50–100%, 100% and over. A group includes its lower edge and excludes its upper edge.

---

## Option A — the AI prompt

Attach three files:

1. Your CSV — or `data/ES_daily_RTH_v1.0_20260917.csv`
2. `reference-output/ES_Weekly_Gap_Analysis_v2.0_20260917.html`
3. `reference-output/ES_Weekly_Gap_Analysis_Stats_v2.0_20260917.md`

---PROMPT START---

I want you to run a Weekly Gap Close study on the attached CSV of daily bars and produce two files: an HTML report and a markdown summary. Mimic the attached ES reference report and markdown exactly.

## The data

The CSV holds daily day-session bars with columns `time`, `open`, `high`, `low`, `close`. Ignore any other columns.

## The rules

1. Group days into calendar weeks (Monday to Sunday). Week open = first trading day's open. Week close = last trading day's close. Week high and low = highest high and lowest low of the week.
2. Weekly gap = this week's open minus last week's close. Positive = gap up, negative = gap down, zero = flat (no gap).
3. Weekly ABR = the average high-to-low range of the previous 8 completed weekly bars, not including the current week.
4. Gap size = absolute gap / weekly ABR x 100.
5. A gap is closed on the first day where low <= last week's close <= high. Search the gap's own week first, then every later week.
6. Record for each gap: the trading day number within its week that closed it (1 = first trading day), the weekday name, and weeks to close (0 = same week).
7. Start at the first week with 8 prior weekly bars.
8. Gap size groups: 0–5, 5–10, 10–15, 15–20, 20–30, 30–50, 50–100, 100+ (lower edge included, upper edge excluded).

## What to compute

1. Data universe: daily bars, weekly bars, weeks with a gap, flat opens, 5-day weeks, holiday-short weeks, weeks starting on Tuesday. Show a population funnel with count, % of the box above and % of all weeks.
2. Gap overview, for all gaps and for gaps up and gaps down separately: closed the same week, closed on the first trading day, not closed that week.
3. Closure timing: same-week closes by trading day number (Day 1 to 5) and by weekday name, with % of all gaps, % of closed gaps and a running total.
4. Gap size table: gaps per group, % of all gaps, closed the same week, closure rate, first-trading-day closes as % of that group's closes, closed within 4 weeks, still open at the end of the data, and the day spread.
5. Size of closed against not-closed gaps: average and middle gap size in % of weekly ABR.
6. Weeks to close: the share of all gaps closed within 1, 2, 3, 4, 8, 13, 26 and 52 weeks. For gaps that missed their own week: closed next week, 2–3 weeks later, 4–12, 13–51, 52 or more, not closed yet. List every gap still open.
7. The latest week in the data as a worked example.

Express every gap size as a % of weekly ABR. Never use points or price units.

**Print the gap overview, closure timing, gap size and weeks-to-close tables as plain text in chat before building any files.** Wait for my confirmation.

## The HTML report

Mimic the attached ES reference exactly: same sections, same colours, same pills (green 65% or more, red 35% or less, amber between), same two charts, same KPI cards. Substitute the instrument, date range and numbers. Single self-contained HTML file, UTF-8, light theme only.

## The markdown summary

Mimic the attached ES markdown: every table from the report plus the day spread by gap size and the full open-gap list.

## Filenames

- `{INSTRUMENT}_Weekly_Gap_Analysis_v1.0_{YYYYMMDD}.html`
- `{INSTRUMENT}_Weekly_Gap_Analysis_Stats_v1.0_{YYYYMMDD}.md`

---PROMPT END---

---

## Option B — the Python script

Needs Python 3 and pandas.

```
pip install pandas
python script/weekly_gap_study.py data/ES_daily_RTH_v1.0_20260917.csv
```

It writes two files next to where you run it:

- `weekly_gap_results.csv` — one row per week: the gap, its size, whether and when it closed
- `weekly_gap_summary.json` — every number in the reference report

Run it on the provided ES file and check three numbers against the reference: 867 gaps, 684 closed the same week (78.9%), 475 closed on the first trading day (54.8%). If they match, your setup is right. Then point it at your own CSV.

---

## Check your AI result

AI chats make counting mistakes on long files. Before you trust a report built by option A, check these three ES numbers:

| Check | ES value |
|---|---|
| Weekly gaps | 867 |
| Closed the same week | 684 (78.9%) |
| Closed within 4 weeks | 777 (89.6%) |

If any differ, ask the AI to show its weekly table for one year and compare it to `weekly_gap_results.csv` from the script.
