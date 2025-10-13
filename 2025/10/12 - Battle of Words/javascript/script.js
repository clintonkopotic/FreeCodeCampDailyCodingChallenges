// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-12
function battle(ourTeam, opponent) {
  if (typeof ourTeam !== 'string' || typeof opponent !== 'string') {
    return undefined;
  }

  const ourTeamWords = ourTeam.split(' ');
  const opponentWords = opponent.split(' ');

  if (!Array.isArray(ourTeamWords) || !Array.isArray(opponentWords) || ourTeamWords.length !== opponentWords.length) {
    return undefined;
  }

  let ourTeamWins = 0;
  let opponentWins = 0;

  for (let i = 0; i < ourTeamWords.length; i++) {
    const ourTeamWordValue = calculateWordValue(ourTeamWords[i]);
    const opponentWordValue = calculateWordValue(opponentWords[i]);

    if (ourTeamWordValue > opponentWordValue) {
      ourTeamWins++;
    }
    else if (ourTeamWordValue < opponentWordValue) {
      opponentWins++;
    }
  }

  if (ourTeamWins > opponentWins) {
    return "We win";
  }
  else if (ourTeamWins < opponentWins) {
    return "We lose";
  }
  else {
    return "Draw";
  }

  function calculateWordValue(word) {
    if (typeof word !== 'string') {
      return undefined;
    }

    const values = " abcdefghijklmnopqrstuvwxyz";
    let result = 0;

    for (const letter of word) {
      if (/[a-z]/.test(letter)) {
        result += values.indexOf(letter);
      }
      else if (/[A-Z]/.test(letter)) {
        result += values.indexOf(letter.toLowerCase()) * 2;
      }
      else {
        return undefined;
      }
    }

    return result;
  }
}

const tests = [
  ["hello world", "hello word", "We win"],
  ["Hello world", "hello world", "We win"],
  ["lorem ipsum", "kitty ipsum", "We lose"],
  ["hello world", "world hello", "Draw"],
  ["git checkout", "git switch", "We win"],
  ["Cheeseburger with fries", "Cheeseburger with Fries", "We lose"],
  ["We must never surrender", "Our team must win", "Draw"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const ourTeam = test[0];
  const opponent = test[1];
  const expected = test[2];
  const actual = battle(ourTeam, opponent);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${ourTeam}\" and \"${opponent}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push();
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
  if (!Array.isArray(failure) || failure.length !== 2) {
    continue;
  }

  const ourTeam = failure[0];
  const opponent = failure[1];

  // eslint-disable-next-line no-useless-escape
  console.log(`  \"${ourTeam}\" and \"${opponent}\".`);
  }
}
else {
  console.log("All tests passed!");
}
