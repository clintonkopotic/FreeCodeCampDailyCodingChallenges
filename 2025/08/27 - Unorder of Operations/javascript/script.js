// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-27
function evaluate(numbers, operators) {
  if (!Array.isArray(numbers) || numbers.length < 2 || !Number.isInteger(numbers[0]) || !Array.isArray(operators) || operators.length === 0) {
    return undefined;
  }

  let result = numbers[0];

  for (let i = 1, j = 0; i < numbers.length; i++, j = (i - 1) % operators.length) {
    const number = numbers[i];

    if (!Number.isInteger(number)) {
      return undefined;
    }

    const operator = operators[j];

    if (operator === "+") {
      result += number;
    }
    else if (operator === "-") {
      result -= number;
    }
    else if (operator === "*") {
      result *= number;
    }
    else if (operator === "/") {
      result /= number;
    }
    else if (operator === "%") {
      result %= number;
    }
    else {
      return undefined;
    }
  }

  return result;
}

const tests = [
  [[5, 6, 7, 8, 9], ['+', '-'], 3],
  [[17, 61, 40, 24, 38, 14], ['+', '%'], 38],
  [[20, 2, 4, 24, 12, 3], ['*', '/'], 60],
  [[11, 4, 10, 17, 2], ['*', '*', '%'], 30],
  [[33, 11, 29, 13], ['/', '-'], -2],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length != 3) {
    continue;
  }

  const numbers = test[0];
  const operators = test[1];
  const expected = test[2];
  const actual = evaluate(numbers, operators);
  const success = expected === actual;

  console.log(`Testing [${numbers}] and [${operators}] (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([numbers, operators]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const numbers = failure[0];
    const operators = failure[1];

    console.log(`  [${numbers}] and [${operators}].`);
  }
}
else {
  console.log("All tests passed!");
}
