// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-28
function getHeadings(csv) {
  if (typeof csv !== 'string') {
    return undefined;
  }

  const headingsSplit = csv.split(',');
  const headings = [];

  for (const headingSplit of headingsSplit) {
    if (typeof headingSplit !== 'string') {
      return undefined;
    }

    headings.push(headingSplit.trim());
  }
  
  return headings;
}

const tests = [
  ["name,age,city", ["name", "age", "city"]],
  ["first name,last name,phone", ["first name", "last name", "phone"]],
  ["username , email , signup date ", ["username", "email", "signup date"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const csv = test[0];
  const expected = test[1];
  const actual = getHeadings(csv);
  const success = areArraysIdentical(expected, actual);

  console.log(`Testing \"${csv}\" (expected [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push(csv);
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

function areArraysIdentical(arr1, arr2) {
  // First, check if the lengths of the arrays are different.
  // If they are, the arrays cannot be identical.
  if (!Array.isArray(arr1) || !Array.isArray(arr2) || arr1.length !== arr2.length) {
    return false;
  }

  // Iterate through the elements of the arrays.
  // Compare each element at the same index in both arrays.
  for (let i = 0; i < arr1.length; i++) {
    // If any element at a given index does not match,
    // the arrays are not identical.
    if (arr1[i] !== arr2[i]) {
      return false;
    }
  }

  // If the loop completes without finding any mismatches,
  // the arrays are identical in content and order.
  return true;
}
