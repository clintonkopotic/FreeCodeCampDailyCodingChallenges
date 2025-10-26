// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-25
function complementaryDNA(strand) {
  if (typeof strand !== 'string') {
    return undefined;
  }

  let result = "";

  for (const letter of strand) {
    if (typeof letter !== 'string' || letter.length !== 1) {
      return undefined;
    }

    if (letter === 'A') {
      result += 'T';
    }
    else if (letter === 'T') {
      result += 'A';
    }
    else if (letter === 'C') {
      result += 'G';
    }
    else if (letter === 'G') {
      result += 'C';
    }
    else {
      return undefined;
    }
  }

  return result;
}

const tests = [
  ["ACGT", "TGCA"],
  ["ATGCGTACGTTAGC", "TACGCATGCAATCG"],
  ["GGCTTACGATCGAAG", "CCGAATGCTAGCTTC"],
  ["GATCTAGCTAGGCTAGCTAG", "CTAGATCGATCCGATCGATC"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const strand = test[0];
  const expected = test[1];
  const actual = complementaryDNA(strand);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${strand}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(strand);
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
