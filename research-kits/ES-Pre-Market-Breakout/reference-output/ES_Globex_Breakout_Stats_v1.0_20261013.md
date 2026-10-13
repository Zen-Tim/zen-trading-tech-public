# ES Day Session Breaks the Globex High or Low · Reference Output v1.0 · 20261013

## The question

How often does the ES day session trade above the Globex high or below the Globex low?
Globex is the overnight session before the day session opens. The commonly quoted figure is about 90% of days.

## Data

- File: `ES_15m_24hour_v1.0_20261013.csv`
- Days with both a Globex session and a day session: 1,884, from 2019-06-03 to 2026-09-18
- Full day sessions used: 1,820. Short sessions left out: 64.

## Result

| Outcome | Days | Share of all full sessions |
|---|---|---|
| Broke the high only | 731 | 40.2% |
| Broke the low only | 601 | 33.0% |
| Broke both | 390 | 21.4% |
| Broke neither | 98 | 5.4% |
| Broke at least one side | 1,722 | 94.6% |

## By year

| Year | Full sessions | Broke at least one side | Share of that year's sessions |
|---|---|---|---|
| 2019 | 145 | 129 | 89.0% |
| 2020 | 251 | 223 | 88.8% |
| 2021 | 251 | 240 | 95.6% |
| 2022 | 250 | 243 | 97.2% |
| 2023 | 248 | 242 | 97.6% |
| 2024 | 249 | 240 | 96.4% |
| 2025 | 247 | 234 | 94.7% |
| 2026 | 179 | 171 | 95.5% |

Verdict: the ES day session breaks at least one side of the Globex range on 94.6% of 1,820 full sessions. The 90% figure holds.
