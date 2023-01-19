class Solution {
    /**
     * @param {number} x1
     * @param {number} y1
     * @param {number} x2
     * @param {number} y2
     */
    solve(x1, y1, x2, y2) {
        if (x1 === x2)
            console.log("Vertical");
        else if (y1 === y2)
            console.log("Horizontal");
        else
            console.log("Try again");
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    const [x1, y1, x2, y2] = readline().split(" ").map(n => +n);
    solution.solve(x1, y1, x2, y2);
} else {
    //This is for manual test
    solution.solve(1, 1, 2, 1);
    solution.solve(2, 3, 2, 8);
    solution.solve(1, 2, 5, 3);
}
