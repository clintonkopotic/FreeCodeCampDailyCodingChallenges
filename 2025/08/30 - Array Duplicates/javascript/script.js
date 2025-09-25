// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-30
function findDuplicates(arr) {
  if (!Array.isArray(arr)) {
    return undefined;
  }

  let result = [];
  let appeared = [];

  for (const item of arr) {
    if (!appeared.includes(item)) {
      appeared.push(item);
    }
    else if (!result.includes(item)) {
      result.push(item);
    }
  }

  return result.sort((a, b) => a - b);
}

console.log(findDuplicates([1, 2, 3, 4, 5]));
console.log(findDuplicates([1, 2, 3, 4, 1, 2]));
console.log(findDuplicates([2, 34, 0, 1, -6, 23, 5, 3, 2, 5, 67, -6, 23, 2, 43, 2, 12, 0, 2, 4, 4]));

/**
 * 1. findDuplicates([1, 2, 3, 4, 5]) should return [].
Failed:2. findDuplicates([1, 2, 3, 4, 1, 2]) should return [1, 2].
Failed:3. findDuplicates([2, 34, 0, 1, -6, 23, 5, 3, 2, 5, 67, -6, 23, 2, 43, 2, 12, 0, 2, 4, 4]) should return [-6, 0, 2, 4, 5, 23].
 */