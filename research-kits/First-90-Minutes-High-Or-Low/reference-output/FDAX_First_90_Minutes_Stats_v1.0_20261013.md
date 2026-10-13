# FDAX High or Low of the Day in the First 90 Minutes · Reference Output v1.0 · 20261013

## The question

How often is the high of the day, or the low of the day, already in by the end of the first 18 five-minute bars?
Al Brooks puts it at about 75% of days.

## Data

- File: `FDAX_5m_Xetra_v1.0_20261013.csv`
- Days in file: 1,303, from 2021-09-01 to 2026-10-09
- Full sessions used: 1,300. Short sessions left out: 3.

## Result: share of full sessions with at least one extreme in by the end of each bar

| By the end of bar | Clock time (Berlin) | High in | Low in | High or low in |
|---|---|---|---|---|
| 6 | 09:30 | 26.0% | 27.5% | 52.9% |
| 12 | 10:00 | 31.2% | 35.4% | 65.3% |
| 18 | 10:30 | 36.9% | 41.1% | 75.5% |
| 24 | 11:00 | 40.6% | 44.5% | 80.7% |
| 36 | 12:00 | 45.4% | 50.5% | 87.4% |

Bar 18 row in days: 981 of 1,300.

## By year, bar 18

| Year | Full sessions | High or low in by bar 18 | Share of that year's sessions |
|---|---|---|---|
| 2021 | 86 | 73 | 84.9% |
| 2022 | 254 | 194 | 76.4% |
| 2023 | 255 | 192 | 75.3% |
| 2024 | 254 | 190 | 74.8% |
| 2025 | 253 | 199 | 78.7% |
| 2026 | 198 | 133 | 67.2% |

Verdict: the high or low of the day is in by the end of bar 18 on 75.5% of 1,300 full FDAX sessions. The 75% claim holds.
