# Zen ATH Bands — All-Time High Pullback Levels

A TradingView indicator that plots the **all-time high (ATH)** and a configurable ladder of percentage pullback levels below it. Seven independently-toggleable levels with customisable % offsets and colours. Built for cash indices and other instruments where ATH and its higher-timeframe pullback zones matter for context.

📖 **Read the full write-up:** [Cash Indices and Higher Time Frame Pullbacks](https://zentradingtech.com/2025/12/19/cash-indices-and-higher-time-frame-pullbacks/)

---

## What it does

Tracks the running all-time high across all loaded bars and draws horizontal lines at the ATH plus up to seven user-defined percentage pullbacks below it (default ladder: 2.5%, 5%, 10%, 15%, 20%, 25%, 30%). Each line is extended to the right edge of the chart and labelled with both the percentage offset and the exact price level.

The reference can be toggled between the running ATH (default) and the current last price, which is useful for measuring a percentage ladder anchored to wherever price sits now rather than to the all-time peak.

Lines are drawn once on the last confirmed bar rather than on every bar, which keeps the indicator lightweight on long-history charts.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open the `Zen_ATH_Bands_v2.0_20260424.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure inputs via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it renders cleanly on GitHub and copy-pastes straight into the Pine Editor without a download step.

---

## Inputs

| Group | Input | Default | Notes |
|---|---|---|---|
| General | Reference (0=Last price, 1=ATH) | 1 | Which price anchor to measure the ladder from |
| General | Right offset (bars) | 30 | How far to the right of the current bar to place the labels |
| Level 0 | Show ATH line | true | The reference line itself |
| Level 0 | ATH Color | teal | |
| Level 1 | Show Level 1 | true | Default 2.5% down |
| Level 2 | Show Level 2 | true | Default 5.0% down |
| Level 3 | Show Level 3 | true | Default 10.0% down |
| Level 4 | Show Level 4 | true | Default 15.0% down |
| Level 5 | Show Level 5 | true | Default 20.0% down |
| Level 6 | Show Level 6 | true | Default 25.0% down |
| Level 7 | Show Level 7 | true | Default 30.0% down |

Every level has its own `% Down` input and its own colour. All seven are independently toggleable — turn off the ones you don't want to see.

---

## How to read it

The ladder is not a forecast — it is a **spatial reference**. When price is pulling back from ATH on a higher timeframe, a quick glance tells you which decile of the drawdown you are trading in. A 3% pullback and a 20% pullback are different conversations, and traders often mix them up because they look the same on a standard chart until you have the levels drawn in.

Common uses:

- **On a weekly or daily chart of a cash index** — see where you are relative to ATH and the 5% / 10% / 20% bands. Correction territory (10%+) and bear market territory (20%+) get immediate visual weight.
- **On an individual stock** — the same logic applies. A 30% drawdown on a megacap is materially different from a 3% pullback.
- **Switch reference to last price** — when you want to measure a ladder from here rather than from the peak, e.g. for sizing a stop or a target relative to current price.

Use it for context and framing, not as a trade trigger.

---

## Version

**v2.0** — current. Adds seven independently-toggleable pullback levels with per-level colour and percentage configuration, dual reference mode (ATH or last price), configurable label offset, and draw-on-last-bar optimisation for long-history charts.

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
