class Solution {
    solve(label) {
        console.log(this.isDangerous(label) === false ? "rahat baash" : "nakhor lite");
    }

    isDangerous(label) {
        let items = this.countLabels(label);
        if (items.numOfReds >= 3 || items.numOfGreen <= 0)
            return true;
        return items.numOfReds >= 2 && items.numOfYellow >= 2;
    }

    countLabels(label) {
        const result = {
            numOfReds: 0,
            numOfYellow: 0,
            numOfGreen: 0
        }

        label = label.toLowerCase();
        for (let i = 0; i < label.length; i++) {
            switch (label[i]) {
                case 'r':
                    result.numOfReds++;
                    break;
                case 'g':
                    result.numOfGreen++;
                    break;
                case 'y':
                    result.numOfYellow++;
                    break;
                default:
                    throw new Error(`${item} not supported.`);
            }
        }
        return result;
    }
}

let solution = new Solution();
if (typeof readline === 'function') {
    // This is for Quera judge
    solution.solve(readline());
} else {
    //This is for manual test
    solution.solve("GGGGG");
    solution.solve("RYRYR");
}
