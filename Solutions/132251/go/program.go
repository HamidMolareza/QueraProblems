package main

import (
    "bufio"
    "fmt"
    "os"
    "strings"
)

func getInputs(numOfLines int) []string {
    inputs := make([]string, numOfLines)
    scanner := bufio.NewScanner(os.Stdin)
    for i := 0; i < numOfLines; i++ {
        scanner.Scan()
        inputs[i] = scanner.Text()
    }
    return inputs
}

func main() {
    // Get inputs
    const NUM_OF_INPUTS = 5
    inputs := getInputs(NUM_OF_INPUTS)

    // Find indexes
    indexes := []int{}
    for i := 0; i < NUM_OF_INPUTS; i++ {
        if (strings.Contains(inputs[i], "FBI")) {
            indexes = append(indexes, i + 1)
        }
    }

    // Print
    if len(indexes) > 0 {
        for i := 0; i < len(indexes); i++ {
            fmt.Printf("%d ", indexes[i])
        }
    } else {
        fmt.Println("HE GOT AWAY!")
    }
}
