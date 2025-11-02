// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-30
function nthPrime(n) {
  if (!Number.isInteger(n) || n <= 0) {
    return undefined;
  }

  let prime = 2;

  for (let i = 1; i < n; i++) {
    let number = prime + 1;

    while (!isPrime(number)) {
      number++;
    }

    prime = number;
  }

  return prime;

  function isPrime(n) {
    if (!Number.isInteger(n) || n <= 1) {
      return undefined;
    }

    // 2 is the only even prime number.
    if (n === 2) {
      return true;
    }

    // Even numbers greater than 2 are not prime.
    if (n % 2 === 0) {
      return false;
    }

    // Check for divisibility by odd numbers from 3 up to the square root of num.
    // We only need to check up to the square root because if a number has a divisor
    // greater than its square root, it must also have a divisor smaller than its square root.
    const limit = Math.sqrt(n);

    for (let i = 3; i <= limit; i += 2) {
      if (n % i === 0) {
        return false; // If divisible, it's not prime.
      }
    }

    return true; // If no divisors were found, it's prime.
  }
}

const tests = [
  [5, 11],
  [10, 29],
  [16, 53],
  [99, 523],
  [1000, 7919],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = nthPrime(n);
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
