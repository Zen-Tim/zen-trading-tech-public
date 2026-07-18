# Zen Open CSC Bars

A TradingView indicator that detects **Consecutive Same-Colour (CSC)** bars at the start of the session — a run of N same-direction bars from the open — and measures the size of the resulting spike.

---

## What it does

From the first bar of each new day, the indicator counts consecutive same-colour bars. When the count of bull or bear bars within the check window reaches the "Bars to Check" input, it flags a **CSC** event: a bullish or bearish label, optional bar colouring, and an alert.

Alongside detection, it tracks two spike measurements:

- **Consecutive Spike** — the high/low range of the first two bars of the session, calculated every day regardless of whether a CSC fires.
- **Total Spike** — the full high/low range of the entire consecutive same-colour run, however long it goes, printed once the streak breaks.

Both spike sizes and the CSC bar type are exposed to the Data Window for use in external logging or study.

---

## How to install

This is a **Pine Script v5** indicator for TradingView. To use it:

1. Open `zen-open-csc-bars-v9.txt` in this folder and copy the entire contents.
2. In TradingView, open the **Pine Editor** (bottom panel).
3. Paste the code into a new script.
4. Click **Save** (give it a name) then **Add to chart**.
5. Configure inputs via the gear icon on the indicator.

---

## Inputs

| Input | Default | Notes |
|---|---|---|
| Bars to Check | 2 | Number of consecutive same-colour bars required to flag a CSC (2–9). |
| Show Bull/Bear Labels | On | Shows a green/red marker on the bar where the CSC condition completes. |
| Show Spike Label on Chart | On | Prints the Total Spike size as a label once a qualifying streak ends. |
| Colour Bars | On | Colours the streak's bars green/red while the run is in progress. |
| ABR Lookback Period | 8 | Lookback for the Average Bar Range calculation (fixed convention — do not change). |
| Show 20 EMA / Show 200 EMA | On | Standard trend EMAs, plotted for context. |
| Alert on Bar Close Only | On | Fires `alert()` calls only on confirmed bar close, avoiding intrabar repaint. |

---

## How to read it

A CSC event means the market opened and printed N bars in the same direction without interruption — a simple read on early-session conviction. The label marks the bar where the run first qualifies; bar colouring shows the run building in real time.

The **Consecutive Spike** is a fixed, always-on measurement of the first two bars' combined range — useful as a baseline regardless of whether a full CSC triggers. The **Total Spike** only resolves once the streak ends, and reflects the full size of however long the run went, not just the check window.

Session boundary is `ta.change(time("D"))`, which follows the exchange session, not calendar midnight.

---

## Alerts

Three alert conditions are exposed for manual TradingView alert setup: Bullish CSC, Bearish CSC, and Any CSC. Dynamic `alert()` calls also fire for bull/bear CSC events (compatible with "Any alert() function call" alerts), gated by the Alert on Bar Close Only input.

---

## Version

**V9** — current.

---

## License

MPL 2.0. Free to use, modify, and share.
