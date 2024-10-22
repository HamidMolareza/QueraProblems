class Solution {
    /**
     * @param {number} order
     */
    solve(order) {
        for (let r = 1; r <= order; r++) {
            let row = [];
            for (let c = 1; c <= order; c++)
                row.push(r * c);
            console.log(row.join(" "));
        }
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(+readline());
} else {
    // solution.solve(1);
    solution.solve(10);
}
