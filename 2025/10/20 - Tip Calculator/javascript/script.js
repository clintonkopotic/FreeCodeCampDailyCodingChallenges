// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-20
function calculateTips(mealPrice, customTip) {
  if (typeof mealPrice !== 'string' || mealPrice.length === 0 || mealPrice[0] !== '$' || typeof customTip !== 'string' || customTip.length === 0 || customTip[customTip.length - 1] !== '%') {
    return undefined;
  }

  const mealPriceValue = parseFloat(mealPrice.slice(1));
  const customTipValue = parseInt(customTip.slice(0, customTip.length - 1));

  return [calculateTip(mealPriceValue, 15), calculateTip(mealPriceValue, 20), calculateTip(mealPriceValue, customTipValue)];

  function calculateTip(priceValue, tipValue) {
    if (!Number.isFinite(priceValue) || priceValue < 0 || !Number.isFinite(tipValue) || tipValue < 0) {
      return undefined;
    }

    return `$${(priceValue * tipValue / 100).toFixed(2)}`;
  }
}

const tests = [
  ["$10.00", "25%", ["$1.50", "$2.00", "$2.50"]],
  ["$89.67", "26%", ["$13.45", "$17.93", "$23.31"]],
  ["$19.85", "9%", ["$2.98", "$3.97", "$1.79"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const mealPrice = test[0];
  const customTip = test[1];
  const expected = test[2];
  const actual = calculateTips(mealPrice, customTip);
  const success = areArraysEqual(expected, actual);

  console.log(`Testing ${mealPrice} and ${customTip} (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push([mealPrice, customTip]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const mealPrice = failure[0];
    const customTip = failure[1];

    console.log(`  ${mealPrice} and ${customTip}.`);
  }
}
else {
  console.log("All tests passed!");
}

function areArraysEqual(arr1, arr2) {
  if (!Array.isArray(arr1) || !Array.isArray(arr2)) {
    return undefined;
  }

  // First, check if the arrays are strictly the same reference.
  if (arr1 === arr2) {
    return true;
  }

  // If either array is null or undefined, they are not equal (unless both are null/undefined, caught by the first check).
  if (arr1 == null || arr2 == null) {
    return false;
  }

  // If the lengths are different, the arrays cannot be equal.
  if (arr1.length !== arr2.length) {
    return false;
  }

  // Iterate through the arrays and compare elements at corresponding indices.
  for (let i = 0; i < arr1.length; i++) {
    if (arr1[i] !== arr2[i]) {
      return false; // Found a differing element, so arrays are not equal.
    }
  }

  // If all checks pass, the arrays are equal.
  return true;
}
