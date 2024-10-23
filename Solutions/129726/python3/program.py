def separator(ls):
    even_list = []
    odd_list = []
    for i in range(len(ls)):
        if ls[i] % 2 == 0:
            even_list.append(ls[i])
        else:
            odd_list.append(ls[i])
    return even_list, odd_list
