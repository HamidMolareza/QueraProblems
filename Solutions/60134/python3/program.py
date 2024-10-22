# https://quera.org/problemset/60134/

def fruits(tuple_of_fruits):
    # Filter fruits based on conditions
    filtered_fruits = [fruit['name'] for fruit in tuple_of_fruits if
                       fruit['shape'] == 'sphere' and 300 <= fruit['mass'] <= 600 and 100 <= fruit['volume'] <= 500]

    # Group fruits based on name
    result = {fruit: filtered_fruits.count(fruit) for fruit in filtered_fruits}
    return result
