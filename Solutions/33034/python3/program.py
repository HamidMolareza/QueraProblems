# Author: [ahkazemi2007](https://github.com/ahkazemi2007)
# [Related Issue](https://github.com/HamidMolareza/Quera/issues/34)

def coloring(ls):
    alent = len(ls)
    blent = len(ls[0])
    clent = len(ls[0][0])

    for i in range(alent):
        for j in range(blent):
            for k in range(clent):
                if (
                        (i != 0 and i != alent - 1)
                        and (1 <= j <= blent - 2)
                        and (1 <= k <= clent - 2)
                ):
                    ls[i][j][k] = 0
                else:
                    ls[i][j][k] = 1
    return ls
