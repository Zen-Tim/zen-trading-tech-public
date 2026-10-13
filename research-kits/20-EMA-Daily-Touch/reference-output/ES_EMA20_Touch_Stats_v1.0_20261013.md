# ES Price Touches the 20 EMA During the Day · Reference Output v1.0 · 20261013

## The question

How often does price touch the 20 EMA on the 5-minute chart at some point during the day session?
Al Brooks puts it at close to every day.

## Data

- File: `ES_5m_RTH_v1.0_20261013.csv`
- Days in file: 2,708, from 2016-01-04 to 2026-10-09
- Full sessions used: 2,683. Short sessions left out: 25.

## Result

| Outcome | Days | Share of all full sessions |
|---|---|---|
| Touched the 20 EMA at least once | 2,682 | 99.96% |
| Never touched it | 1 | 0.04% |
| First touch on bar 1 | 449 | 16.7% |
| First touch after bar 1 | 2,233 | 83.2% |

Days that never touched: 2024-01-08.

Bar 1 can touch the EMA simply because the day opens near it.
The first-touch rows show how much of the result comes from bar 1 alone.

Verdict: price touches the 20 EMA on 99.96% of 2,683 full ES sessions.
