// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-23
function isUnnaturalPrime(n) {
  if (!Number.isInteger(n)) {
    return undefined;
  }

  const absN = Math.abs(n);
  
  // Numbers less than or equal to 1 are not prime
  if (absN <= 1) {
    return false;
  }

  // 2 is the only even prime number
  if (absN === 2) {
    return true;
  }

  // Even numbers greater than 2 are not prime
  if (absN % 2 === 0) {
    return false;
  }

  // Check for divisibility by odd numbers from 3 up to the square root of num
  // We only need to check up to the square root because if a number has a divisor
  // greater than its square root, it must also have a divisor smaller than its square root.
  const limit = Math.sqrt(absN);
  for (let i = 3; i <= limit; i += 2) {
    if (absN % i === 0) {
      return false; // If divisible, it's not prime
    }
  }

  return true; // If no divisors were found, it's prime
}

const tests = [
  [1, false],
  [-1, false],
  [19, true],
  [-23, true],
  [0, false],
  [97, true],
  [-61, true],
  [99, false],
  [-44, false],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = isUnnaturalPrime(n);
  const success = expected === actual;

  console.log(`Testing ${n} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(n);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
  }
}
else {
  console.log("All tests passed!");
}
