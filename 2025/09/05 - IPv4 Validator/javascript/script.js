// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-05
function isValidIPv4(ipv4) {
  if (typeof ipv4 !== 'string') {
    return undefined;
  }

  const octets = ipv4.split(".");

  if (!Array.isArray(octets) || octets.length !== 4) {
    return false;
  }

  for (const octet of octets) {
    if (typeof octet !== 'string' || octet.length === 0 || octet.length > 3 || (octet[0] === "0" && octet.length !== 1)) {
      return false;
    }

    const octetValue = Number.parseInt(octet);

    if (Number.isNaN(octetValue) || octetValue < 0 || octetValue > 255) {
      return false;
    }
  }

  return true;
}

const tests = [
  ["192.168.1.1", true],
  ["0.0.0.0", true],
  ["255.01.50.111", false],
  ["255.00.50.111", false],
  ["256.101.50.115", false],
  ["192.168.101.", false],
  ["192168145213", false],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const ipv4 = test[0];
  const expected = test[1];

  if (typeof ipv4 !== 'string' || ipv4.length === 0 || typeof expected !== 'boolean') {
    continue;
  }

  const actual = isValidIPv4(ipv4);
  const success = expected === actual;
  console.log("Testing \"" + ipv4 + "\" (expecting " + expected + ")..." + actual + " (success: " + success + ").");

  if (success !== true) {
    failures.push(ipv4);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log("  \"" + failure + "\".");
  }
}
else {
  console.log("All tests passed!");
}
