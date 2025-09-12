function reverseSentence(sentence) {
  if (typeof sentence !== 'string' || sentence.length === 0) {
    return undefined;
  }

  const words = sentence.split(" ");
  let result = "";

  for (let i = (words.length - 1); i >= 0; i--) {
    let word = words[i];

    if (typeof word === 'string') {
      word = word.trim();

      if (word.length > 0) {
        if (i === words.length - 1) {
          result = word;
        }
        else {
          result = result.concat(" ", word);
        }
      }
    }
  }

  return result;
}
