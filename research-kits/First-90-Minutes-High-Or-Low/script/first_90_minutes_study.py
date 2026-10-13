"""High or low of the day inside the first 90 minutes. Zen Trading Tech research kit v1.0 20261013.

Input: a 5-minute CSV with columns date, time, bar, open, high, low, close (one day session per date).
Rules:
- The first 90 minutes is bars 1 to 18 on the 5-minute chart.
- Only full sessions count: 81 bars for ES (08:30 to 15:15 Chicago), 102 bars for FDAX (09:00 to 17:30 Berlin).
  Short holiday sessions are left out and counted separately.
- The bar that makes the high of the day is the FIRST bar whose high equals the session's highest high.
  The same rule applies to the low.
- A day counts when the high of the day, or the low of the day, or both, was made in bars 1 to 18.
Usage: python first_90_minutes_study.py <csv> <ES|FDAX> [output.md]
"""
import sys
import pandas as pd

FULL = {"ES": 81, "FDAX": 102}
OPEN = {"ES": (8, 30), "FDAX": (9, 0)}
src, inst = sys.argv[1], sys.argv[2].upper()
out = sys.argv[3] if len(sys.argv) > 3 else None
d = pd.read_csv(src)
d["day_high"] = d.groupby("date").high.transform("max")
d["day_low"] = d.groupby("date").low.transform("min")
g = d.groupby("date")
day = pd.DataFrame({
    "bars": g.size(),
    "hod_bar": d[d.high == d.day_high].groupby("date").bar.min(),
    "lod_bar": d[d.low == d.day_low].groupby("date").bar.min(),
})
full = day[day.bars == FULL[inst]].copy()
short = len(day) - len(full)
n = len(full)
full["first"] = full[["hod_bar", "lod_bar"]].min(axis=1)

def clock(bar):
    h, m = OPEN[inst]
    t = h * 60 + m + bar * 5
    return f"{t // 60:02d}:{t % 60:02d}"

zone = "Chicago" if inst == "ES" else "Berlin"
lines = [
    f"# {inst} High or Low of the Day in the First 90 Minutes · Reference Output v1.0 · 20261013",
    "",
    "## The question",
    "",
    "How often is the high of the day, or the low of the day, already in by the end of the first 18 five-minute bars?",
    "The commonly quoted figure is about 75% of days.",
    "",
    "## Data",
    "",
    f"- File: `{src.split('/')[-1]}`",
    f"- Days in file: {len(day):,}, from {day.index.min()} to {day.index.max()}",
    f"- Full sessions used: {n:,}. Short sessions left out: {short}.",
    "",
    "## Result: share of full sessions with at least one extreme in by the end of each bar",
    "",
    f"| By the end of bar | Clock time ({zone}) | High in | Low in | High or low in |",
    "|---|---|---|---|---|",
]
for b in (6, 12, 18, 24, 36):
    lines.append(f"| {b} | {clock(b)} | {(full.hod_bar <= b).mean() * 100:.1f}% | {(full.lod_bar <= b).mean() * 100:.1f}% | {(full['first'] <= b).mean() * 100:.1f}% |")
k = int((full["first"] <= 18).sum())
lines += [
    "",
    f"Bar 18 row in days: {k:,} of {n:,}.",
    "",
    "## By year, bar 18",
    "",
    "| Year | Full sessions | High or low in by bar 18 | Share of that year's sessions |",
    "|---|---|---|---|",
]
full["year"] = full.index.str[:4]
for y, x in full.groupby("year"):
    lines.append(f"| {y} | {len(x)} | {int((x['first'] <= 18).sum())} | {(x['first'] <= 18).mean() * 100:.1f}% |")
share = k / n * 100
lines += ["", f"Verdict: the high or low of the day is in by the end of bar 18 on {share:.1f}% of {n:,} full {inst} sessions. " + ("The 75% figure holds." if share >= 75 else "Below the 75% figure.")]
text = "\n".join(lines) + "\n"
print(text)
if out:
    open(out, "w", encoding="utf-8").write(text)
