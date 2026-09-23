# Zen Gaps

A TradingView Pine Script v5 indicator that marks gaps between bars, tick gaps between one bar's close and the next bar's open, and urgent bars. Each part has its own On/Off switch.

## Gaps

A gap is space between a bar and the bar two back: the bar's low above the high two bars back (bull), or its high below the low two bars back (bear). The gap is drawn as a box.

- **Strong gap (light blue box):** both bars are the same colour as the gap's direction — two bull bars for a bull gap, two bear bars for a bear gap.
- **Normal gap (faint green or red box):** the two bars are different colours.

## Tick gaps

A short line at the open when a bar opens away from the prior bar's close. Green when the open is above the prior close, red when below.

## Urgent bars

An arrow on a bar with a big body and almost no tail on the side it opened from.

- **Bull urgent bar:** body more than 51% of the bar's range, and bottom tail under 15% of the range.
- **Bear urgent bar:** body more than 51% of the bar's range, and top tail under 15% of the range.

## Clarity bars

An urgent bar that also opened with a tick gap in the same direction is coloured solid — blue for bull, red for bear.

## Version History

- **v5.0 (2026-09-23):** Current version.
