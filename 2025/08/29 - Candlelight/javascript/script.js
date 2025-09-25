// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-29
function burnCandles(candles, leftoversNeeded) {
  if (!Number.isInteger(candles) || !Number.isInteger(leftoversNeeded)) {
    return undefined;
  }

  // Burn through the candles to get the first batch of leftovers.
  let candlesBurnt = candles;
  let leftovers = candles;

  // Continue to recycle and burn until there isn't enough to recycle more.
  while (leftovers >= leftoversNeeded) {
    // Recycle the leftovers into new candles by getting the quotient of dividing the leftovers by the leftoversNeeded for each new candle.
    const newCandles = Math.floor(leftovers / leftoversNeeded);

    // Keep track of how many leftovers that weren't able to be recycled, or in otherwords, getting the remainder of dividing the leftovers by the leftoversNeeded for each new candle.
    leftovers = leftovers % leftoversNeeded;

    // Burn through the new candles.
    candlesBurnt += newCandles;

    // Update the leftovers with the new candles burnt for the next cycle. 
    leftovers += newCandles;
  }

  // Return how many candles were burned in total.
  return candlesBurnt;
}

const tests = [
  [7, 2, 13],
  [10, 5, 12],
  [20, 3, 29],
  [17, 4, 22],
  [2345, 3, 3517],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const candles = test[0];
  const leftoversNeeded = test[1];
  const expected = test[2];
  const actual = burnCandles(candles, leftoversNeeded);
  const success = expected === actual;

  console.log(`Testing ${candles} and ${leftoversNeeded} (expecting ${expected})...${actual} (success: ${success}).`)

  if (!success) {
    failures.push([candles, leftoversNeeded]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const candles = failure[0];
    const leftoversNeeded = failure[1];

    console.log(` ${candles} and ${leftoversNeeded}.`);
  }
}
else {
  console.log("All tests passed!");
}
