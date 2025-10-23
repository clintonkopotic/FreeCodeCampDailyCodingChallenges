# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-20
import math

def calculate_tips(meal_price, custom_tip):
    if (not isinstance(meal_price, str)) or (not meal_price.startswith('$')) or (not isinstance(custom_tip, str)) or (not custom_tip.endswith('%')):
        return None
    
    meal_price_value = float(meal_price[1:])
    custom_tip_value = int(custom_tip[:-1])
    
    def calculate_tip(price_value, tip_value):
        if (not math.isfinite(price_value)) or (price_value < 0) or (not math.isfinite(tip_value)) or (tip_value < 0):
            return None
        
        return f"${price_value * tip_value / 100:.2f}"

    return [
        calculate_tip(meal_price_value, 15),
        calculate_tip(meal_price_value, 20),
        calculate_tip(meal_price_value, custom_tip_value),
    ]

tests = tests = [
    ["$10.00", "25%", ["$1.50", "$2.00", "$2.50"]],
    ["$89.67", "26%", ["$13.45", "$17.93", "$23.31"]],
    ["$19.85", "9%", ["$2.98", "$3.97", "$1.79"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        meal_price = test[0]
        custom_tip = test[1]
        expected = test[2]
        actual = calculate_tips(meal_price, custom_tip)
        success = expected == actual
        print(f"Testing {meal_price} and {custom_tip} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([meal_price, custom_tip])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            meal_price = failure[0]
            custom_tip = failure[1]
            print(f"  {meal_price} and {custom_tip}.")
    else:
        print("All tests passed!")
