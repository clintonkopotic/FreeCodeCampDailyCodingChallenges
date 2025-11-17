# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-04

def image_search(images, term):
    if (not isinstance(images, list)) or (not isinstance(term, str)):
        return None
    
    foundImages = []
    lowerCaseTerm = term.lower()
    
    for image in images:
        if lowerCaseTerm in image.lower():
            foundImages.append(image)
    
    return foundImages

tests = tests = [
    [["dog.png", "cat.jpg", "parrot.jpeg"], "dog", ["dog.png"]],
    [["Sunset.jpg", "Beach.png", "sunflower.jpeg"], "sun", ["Sunset.jpg", "sunflower.jpeg"]],
    [["Moon.png", "sun.jpeg", "stars.png"], "PNG", ["Moon.png", "stars.png"]],
    [["cat.jpg", "dogToy.jpeg", "kitty-cat.png", "catNip.jpeg", "franken_cat.gif"], "Cat", ["cat.jpg", "kitty-cat.png", "catNip.jpeg", "franken_cat.gif"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        images = test[0]
        term = test[1]
        expected = test[2]
        actual = image_search(images, term)
        success = expected == actual
        print(f"Testing {images} and \"{term}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([images, term])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            images = failure[0]
            term = failure[1]
            print(f"  {images} and \"{term}\".")
    else:
        print("All tests passed!")
