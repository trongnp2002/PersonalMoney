import { getRandomColorWithOpacity } from "../utils/ChartUtils.js";
import { getArrayOfPercen } from "../utils/NumberUtils.js";

const chartDataModel = {
    datasets: [{
        data: [],
        backgroundColor: [

        ],
        hoverBackgroundColor: [

        ]

    }],
    labels: []
}

export const getDataModelBudget = (data) => {
    const arrayBudget = [];
    const arrayBackGround = [];
    const arrayLabel = [];
    $.each(data, (index, value) => {
        arrayBudget.push(value.budget);
        arrayBackGround.push(getRandomColorWithOpacity(0.9));
        arrayLabel.push(value.name);
    })
    const vl = {
        data: getArrayOfPercen(arrayBudget),
        backgroundColor: arrayBackGround,
        hoverBackgroundColor: arrayBackGround,
    }
    return {
        ...chartDataModel,
        datasets: [vl],
        labels: arrayLabel,
    };
}

export const getDataModelProcess = (data) => {
    const arrayProcess = [];
    const arrayBackGround = [];
    const arrayLabel = [];
    $.each(data, (index, value) => {
        arrayProcess.push(value.total);
        arrayBackGround.push(getRandomColorWithOpacity(0.9));
        arrayLabel.push(value.name);
    })
    const vl = {
        data: getArrayOfPercen(arrayProcess),
        backgroundColor: arrayBackGround,
        hoverBackgroundColor: arrayBackGround,
    }
    return {
        ...chartDataModel,
        datasets: [vl],
        labels: arrayLabel,
    };
}

export const getDataModelSpent = (data) => {
    const value = data.totalExpense / (data.budgetExpense > data.totalExpense ? data.budgetExpense : data.totalExpense);
    const arrayProcess = [value];
    const arrayBackGround = [getRandomColorWithOpacity(0.9)];
    const arrayLabel = ["Expense"];

    const vl = {
        data: arrayProcess,
        backgroundColor: arrayBackGround,
        hoverBackgroundColor: arrayBackGround,
    }
    return {
        ...chartDataModel,
        datasets: [vl],
        labels: arrayLabel,
    };
}
