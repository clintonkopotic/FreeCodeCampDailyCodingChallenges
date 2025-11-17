// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-07
function combinations(cards) {
  const cardsInDeck = 52;

  if (!Number.isInteger(cards) || cards < 0 || cards > cardsInDeck) {
    return undefined;
  }
  else if (cards === 0 || cards === cardsInDeck) {
    return 1; // Only one way to choose 0 or all items.
  }
  else if (cards > (cardsInDeck / 2)) {
    cards = cardsInDeck - cards; // Optimize calculation for symmetry.
  }

  let result = 1;

  for (let i = 1; i <= cards; i++) {
    result = result * (cardsInDeck - i + 1) / i;
  }
  
  return result;
}

const tests = [
  [52, 1],
  [1, 52],
  [2, 1326],
  [5, 2598960],
  [10, 15820024220],
  [50, 1326],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const cards = test[0];
  const expected = test[1];
  const actual = combinations(cards);
  const success = expected === actual;

  console.log(`Testing ${cards} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(cards);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
  }
}
else {
  console.log("All tests passed!");
}
