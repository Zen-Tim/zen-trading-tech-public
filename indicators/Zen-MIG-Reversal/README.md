# Zen MIG Reversal

A TradingView Pine Script v5 indicator that marks MIG reversals: three same-colour bars in a row with a gap between the first bar and the third bar, forming on the far side of the moving average. A bull MIG reversal forms below the average. A bear MIG reversal forms above it.

## The rule

- **Three bars, one colour.** Three bull bars in a row for a bull signal, three bear bars for a bear signal.
- **The gap.** Default "Micro gap (Brooks)": the third bar's low sits above the first bar's high (bull), or the third bar's high sits below the first bar's low (bear). "Visible gap (strict)" also needs the middle bar to clear the first bar.
- **The moving average test.** Default "Tail beyond MA": for a bull signal the third bar's low is below the average; for a bear signal its high is above it. Other choices: the close beyond the average, or the whole bar beyond it.
- **Bar close.** By default a signal only prints once the bar has closed.

## On the chart

A shaded box covers the gap and an arrow marks the signal bar. Blue for bull, red for bear.

## Inputs

| Group | Input | Default |
|---|---|---|
| Moving Average | MA Type | EMA |
| Moving Average | MA Length | 20 |
| Moving Average | Show MA Line | On |
| Signal | Gap Rule | Micro gap (Brooks) |
| Signal | MA Test | Tail beyond MA |
| Signal | Signal on bar close only | On |
| Display | Show Arrows / Show Gap Boxes | On / On |
| Display | Bull Colour / Bear Colour | Blue / Red |

## Alerts

Bull MIG Reversal, Bear MIG Reversal, and Any MIG Reversal.

## Version History

- **v2.0 (2026-09-23):** Current version.
