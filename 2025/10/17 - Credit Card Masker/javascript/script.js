// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-17
function mask(card) {
  if (typeof card !== 'string' || card.length !== 19) {
    return undefined;
  }

  // 0         1   
  // 0123-5678-0123-5678
  return `****${card[4]}****${card[9]}****${card.slice(14)}`;
}

const tests = [
  ["4012-8888-8888-1881", "****-****-****-1881"],
  ["5105 1051 0510 5100", "**** **** **** 5100"],
  ["6011 1111 1111 1117", "**** **** **** 1117"],
  ["2223-0000-4845-0010", "****-****-****-0010"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const card = test[0];
  const expected = test[1];
  const actual = mask(card);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${card}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(card);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
