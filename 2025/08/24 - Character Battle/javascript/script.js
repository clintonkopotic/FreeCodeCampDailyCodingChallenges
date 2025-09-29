// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-24
function battle(myArmy, opposingArmy) {
  if (typeof myArmy !== 'string' || typeof opposingArmy !== 'string') {
    return undefined;
  }

  if (myArmy.length > opposingArmy.length) {
    return "Opponent retreated";
  }
  else if (myArmy.length < opposingArmy.length) {
    return "We retreated";
  }

  let battlesWon = 0;

  for (let i = 0; i < myArmy.length; i++) {
    const myCharacter = myArmy[i];
    const myValue = getValue(myCharacter);
    const opposingCharacter = opposingArmy[i];
    const opposingValue = getValue(opposingCharacter);

    if (myValue > opposingValue) {
      battlesWon++;
    }
    else if (myValue < opposingValue) {
      battlesWon--;
    }
  }

  if (battlesWon > 0) {
    return "We won";
  }
  else if (battlesWon < 0) {
    return "We lost";
  }

  return "It was a tie";

  function getValue(character) {
    if (typeof character !== 'string') {
      return 0;
    }

    if (/[a-z]/.test(character)) {
      return character.charCodeAt(0) - 'a'.charCodeAt(0) + 1;
    }
    else if (/[A-Z]/.test(character)) {
      return character.charCodeAt(0) - 'A'.charCodeAt(0) + 27;
    }
    else if (/[0-9]/.test(character)) {
      return character.charCodeAt(0) - '0'.charCodeAt(0);
    }

    return 0;
  }
}

const tests = [
  ["Hello", "World", "We lost"],
  ["pizza", "salad", "We won"],
  ["C@T5", "D0G$", "We won"],
  ["kn!ght", "orc", "Opponent retreated"],
  ["PC", "Mac", "We retreated"],
  ["Wizards", "Dragons", "It was a tie"],
  ["Mr. Smith", "Dr. Jones", "It was a tie"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const myArmy = test[0];
  const opposingArmy = test[1];
  const expected = test[2];
  const actual = battle(myArmy, opposingArmy);
  const success = expected === actual;

  console.log(`Testing \"${myArmy}\" and \"${opposingArmy}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push([myArmy, opposingArmy]);
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
