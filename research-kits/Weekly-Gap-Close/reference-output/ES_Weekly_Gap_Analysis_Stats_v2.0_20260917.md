# ES RTH Weekly Gap Analysis -- Statistics v2.0 -- 20260917

Complete statistical record for the ES RTH weekly gap study. Every number in the companion HTML report is here, plus the tables the report leaves out.

The question: when ES opens the week away from last week's close, how often does price trade back to that close, on which day, and how many weeks does it take?

---

## Data Universe

| Item | Value |
|---|---|
| Instrument | CME E-Mini S&P 500 futures, RTH only (08:30 to 15:15 America/Chicago) |
| Source file | ES_daily_RTH_2009_2026.csv (60-minute RTH bars from the Zen Trading Tech public repo, rolled up to daily; 10 and 11 Sep 2026 from the UUBW daily file) |
| Daily bars in the study | 4,215 |
| Date range | 2009-12-07 to 2026-09-11 |
| Weekly bars | 875 |
| Weeks with a gap | 867 (99.1%) |
| Flat opens | 8 |
| 5-day weeks | 716 |
| Holiday-short weeks | 159 |
| Weeks starting on Tuesday | 85 |
| Weekly ABR | Average high-to-low range of the previous 8 weekly bars |
| Excluded days | 29 and 30 Oct 2012 (storm closure, one partial bar each) |

### Rules

- Week open = first trading day's open. Week close = last trading day's close.
- Weekly gap = this week's open minus last week's close.
- Gap size = absolute gap divided by weekly ABR, times 100.
- Closed = a day's low is at or below last week's close and its high is at or above it.
- First trading day = Monday, or Tuesday when Monday is a holiday.
- Weeks to close: 0 = same week, 1 = next week, and so on.
- The study starts at the first week with 8 prior weekly bars.

---

## 1. Population Funnel

| Box | Count | % of box above | % of all weeks |
|---|---|---|---|
| All weeks | 875 | -- | 100% |
| Opened with a gap | 867 | 99.1% | 99.1% |
| + gap up | 470 | 54.2% | 53.7% |
| + gap down | 397 | 45.8% | 45.4% |

## 2. Gap Overview

| Metric | Count | % of all gaps |
|---|---|---|
| Closed the same week | 684 | 78.9% |
| Closed on the first trading day | 475 | 54.8% |
| Closed later that week | 209 | 24.1% |
| Not closed that week | 183 | 21.1% |

### Gap up (n=470, average size 16.5% of weekly ABR)

| Outcome | Count | % |
|---|---|---|
| Closed the same week | 356 | 75.7% |
| Closed on the first trading day | 248 | 52.8% |
| Not closed that week | 114 | 24.3% |

### Gap down (n=397, average size 18.7% of weekly ABR)

| Outcome | Count | % |
|---|---|---|
| Closed the same week | 328 | 82.6% |
| Closed on the first trading day | 227 | 57.2% |
| Not closed that week | 69 | 17.4% |

## 3. Closure Timing

### By trading day of the week

| Day | Gaps closed | % of all gaps | % of closed gaps | Cumulative |
|---|---|---|---|---|
| Day 1 (first trading day) | 475 | 54.8% | 69.4% | 69.4% |
| Day 2 | 100 | 11.5% | 14.6% | 84.1% |
| Day 3 | 58 | 6.7% | 8.5% | 92.5% |
| Day 4 | 33 | 3.8% | 4.8% | 97.4% |
| Day 5 | 18 | 2.1% | 2.6% | 100.0% |

### By weekday name

| Day | Gaps closed | % of all gaps | % of closed gaps | Cumulative |
|---|---|---|---|---|
| Monday | 433 | 49.9% | 63.3% | 63.3% |
| Tuesday | 131 | 15.1% | 19.2% | 82.5% |
| Wednesday | 66 | 7.6% | 9.6% | 92.1% |
| Thursday | 32 | 3.7% | 4.7% | 96.8% |
| Friday | 22 | 2.5% | 3.2% | 100.0% |

## 4. Gap Size vs Closure Rate (% of weekly ABR, 8-bar)

| Gap size | Gaps | % of all gaps | Closed same week | Rate | First trading day (% of closed) | Closed within 4 weeks | Not closed by 11 Sep 2026 |
|---|---|---|---|---|---|---|---|
| 0–5% | 182 | 21.0% | 173 | 95.1% | 163 (94.2%) | 175 (96.2%) | 1 |
| 5–10% | 180 | 20.8% | 169 | 93.9% | 134 (79.3%) | 177 (98.3%) | 0 |
| 10–15% | 142 | 16.4% | 109 | 76.8% | 75 (68.8%) | 125 (88.0%) | 2 |
| 15–20% | 103 | 11.9% | 76 | 73.8% | 45 (59.2%) | 90 (87.4%) | 0 |
| 20–30% | 132 | 15.2% | 85 | 64.4% | 36 (42.4%) | 112 (84.8%) | 6 |
| 30–50% | 89 | 10.3% | 52 | 58.4% | 20 (38.5%) | 69 (77.5%) | 6 |
| 50–100% | 30 | 3.5% | 17 | 56.7% | 2 (11.8%) | 23 (76.7%) | 3 |
| 100%+ | 9 | 1.0% | 3 | 33.3% | 0 (0.0%) | 6 (66.7%) | 0 |

