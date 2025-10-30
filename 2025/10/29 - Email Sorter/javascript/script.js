// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-29
function sort(emails) {
  if (!Array.isArray(emails)) {
    return undefined;
  }

  return emails.sort((email1, email2) => {
    const email1Parts = parseEmail(email1);
    const email2Parts = parseEmail(email2);

    if (email1Parts[0] < email2Parts[0]) {
      return -1;
    }
    else if (email1Parts[0] > email2Parts[0]) {
      return 1;
    }
    else if (email1Parts[1] < email2Parts[1]) {
      return -1;
    }
    else if (email1Parts[1] > email2Parts[1]) {
      return 1;
    }

    return 0;
  });

  function parseEmail(email) {
    if (typeof email !== 'string' || email.length === 0) {
      return undefined;
    }

    const emailParts = email.split('@');

    if (emailParts.length !== 2) {
      return undefined;
    }

    return [emailParts[1].toLowerCase(), emailParts[0].toLowerCase()];
  }
}

const tests = [
  [["jill@mail.com", "john@example.com", "jane@example.com"], ["jane@example.com", "john@example.com", "jill@mail.com"]],
  [["bob@mail.com", "alice@zoo.com", "carol@mail.com"], ["bob@mail.com", "carol@mail.com", "alice@zoo.com"]],
  [["user@z.com", "user@y.com", "user@x.com"], ["user@x.com", "user@y.com", "user@z.com"]],
  [["sam@MAIL.com", "amy@mail.COM", "bob@Mail.com"], ["amy@mail.COM", "bob@Mail.com", "sam@MAIL.com"]],
  [["simon@beta.com", "sammy@alpha.com", "Sarah@Alpha.com", "SAM@ALPHA.com", "Simone@Beta.com", "sara@alpha.com"], ["SAM@ALPHA.com", "sammy@alpha.com", "sara@alpha.com", "Sarah@Alpha.com", "simon@beta.com", "Simone@Beta.com"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const emails = test[0];
  const expected = test[1];
  const actual = sort(emails);
  const success = areArraysEqual(expected, actual);

  console.log(`Testing [${emails}] (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push(emails);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  [${failure}].`);
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
