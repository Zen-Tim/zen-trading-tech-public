# Zen OOH / OOL — Open on High / Open on Low

A TradingView indicator that flags days where the **opening bar** of a session prints its open exactly at the **high** or **low** of that bar. These "shaved opens" are a high-conviction directional tell on the first bar of the day.

📖 **Read the full write-up:** [Shaved Opens Revisited — Open on High, Open on Low](https://zentradingtech.com/2026/04/11/shaved-opens-revisited-open-on-high-open-on-low/)

---

## What it does

On the first bar of each new trading day, the indicator checks whether `open == high` (Open on High, bearish) or `open == low` (Open on Low, bullish). When it finds one, it:

- **Plots an arrow** above (O=H) or below (O=L) the bar
- **Highlights the opening bar** with a translucent background band
- **Counts occurrences** in a stats table (top right) showing total days, O=L count, O=H count, and either as a percentage of all days observed
- **Fires an alert** (optional) so you can be notified live

The "either" stat is the headline number — across the symbols and timeframes Zen tracks, it sits in a meaningful range that makes shaved opens worth watching as a daily probability cue.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open the `zen-ooh-ool-v3.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure inputs via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it's easy to view directly on GitHub and copy-paste into the Pine Editor without download steps.

---

## Inputs

| Group | Input | Default | Notes |
|---|---|---|---|
| Logic | Tolerance (ticks) | 0 | 0 = exact match. Increase to allow open within N ticks of HOD/LOD |
| Display | Show arrows | true | Up arrow for O=L, down arrow for O=H |
| Display | Show arrow labels | true | Adds "O=L" / "O=H" text on the arrow |
| Display | Highlight opening bar | true | Translucent background band on the bar |
| Display | Background transparency | 85 | Higher = more translucent (50–99) |
| Display | Show stats table | true | Top-right corner running tally |
| Colors | O=L colour | teal | Bullish shave |
| Colors | O=H colour | red | Bearish shave |
| Alerts | Alert on Open = Low | true | Once-per-bar-close |
| Alerts | Alert on Open = High | true | Once-per-bar-close |

---

## How to read it

- **Open = Low** → the session opened on its lowest tick. Bears had no presence at the open. Bias is bullish for the session — fade pullbacks down toward the open.
- **Open = High** → the session opened on its highest tick. Bulls had no presence at the open. Bias is bearish for the session — fade pullbacks up toward the open.

The blog post covers nuance, base rates, and how to combine this with other context.

---

## Version

**v3** — current. Adds tolerance input, stats table, alerts, and translucent bar highlighting on top of the original O=H / O=L detection.

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.