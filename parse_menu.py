import sys
import itertools
import pandas

phrases = [
    '(g)',
    '(Kcals)',
    '(mg)',
    '2,000 calories a day is used for general nutrition advice, but calorie needs vary.',
    'Updated: 9/05/2018',
    'Disclaimer: The nutritional information on this site is for the serving size ' + 
    'listed, based on adherence to the recipe, as developed and tested in our culinary '+
    'center. Any changes from the recipe, such as changes to meet local taste preferences, product ',
    'substitutions or serving size modifications will change the nutrient content of an item, and '
    + 'render the information inaccurate. We hope this information will help you meet your nutritional goals.'
]

def hasNumbers(s):
    return any(char.isdigit() for char in s)

def pairwise(iterable):
    a, b = itertools.tee(iterable)
    next(b, None)
    return zip(a, b) 

def tokenize(token):
    return len(token.strip()) > 0 and not token in phrases

def main():
    if (len(sys.argv) < 2):
        print("Please provide one file to input.")

    with open(sys.argv[1]) as in_file:
        data = in_file.read()

    # Remove new lines
    data = data.split("\n")

    #Clean whitespace and stop phrases
    data = [token for token in filter(tokenize, data)]

    # Find all of the menus starting points
    heads = [i for i in range(len(data)) if 'Bistro 1908' in data[i]]

    # Seperate the data into menus based on the head indices
    menus = [data[sl[0]:sl[1]] for sl in [p for p in pairwise(heads)]]

    out = []    
    # Print the menu
    for m in range(len(menus)):
        out.append([])
        for i in range(len(menus[m])):
            print(menus[m][i])
        break


if __name__ == "__main__":
    main()