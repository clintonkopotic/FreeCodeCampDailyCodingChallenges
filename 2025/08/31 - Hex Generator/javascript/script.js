// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-31
function generateHex(color) {
  if (typeof color !== 'string') {
    return undefined;
  }

  color = color.trim().toLowerCase();

  const dominateColorNumber = Math.floor(Math.random() * 100 + 156);
  const dominateColor = dominateColorNumber.toString(16).padStart(2, '0').toUpperCase();
  const secondColor = Math.floor(Math.random() * dominateColorNumber).toString(16).padStart(2, '0').toUpperCase();
  const thirdColor = Math.floor(Math.random() * dominateColorNumber).toString(16).padStart(2, '0').toUpperCase();

  if (color === "red") {
    return `${dominateColor}${secondColor}${thirdColor}`;
  }
  else if (color === "green") {
    return `${secondColor}${dominateColor}${thirdColor}`;
  }
  else if (color === "blue") {
    return `${secondColor}${thirdColor}${dominateColor}`;
  }

  return "Invalid color";
}

let failures = [];

// Test 1. - generateHex("yellow") should return "Invalid color".
const color1 = "yellow";
const expected1 = "Invalid color";
const actual1 = generateHex(color1);
const success1 = "Invalid color" === actual1;
console.log(`Testing \"${color1}\" (expecting \"${expected1}\")...\"${actual1}\" (success: ${success1}).`);

if (!success1) {
  failures.push(color1);
}

// Test 2. - generateHex("red") should return a six-character string.
const color2 = "red";
const expected2 = 6;
const actual2 = generateHex(color2);
const actual2Length = actual2.length;
const success2 = expected2 === actual2Length;
console.log(`Testing \"${color2}\" (expecting six-character string)...\"${actual2}\" (length: ${actual2Length}; success: ${success2}).`);

if (!success2) {
  failures.push(color2);
}

// Test 3. - generateHex("red") should return a valid six-character hex color code.
function validSixCharacterHexColorCode(str) {
  if (typeof str !== 'string') {
    return undefined;
  }

  if (str.length !== 6) {
    return false;
  }

  for (const character of str) {
    if (!/[0-9A-F]/.test(character.toUpperCase())) {
      return false;
    }
  }

  return true;
}

const color3 = "red";
const expected3 = 6;
const actual3 = generateHex(color3);
const actual3Length = actual3.length;
const validLength3 = expected3 === actual3Length;
const validHexColorCode3 = validSixCharacterHexColorCode(actual3);
const success3 = validLength3 && validHexColorCode3;

console.log(`Testing \"${color3}\" (expecting valid six-character hex color code)...\"${actual3}\" (length: ${actual3Length}; valid hex color code: ${validHexColorCode3}; success: ${success3}).`);

if (!success3) {
  failures.push(color3);
}

// Test 4. - generateHex("red") should return a valid hex color with a higher red value than other colors.
function parseIntoRgbIntArray(str) {
  if (typeof str !== 'string'  || !validSixCharacterHexColorCode(str)) {
    return undefined;
  }

  const red = parseInt(str.slice(0, 2), 16);
  const green = parseInt(str.slice(2, 4), 16);
  const blue = parseInt(str.slice(4, 6), 16);

  return [red, green, blue];
}

const color4 = "red";
const actual4 = generateHex(color4);
const rgbInts4 = parseIntoRgbIntArray(actual4);
const success4 = rgbInts4[0] > rgbInts4[1] && rgbInts4[0] > rgbInts4[2];

console.log(`Testing \"${color4}\" (expecting valid hex color with a higher red value than other colors)...\"${actual4}\" (red: ${rgbInts4[0]}; green: ${rgbInts4[1]}; blue: ${rgbInts4[2]}; success: ${success4}).`);

if (!success4) {
  failures.push(color4);
}

// Test 5. - Calling generateHex("red") twice should return two different hex color values where red is dominant.
const color5 = "red";
const actual51 = generateHex(color5);
const rgbInts51 = parseIntoRgbIntArray(actual51);
const success51 = rgbInts51[0] > rgbInts51[1] && rgbInts51[0] > rgbInts51[2];
const actual52 = generateHex(color5);
const rgbInts52 = parseIntoRgbIntArray(actual52);
const success52 = rgbInts52[0] > rgbInts52[1] && rgbInts52[0] > rgbInts52[2];

console.log(`Testing \"${color5}\" (expecting two different hex color values where ${color5} is dominant):`);
console.log(`  \"${actual51}\" (red: ${rgbInts51[0]}; green: ${rgbInts51[1]}; blue: ${rgbInts51[2]}; success: ${success51}).`);
console.log(`  \"${actual52}\" (red: ${rgbInts52[0]}; green: ${rgbInts52[1]}; blue: ${rgbInts52[2]}; success: ${success52}).`);
const success53 = actual51 !== actual52;
console.log(`  Different hex color values: ${success53}.`);
const success5 = success51 && success52 && success53;

if (!success5) {
  failures.push(color5);
}

// Test 6. - Calling generateHex("green") twice should return two different hex color values where green is dominant.
const color6 = "green";
const actual61 = generateHex(color6);
const rgbInts61 = parseIntoRgbIntArray(actual61);
const success61 = rgbInts61[1] > rgbInts61[0] && rgbInts61[1] > rgbInts61[2];
const actual62 = generateHex(color6);
const rgbInts62 = parseIntoRgbIntArray(actual62);
const success62 = rgbInts62[1] > rgbInts62[0] && rgbInts62[1] > rgbInts62[2];

console.log(`Testing \"${color6}\" (expecting two different hex color values where ${color6} is dominant):`);
console.log(`  \"${actual61}\" (red: ${rgbInts61[0]}; green: ${rgbInts61[1]}; blue: ${rgbInts61[2]}; success: ${success61}).`);
console.log(`  \"${actual62}\" (red: ${rgbInts62[0]}; green: ${rgbInts62[1]}; blue: ${rgbInts62[2]}; success: ${success62}).`);
const success63 = actual61 !== actual62;
console.log(`  Different hex color values: ${success63}.`);
const success6 = success61 && success62 && success63;

if (!success6) {
  failures.push(color6);
}

// Test 7. - Calling generateHex("blue") twice should return two different hex color values where blue is dominant.
const color7 = "blue";
const actual71 = generateHex(color7);
const rgbInts71 = parseIntoRgbIntArray(actual71);
const success71 = rgbInts71[2] > rgbInts71[0] && rgbInts71[2] > rgbInts71[1];
const actual72 = generateHex(color7);
const rgbInts72 = parseIntoRgbIntArray(actual72);
const success72 = rgbInts72[2] > rgbInts72[0] && rgbInts72[2] > rgbInts72[1];

console.log(`Testing \"${color7}\" (expecting two different hex color values where ${color7} is dominant):`);
console.log(`  \"${actual71}\" (red: ${rgbInts71[0]}; green: ${rgbInts71[1]}; blue: ${rgbInts71[2]}; success: ${success71}).`);
console.log(`  \"${actual72}\" (red: ${rgbInts72[0]}; green: ${rgbInts72[1]}; blue: ${rgbInts72[2]}; success: ${success72}).`);
const success73 = actual71 !== actual72;
console.log(`  Different hex color values: ${success73}.`);
const success7 = success71 && success72 && success73;

if (!success7) {
  failures.push(color7);
}

// Done.
if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
