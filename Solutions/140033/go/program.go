package main

import (
    "fmt"
    "strings"
)

func main() {
    var input string
    fmt.Scanln(&input)

    vowelCount := 0
    vowels := "aeiou"
    for _, char := range input {
        if strings.Contains(vowels, string(char)) {
            vowelCount++
        }
    }

    fmt.Println(vowelCount)
}
