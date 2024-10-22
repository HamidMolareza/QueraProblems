class Solution {
    /**
     * @param {number} numOfRepeat
     */
    solve(numOfRepeat) {
        let text = "man khoshghlab hastam\n";
        const result = text.repeat(numOfRepeat);
        console.log(result);
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(parseInt(readline()));
} else {
    //This is for manual test
    solution.solve(3);
}
