# Zen TSaM Swing Chart · Multi-Mode 2/3/4

A TradingView Pine Script v5 indicator that draws price-based swing charts using the classic 2-bar reversal rule from Perry Kaufman's *Trading Systems and Methods* (Fig 5.3, p.184) — and runs three reversal thresholds (2, 3, and 4 bars) simultaneously on the same chart instead of forcing a single setting.

---

## What it does

A swing chart is an event-driven trend method — no time component, no moving average. It reacts only to price crossing a threshold. A swing reversal confirms once price prints N consecutive bars moving against the current swing without a new extreme (default N=2, Kaufman's base rule).

This indicator runs the rule at three thresholds at once:

- **2-bar** (solid) — the most sensitive, most prone to whipsaw
- **3-bar** (lighter) — a middle setting
- **4-bar** (lightest) — the least sensitive, most lag

An optional background zone shades the bars where all three models agree on direction — the closest thing to a consensus read the method can give.

The current, unconfirmed swing leg is drawn as a dashed line at the right edge of the chart. It is not a signal — by construction, this method can't confirm a reversal until the bar count is met.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open the `Zen_TSaM_Swing_Chart_v2.2_20260704.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure inputs via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it renders cleanly on GitHub and copy-pastes straight into the Pine Editor without a download step.

---

## Inputs

### Swing Modes
| Input | Default | Notes |
|---|---|---|
| Show All (2/3/4) | Off | Overrides the three toggles below and shows all three models at once. |
| Show 2-Bar (base method) | On | The Kaufman base rule. |
| Show 3-Bar | Off | |
| Show 4-Bar | Off | |
| Show Agreement Zone | On | Shades the background when all three models agree on current swing direction. |

### Visuals
| Input | Default | Notes |
|---|---|---|
| Upswing Color | Teal | |
| Downswing Color | Red | |
| Agreement Zone Transparency | 85 | 50–95 range. |

---

## How to read it

- **2-bar swings** confirm fastest but reverse most often — expect whipsaw in choppy stretches.
- **4-bar swings** confirm slowest but filter out more noise — expect the line to lag well behind the actual turn.
- The **agreement zone** is a visual filter, not a backtested signal in this release. Treat it as a starting point for your own research, not a conclusion.

---

## Background

Built from the swing chart construction rule in Perry Kaufman's *Trading Systems and Methods*, Chapter 5 ("Event-Driven Trends"), Figure 5.3 (p.184). Kaufman splits trend recognition into time-based methods (moving averages, momentum) and event-driven methods that ignore time and react only to price crossing a threshold — swing charts, point-and-figure, N-day breakouts. This indicator is the second family.

The lineage on swing charts runs back further, including William F. Eng's 1988 book *The Technical Analysis of Stocks, Options and Futures*, one of the earlier mechanical write-ups of the method.

**Read the write-up:** link to be added once the post is published on zentradingtech.com.

---

## Version

**v2.2** — current. Dashed live/in-progress leg re-added for the unconfirmed swing at the right edge of the chart.

---

## License

MIT. Free to use, modify, and share.
