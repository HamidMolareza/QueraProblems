class Solution {
    /**
     * @param {string} str
     */
    solve(str) {
        console.log(str);
        for (let i = 1; i < str.length; i++)
            console.log(`${str[i].repeat(i + 1)}${str.substring(i + 1, str.length)}`)
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(readline());
} else {
    //This is for manual test
    solution.solve("golabi");
    solution.solve("codecup");
}
