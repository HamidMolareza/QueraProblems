// https://quera.org/problemset/134354/

function timeit(fn) {
    const timeNow = () => new Date().getTime();

    return function (...args) {
        const start = timeNow();

        return new Promise((res, rej) => {
            Promise.resolve(fn(...args))
                .then((value) =>
                    res({value, time: timeNow() - start})
                ).catch(rej);
        });
    };
}

function fn(t) {
    return new Promise((res, _) => {
        setTimeout(() => res(`done after ${t}ms`), t);
    });
}
timeit(fn)(25).then(ans => {
    // ans === {value: "done after 25ms", time: 25}
    console.log(ans);
})


// const fn = (a, b) => a + b;
// timeit(fn)(5, 10).then(ans => {
//     // ans === {value: 15, time: 500} // true
//     console.log(ans);
// })
