class Solution {
    solve(name) {
        console.log(`Hello ${name}!`);
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(readline());
} else {
    //This is for manual test
    solution.solve("Hamid");
    solution.solve("Amir");
}
