function costToFill(tankSize, fuelLevel, pricePerGallon) {
  if (typeof tankSize !== 'number' || typeof fuelLevel !== 'number' || typeof pricePerGallon !== 'number') {
    return undefined;
  }

  const gallonsToFill = tankSize - fuelLevel;
  const costToFill = gallonsToFill * pricePerGallon;

  return `$${costToFill.toFixed(2)}`;
}

const tests = [
  { tankSize: 20, fuelLevel: 0, pricePerGallon: 4.00, expected: "$80.00" },
  { tankSize: 15, fuelLevel: 10, pricePerGallon: 3.50, expected: "$17.50" },
  { tankSize: 18, fuelLevel: 9, pricePerGallon: 3.25, expected: "$29.25" },
  { tankSize: 12, fuelLevel: 12, pricePerGallon: 4.99, expected: "$0.00" },
  { tankSize: 15, fuelLevel: 9.5, pricePerGallon: 3.98, expected: "$21.89" },
];

let failures = [];

for (const test of tests) {
  if (typeof test !== 'object') {
    continue;
  }

  const actual = costToFill(test.tankSize, test.fuelLevel, test.pricePerGallon);
  const success = test.expected === actual;
  console.log(`Testing ${testToString(test)}...\"${actual}\" (success: ${success}).`);

  if (success !== true) {
    failures.push(test);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
  }
}
else {
  console.log("All tests passed!");
}

function testToString(test) {
  if (typeof test !== 'object') {
    return undefined;
  }

  let result = "{";
  let count = 0;

  for (const [key, value] of Object.entries(test)) {
    count++;

    if (count === 1) {
      result += " ";
    }
    else if (count > 1) {
      result += ", ";
    }

    result += `${key}: `;

    if (typeof value === 'string') {
      result += `\"${value}\"`;
    }
    else {
      result += `${value}`;
    }
  }

  if (count > 0) {
    result += " ";
  }

  return result + "}";
}
