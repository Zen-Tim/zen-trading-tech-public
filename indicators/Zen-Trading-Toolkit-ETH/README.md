# Zen Trading Toolkit — ETH Edition v1.3

Same three-module toolkit as [Zen Trading Toolkit](../Zen-Trading-Toolkit), rebuilt for continuous/near-24hr instruments (e.g. GC/GOLD) that don't have an RTH/ETH split. One session, one column, no instrument presets.

- **Module 1 — ABR Measured Moves** from yesterday's close (0.5x / 1x / 1.5x / 2x)
- **Module 2 — Opening Range** (default 18 bars) with projections
- **Module 3 — Volatility Stats Table** — ABR, ADR, % ADR, OR %, Swing target, Scalp target

Day boundary is `timeframe.change("D")`, aligned to the instrument's own trade-date roll rather than calendar midnight.

## Install

1. Open TradingView → Pine Editor
2. Paste the contents of `zen-trading-toolkit-eth-v1.3.pine`
3. Save → Add to chart

## Known issue (v1.3)

Module 3's ADR is computed from a bar-by-bar accumulated day high/low, while Module 1's yesterday levels and Module 2's ADR both pull from TradingView's `"D"` security series. On clean sessions these agree; on partial/gapped sessions (e.g. Sunday reopen segments) Module 3 can drift from Modules 1/2. A v1.4 aligning all three to the same source is planned.

— Tim
[zentradingtech.com](https://zentradingtech.com)
