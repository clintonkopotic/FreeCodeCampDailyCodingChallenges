// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-27
function isSpam(number) {
  if (typeof number !== 'string') {
    return undefined;
  }

  // Parse number into country code, area code, prefix, and suffix.
  const pattern = /^\+(?<countryCode>\d+)\s\((?<areaCode>\d{3})\)\s(?<prefix>\d{3})\-(?<suffix>\d{4})$/;
  const match = number.match(pattern);

  if (!Array.isArray(match) || match.length !== 5 || typeof match.groups !== 'object' || typeof match.groups.countryCode !== 'string' || typeof match.groups.areaCode !== 'string' || typeof match.groups.prefix !== 'string' || typeof match.groups.suffix !== 'string') {
    return undefined;
  }

  const countryCodeString = match.groups.countryCode;
  const countryCodeNumber = parseInt(countryCodeString);

  if (typeof countryCodeString !== 'string' || !Number.isInteger(countryCodeNumber)) {
    return undefined;
  }

  // The country code is greater than 2 digits long or doesn't begin with a zero (0).
  if (countryCodeString.length > 2 || countryCodeString[0] !== '0') {
    return true;
  }

  const areaCodeString = match.groups.areaCode;
  const areaCodeNumber = parseInt(areaCodeString);

  if (typeof areaCodeString !== 'string' || !Number.isInteger(areaCodeNumber)) {
    return undefined;
  }

  // The area code is greater than 900 or less than 200.
  if (areaCodeNumber > 900 || areaCodeNumber < 200) {
    return true;
  }

  const prefixString = match.groups.prefix;
  const prefixNumber = parseInt(prefixString);

  if (typeof prefixString !== 'string' || !Number.isInteger(prefixNumber)) {
    return undefined;
  }

  let sumOfPrefixDigits = 0;

  for (const prefixChar of prefixString) {
    const prefixDigit = parseInt(prefixChar);

    if (Number.isInteger(prefixDigit)) {
      sumOfPrefixDigits += prefixDigit;
    }
  }

  const suffixString = match.groups.suffix;
  const suffixNumber = parseInt(suffixString);

  if (typeof suffixString !== 'string' || !Number.isInteger(suffixNumber)) {
    return undefined;
  }

  // The sum of first three digits of the local number appears within last four digits of the local number.
  if (suffixString.includes(sumOfPrefixDigits)) {
    return true;
  }

  const numberNoFormattingChars = `${countryCodeString}${areaCodeString}${prefixString}${suffixString}`;

  if (numberNoFormattingChars.length < 4) {
    return undefined;
  }
  
  // The number has the same digit four or more times in a row (ignoring the formatting characters).
  for (let i = 3; i < numberNoFormattingChars.length; i++) {
    if (numberNoFormattingChars[i - 3] === numberNoFormattingChars[i] 
      && numberNoFormattingChars[i - 2] === numberNoFormattingChars[i]
      && numberNoFormattingChars[i - 1] === numberNoFormattingChars[i]) {
        return true;
    }
  }

  return false;
}

const tests = [
  ["+0 (200) 234-0182", false],
  ["+091 (555) 309-1922", true],
  ["+1 (555) 435-4792", true],
  ["+0 (955) 234-4364", true],
  ["+0 (155) 131-6943", true],
  ["+0 (555) 135-0192", true],
  ["+0 (555) 564-1987", true],
  ["+00 (555) 234-0182", false],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(tests) || test.length !== 2) {
    continue;
  }

  const number = test[0];
  const expected = test[1];
  const actual = isSpam(number);
  const success = expected === actual;

  console.log(`Testing \"${number}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(number);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
