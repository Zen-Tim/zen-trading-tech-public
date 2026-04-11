# Zen OOH / OOL — Open on High / Open on Low

A TradingView indicator that flags sessions where the **first bar of the day** prints its open exactly at the **high** or **low** of that opening bar. These "shaved opening bars" are a directional tell about who showed up at the open — bulls or bears.

📖 **Read the full write-up:** [Shaved Opens Revisited — Open on High, Open on Low](https://zentradingtech.com/2026/04/11/shaved-opens-revisited-open-on-high-open-on-low/)

---

## What it does

On the first bar of each new trading day, the indicator checks whether the bar's open equals its own high (`open == high` → Open on High, bearish) or its own low (`open == low` → Open on Low, bullish). Note this is about the **opening bar itself**, not about the high or low of the whole day — a shaved opening bar may or may not end up holding as the day's extreme. It is a subset of the broader question of which bar of the day prints HOD or LOD.

When the indicator finds a shaved opening bar, it:

- **Plots an arrow** above (O=H) or below (O=L) the bar
- **Highlights the opening bar** with a translucent background band
- **Counts occurrences** in a stats table (top right) showing total days observed, O=L count, O=H count, and either as a percentage
- **Fires an alert** (optional) so you can be notified live

The "either" stat is the headline number — across the symbols and timeframes Zen tracks, it sits at a level that makes shaved opening bars worth watching as a daily directional cue.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open the `zen-ooh-ool-v3.txt` file in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** (bottom panel)
3. **Paste** the code into a new script
4. Click **Save** (give it a name) then **Add to chart**
5. Configure inputs via the gear icon on the indicator

> The source is provided as a `.txt` file rather than `.pine` so it renders cleanly on GitHub and copy-pastes straight into the Pine Editor without a download step.

---

## Inputs

| Group | Input | Default | Notes |
|---|---|---|---|
| Logic | Tolerance (ticks) | 0 | 0 = exact match. Increase to allow open within N ticks of the opening bar's high or low |
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

- **Open = Low (O=L)** → the opening bar opened on its own low. No sellers were willing to take price below the open on the first bar — bullish tell.
- **Open = High (O=H)** → the opening bar opened on its own high. No buyers were willing to take price above the open on the first bar — bearish tell.

A shaved opening bar is a statement about who controlled the very first bar of the session. Whether that control persists through the day is a separate question — see the blog post for context, base rates, and how to combine this with other read.

---

## Version

**v3** — current. Adds tolerance input, stats table, alerts, and translucent bar highlighting on top of the original O=H / O=L detection.

---

## License

MIT. Free to use, modify, and share. Attribution to [Zen Trading Tech](https://zentradingtech.com) appreciated.