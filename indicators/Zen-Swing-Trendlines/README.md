# Zen Swing Trendlines

Joins swing points into trendlines and projects them forward. A bear line runs from an older swing high down to a newer, lower swing high. A bull line runs from an older swing low up to a newer, higher swing low. Built as a scalp-zone tool: a line either holds or it fails, and a failure is a second entry.

## Swing points

Swing timing follows the Zen Swing 123 rule, taken from Perry Kaufman's *Trading Systems and Methods*. A swing flips after N bars in a row move against it. N is 1, 2, 3 or 4.

- **2** is Kaufman's base method.
- **1** catches one-bar pullbacks and gives many more swing points.

The anchor is the true highest high (or lowest low) since the previous swing point, not the bar that confirmed the flip. Zen TSaM Swing Chart v2.2 starts each leg at the confirm bar instead, so the two indicators place their points differently on purpose.

## How a line is built

A line is only built on the bar its second swing point is confirmed. Everything drawn to the left of that bar is drawn after the fact.

Tolerance is the share of ABR a wick or a close may pass the line and still count as sitting on it. ABR is the 8-bar average range of prior bars, so tolerance scales with the timeframe rather than being a fixed tick count.

Five tests, in order:

1. **Clean** — no wick between the two anchors passes the line by more than tolerance.
2. **Intact** — no close through the line, beyond tolerance, from the second anchor to the confirm bar.
3. **Slope** — points per bar divided by ABR must not exceed the maximum steepness. This blocks near-vertical lines.
4. **Ends** — a line ends when a bar closes through it (broken) or when it reaches its life limit (expired). Ended lines are left faded, not deleted.
5. **Tidy** — per new swing point, keep the steepest and the shallowest candidate only; one live line per start point; a cap on live lines per side. Replaced and capped lines end faded so every line that appeared stays visible.

Live lines hide when price is far away. That test runs on the last bar only, so historical bars keep all their lines.

## Inputs

| Group | Input | Default |
|---|---|---|
| Swing Points | Bars against to confirm | 1 (options 1/2/3/4) |
| Swing Points | Show swing points | Off |
| Line Builder | Older swing points to try | 5 |
| Line Builder | Max bars between anchors | 150 |
| Line Builder | Max steepness (ABR per bar) | 1.0 |
| Line Builder | Tolerance (share of ABR) | 0.10 |
| Line Builder | Steepest + shallowest only | On |
| Line Builder | One live line per start point | On |
| Relevance | Max live lines per side | 3 |
| Relevance | Line life (bars after it appears) | 78 |
| Relevance | Hide live line beyond (ABR from price) | 4.0 |
| Relevance | Today's session only | Off |
| Visuals | Project live lines forward (bars) | 10 |
| Visuals | Bear line / Bull line | `#d1242f` / `#1f6feb` |
| Visuals | Line width | 1 |
| Visuals | Show broken / expired lines | On |
| Visuals | Broken / expired transparency | 70 |
| Visuals | Mark bar where line appears | Off |

"Steepest + shallowest only" keeps the nearest and the farthest start point in the candidate set. Because a line has to clear every wick between its two anchors, an older start point almost always yields a shallower line, so nearest and farthest is steepest and shallowest on real data.

## Notes

- Ended lines are faded, never deleted. TradingView caps a script at 500 lines and drops the oldest once that cap is reached, so on a long history the earliest faded lines disappear. That is the platform, not a fault in the script.
- A hidden live line is drawn at full transparency rather than removed, so it still counts toward that 500-line cap.
- Each confirmed swing point runs an anchor search plus a wick check and a close check across the candidate span. On a long history at a fast timeframe that work can be heavy. If TradingView reports a calculation timeout, lower "Older swing points to try" or "Max bars between anchors" first.
- "Today's session only" clears every line and swing point at the regular-session open. Off by default, so lines carry across sessions.

## Version History

- **v1.2 (2026-09-10):** Swing point triangles and the "line appears" dots both default to off.
- **v1.1 (2026-09-10):** Tolerance became a share of ABR instead of a tick count, so it scales across timeframes. Replaced and capped lines now end faded instead of being deleted, so every "appears" dot keeps its line.
- **v1.0 (2026-09-10):** First build.
