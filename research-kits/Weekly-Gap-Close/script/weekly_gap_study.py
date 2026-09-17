"""ES Weekly Gap Close study. Input: daily RTH OHLC CSV (time,open,high,low,close).
Rules:
- Week = calendar week (Monday to Sunday). Week open = first trading day open. Week close = last trading day close.
- Weekly gap = this week's open minus last week's close. Zero = flat, no gap.
- Weekly ABR = average high-to-low range of the previous 8 completed weekly bars.
- Gap size = absolute gap / weekly ABR x 100.
- Closed = any day's high >= last week's close >= that day's low.
- Weeks to close: 0 = same week, 1 = the next week, and so on. Blank = not closed by the end of the data.
- The study starts at the first week that has 8 prior weeks for the ABR.
"""
import sys, json
import pandas as pd, numpy as np
src = sys.argv[1] if len(sys.argv) > 1 else "ES_daily_RTH_v1.0_20260917.csv"
d = pd.read_csv(src)
d["date"] = pd.to_datetime(d["time"].astype(str).str[:10])
d = d.sort_values("date").reset_index(drop=True)
d["weekday"] = d.date.dt.day_name()
d["week"] = d.date.dt.to_period("W-SUN")
W = d.groupby("week").agg(start=("date","first"), end=("date","last"), open=("open","first"),
      high=("high","max"), low=("low","min"), close=("close","last"), days=("date","size"),
      first_day=("weekday","first")).reset_index()
W["range"] = W.high - W.low
W["abr8"] = W["range"].shift(1).rolling(8).mean()
W["prior_close"] = W.close.shift(1)
W["gap"] = W.open - W.prior_close
W["gap_pct_abr"] = W.gap.abs() / W.abr8 * 100
W["direction"] = np.select([W.gap > 0, W.gap < 0], ["Up", "Down"], "Flat")
days_by_week = {w: g for w, g in d.groupby("week")}
weeks = list(W.week)
out = []
for i in range(len(W)):
    if pd.isna(W.abr8[i]): continue
    r = W.iloc[i]; lvl = r.prior_close
    res = dict(close_date=None, close_weekday=None, close_trading_day=None, weeks_to_close=None)
    if r.direction != "Flat":
        for j in range(i, len(W)):
            g = days_by_week[weeks[j]]
            hit = g[(g.low <= lvl) & (g.high >= lvl)]
            if len(hit):
                h = hit.iloc[0]
                res.update(close_date=h.date.strftime("%Y-%m-%d"), weeks_to_close=j - i)
                if j == i:
                    res.update(close_weekday=h.weekday, close_trading_day=int(g.index.get_loc(h.name)) + 1)
                break
    out.append(dict(week_start=r.start.strftime("%Y-%m-%d"), week_end=r.end.strftime("%Y-%m-%d"),
        trading_days=int(r.days), first_day=r.first_day, open=r.open, high=r.high, low=r.low, close=r.close,
        prior_close=r.prior_close, gap_pct_abr=round(r.gap_pct_abr, 2), direction=r.direction, **res))
R = pd.DataFrame(out)
R.to_csv("weekly_gap_results.csv", index=False)

G = R[R.direction != "Flat"]; n = len(G)
pct = lambda a, b: round(a / b * 100, 1) if b else None
S = {}
S["universe"] = dict(daily_bars=int(((d.date >= R.week_start.min()) & (d.date <= R.week_end.max())).sum()),
    first=R.week_start.min(), last=R.week_end.max(), weeks=len(R), gaps=n, flat=int((R.direction=="Flat").sum()),
    five_day_weeks=int((R.trading_days==5).sum()), short_weeks=int((R.trading_days<5).sum()),
    tuesday_starts=int((R.first_day=="Tuesday").sum()))
wk = G[G.weeks_to_close == 0]
S["overall"] = dict(closed_week=len(wk), closed_week_pct=pct(len(wk), n),
    first_day=int((G.close_trading_day==1).sum()), first_day_pct=pct((G.close_trading_day==1).sum(), n),
    later_in_week=int((G.close_trading_day>1).sum()), later_pct=pct((G.close_trading_day>1).sum(), n),
    not_in_week=n-len(wk), not_in_week_pct=pct(n-len(wk), n))
