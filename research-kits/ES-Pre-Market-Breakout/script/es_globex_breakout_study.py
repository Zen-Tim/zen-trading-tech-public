"""ES day session breaking the Globex high or low. Zen Trading Tech research kit v1.0 20261013.

Input: a 15-minute 24-hour ES CSV with columns time_chicago, open, high, low, close.
time_chicago is the bar's START time in Chicago local time.
Rules:
- Globex (overnight) session for a trading day: bars starting 17:00 the evening before, through the bar starting 08:15.
  A bar starting at or after 17:00 belongs to the next calendar day. Sunday evening belongs to Monday.
- Day session (RTH): bars starting 08:30 through the bar starting 15:00. That is 27 bars, ending 15:15.
- Bars starting 15:15 to 16:45 belong to neither session.
- Only full day sessions count (27 bars) that also have Globex bars. Short holiday sessions are left out.
- Broke the Globex high: a day-session high ABOVE the Globex high. Equal does not count.
- Broke the Globex low: a day-session low BELOW the Globex low. Equal does not count.
Usage: python es_globex_breakout_study.py <csv> [output.md]
"""
import sys
import pandas as pd

src = sys.argv[1]
out = sys.argv[2] if len(sys.argv) > 2 else None
d = pd.read_csv(src, parse_dates=["time_chicago"])
hm = d.time_chicago.dt.strftime("%H:%M")
d["session"] = d.time_chicago.dt.normalize() + pd.to_timedelta((hm >= "17:00").astype(int), unit="D")
d["part"] = "other"
d.loc[(hm >= "17:00") | (hm < "08:30"), "part"] = "globex"
d.loc[(hm >= "08:30") & (hm < "15:15"), "part"] = "rth"
gx = d[d.part == "globex"].groupby("session").agg(gx_high=("high", "max"), gx_low=("low", "min"))
rth = d[d.part == "rth"].groupby("session").agg(rth_high=("high", "max"), rth_low=("low", "min"), rth_bars=("high", "size"))
day = rth.join(gx, how="inner")
full = day[day.rth_bars == 27].copy()
short = len(day) - len(full)
full["up"] = full.rth_high > full.gx_high
full["down"] = full.rth_low < full.gx_low
n = len(full)
c = lambda m: (int(m.sum()), f"{m.mean() * 100:.1f}%")
rows = [("Broke the high only", full.up & ~full.down), ("Broke the low only", full.down & ~full.up),
        ("Broke both", full.up & full.down), ("Broke neither", ~full.up & ~full.down),
        ("Broke at least one side", full.up | full.down)]
lines = [
    "# ES Day Session Breaks the Globex High or Low · Reference Output v1.0 · 20261013",
    "",
    "## The question",
    "",
    "How often does the ES day session trade above the Globex high or below the Globex low?",
    "Globex is the overnight session before the day session opens. Al Brooks puts it at about 90% of days.",
    "",
    "## Data",
    "",
    f"- File: `{src.split('/')[-1]}`",
    f"- Days with both a Globex session and a day session: {len(day):,}, from {full.index.min():%Y-%m-%d} to {full.index.max():%Y-%m-%d}",
    f"- Full day sessions used: {n:,}. Short sessions left out: {short}.",
    "",
    "## Result",
    "",
    "| Outcome | Days | Share of all full sessions |",
    "|---|---|---|",
] + [f"| {name} | {c(m)[0]:,} | {c(m)[1]} |" for name, m in rows] + [
    "",
    "## By year",
    "",
    "| Year | Full sessions | Broke at least one side | Share of that year's sessions |",
    "|---|---|---|---|",
]
full["year"] = full.index.year
for y, x in full.groupby("year"):
    m = x.up | x.down
    lines.append(f"| {y} | {len(x)} | {int(m.sum())} | {m.mean() * 100:.1f}% |")
share = (full.up | full.down).mean() * 100
lines += ["", f"Verdict: the ES day session breaks at least one side of the Globex range on {share:.1f}% of {n:,} full sessions. " + ("The 90% claim holds." if share >= 90 else "Below the 90% claim.")]
text = "\n".join(lines) + "\n"
print(text)
if out:
    open(out, "w", encoding="utf-8").write(text)
