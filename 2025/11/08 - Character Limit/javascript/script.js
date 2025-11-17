// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-08
function canPost(message) {
  if (typeof message !== 'string') {
    return undefined;
  }
  
  if (message.length <= 40) {
    return "short post";
  }
  else if (message.length <= 80) {
    return "long post";
  }
  else {
    return "invalid post"
  }
}

const tests = [
  ["Hello world", "short post"],
  ["This is a longer message but still under eighty characters.", "long post"],
  ["This message is too long to fit into either of the character limits for a social media post.", "invalid post"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const message = test[0];
  const expected = test[1];
  const actual = canPost(message);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${message}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(message);
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
