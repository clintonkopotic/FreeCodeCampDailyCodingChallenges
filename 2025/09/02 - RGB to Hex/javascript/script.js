// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-02
function rgbToHex(rgb) {
  const prefix = "rgb(";
  const suffix = ")";
  if (typeof rgb !== 'string' || rgb.length === 0 || !rgb.startsWith(prefix) || !rgb.endsWith(suffix)) {
    return undefined;
  }

  const valueStrings = rgb
    .slice(prefix.length, rgb.length - suffix.length)
    .split(",")
    .map(str => str.trim());

  if (!Array.isArray(valueStrings) || valueStrings.length !== 3) {
    return undefined;
  }

  let result = "#";

  for (var valueString of valueStrings) {
    if (typeof valueString !== 'string' || valueString.length === 0) {
      return undefined;
    }

    const value = parseInt(valueString);

    if (!Number.isInteger(value) || value < 0) {
      return undefined;
    }

    result += value.toString(16).padStart(2, "0");
  }

  return result;
}

const tests = [
  ["rgb(255, 255, 255)", "#ffffff"],
  ["rgb(1, 11, 111)", "#010b6f"],
  ["rgb(173, 216, 230)", "#add8e6"],
  ["rgb(79, 123, 201)", "#4f7bc9"],
  ["rgb(0, 0, 0)", "#ffffff"],
]

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const rgb = test[0];
  const expected = test[1];
  const actual = rgbToHex(rgb);
  const success = expected === actual;
  console.log(`Testing \"${rgb}\" (expecting \"${expected}\")...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(rgb);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (typeof failure !== 'string') {
      continue;
    }

    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
