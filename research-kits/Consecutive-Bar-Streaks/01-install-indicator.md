# Step 1 — Install the Indicator

This step adds the **Zen Consecutive Bar Streak** indicator to TradingView. It computes two values per bar and exposes them in the Data Window, ready to be picked up by the CSV export in step 2.

The indicator also colours bars on the chart — green when a bull streak reaches 2 or more, red when a bear streak reaches 2 or more. This is purely visual; it does not affect the exported data.

---

## What it computes

Two columns:

| Column | Definition |
|---|---|
| **Bar Direction** | `+1` if close > open (bull bar), `-1` if close < open (bear bar), `0` if close == open (doji) |
| **Streak** | Running signed count of consecutive same-direction bars. Resets to `+1` or `-1` on direction change, `0` on doji |

**Examples of Streak values:**

| Sequence | Streak values |
|---|---|
| Bull, Bull, Bull, Bear | +1, +2, +3, -1 |
| Bear, Bear, Doji, Bull | -1, -2, 0, +1 |

The study uses the absolute value of Streak to identify run depth, and Bar Direction to determine whether the next bar continues or reverses.

---

## Install

1. Open `indicator/Zen_Consecutive_Bar_Streak_v1_0.txt` in this folder and **copy the entire contents**
2. In TradingView, open the **Pine Editor** — bottom panel, or via the menu bar at the bottom
3. **Paste** the code into a new script (clear out the default template first)
4. Click **Save** and give it a name — "Zen Consecutive Bar Streak" works
5. Click **Add to chart**

The indicator is now applied. You will see bars turning green or red where streaks of 2 or more are running.

---

## Confirm it's working

Open the **Data Window** panel on the right side of the chart (keyboard shortcut: **Alt + D**, or **Cmd + D** on Mac).

Hover over any bar. You should see:

```
Zen CBS v1.0
  Bar Direction   +1 / -1 / 0
  Streak          +2 / -3 / etc.
```

If those two values appear, the indicator is working correctly.

---

## Version note

This is a **Pine Script v6** indicator. TradingView supports v6 on all account types. If you see a compile error on paste, check that you copied the full file contents — the `@version=6` declaration must be on the first line.

---

Once installed, go to [step 2 — download the data](./02-download-data.md).
