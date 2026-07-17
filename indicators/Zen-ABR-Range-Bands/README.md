# Zen ABR Range Bands

Plots a 20-bar EMA with optional bands at 1x, 2x, 3x, and 4x the 8-bar Average Bar Range (ABR) above and below it.

## What it does

- **EMA:** Configurable length (default 20), toggleable on/off.
- **ABR:** 8-bar SMA of bar range (high − low). Lookback is fixed at 8 and is not user-configurable.
- **Bands:** Four independently toggleable band pairs — EMA ± 1x ABR, ± 2x ABR, ± 3x ABR, ± 4x ABR. Each has its own color input, defaulting to a greyscale palette (dark to light) at 50% transparency, suited to light-grey chart backgrounds.

## Inputs

| Group | Input | Default |
|---|---|---|
| Moving Average | Show EMA | On |
| Moving Average | EMA Length | 20 |
| Moving Average | EMA Color | `#1A1A1A` @ 50% |
| ABR Bands | Show 1x / 2x / 3x / 4x ABR Band | 1x on, others off |
| ABR Bands | Band Colors | Greyscale (`#2B2B2B` → `#737373`) @ 50% |
| ABR Bands | Band Line Width | 1 |

## Version History

- **v1.0 (2026-07-17):** Initial release.
