package main

import (
	"fmt"
	"sort"
)

func main() {
	// Input the size of the arrays
	var n int
	fmt.Scan(&n)

	// Input the first array of integers
	n1 := make([]int, n)
	for i := 0; i < n; i++ {
		fmt.Scan(&n1[i])
	}

	// Input the second array of integers
	n2 := make([]int, n)
	for i := 0; i < n; i++ {
		fmt.Scan(&n2[i])
	}

	// Use a slice to store elements based on the condition
	var m []int
	for i := 0; i < n; i++ {
		if n2[i] == 1 {
			m = append(m, n1[i])
		}
	}

	// Sort the slice
	sort.Ints(m)

	// Print the elements separated by a space
	for _, t := range m {
		fmt.Print(t, " ")
	}
}
