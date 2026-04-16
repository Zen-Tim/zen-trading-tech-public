# Zen Big Bar Midpoints

A TradingView indicator that detects **big bars** — bars with range greater than a multiple of the recent average bar range — and draws reference levels on them: two midpoints plus optional fractional levels. Useful for flagging where price is relative to the structure of a significant bar after it prints.

---

## What it does

For every bar, the indicator checks whether the bar range is bigger than a threshold multiple of the **Average Bar Range (ABR)** over the last N bars (default: 1.6 x ABR, 8-bar lookback). If so, it classifies the bar as bullish (close > open) or bearish (close < open) and draws:

- **Midpoint 1 — High-Low mid:** `(high + low) / 2`. The midpoint of the whole bar.
- **Midpoint 2 — Extreme-Close mid:** for a bull bar, `(low + close) / 2` — the midpoint between the low and the close. For a bear bar, `(high + close) / 2` — the midpoint between the high and the close.
- **Fractional levels (optional):** 0.33 and 0.66 of the bar range, measured from the low. Useful for rejection-zone reference.

Each set has its own display mode: **Lines** (dotted), **Boxes** (translucent fill between the two levels), or **Off**. Bull-bar and bear-bar colours are independent, and you can turn off either direction entirely.

Levels are drawn extending N bars forward from the big bar (default 10) so you can see if price revisits them.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open `zen-big-bar-midpoints-v1.8.txt` in this folder and copy the entire contents.
2. In TradingView, open the **Pine Editor** (bottom panel).
3. Paste the code into a new script.
4. Click **Save** (give it a name) then **Add to chart**.
5. Configure inputs via the gear icon on the indicator.

---

## Inputs

### Big Bar Detection
| Input | Default | Notes |
|---|---|---|
| ABR Lookback | 8 | Bars used to compute average bar range. |
| Big Bar Threshold (x ABR) | 1.6 | Bar range must exceed this multiple of ABR to qualify. |
| Show Bull Bars | true | Draw levels on qualifying bull bars. |
| Show Bear Bars | true | Draw levels on qualifying bear bars. |

### Midpoints (H-L mid & Extreme-Close mid)
| Input | Default | Notes |
|---|---|---|
| Display Mode | Lines | Lines / Boxes / Off. Box mode fills between the two midpoints. |
| Bull Colour | blue | Colour for midpoints on bull big bars. |
| Bear Colour | red | Colour for midpoints on bear big bars. |
| Box Fill Transparency | 75 | 0 = solid, 100 = invisible. |

### Fractional Levels (0.33 / 0.66)
| Input | Default | Notes |
|---|---|---|
| Display Mode | Off | Lines / Boxes / Off. Box mode fills between the two fractions. |
| Lower Fraction | 0.33 | Distance from low as a fraction of bar range. |
| Upper Fraction | 0.66 | Distance from low as a fraction of bar range. |
| Bull Colour | blue | Colour for fractional levels on bull big bars. |
| Bear Colour | red | Colour for fractional levels on bear big bars. |
| Box Fill Transparency | 50 | 0 = solid, 100 = invisible. |

### Line Style
| Input | Default | Notes |
|---|---|---|
| Extension (bars) | 10 | How many bars forward the levels extend. |
| Line Width | 2 | 1-4. |

---

## How to read it

When a big bar prints, the midpoints mark the two structural halves of that bar:

- **H-L mid** is the geometric middle of the whole range. Price returning here shows the bar's conviction has been half-reclaimed.
- **Extreme-Close mid** sits between the close and the far extreme (low for bull, high for bear). On a strong bar the close is near the extreme, so this midpoint is close to the close itself — it's a finer line for "has the closing strength been given back?"

If price holds above both midpoints of a bull big bar on the next leg, the move is intact. Slipping below the H-L mid is the first meaningful give-back. Slipping under the low of the big bar is the structural break.

The 0.33 / 0.66 levels are optional reference fractions for traders who use them for pullback or rejection entries inside the big bar's structure.

---

## Version

**v1.8** — current. Dual display mode (Lines or Boxes), independent bull/bear toggles, configurable line extension and width, separate transparency controls for midpoints vs fractions.

---

## License

MIT. Free to use, modify, and share.