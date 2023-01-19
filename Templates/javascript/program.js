class Solution {
    /**
     * @param {string} name
     */
    solve(name) {
        console.log(`Hello ${name}!`);
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(readline());

    /* Samples:
    const [x1, y1, x2, y2] = readline().split(" ").map(n => +n);
    const A = +readline();
     */
} else {
    //This is for manual test
    solution.solve("Hamid");
    solution.solve("Amir");
}
