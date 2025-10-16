// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15
function stripTags(html) {
  if (typeof html !== 'string') {
    return undefined;
  }
  
  return html.replace(/<[^>]*>/g, '');
}

/**
 * HTML Tag Stripper
Given a string of HTML code, remove the tags and return the plain text content.

The input string will contain only valid HTML.
HTML tags may be nested.
Remove the tags and any attributes.
For example, '<a href="#">Click here</a>' should return "Click here".
 */

const tests = [
  ['<a href="#">Click here</a>', "Click here"],
  ['<p class="center">Hello <b>World</b>!</p>', "Hello World!"],
  ['<img src="cat.jpg" alt="Cat">', ""],
  ['<main id="main"><section class="section">section</section><section class="section">section</section></main>', "sectionsection"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const html = test[0];
  const expected = test[1];
  const actual = stripTags(html);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${html}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(html);
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
