# Zen Trading Toolkit v1.8.1

Three research modules in one indicator, each independently toggleable.

- **Module 1 — ABR Measured Moves** from yesterday's close (0.5x / 1x / 1.5x / 2x)
- **Module 2 — Opening Range** (default 18 bars = 90 min on 5m) with projections
- **Module 3 — Volatility Stats Table** — ABR, RTH/ETH ADR, % ADR, OR %, Swing target, Scalp target

Built for ES, FDAX, HSI, and Nikkei 225 futures. Instrument preset automatically sets RTH session and timezone. Custom mode available for any other market.

## Install

1. Open TradingView → Pine Editor
2. Paste the contents of `zen-trading-toolkit-v1.8.1.pine`
3. Save → Add to chart
4. Settings → pick your instrument

## Notes

- All metrics are exposed as Data Window plots as of v1.8, so you can hide the table and still read the values in the side panel.
- Default lookback is 8 days (the research default for ABR/ADR on index futures).
- RTH-only tracking: ADR uses session-filtered highs/lows, not 24h ranges. On RTH-only charts, snapshot + rollover both happen on `first_bar` to handle the missing `session_end` transition.

Happy trading!

— Tim
[zentradingtech.com](https://zentradingtech.com)
