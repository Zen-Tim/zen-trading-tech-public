# Zen CTMC — EMA Pullback Patterns

A TradingView Pine Script v6 indicator that marks two pullback patterns on the wrong side of a 20-bar EMA. EMA means exponential moving average: an average line that gives more weight to recent bars. The signal bar is coloured red for a bear pattern and green for a bull pattern.

## The two patterns

- **3-bar pattern:** three bear bars in a row, all three closing above the EMA. The bull version is three bull bars in a row, all three closing below the EMA. A doji (a bar that closes where it opened) breaks the run.
- **Micro channel:** four bars with three lower highs in a row, all four closing above the EMA. The bull version is three higher lows in a row, all four closing below the EMA. "Full micro channel" also asks for lower lows (bear) or higher highs (bull).

## Signal bar

The signal bar is bar 3 of the 3-bar pattern, or bar 4 of the micro channel. By default only the first bar that completes a pattern is marked. Switch on "Mark every bar" to mark each bar the pattern stays true. Signals only print once the bar has closed.

## Labels (off by default)

"3" for the 3-bar pattern, "MC" for the micro channel, and "3+MC" when both complete on the same bar.

## Alerts

Bear 3 above EMA, Bull 3 below EMA, Bear MC above EMA, Bull MC below EMA.

## Version History

- **v1.1 (2026-09-23):** Current version.
