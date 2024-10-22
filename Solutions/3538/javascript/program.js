class Solution {
    /**
     * @param {string[][]} input
     * @return {void}
     */
    solve(input) {
        let allBadDays = input.reduce((badDays, list) => new Set([...badDays, ...list]));
        let numOfGoodDays = this.countGoodDays([...allBadDays]);
        console.log(numOfGoodDays)
    }

    /**
     * @param {string[]} badDays
     * @return {number}
     */
    countGoodDays(badDays) {
        if (badDays.length >= 7) return 0;
        let goodDays = ["shanbe", "1shanbe", "2shanbe", "3shanbe", "4shanbe", "5shanbe", "jome"]
            .filter(day => !badDays.includes(day));
        return goodDays.length;
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    const numOfPersons = 3;
    let badDays = [];
    for (let i = 0; i < numOfPersons; i++)
        badDays.push(getPersonDays());
    solution.solve(badDays);
} else {
    //This is for manual test
    solution.solve([["shanbe", "1shanbe", "2shanbe", "3shanbe"], ["5shanbe"], ["1shanbe", "3shanbe", "5shanbe"]]);
    solution.solve([["shanbe", "2shanbe"], ["1shanbe", "3shanbe"], ["jome", "5shanbe", "4shanbe"]]);
}

function getPersonDays() {
    readline(); //skip
    return readline().split(" ");
}
