"""Price touching the 20 EMA during the day. Zen Trading Tech research kit v1.0 20261013.

Input: a 5-minute CSV with columns date, time, bar, open, high, low, close, ema20.
ema20 is a 20-bar exponential moving average of the 5-minute close:
  ema = previous ema + (2 / 21) x (close - previous ema), seeded with the first close in the history,
  computed on day-session bars only and carried from one day to the next. Rounded to 2 decimals.
Rules:
- A bar touches the 20 EMA when the EMA sits inside that bar's range: low <= ema20 <= high.
- Only full sessions count: 81 bars for ES (08:30 to 15:15 Chicago), 102 bars for FDAX (09:00 to 17:30 Berlin).
- A day counts when at least one bar touches.
Usage: python ema20_touch_study.py <csv> <ES|FDAX> [output.md]
"""
import sys
import pandas as pd

FULL = {"ES": 81, "FDAX": 102}
src, inst = sys.argv[1], sys.argv[2].upper()
out = sys.argv[3] if len(sys.argv) > 3 else None
d = pd.read_csv(src)
d["touch"] = (d.low <= d.ema20) & (d.ema20 <= d.high)
g = d.groupby("date")
day = pd.DataFrame({"bars": g.size(), "touched": g.touch.any(),
                    "first_touch": d[d.touch].groupby("date").bar.min()})
full = day[day.bars == FULL[inst]].copy()
short = len(day) - len(full)
n = len(full)
t = int(full.touched.sum())
misses = list(full.index[~full.touched])
lines = [
    f"# {inst} Price Touches the 20 EMA During the Day · Reference Output v1.0 · 20261013",
    "",
    "## The question",
    "",
    "How often does price touch the 20 EMA on the 5-minute chart at some point during the day session?",
    "Al Brooks puts it at close to every day.",
    "",
    "## Data",
    "",
    f"- File: `{src.split('/')[-1]}`",
    f"- Days in file: {len(day):,}, from {day.index.min()} to {day.index.max()}",
    f"- Full sessions used: {n:,}. Short sessions left out: {short}.",
    "",
    "## Result",
    "",
    "| Outcome | Days | Share of all full sessions |",
    "|---|---|---|",
    f"| Touched the 20 EMA at least once | {t:,} | {t / n * 100:.2f}% |",
    f"| Never touched it | {n - t} | {(n - t) / n * 100:.2f}% |",
    f"| First touch on bar 1 | {int((full.first_touch == 1).sum()):,} | {(full.first_touch == 1).mean() * 100:.1f}% |",
    f"| First touch after bar 1 | {int((full.first_touch > 1).sum()):,} | {(full.first_touch > 1).mean() * 100:.1f}% |",
    "",
    "Days that never touched: " + (", ".join(misses) if misses else "none") + ".",
    "",
    "Bar 1 can touch the EMA simply because the day opens near it.",
    "The first-touch rows show how much of the result comes from bar 1 alone.",
    "",
    f"Verdict: price touches the 20 EMA on {t / n * 100:.2f}% of {n:,} full {inst} sessions.",
]
text = "\n".join(lines) + "\n"
print(text)
if out:
    open(out, "w", encoding="utf-8").write(text)
