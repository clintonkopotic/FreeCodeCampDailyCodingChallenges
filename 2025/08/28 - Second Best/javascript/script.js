// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-28
function getLaptopCost(laptops, budget) {
  if (!Array.isArray(laptops) || laptops.length === 0 || !Number.isInteger(budget) || budget <= 0) {
    return undefined;
  }

  // Remove duplicates and ensure each is a positive integer.
  const laptopsUnique = new Set();

  for (const laptop of laptops) {
    if (Number.isInteger(laptop) && laptop > 0) {
      laptopsUnique.add(laptop);
    }
  }

  // Sort descending so the most expensive is the first element.
  const laptopsSorted = [...laptopsUnique].sort((a, b) => b - a);

  // Check to see if the second most expensive laptop is within budget
  if (laptopsSorted.length >= 2 && laptopsSorted[1] <= budget) {
    return laptopsSorted[1];
  }

  // Go through each laptop from most to least expensive and return the first one that is within budget.
  for (const laptop of laptopsSorted) {
    if (Number.isInteger(laptop) && laptop > 0 && laptop <= budget) {
      return laptop;
    }
  }

  // No laptops are within budget.
  return 0;
}

const tests = [
  [[1500, 2000, 1800, 1400], 1900, 1800],
  [[1500, 2000, 2000, 1800, 1400], 1900, 1800],
  [[2099, 1599, 1899, 1499], 2200, 1899],
  [[2099, 1599, 1899, 1499], 1000, 0],
  [[1200, 1500, 1600, 1800, 1400, 2000], 1450, 1400],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) && test.length != 3) {
    continue;
  }

  const laptops = test[0];
  const budget = test[1];
  const expected = test[2];
  const actual = getLaptopCost(laptops, budget);
  const success = expected === actual;

  console.log(`Testing [${laptops}] and ${budget} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([laptops, expected]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const laptops = failure[0];
    const budget = failure[1];

    console.log(`  [${laptops}] and ${budget}.`);
  }
}
else {
  console.log("All tests passed!");
}
