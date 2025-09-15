// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-07
function parseRomanNumeral(numeral) {
  if (typeof numeral !== 'string' || numeral.length === 0) {
    return undefined;
  }

  const letterValueMap = {
    'I': 1,
    'V': 5,
    'X': 10,
    'L': 50,
    'C': 100,
    'D': 500,
    'M': 1_000,
  }
  let result = 0;
  let lastValue = 0;

  for (let i = 0; i < numeral.length; i++) {
    const letter = numeral.charAt(i);
    const value = letterValueMap[letter] || undefined;

    if (i > 0) {
      if (lastValue < value) {
        result -= lastValue;
      }
      else {
        result += lastValue;
      }
    }

    lastValue = value;
  }

  result += lastValue;

  return result;
}

const tests = [
  ["III", 3],
  ["IV", 4],
  ["XXVI", 26],
  ["XCIX", 99],
  ["CDLX", 460],
  ["DIV", 504],
  ["MMXXV", 2025],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const numeral = test[0];
  const expected = test[1];

  if (typeof numeral !== 'string' || numeral.length === 0 || !Number.isInteger(expected)) {
    continue;
  }

  const actual = parseRomanNumeral(numeral);
  const success = expected === actual;
  console.log("Testing \"" + numeral + "\" (expecting " + expected + ")..." + actual + " (success: " + success + ").");

  if (success !== true) {
    failures.push(numeral);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}`);
  }
}
else {
  console.log("All numerals were parsed correctly.");
}

const minNumeralNumber = 1;
const maxNumeralNumber = 3_999;
console.log("");
console.log("Counting from " + minNumeralNumber + " to " + maxNumeralNumber + ":");
failures = [];
let longestNumeral = "";
let longestAsDecimal = 0;
console.log(`${"Numeral".padStart(15)} | ${"Parsed".padStart(6)} | ${"Expected".padStart(8)} | ${"Correct".padStart(7)}`);
console.log(`${"".padStart(15, "-")}-+-${"".padStart(6, "-")}-+-${"".padStart(8, "-")}-+-${"".padStart(7, "-")}`);

for (let number = minNumeralNumber; number <= maxNumeralNumber; number++) {
  const numeral = convertToRomalNumeral(number);
  const parsedValue = parseRomanNumeral(numeral);
  const success = number === parsedValue;
  console.log(`${numeral.padStart(15)} | ${parsedValue.toString().padStart(6)} | ${number.toString().padStart(8)} | ${success.toString().padStart(7)}`);

  if (numeral.length > longestNumeral.length) {
    longestNumeral = numeral;
    longestAsDecimal = number;
  }

  if (!success) {
    failures.push(numeral);
  }
}

console.log("");

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following numerals were not parsed correctly:");

  for (var failure of failures) {
    console.log(`  ${failure}`);
  }
}
else {
  console.log("All numerals were parsed correctly.");
}

console.log("");
console.log(`Longest numeral of ${longestNumeral.length} characters is ${longestNumeral} or in decimal ${longestAsDecimal}.`);


function convertToRomalNumeral(number) {
  if (!Number.isInteger(number) || number <= 0 || number >= 4_000) {
    return undefined;
  }

  const thousandsDigitValues = {
    1: "M",
    2: "MM",
    3: "MMM",
  };
  const hundredsDigitValues = {
    0: "",
    1: "C",
    2: "CC",
    3: "CCC",
    4: "CD",
    5: "D",
    6: "DC",
    7: "DCC",
    8: "DCCC",
    9: "CM",
  };
  const tensDigitValues = {
    0: "",
    1: "X",
    2: "XX",
    3: "XXX",
    4: "XL",
    5: "L",
    6: "LX",
    7: "LXX",
    8: "LXXX",
    9: "XC",
  };
  const onesDigitValues = {
    0: "",
    1: "I",
    2: "II",
    3: "III",
    4: "IV",
    5: "V",
    6: "VI",
    7: "VII",
    8: "VIII",
    9: "IX",
  };
  const numberAsString = number.toString();
  let digitValue = 10 ** (numberAsString.length - 1);
  let result = "";

  for (let i = 0; i < numberAsString.length; i++) {
    const digit = parseInt(numberAsString.charAt(i));

    if (digitValue === 1_000) {
      result += thousandsDigitValues[digit];
    }
    else if (digitValue === 100) {
      result += hundredsDigitValues[digit];
    }
    else if (digitValue === 10) {
      result += tensDigitValues[digit];
    }
    else if (digitValue === 1) {
      result += onesDigitValues[digit];
    }

    digitValue /= 10;
  }

  return result;
}
