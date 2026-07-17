# Zen ABR Against

Marks Average Bar Range (ABR) levels as short tick marks above and below price, and colors any bar that breaks a prior bar's ABR level against the trend direction implied by that break.

Inspired by the "boxes against you" breakout-failure concept discussed in Perry Kaufman's *Trading Systems and Methods*, in the chapter covering point-and-figure / box-based breakout systems. A "box" there is one Average Bar Range; the question the chapter raises — how many boxes against a position signals a genuine reversal versus noise — is left open in the text. This indicator lets you test that visually and, eventually, statistically on real data rather than assume a fixed number.

## What it does

**ABR Bands** — plots the close ± (1x/2x/3x/4x) ABR as short tick marks (not connected lines) so each bar's own band levels are visible without implying a trend line between bars. ABR itself is a fixed 8-bar SMA of (high − low).

**Breakout Bar Coloring** — checks whether the current bar's high/low took out a prior bar's ABR level:
- Broke above the reference bar's `close + (level × ABR)` → colored (default blue)
- Broke below the reference bar's `close − (level × ABR)` → colored (default red)
- Broke both in the same bar (rare) → colored (default purple)

Two independent settings control the test:
- **Bars Back to Check** — which prior bar to measure from (default 1 = prior bar)
- **ABR Multiplier Level** — how many ABR "boxes" away counts as a break (default 3)

## Inputs

| Group | Input | Default |
|---|---|---|
| ABR | ABR Lookback | 8 (fixed/sacrosanct — do not change) |
| Bands | Show 1x/2x/3x/4x ABR | 1x on, rest off |
| Colors | 1x–4x Color | light grey → near-black gradient |
| Breakout | Enable Breakout Bar Coloring | on |
| Breakout | Bars Back to Check | 1 |
| Breakout | ABR Multiplier Level | 3 |
| Breakout | Broke Above / Below / Both Color | blue / red / purple |

## Open question

Is 3x ABR the right threshold for a meaningful break, or is it 2x, 4x, or instrument-dependent? Kaufman doesn't settle it definitively for this exact framing, and it hasn't been tested yet across ES/FDAX/HSI/NK225. That's the next research step — this indicator is the visual/diagnostic tool, not the answer.

— Zen_Tim_Trades
