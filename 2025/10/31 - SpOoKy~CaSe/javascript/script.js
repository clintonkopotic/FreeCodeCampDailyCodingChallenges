// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-31
function spookify(boo) {
  if (typeof boo !== 'string') {
    return undefined;
  }

  let spookyTilde = boo.replace(/[_-]/g, '~');
  let spookyCase = "";
  let capitalizeLetter = true;

  for (const character of spookyTilde) {
    spookyCase += capitalizeLetter ? character.toUpperCase() : character.toLowerCase();

    if (character !== '~') {
      capitalizeLetter = !capitalizeLetter;
    }
  }

  return spookyCase;
}

const tests = [
  ["hello_world", "HeLlO~wOrLd"],
  ["Spooky_Case", "SpOoKy~CaSe"],
  ["TRICK-or-TREAT", "TrIcK~oR~tReAt"],
  ["c_a-n_d-y_-b-o_w_l", "C~a~N~d~Y~~b~O~w~L"],
  ["thE_hAUntEd-hOUsE-Is-fUll_Of_ghOsts", "ThE~hAuNtEd~HoUsE~iS~fUlL~oF~gHoStS"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const boo = test[0];
  const expected = test[1];
  const actual = spookify(boo);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${boo}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(boo);
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
