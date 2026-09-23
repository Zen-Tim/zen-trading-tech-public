# Zen REMA — Rainbow EMA

A TradingView Pine Script v6 indicator that draws a 20-bar EMA and colours the line by its slope and by where the last two bars closed. EMA means exponential moving average: an average line that gives more weight to recent bars.

## Line colour

- **Blue:** EMA sloping up, and the last two bars are bull bars that closed above the EMA.
- **Green:** EMA sloping up, without two bull closes above it.
- **Red:** EMA sloping down, and the last two bars are bear bars that closed below the EMA.
- **Orange:** EMA sloping down, without two bear closes below it.
- **Grey:** EMA flat.

The slope is an angle. It is the EMA's change over the last 3 bars, divided by the average bar range (10-bar ATR), turned into degrees. Scaling by bar range means the same angle reads the same on any market.

## Optional parts (all off by default)

- **Acceleration:** draws a thin line from the EMA to the bar's high when the angle is above +10 degrees, or to the bar's low when below −10 degrees.
- **Always In:** a blue triangle marks the first time two bull bars close above the EMA. A red triangle marks the first time two bear bars close below it. The state resets each new day, and the first 2 bars of the day are ignored.
- **MIG Reversals:** labels a gap between a bar and the bar two back, where both bars are the same colour, but only when the gap goes against the current Always In side.

## Data Window

Four values show in the Data Window only, so they never squash the price scale: the slope angle, the Always In state (1 long, −1 short, 0 none), and two scalp sizes — 0.8x and 0.4x the 8-bar average bar range.

## Version History

- **v6.0 (2026-09-23):** Current version.
