// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-28
function navigate(commands) {
  if (!Array.isArray(commands) || commands.length === 0) {
    return undefined;
  }
  
  const visitPagePrefix = "Visit ";
  let history = ["Home"];
  let currentPageHistoryIndex = 0;

  for (const command of commands) {
    if (typeof command !== 'string' || command.length === 0) {
      return undefined;
    }

    if (command.startsWith(visitPagePrefix)) {
      history = history.slice(0, currentPageHistoryIndex + 1);
      history.push(command.slice(visitPagePrefix.length));
      currentPageHistoryIndex++;
    }
    else if (command === "Back") {
      if (currentPageHistoryIndex !== 0) {
        currentPageHistoryIndex--;
      }
    }
    else if (command === "Forward") {
      if (currentPageHistoryIndex !== history.length - 1) {
        currentPageHistoryIndex++;
      }
    }
    else {
      return undefined;
    }
  }

  return history[currentPageHistoryIndex];
}

const tests = [
  [["Visit About Us", "Back", "Forward"], "About Us"],
  [["Forward"], "Home"],
  [["Back"], "Home"],
  [["Visit About Us", "Visit Gallery"], "Gallery"],
  [["Visit About Us", "Visit Gallery", "Back", "Back"], "Home"],
  [["Visit About", "Visit Gallery", "Back", "Visit Contact", "Forward"], "Contact"],
  [["Visit About Us", "Visit Visit Us", "Forward", "Visit Contact Us", "Back"], "Visit Us"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const commands = test[0];
  const expected = test[1];
  const actual = navigate(commands);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing [${commands}] (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(commands);
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