### Day spread of same-week closes, by gap size (% of that group's same-week closes)

| Gap size | Day 1 | Day 2 | Day 3 | Day 4 | Day 5 |
|---|---|---|---|---|---|
| 0–5% | 94.2% | 2.3% | 2.9% | 0.6% | 0.0% |
| 5–10% | 79.3% | 11.8% | 3.6% | 3.0% | 2.4% |
| 10–15% | 68.8% | 14.7% | 8.3% | 6.4% | 1.8% |
| 15–20% | 59.2% | 17.1% | 17.1% | 3.9% | 2.6% |
| 20–30% | 42.4% | 27.1% | 21.2% | 4.7% | 4.7% |
| 30–50% | 38.5% | 30.8% | 7.7% | 17.3% | 5.8% |
| 50–100% | 11.8% | 47.1% | 17.6% | 11.8% | 11.8% |
| 100%+ | 0.0% | 0.0% | 0.0% | 66.7% | 33.3% |

## 5. Closed vs Not Closed -- Size

| Group | Gaps | Average % of weekly ABR | Middle gap % of weekly ABR |
|---|---|---|---|
| Closed the same week | 684 | 14.5% | 10.0% |
| Not closed that week | 183 | 28.6% | 21.8% |

## 6. How Many Weeks Until the Gap Closes

| Time since gap open | Closed | % of all 867 gaps | Still open |
|---|---|---|---|
| Same week | 684 | 78.9% | 183 |
| Within 2 weeks | 746 | 86.0% | 121 |
| Within 3 weeks | 762 | 87.9% | 105 |
| Within 4 weeks | 777 | 89.6% | 90 |
| Within 8 weeks | 798 | 92.0% | 69 |
| Within 13 weeks | 814 | 93.9% | 53 |
| Within 26 weeks | 824 | 95.0% | 43 |
| Within 52 weeks | 833 | 96.1% | 34 |

### The 183 gaps not closed in their own week

| Closed | Gaps | % of these | % of all gaps |
|---|---|---|---|
| The next week | 62 | 33.9% | 7.2% |
| 2 to 3 weeks later | 31 | 16.9% | 3.6% |
| 4 to 12 weeks later | 37 | 20.2% | 4.3% |
| 13 to 51 weeks later | 19 | 10.4% | 2.2% |
| 52 weeks or more later | 16 | 8.7% | 1.8% |
| Not closed by 11 Sep 2026 | 18 | 9.8% | 2.1% |

### Gaps still open at 11 Sep 2026 (18 gaps, 16 gaps up)

| Week of | Direction | Gap size, % weekly ABR |
|---|---|---|
| 2011-11-28 | Up | 59.2% |
| 2012-01-03 | Up | 40.7% |
| 2012-11-19 | Up | 29.9% |
| 2012-12-24 | Down | 12.3% |
| 2012-12-31 | Up | 28.1% |
| 2016-02-16 | Up | 30.0% |
| 2016-11-07 | Up | 75.6% |
| 2020-04-06 | Up | 34.9% |
| 2020-05-18 | Up | 39.1% |
| 2020-05-26 | Up | 40.1% |
| 2020-11-02 | Up | 24.8% |
| 2020-11-09 | Up | 96.8% |
| 2022-10-17 | Up | 30.1% |
| 2023-10-30 | Up | 23.6% |
| 2025-05-12 | Up | 49.7% |
| 2025-05-27 | Up | 21.4% |
| 2026-08-03 | Up | 12.9% |
| 2026-09-08 | Down | 4.2% |

## 7. Last Week -- 8 to 11 Sep 2026

- Monday 7 Sep was a holiday. First trading day Tuesday. 4 trading days.
- Gap down, 4.24% of weekly ABR (the 0-5% group).
- Not closed that week.
- In the 0-5% group, 173 of 182 gaps (95.1%) closed the same week. Last week was one of the 9 that did not.

## Notes

- Gaps down closed the same week more often than gaps up (82.6% of 397 against 75.7% of 470).
- Gap size decides most of the result: 95.1% of gaps under 5% of weekly ABR closed the same week, 33.3% of the 9 gaps over 100%.
- Groups with fewer than 30 gaps are examples, not rates.
- v1.0 (20260304) reported 79.9% of 834 gaps closed the same week over Dec 2009 to Feb 2026. v2.0 on the new file gives 78.9% of 867 to Sep 2026. Over v1.0's own window this method gives 78.4%. v1.0's script was not kept, so the cause of the difference (79.9% against 78.4%) is unknown.

## Version History

| Version | Date | Changes |
|---|---|---|
| v1.0 | 20260304 | Original study, Dec 2009 to Feb 2026. |
| v2.0 | 20260917 | Rebuilt to 11 Sep 2026 on the public ES data. First-trading-day timing, sizes in % of weekly ABR only, weeks-to-close section, open-gap list, last-week example. Script published in the research kit. |
