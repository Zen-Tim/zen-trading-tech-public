# Zen Z Bars

A TradingView indicator that flags **Z Bars** — bars whose range is a large multiple of the recent average bar range — and colour-codes them by how extreme they are. Built for spotting climactic / high-conviction bars and their follow-through on intraday charts.

---

## What it does

For every bar, the indicator computes the **Average Bar Range (ABR)** over a lookback period (default 8 bars) and divides the current bar range by it to get a `z_multiple`. A bar qualifies as a Z Bar when:

- its body is large enough relative to its range (Body Range Limit), **and**
- its range is at least a minimum absolute size (Bar Range Minimum), **and**
- its `z_multiple` clears one of four threshold tiers.

Qualifying bars are coloured by tier, get a midpoint reference line, and the bar after a Z Bar is flagged with an arrow if it continues in the same direction (follow-through).

### Tiers and colours
| Tier | Default multiple | Colour |
|---|---|---|
| Tier 1 | > 1.1x ABR | purple |
| Tier 2 | ≥ 1.5x ABR | magenta |
| Tier 3 | ≥ 2x ABR | cyan |
| Tier 4 | ≥ 3x ABR | lime |

The colour cascade resolves largest-tier-first, so a bar always takes the colour of the highest tier it qualifies for.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open `Zen_Z_Bars_v1.1_20260608.txt` in this folder and copy the entire contents.
2. In TradingView, open the **Pine Editor** (bottom panel).
3. Paste the code into a new script.
4. Click **Save** (give it a name) then **Add to chart**.
5. Configure inputs via the gear icon on the indicator.

---

## Inputs

### Z-Bar Detection
| Input | Default | Notes |
|---|---|---|
| Lookback Period | 8 | Bars used to compute average bar range (ABR). |
| Body Range Limit | 45 | Minimum body-to-range %, expressed as a whole number. |
| Bar Range Minimum | 5 | Minimum absolute bar range (points) to qualify. |
| Bar Range Multiple 1 | 1.1 | Tier 1 threshold (strictly greater than). |
| Bar Range Multiple 1.5 | 1.5 | Tier 2 threshold. |
| Bar Range Multiple 2 | 2 | Tier 3 threshold. |
| Bar Range Multiple 3 | 3 | Tier 4 threshold. |

### Z-Bar Tiers (v1.1)
| Input | Default | Notes |
|---|---|---|
| Show Tier 1 (1.1x) | On | Toggle visibility of tier 1 bars. |
| Show Tier 2 (1.5x) | On | Toggle visibility of tier 2 bars. |
| Show Tier 3 (2x) | On | Toggle visibility of tier 3 bars. |
| Show Tier 4 (3x) | On | Toggle visibility of tier 4 bars. |

Turning a tier off removes it from the colour cascade. Because tiers are cumulative, disabling the upper tiers lets large bars fall through to whatever lower tier is still enabled — so you can isolate just the small Z Bars, just the extreme ones, or any band in between.

---

## How to read it

A Z Bar is a bar that prints much larger than the recent average — a sign of a sudden expansion in conviction or volatility. The tier tells you how extreme: a Tier 4 (lime) bar is at least 3x the average range and is usually a climactic move.

The midpoint line marks the geometric centre of the Z Bar, a natural reference for whether the move is being held or given back on subsequent bars.

The follow-through arrow (below a bull bar, above a bear bar) marks the bar *after* a Z Bar that continued in the same direction — a simple read on whether the expansion had immediate continuation.

Note that Tier 1 uses a strict greater-than (`>`), so a bar exactly at 1.1x ABR does not trigger; the other tiers use `≥`.

---

## Version

**v1.1** — current. Added independent On/Off toggles for each of the four tiers so individual bands can be hidden or isolated.

---

## License

MPL 2.0. Free to use, modify, and share.
