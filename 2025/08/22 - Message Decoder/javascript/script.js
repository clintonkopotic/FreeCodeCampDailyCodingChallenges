// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-22
function decode(message, shift) {
  if (typeof message !== 'string' || !Number.isInteger(shift)) {
    return undefined;
  }
  const aCodePoint = 'a'.codePointAt(0);
  const ACodePoint = 'A'.codePointAt(0);
  let result = "";

  for (let i = 0; i < message.length; i++) {
    if (/[A-Z]/.test(message[i])) {
      result += decodeCodePoint(message.codePointAt(i), ACodePoint, shift);
    }
    else if (/[a-z]/.test(message[i])) {
      result += decodeCodePoint(message.codePointAt(i), aCodePoint, shift);
    }
    else {
      result += message[i];
    }
  }

  return result;

  function decodeCodePoint(codePoint, _aCodePoint, shift) {
    codePoint -= _aCodePoint;
    codePoint -= shift;
    codePoint %= 26;

    if (codePoint < 0) {
      codePoint += 26;
    }

    codePoint += _aCodePoint;
    
    return String.fromCodePoint([codePoint]);
  }
}

const tests = [
  ["Xlmw mw e wigvix qiwweki.", 4, "This is a secret message."],
  ["Byffi Qilfx!", 20, "Hello World!"],
  ["Zqd xnt njzx?", -1, "Are you okay?"],
  ["oannLxmnLjvy", 9, "freeCodeCamp"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const message = test[0];
  const shift = test[1];
  const expected = test[2];
  const actual = decode(message, shift);
  const success = expected === actual;

  console.log(`Testing \"${message}\" and ${shift} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push([message, shift]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
  if (!Array.isArray(failure) || failure.length !== 2) {
    continue;
  }

  const message = failure[0];
  const shift = failure[1];

  console.log(`  \"${message}\" and ${shift}.`);
  }
}
else {
  console.log("All tests passed!");
}
