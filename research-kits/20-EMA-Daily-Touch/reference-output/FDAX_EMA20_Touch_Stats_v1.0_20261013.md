# FDAX Price Touches the 20 EMA During the Day · Reference Output v1.0 · 20261013

## The question

How often does price touch the 20 EMA on the 5-minute chart at some point during the day session?
The commonly quoted figure is close to every day.

## Data

- File: `FDAX_5m_Xetra_v1.0_20261013.csv`
- Days in file: 1,303, from 2021-09-01 to 2026-10-09
- Full sessions used: 1,300. Short sessions left out: 3.

## Result

| Outcome | Days | Share of all full sessions |
|---|---|---|
| Touched the 20 EMA at least once | 1,300 | 100.00% |
| Never touched it | 0 | 0.00% |
| First touch on bar 1 | 261 | 20.1% |
| First touch after bar 1 | 1,039 | 79.9% |

Days that never touched: none.

Bar 1 can touch the EMA simply because the day opens near it.
The first-touch rows show how much of the result comes from bar 1 alone.

Verdict: price touches the 20 EMA on 100.00% of 1,300 full FDAX sessions.
