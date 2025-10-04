// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-03
function checkStrength(password) {
  if (typeof password !== 'string') {
    return undefined;
  }

  let rulesMeet = 0;

  // Rule 1. At least 8 characters long.
  if (password.length >= 8) {
    rulesMeet++;
  }

  // Rule 2. Contains both uppercase and lowercase letters.
  if (/[A-Z]/.test(password) && /[a-z]/.test(password)) {
    rulesMeet++;
  }

  // Rule 3. Contains at least one number.
  if (/[0-9]/.test(password)) {
    rulesMeet++;
  }

  // Rule 4. Contains at least one special character from this set: !, @, #, $, %, ^, &, or *.
  if (/[!@#$%^&*]/.test(password)) {
    rulesMeet++;
  }

  if (rulesMeet < 2) {
    return "weak";
  }
  else if (rulesMeet < 4) {
    return "medium";
  }
  else {
    return "strong";
  }
}

const tests = [
  ["123456", "weak"],
  ["pass!!!", "weak"],
  ["Qwerty", "weak"],
  ["PASSWORD", "weak"],
  ["PASSWORD!", "medium"],
  ["PassWord%^!", "medium"],
  ["qwerty12345", "medium"],
  ["PASSWORD!", "medium"],
  ["PASSWORD!", "medium"],
  ["S3cur3P@ssw0rd", "strong"],
  ["C0d3&Fun!", "strong"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const password = test[0];
  const expected = test[1];
  const actual = checkStrength(password);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${password}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(password);
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
