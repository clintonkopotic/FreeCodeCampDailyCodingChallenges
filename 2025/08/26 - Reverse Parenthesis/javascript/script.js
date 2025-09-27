// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-26
function decode(s) {
  if (typeof s !== 'string') {
    return undefined;
  }

  let result = s;
  let lastOpenIndex = undefined;
  let i = 0;

  while (typeof result === 'string' && i < result.length && result.includes("(")) {
    const character = result[i];

    if (character === ")") {
      if (!Number.isInteger(lastOpenIndex) || lastOpenIndex < 0 || lastOpenIndex >= result.length || lastOpenIndex >= i) {
        return undefined;
      }

      const before = result.slice(0, lastOpenIndex);
      const decoded = result.slice(lastOpenIndex + 1, i).split('').reverse().join('');
      const after = result.slice(i + 1, result.length);
      
      result = before + decoded + after;
      lastOpenIndex = -1;
      i = 0;

      continue;
    }
    else if (character === "(") {
      lastOpenIndex = i;
    }

    i++;
  }

  return result;
}

const tests = [
  ["(f(b(dc)e)a)", "abcdef"],
  ["((is?)(a(t d)h)e(n y( uo)r)aC)", "Can you read this?"],
  ["f(Ce(re))o((e(aC)m)d)p", "freeCodeCamp"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const s = test[0];
  const expected = test[1];
  const actual = decode(s);
  const success = expected === actual;

  console.log(`Testing \"${s}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(s);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