S["direction"] = {}
for dr in ["Up", "Down"]:
    x = G[G.direction == dr]; c = (x.weeks_to_close == 0).sum(); f = (x.close_trading_day == 1).sum()
    S["direction"][dr] = dict(n=len(x), share=pct(len(x), n), closed_week=int(c), closed_week_pct=pct(c, len(x)),
        first_day=int(f), first_day_pct=pct(f, len(x)), not_closed=int(len(x)-c), not_closed_pct=pct(len(x)-c, len(x)),
        avg_gap_pct_abr=round(x.gap_pct_abr.mean(), 1))
# closure day by weekday and by trading day number
S["by_weekday"] = []; cum = 0
for day in ["Monday","Tuesday","Wednesday","Thursday","Friday"]:
    c = int((wk.close_weekday == day).sum()); cum += c
    S["by_weekday"].append(dict(day=day, closed=c, of_all=pct(c, n), of_closed=pct(c, len(wk)), cumulative=pct(cum, len(wk))))
S["by_trading_day"] = []; cum = 0
for k in range(1, 6):
    c = int((wk.close_trading_day == k).sum()); cum += c
    S["by_trading_day"].append(dict(day=k, closed=c, of_all=pct(c, n), of_closed=pct(c, len(wk)), cumulative=pct(cum, len(wk))))
# gap size buckets
edges = [0,5,10,15,20,30,50,100,1e9]; labels = ["0–5%","5–10%","10–15%","15–20%","20–30%","30–50%","50–100%","100%+"]
G = G.assign(bucket=pd.cut(G.gap_pct_abr, edges, labels=labels, right=False))
S["buckets"] = []
for b in labels:
    x = G[G.bucket == b]; c = x[x.weeks_to_close == 0]
    spread = {dd: pct((c.close_trading_day == k).sum(), len(c)) for k, dd in enumerate(["d1","d2","d3","d4","d5"], 1)}
    S["buckets"].append(dict(bucket=b, n=len(x), of_all=pct(len(x), n), closed=len(c), rate=pct(len(c), len(x)),
        first_day=int((c.close_trading_day==1).sum()), first_day_of_closed=pct((c.close_trading_day==1).sum(), len(c)),
        within4=int((x.weeks_to_close<=3).sum()), within4_pct=pct((x.weeks_to_close<=3).sum(), len(x)),
        not_yet=int(x.weeks_to_close.isna().sum()), spread=spread))
cl = G[G.weeks_to_close == 0]; nc = G[G.weeks_to_close != 0]
S["closed_vs_not"] = dict(closed_n=len(cl), closed_avg=round(cl.gap_pct_abr.mean(),1), closed_median=round(cl.gap_pct_abr.median(),1),
    not_n=len(nc), not_avg=round(nc.gap_pct_abr.mean(),1), not_median=round(nc.gap_pct_abr.median(),1))
# weeks to close ladder
S["ladder"] = []
for label, k in [("Same week",0),("Within 2 weeks",1),("Within 3 weeks",2),("Within 4 weeks",3),("Within 8 weeks",7),("Within 13 weeks",12),("Within 26 weeks",25),("Within 52 weeks",51)]:
    c = int((G.weeks_to_close <= k).sum()); S["ladder"].append(dict(label=label, weeks=k+1, closed=c, pct=pct(c, n)))
S["after_week1"] = dict(n=int((G.weeks_to_close != 0).sum()),
    next_week=int((G.weeks_to_close==1).sum()), w2_3=int(G.weeks_to_close.between(2,3).sum()),
    w4_12=int(G.weeks_to_close.between(4,12).sum()), w13_51=int(G.weeks_to_close.between(13,51).sum()),
    w52plus=int((G.weeks_to_close>=52).sum()), not_yet=int(G.weeks_to_close.isna().sum()),
    not_yet_up=int((G.weeks_to_close.isna() & (G.direction=="Up")).sum()))
S["not_yet_list"] = G[G.weeks_to_close.isna()][["week_start","direction","gap_pct_abr"]].to_dict("records")
S["last_week"] = R.iloc[-1][["week_start","week_end","trading_days","first_day","direction","gap_pct_abr","weeks_to_close"]].astype(str).to_dict()
json.dump(S, open("weekly_gap_summary.json","w"), indent=1, default=str)
print(json.dumps(S, indent=1, default=str))
