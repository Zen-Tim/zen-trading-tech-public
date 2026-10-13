# ES High or Low of the Day in the First 90 Minutes · Reference Output v1.0 · 20261013

## The question

How often is the high of the day, or the low of the day, already in by the end of the first 18 five-minute bars?
Al Brooks puts it at about 75% of days.

## Data

- File: `ES_5m_RTH_v1.0_20261013.csv`
- Days in file: 2,708, from 2016-01-04 to 2026-10-09
- Full sessions used: 2,683. Short sessions left out: 25.

## Result: share of full sessions with at least one extreme in by the end of each bar

| By the end of bar | Clock time (Chicago) | High in | Low in | High or low in |
|---|---|---|---|---|
| 6 | 09:00 | 24.3% | 29.6% | 53.6% |
| 12 | 09:30 | 33.0% | 39.5% | 70.5% |
| 18 | 10:00 | 38.5% | 47.4% | 80.7% |
| 24 | 10:30 | 44.1% | 52.7% | 87.1% |
| 36 | 11:30 | 50.7% | 60.7% | 93.1% |

Bar 18 row in days: 2,165 of 2,683.

## By year, bar 18

| Year | Full sessions | High or low in by bar 18 | Share of that year's sessions |
|---|---|---|---|
| 2016 | 251 | 210 | 83.7% |
| 2017 | 249 | 218 | 87.6% |
| 2018 | 248 | 199 | 80.2% |
| 2019 | 249 | 202 | 81.1% |
| 2020 | 247 | 183 | 74.1% |
| 2021 | 251 | 212 | 84.5% |
| 2022 | 250 | 204 | 81.6% |
| 2023 | 248 | 191 | 77.0% |
| 2024 | 249 | 184 | 73.9% |
| 2025 | 247 | 198 | 80.2% |
| 2026 | 194 | 164 | 84.5% |

Verdict: the high or low of the day is in by the end of bar 18 on 80.7% of 2,683 full ES sessions. The 75% claim holds.
