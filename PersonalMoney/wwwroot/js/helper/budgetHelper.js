import { onPostAdjust, onGetAdjust, onGetData, onPostDistribute } from "../api/budgetPageApi.js";
import { adjustModel, originDistributeModel, distributeModel } from "../const/model.js";
import { getRandomColorWithOpacity } from "../utils/ChartUtils.js";
import { getArrayOfPercen } from "../utils/NumberUtils.js";
import { createStatusMessageBox } from "./popupHelper.js";
import { generateBudgetJunGoalsTableBody, generateChartSpent } from "../component/budgetComponent.js"
import { generateSlider } from "../component/distributeComponent.js";

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

const setAdjustValue = (spendMonth, earnMonth) => {
    console.log(spendMonth)
    $('#txtSpendYear').val(spendMonth * 12);
    $('#txtSaveMonth').val(earnMonth - spendMonth);
}


export const rangeSlider = function () {
    const slider = $('.range-slider'),
        range = $('.range-slider__range'),
        value = $('.range-slider__value');
    const currentValue = [];

    range.each(function () {
        const value = $(this).val();
        currentValue.push(value);
    });

    value.each(function () {
        var value = $(this).prev().attr('value');
        $(this).html(value);

    });

    range.on('input', function () {
        $(this).next(value).html(this.value);
    });
};


export const onInputAdjust = () => {
    const spendMonth = $('#txtSpendMonth').val();
    const earnMonth = $('#txtEarnMonth').val();
    if (Number(earnMonth) < Number(spendMonth)) {
        createStatusMessageBox({success:false, message:"Earning money should be bigger than Spending money"} ,'#set-adjust-body',3000)
    }
    setTimeout(setAdjustValue(spendMonth, earnMonth),300)
    
} 



export const onHandleGetAdjust = async () => {
    await onGetAdjust().then(
        response => {
            if (!response.success) {
                createStatusMessageBox(response, '#set-adjust-body', 5000)
                return;
            }
            if (response.data != null) {
                $('#txtSpendMonth').val(response.data.monthlySpending);
                $('#txtEarnMonth').val(response.data.monthlyEarning);
                $('#txtSaveMonth').val(response.data.monthlySaving);
                $('#txtSpendYear').val(response.data.annuallySpending);
            }
        }
    )
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
    const value = 100* data.totalExpense / (data.budgetExpense > data.totalExpense ? data.budgetExpense : data.totalExpense);
    const arrayProcess = [value,(100-value)];
    const arrayBackGround = [getRandomColorWithOpacity(0.9), getRandomColorWithOpacity(0.9)];
    const arrayLabel = ["Expense",""];

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
// handle fetch data
export const onHandleGetData = async () => {
    await onGetData().then(response => {
        generateBudgetJunGoalsTableBody(response.data);
        originDistributeModel.data = response.data;
        generateSlider(response.data);
    });
}

export const onHandleChangeDate = async () => {
    var pickedDate = $('#reservationDate').val().split('/');
    await onGetData(pickedDate[0], pickedDate[1]).then(response => {
        generateBudgetJunGoalsTableBody(response.data);
        originDistributeModel.data = response.data;
        generateSlider(response.data);
    });
}
const onRefresh = async () => {
    await onHandleGetData();
}

export const onHandleConfirmDistribute = async() => {
    await onPostDistribute(distributeModel).then(
        response => createStatusMessageBox(response, '#distribute-alert', 5000)
    );
    await onRefresh();
}


export const onHandleConfirm = async () => {
    const spendMonth = $('#txtSpendMonth').val();
    const earnMonth = $('#txtEarnMonth').val();
    if (spendMonth.trim() == '' || earnMonth.trim() == '') {
        createStatusMessageBox({ success: false, message: "Data cannot be empty!!" }, '#set-adjust-body', 5000)
        return;
    }
    const model = { ...adjustModel, spend: spendMonth, earn: earnMonth };
    await onPostAdjust(model).then(
        response => createStatusMessageBox(response, '#set-adjust-body', 5000)
    )
    await onRefresh();
}
// refresh
export const onHandleRefreshDistribute = () => {
    generateSlider(originDistributeModel.data);
}
