"""Bar 1 as the high or low of the day. Zen Trading Tech research kit v1.0 20261013.

Input: a 5-minute CSV with columns date, time, bar, open, high, low, close (one day session per date).
Rules:
- Bar 1 is the first 5-minute bar of the day session.
- Only full sessions count: 81 bars for ES (08:30 to 15:15 Chicago), 102 bars for FDAX (09:00 to 17:30 Berlin).
  Short holiday sessions are left out and counted separately.
- Bar 1 makes the high of the day when bar 1's high equals the highest high of the session.
  A later bar that only matches it does not take it away, because bar 1 printed that price first.
- The same rule applies to the low.
Usage: python bar1_high_low_study.py <csv> <ES|FDAX> [output.md]
"""
import sys
import pandas as pd

FULL = {"ES": 81, "FDAX": 102}
src, inst = sys.argv[1], sys.argv[2].upper()
out = sys.argv[3] if len(sys.argv) > 3 else None
d = pd.read_csv(src)
g = d.groupby("date")
day = pd.DataFrame({
    "bars": g.size(),
    "day_high": g.high.max(),
    "day_low": g.low.min(),
    "b1_high": g.apply(lambda x: x.loc[x.bar == 1, "high"].iloc[0], include_groups=False),
    "b1_low": g.apply(lambda x: x.loc[x.bar == 1, "low"].iloc[0], include_groups=False),
})
full = day[day.bars == FULL[inst]].copy()
short = len(day) - len(full)
full["hod"] = full.b1_high == full.day_high
full["lod"] = full.b1_low == full.day_low
full["either"] = full.hod | full.lod
n = len(full)
pct = lambda k: f"{k / n * 100:.1f}%"
lines = [
    f"# {inst} Bar 1 as the High or Low of the Day · Reference Output v1.0 · 20261013",
    "",
    "## The question",
    "",
    "How often does the first 5-minute bar of the day session make the high of the day or the low of the day?",
    "The commonly quoted figure is about 20% of days.",
    "",
    "## Data",
    "",
    f"- File: `{src.split('/')[-1]}`",
    f"- Days in file: {len(day):,}, from {day.index.min()} to {day.index.max()}",
    f"- Full sessions used: {n:,}. Short sessions left out: {short}.",
    "",
    "## Result, all full sessions",
    "",
    "| Outcome | Days | Share of all full sessions |",
    "|---|---|---|",
    f"| Bar 1 makes the high of the day | {int(full.hod.sum()):,} | {pct(full.hod.sum())} |",
    f"| Bar 1 makes the low of the day | {int(full.lod.sum()):,} | {pct(full.lod.sum())} |",
    f"| Bar 1 makes both | {int((full.hod & full.lod).sum()):,} | {pct((full.hod & full.lod).sum())} |",
    f"| Bar 1 makes either one | {int(full.either.sum()):,} | {pct(full.either.sum())} |",
    f"| Bar 1 makes neither | {int((~full.either).sum()):,} | {pct((~full.either).sum())} |",
    "",
    "## By year",
    "",
    "| Year | Full sessions | Bar 1 makes either one | Share of that year's sessions |",
    "|---|---|---|---|",
]
full["year"] = full.index.str[:4]
for y, x in full.groupby("year"):
    lines.append(f"| {y} | {len(x)} | {int(x.either.sum())} | {x.either.mean() * 100:.1f}% |")
lines += ["", f"Verdict: bar 1 makes the high or low of the day on {pct(full.either.sum())} of {n:,} full {inst} sessions. The figure of about 20% is too low." if full.either.mean() >= 0.2 else f"Verdict: {pct(full.either.sum())} of days, below the 20% figure."]
text = "\n".join(lines) + "\n"
print(text)
if out:
    open(out, "w", encoding="utf-8").write(text)
