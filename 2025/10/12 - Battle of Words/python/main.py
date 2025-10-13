# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-12
def battle(our_team, opponent):
    if not isinstance(our_team, str) or not isinstance(opponent, str):
        return None
    
    our_team_words = our_team.split()
    opponent_words = opponent.split()

    if len(our_team_words) != len(opponent_words):
        return None

    def calculate_word_value(word):
        if not isinstance(word, str):
            return None
        
        values = " abcdefghijklmnopqrstuvwxyz"
        result = 0

        for letter in word:
            if letter.islower():
                result += values.find(letter)
            elif letter.isupper():
                result += values.find(letter.lower()) * 2
            else:
                return None

        return result
    
    our_team_wins = 0
    opponent_wins = 0
    
    for i in range(0, len(our_team_words)):
        our_team_word_value = calculate_word_value(our_team_words[i])
        opponent_word_value = calculate_word_value(opponent_words[i])
        
        if our_team_word_value > opponent_word_value:
            our_team_wins += 1
        elif our_team_word_value < opponent_word_value:
            opponent_wins += 1
    
    if our_team_wins > opponent_wins:
        return "We win"
    elif our_team_wins < opponent_wins:
        return "We lose"
    else:
        return "Draw"

tests = tests = [
    ["hello world", "hello word", "We win"],
    ["Hello world", "hello world", "We win"],
    ["lorem ipsum", "kitty ipsum", "We lose"],
    ["hello world", "world hello", "Draw"],
    ["git checkout", "git switch", "We win"],
    ["Cheeseburger with fries", "Cheeseburger with Fries", "We lose"],
    ["We must never surrender", "Our team must win", "Draw"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        our_team = test[0]
        opponent = test[1]
        expected = test[2]
        actual = battle(our_team, opponent)
        success = expected == actual
        print(f"Testing \"{our_team}\" and \"{opponent}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append([our_team, opponent])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            our_team = failure[0]
            opponent = failure[1]
            print(f"  \"{our_team}\" and \"{opponent}\".")
    else:
        print("All tests passed!")
