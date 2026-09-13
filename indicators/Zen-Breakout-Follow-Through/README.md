# Zen Breakout Follow Through

Marks a four-bar sequence: a bar that breaks out, a second bar that follows through in the same direction, and the level where an entry would trigger above (or below) the follow-through bar. A practice tool for separating three states that look alike in real time — a breakout bar on its own, a breakout with follow-through, and an actual entry.

## The rule

Bull sequence, four bars:

- **Bar A** — the prior bar. No conditions on it.
- **Bar B — breakout** — `high(B) > high(A)` and `low(B) >= low(A)`.
- **Bar C — follow-through** — `high(C) > high(B)` and `low(C) >= low(B)`.
- **Bar D — entry** — price trades above `high(C)`.

Bear is the mirror: lower low, no higher high, entry below `low(C)`.

Inside bars and outside bars are excluded by the rule itself, not by a filter. An inside bar never takes out the prior high. An outside bar always takes out the prior low. Neither can be Bar B or Bar C.

## What it does

- **Detection** runs on closed bars only. The setup is registered on the bar after the follow-through bar closes.
- **The line** is drawn at the follow-through bar's high (bull) or low (bear), three bars long by default. Faded while the setup is pending, solid once price trades through it.
- **Expiry:** by default the setup is live for one bar. If the entry does not trigger, the line is deleted. The "Bars allowed for entry" input extends that window up to 20 bars.
- **Trigger** fires intrabar, the moment price trades through the level. A triangle marks the trigger bar, below the bar for a bull entry and above it for a bear entry.
- **Alerts** fire on trigger with the ticker, the timeframe and the level in the message, once per bar.
- **Data Window** exports eight values: setup complete, entry trigger, pending count and last trigger level, for each direction.

No session logic, no instrument presets, no ABR. It runs on any instrument and any timeframe as-is.

## Inputs

| Group | Input | Default |
|---|---|---|
| Signal | Bull setups | On |
| Signal | Bear setups | On |
| Signal | Bars allowed for entry | 1 (max 20) |
| Signal | Follow-through bar must hold above the breakout bar low | On |
| Signal | Breakout and follow-through bars must close in direction | Off |
| Drawing | Line length (bars) | 3 |
| Drawing | Line width | 1 |
| Drawing | Bull pending / triggered colour | `#009688` @ 55% / `#009688` |
| Drawing | Bear pending / triggered colour | `#E53935` @ 55% / `#E53935` |
| Drawing | Trigger markers | On |
| Alerts | Fire alert on trigger | On |

Turning "Follow-through bar must hold above the breakout bar low" off lets the follow-through bar be an outside bar — it then only has to take out the breakout bar's high.

Turning "Breakout and follow-through bars must close in direction" on adds a close filter to both bars. Off is pure high/low geometry.

## Notes

- A trigger is a level being traded through and nothing else. A bar that trades one tick beyond the level and closes back inside it still counts as a trigger. That is a failed breakout, and the indicator does not judge it.
- Sequences can chain. The follow-through bar of one setup can be the breakout bar of the next.
- The entry still counts if the trigger bar later becomes an outside bar.

## Version History

- **v1.1 (2026-09-13):** Sequence extended by one bar. The line now sits at the follow-through bar (Bar C), not the breakout bar (Bar B). Line width reduced to a single input, default 1. Line length fixed at 3 bars with no extension on trigger.
- **v1.0 (2026-09-13):** Initial release. Three-bar sequence with the line at the breakout bar.
