# ES Weekly Consecutive Bar Streaks v1.2 · 20260610

**Instrument:** CME Mini ES (ES1!) · Weekly bars  
**Date range:** 1997-09-08 to 2026-06-08  
**Total bars:** 1,501  
**Version:** v1.2 · 20260610

---

## Data Universe

- **Instrument:** CME E-mini S&P 500 (ES1!), weekly bars
- **Source:** TradingView export with pre-computed Bar Direction and Consecutive Bar columns
- **Date range:** 1997-09-08 to 2026-06-08
- **Total bars:** 1,501
- **Bar Direction definition:** +1 if close > open, -1 if close < open, 0 if close == open (doji)
- **Consecutive bars definition:** running signed count of same-direction bars; resets on direction change or doji
- **Doji bars:** 5 (0.3% of all bars) — doji ends a run of consecutive bars and is excluded from continuation analysis

---

## Research

### Overall Population

| Metric | Count | % of Total |
|---|---|---|
| Total bars | 1,501 | 100% |
| Bull bars (+1) | 841 | 56.1% |
| Bear bars (-1) | 655 | 43.6% |
| Doji bars (0) | 5 | 0.3% |
| Max consecutive bull bars | 11 | — |
| Max consecutive bear bars | 8 | — |

---

### Bull Consecutive Bars — Continuation Probability

"After N consecutive bull weeks, what is the probability the next week is also bull?"

| Consecutive Bull Bars | Occurrences | Occ% of Total | Continue | Cont% |
|---|---|---|---|---|
| 2 | 202 | 13.5% | 111 | 55.0% |
| 3 | 111 | 7.4% | 61 | 55.0% |
| 4 | 61 | 4.1% | 41 | 67.2% |
| 5 | 41 | 2.7% | 20 | 48.8% |
| 6 | 20 | 1.3% | 11 | 55.0% |
| 7 | 11 | 0.7% | 7 | 63.6% |
| 8 | 7 | 0.5% | 5 | 71.4% |
| 9 | 5 | 0.3% | 1 | 20.0% |
| 10 | 1 | 0.1% | 1 | 100.0% |
| 11 | 1 | 0.1% | 0 | 0.0% |

**Notes:**
- Rows 9–11 have sample sizes of 1–5. No conclusions can be drawn from those rows.
- 4 consecutive bull bars shows the strongest continuation reading with a meaningful sample: 67.2% (n=61).
- Rows 2, 3, and 6 cluster tightly around 55%, close to the unconditional bull rate of 56.1%.
- There is no evidence of mean reversion at any run length with a reliable sample size.

---

### Bear Consecutive Bars — Continuation Probability

"After N consecutive bear weeks, what is the probability the next week is also bear?"

Cont% below 50% means reversal is more likely than continuation.

| Consecutive Bear Bars | Occurrences | Occ% of Total | Continue | Cont% |
|---|---|---|---|---|
| 2 | 159 | 10.6% | 67 | 42.1% |
| 3 | 67 | 4.5% | 31 | 46.3% |
| 4 | 31 | 2.1% | 10 | 32.3% |
| 5 | 10 | 0.7% | 6 | 60.0% |
| 6 | 6 | 0.4% | 2 | 33.3% |
| 7 | 2 | 0.1% | 1 | 50.0% |
| 8 | 1 | 0.1% | 0 | 0.0% |

**Notes:**
- Rows 5–8 have sample sizes of 1–10. Treat with extreme caution.
- 2–4 consecutive bear bars show consistent reversal bias: 57.9%, 53.7%, 67.7%.
- The reversal bias at 2 consecutive bear bars (57.9% reversal) contrasts with 2 consecutive bull bars (55.0% continuation). Bear is the more mean-reverting side.
- 5 consecutive bear bars (60% continue) is the only exception but n=10 limits any conclusion.

---

### Consecutive Bar Runs — Population Funnel

How many distinct runs reached each depth? % of total is expressed as a share of all 1,501 weekly bars.

| Consecutive Bars | Bull Runs | Bull % of Total | Bear Runs | Bear % of Total |
|---|---|---|---|---|
| 2 | 202 | 13.5% | 159 | 10.6% |
| 3 | 111 | 7.4% | 67 | 4.5% |
| 4 | 61 | 4.1% | 31 | 2.1% |
| 5 | 41 | 2.7% | 10 | 0.7% |
| 6 | 20 | 1.3% | 6 | 0.4% |
| 7 | 11 | 0.7% | 2 | 0.1% |
| 8 | 7 | 0.5% | 1 | 0.1% |
| 9 | 5 | 0.3% | — | — |
| 10 | 1 | 0.1% | — | — |
| 11 | 1 | 0.1% | — | — |

Bull consecutive runs extend deeper than bear (max 11 vs max 8), consistent with the secular upward drift in the S&P 500 over this period. Bear runs thin out much faster — only 10 runs ever reached 5 consecutive bear weeks in 29 years.

---

## Vocabulary

| Term | Definition |
|---|---|
| Bar Direction | +1 = close > open (bull bar); -1 = close < open (bear bar); 0 = close == open (doji) |
| Consecutive bars | Count of same-direction bars in a row. Resets to 1 on direction change, 0 on doji |
| Continue | Next bar has the same direction as the current run |
| Reverse | Next bar has the opposite direction |
| Occurrences | Number of weekly bars that were the Nth bar in a consecutive run |
| Occ% of Total | Occurrences as a percentage of all 1,501 bars in the study |

---

## Version History

| Version | Date | Changes |
|---|---|---|
| v1.0 | 20260610 | Initial release — weekly ES data, consecutive bar population and continuation tables |
| v1.1 | 20260610 | Removed Rev% and Doji columns; tables start at 2; funnel table shows % of total; replaced streak/depth terminology |
| v1.2 | 20260610 | Removed Summary section; added Occ% of Total column to bull and bear continuation tables; removed Overall Population prose sentence; removed Sanity Checks section |

---

*ES Weekly Consecutive Bar Streaks · v1.2 · 20260610*
