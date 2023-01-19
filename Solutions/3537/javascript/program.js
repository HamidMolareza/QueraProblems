class Solution {
    /**
     * @param {number} number
     */
    solve(number) {
        let result = "W" + "o".repeat(number) + "w!";
        console.log(result);
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(parseInt(readline()));
} else {
    //This is for manual test
    solution.solve(1);
    solution.solve(4);
}
