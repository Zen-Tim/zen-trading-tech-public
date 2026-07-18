# Zen ABR Range Bands

Plots a 20-bar EMA with optional bands at 1x, 2x, 3x, and 4x the 8-bar Average Bar Range (ABR) above and below it. Optionally highlights any bar whose wick touches a chosen ABR level.

## What it does

- **EMA:** Configurable length (default 20), toggleable on/off.
- **ABR:** 8-bar SMA of bar range (high − low). Lookback is fixed at 8 and is not user-configurable.
- **Bands:** Four independently toggleable band pairs — EMA ± 1x ABR, ± 2x ABR, ± 3x ABR, ± 4x ABR. Each has its own color input, defaulting to a greyscale palette (dark to light) at 50% transparency, suited to light-grey chart backgrounds.
- **Bar Highlight (v1.1):** Eight independent checkboxes — one per ABR level (1x/2x/3x/4x) per side (above/below the EMA). When a bar's wick touches or exceeds a checked level, the bar itself is recolored: red for a hit above the EMA (short side), blue for a hit below (long side). Levels are cumulative — a 4x hit also satisfies 2x/3x — so each checkbox fires independently off its own threshold. On the rare bar that hits both sides at once, the above/red side takes priority.

## Inputs

| Group | Input | Default |
|---|---|---|
| Moving Average | Show EMA | On |
| Moving Average | EMA Length | 20 |
| Moving Average | EMA Color | `#1A1A1A` @ 50% |
| ABR Bands | Show 1x / 2x / 3x / 4x ABR Band | 1x on, others off |
| ABR Bands | Band Colors | Greyscale (`#2B2B2B` → `#737373`) @ 50% |
| ABR Bands | Band Line Width | 1 |
| Bar Highlight | Highlight 1x / 2x / 3x / 4x Above EMA (Red) | Off |
| Bar Highlight | Highlight 1x / 2x / 3x / 4x Below EMA (Blue) | Off |
| Bar Highlight | Above / Below Highlight Color | Red / Blue |

## Version History

- **v1.1 (2026-07-18):** Added the Bar Highlight group — eight independent checkboxes to color-code bars hitting 1x-4x ABR above (red) or below (blue) the EMA, using the same wick-touch rule as the entry logic in the ABR-from-MA performance study.
- **v1.0 (2026-07-17):** Initial release.
