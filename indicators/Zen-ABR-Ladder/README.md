# Zen ABR Scalping Ladder

**Get it on TradingView:** [Zen ABR Scalping Ladder](https://www.tradingview.com/script/eAD6znU2-Zen-ABR-Scalping-Ladder/)

A TradingView Pine Script v6 sub-pane indicator that answers one question: is a scalp-sized target realistic on the timeframe you are looking at right now?

It plots five Average Bar Range (ABR) lines at lookbacks 2, 4, 8, 16 and 32, and one scalp reference line at a percentage of the Average Daily Range (ADR), default 10%. Everything is drawn in price points, so the comparison is direct. A translucent grey cloud fills the gap between the scalp line and the ABR lines, and darkens where the lines agree.

ABR is not standard terminology. It is the average of the bar range (high minus low) over the last N bars, and it is the ruler used across the Zen indicator suite. ADR is the same idea on the daily bar.

Note on naming: the TradingView publication is titled **Zen ABR Scalping Ladder**. The script's own chart title, set inside the source, is `Zen ABR Ladder v2.1`. Same indicator.

## What it does

- **Five ABR lines.** Lookbacks are locked at 2, 4, 8, 16 and 32. Each line has its own on/off switch, colour and width. The current forming bar is excluded from every average, so no line moves as the bar builds.
- **Scalp reference line.** A percentage of ADR, drawn in points. Default 10% of the average daily range. Set it to whatever your own scalp target is.
- **The grey cloud.** One translucent fill per ABR line, all measured from the scalp line, for ABR 4, 8, 16 and 32. Where several lines sit on the same side of the scalp line, the fills stack and the grey gets darker on its own. No single line has to be picked as the driver.
- **Middle-three mode.** Drops the fastest line (ABR 2) and the slowest (ABR 32) and fills from the average of ABR 4, 8 and 16 instead. A tamer top edge on fast markets such as FDAX.
- **Pane cap.** On by default at 2x the scalp line. Anything above that draws flat at the cap instead of stretching the vertical scale. One volatility spike no longer squashes the readable zone. The value table always shows the true uncapped number.
- **Three fixed point levels.** Off by default. Use them to pin your own point targets into the pane.
- **Value table.** Off by default. Shows every live line in points and as a percentage of ADR.

## How to read it

Line above the scalp line means an average bar on this timeframe is bigger than the scalp target. One bar can carry the trade.

Line below the scalp line means an average bar is smaller than the scalp target. The target now needs several bars, so either wait for a bigger timeframe or take fewer points.

The darker the grey, the more of the ABR lookbacks agree.

## Inputs

| Group | Input | Default |
|---|---|---|
| Pane Cap | Cap pane at | On |
| Pane Cap | x scalp line | 2.0 |
| ABR Lines | ABR 2 / 4 / 8 / 16 / 32 | ABR 8 on, others off |
| ABR Lines | Colour and width per line | Grey `#607D8B` for ABR 8, blue ramp for the rest, width 1 |
| Scalp Reference Line | ADR Lookback (completed daily bars) | 8 |
| Scalp Reference Line | Scalp line | On |
| Scalp Reference Line | % of ADR | 10.0 |
| Scalp Reference Line | Full ADR (100%) | Off |
| Shading | Mode | Stacked (4/8/16/32) |
| Shading | Shade colour / Transparency | `#546E7A` / 90 |
| Shading | Show middle-three line | Off |
| Shading | Mark scalp crossings | Off |
| Fixed Levels (points) | Level 1 / 2 / 3 | Off, 10 / 5 / 3 points |
| Table | Show value table | Off |
| Table | Position | Top Right |

## Notes

- **ADR uses TradingView's standard daily bar**, which includes overnight trade. That is deliberate: the indicator needs no session string, no timezone and no instrument preset, so it runs on any chart of any market straight out of the box. If you want an RTH-only ADR, edit the `request.security` call to use a session-filtered series.
- **On a weekly or monthly chart**, the ADR calculation falls back to the chart timeframe, so the reference line becomes a percentage of the average weekly or monthly range. Intended use is intraday and daily.
- **ABR 2 feeds no shading layer.** Switching it off removes it from the pane scale completely. The other four keep live values when hidden, because a fill needs two live series to draw between.

## Install

1. Copy the whole of `zen-abr-ladder-v2.1.txt`
2. In TradingView, open the **Pine Editor**, paste into a new script
3. Save and add to chart. It opens in its own pane below price.

Or add it straight from the [TradingView script page](https://www.tradingview.com/script/eAD6znU2-Zen-ABR-Scalping-Ladder/).

## Version History

- **v2.1 (2026-09-13):** Points only, unit-mode dropdown removed. Pane cap on by default at 2x the scalp line. ABR 2 now leaves the pane entirely when switched off. Published to TradingView as Zen ABR Scalping Ladder.
- **v2.0 (2026-09-13):** Added a percent-of-ADR unit mode. Withdrawn in v2.1.
- **v1.9 (2026-09-13):** Default view set to grey cloud, grey ABR 8, grey scalp line.
- **v1.8 (2026-09-13):** Line switches hide colour only, so the cloud survives with every line switched off.
- **v1.7 (2026-09-13):** ABR 2 stopped contributing a shading layer.
- **v1.6 (2026-09-13):** Spare slots removed, lookbacks locked at 2/4/8/16/32. Shading added.
- **v1.5 (2026-09-13):** Three fixed point levels added.
- **v1.4 (2026-09-13):** Editable lookbacks, three spare slots, auto shading.
- **v1.3 (2026-09-13):** 40% ADR line removed. Blue ramp introduced.
- **v1.2 (2026-09-13):** Every drawn element given its own on/off switch.
- **v1.1 (2026-09-13):** Daily bar replaces RTH session tracking.
