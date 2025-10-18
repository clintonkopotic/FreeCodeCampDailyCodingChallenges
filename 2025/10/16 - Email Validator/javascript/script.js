// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-16
function validate(email) {
  if (typeof email !== 'string') {
    return undefined;
  }

  const parts = email.split('@');

  if (!Array.isArray(parts) || parts.length !== 2) {
    return false;
  }

  const localPart = parts[0];

  if (!(/^[0-9a-zA-Z._-]+$/.test(localPart)) || localPart.startsWith(".") || localPart.endsWith(".") || localPart.includes("..")) {
    return false;
  }

  const domainPart = parts[1];

  if (domainPart.startsWith(".") || domainPart.endsWith(".") || domainPart.includes("..") || !domainPart.includes('.')) {
    return false;
  }

  const domainDotParts = domainPart.split('.');

  if (!Array.isArray(domainDotParts) || domainDotParts.length < 2) {
    return false;
  }

  for (const domainDotPart of domainDotParts) {
    if (typeof domainDotPart !== 'string' || domainDotPart.length === 0) {
      return false;
    }
  }

  let letterCount = 0;
  
  for (const lastDomainDotPartCharacter of domainDotParts[domainDotParts.length - 1]) {
    if (!(/[a-zA-Z]/.test(lastDomainDotPartCharacter))) {
      return false;
    }

    letterCount++;
  }

  return letterCount >= 2;
}

const tests = [
  ["a@b.cd", true],
  ["hell.-w.rld@example.com", true],
  [".b@sh.rc", false],
  ["example@test.c0", false],
  ["freecodecamp.org", false],
  ["develop.ment_user@c0D!NG.R.CKS", true],
  ["hello.@wo.rld", false],
  ["hello@world..com", false],
  ["git@commit@push.io", false],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const email = test[0];
  const expected = test[1];
  const actual = validate(email);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${email}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(email);
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
