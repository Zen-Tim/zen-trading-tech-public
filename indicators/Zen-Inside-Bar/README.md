# Zen Inside Bar — Inside Bar Highlighter

A TradingView indicator that highlights inside bars — bars whose high and low are both contained within the prior bar's range — with a configurable colour, plus an alert condition for when one prints.

---

## What it does

On every bar, the indicator checks whether the current high is less than or equal to the prior bar's high **and** the current low is greater than or equal to the prior bar's low. If both are true, the bar is coloured with the configured highlight colour. An alert condition fires on the same test, so you can set a TradingView alert to notify you the moment an inside bar closes.

---

## How to install

This is a **Pine Script v6** indicator for TradingView. To use it:

1. Open the `zen-inside-bar-v3.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure the highlight colour via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it renders cleanly on GitHub and copy-pastes straight into the Pine Editor without a download step.

---

## Inputs

| Group | Input | Default | Notes |
|---|---|---|---|
| — | Inside Bar Colour | Yellow (`#FFEA00`) | Bar colour applied when an inside bar is detected |

---

## How to read it

- **A coloured bar** means that bar's entire range sat inside the previous bar's range — no new high, no new low. This is a contraction signal often associated with indecision or a pause before continuation/reversal.
- **Set an alert** on the built-in "Inside Bar" alert condition to get notified the instant one closes, without watching the chart.

---

## Version

**v3** — current.

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.
