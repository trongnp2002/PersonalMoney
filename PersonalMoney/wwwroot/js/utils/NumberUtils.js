export const getRandomNumber = (min, max) => {
    return Math.random() * (max - min) + min;
}

export const getArrayOfPercen = (data) => {
    const total = data.reduce((tol, num) => tol + num, 0);
    const newArray = [];
    data.forEach((value) => {
        newArray.push(100*value / (total==0?1:total));
    })
    return newArray;
}

export const emptyArray = (array) => {
    while (array.length > 0) {
        array.pop();
    }
}