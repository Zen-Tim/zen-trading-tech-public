# Zen Leg Pullback Levels

Draws pullback levels on every completed leg, six slots, each with its own percentage and its own on/off switch. A level's percentage is how much of the leg has been given back, measured from the leg's far end. 50% is the leg's midpoint. 66% gives back two thirds.

Leg detection copies the leg walk used in the UUBW v4 research app, bar for bar.

## The leg walk

A threshold-reversal walk, reset at every new day.

- The walk starts the day flat, with the day's open as the pivot.
- It turns up once price runs the threshold above the open, and down once price runs the threshold below it.
- While the walk is up, a new high extends the leg. A pullback of one full threshold off that high ends the leg and starts a down leg from it. Down legs are the mirror.
- A leg is recorded at the moment it is confirmed, which is when the counter-move reaches the threshold, not when the extreme printed.

The threshold is `k` times the prior day's average day range. Default `k` is 0.15 and the default lookback is 8 days, which is the combination the UUBW research uses.

Two differences from the research figures are worth knowing:

- The research walk runs on five-minute bars inside the regular session. This indicator walks whatever bars the chart shows, so leg counts only line up with the research on a five-minute chart set to regular hours.
- The day range comes from TradingView's standard daily bar, which on futures covers the whole 24-hour session. The research uses the regular-session day range. The threshold is therefore a little wider here than in the research. Everything still scales with volatility and stays comparable across instruments, and the indicator needs no session setup to run on any chart.

The last leg of each day is not drawn. It is still running when the session ends, so it has no confirmation bar.

## Levels

Each level is a horizontal line starting at the bar where the leg ended.

Because a leg only confirms after a full threshold pullback, the confirmation can arrive several bars after the leg's extreme. **Extend level to confirm bar** (on by default) runs the line from the leg's end through to the confirm bar plus the line length, so the level is still in front of price when price pulls back into it. Switched off, the line is a fixed-length segment starting at the leg's end, which on a slow pullback can sit entirely behind the current bar.

Up legs draw blue, down legs red. Each slot keeps its own set of lines, so "legs to show at once" trims each slot independently.

## Inputs

| Group | Input | Default |
|---|---|---|
| Leg detection | ADR lookback (days) | 8 |
| Leg detection | Leg threshold (x prior ADR) | 0.15 |
| Pullback levels | Level 1 / 2 / 3 | On — 50% / 66% / 75% |
| Pullback levels | Level 4 / 5 / 6 | Off — 25% / 33% / 20% |
| Pullback levels | Line length (bars) | 3 |
| Pullback levels | Extend level to confirm bar | On |
| Pullback levels | Level colour, up leg / down leg | Blue / Red |
| Display | Legs to show at once | 10 |
| Display | Show the leg zigzag | Off |
| Display | Label each leg's point size | Off |
| Display | Show threshold info box | On |

The info box sits top right and prints the prior day's average range and the resulting threshold in points, so the number on the chart can be checked against the research figures directly.

## Alerts

One alert condition per level, firing when price crosses that level of the most recent completed leg. Six in total.

## Version History

- **v1.2 (2026-09-13):** ADR lookback default changed from 14 days to 8, to match the UUBW threshold. Level lines now run through to the confirm bar by default, with a toggle for the old fixed-length behaviour. Licence and attribution header added.
- **v1.1 (2026-09-10):** Six level slots, optional leg zigzag and leg-size labels, threshold info box.
