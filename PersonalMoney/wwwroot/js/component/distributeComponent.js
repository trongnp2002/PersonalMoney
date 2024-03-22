import { distributeModel, distributeElement } from "../const/model.js";
import { emptyArray } from "../utils/NumberUtils.js";
import { distributeRange } from "./rangeSlider.js";

const currentValue = [...distributeModel.distributeList];
var maxValue = 0;
const rangeSlider = function () {
    const rangeSlider = function () {
        const slider = $('.range-slider'),
            range = $('.range-slider__range'),
            value = $('.range-slider__value');

        getCurrentValue();
        let currMax = updateMaxValue();
        value.each(function () {
            var value = $(this).prev().attr('value');
            $(this).html(value);
        });

        range.on('input', function () {
            const budget = $(this).val();
            const id = $(this).data('distribute-id');
            const model = { ...distributeElement, id, budget }

            currMax = updateMaxValue();
            if (currMax < 0) {
                let newBudget = findById(id).budget;
                $(this).val(newBudget ? newBudget : 0);
                return;
            }
            $(this).next(value).html(this.value);
            getCurrentValue();
            updateModel(model);


        });
    };

    rangeSlider();
};

const findById = (id) => {
    let data = {};
    currentValue.forEach((element, index) => {
        if (element.id === id) {
            data = element;
        }
    })
    return data;
}

const updateMaxValue = () => {
    const range = $('.range-slider__range');
    let currMax = maxValue;
    range.each(function () {
        const value = $(this).val();
        currMax -= value;
    });
    return currMax;
}

const updateModel = (model) => {
    let isUpdate = false;
    distributeModel.distributeList.forEach((element, index) => {
        if (element.id == model.id) {
            element.budget = model.budget;
            isUpdate = true;
        }
    })
    if (isUpdate === false) {
        distributeModel.distributeList.push(model);
    }
}

/*const isAsc = (model) => {
    let flag = false
    currentValue.forEach((element, index) => {
        console.group();
        console.log('element', element);
        console.log('model', model)
        console.groupEnd();
        if (element.id == model.id) {
            if (element.budget < model.budget) {
                flag = true;
            }
            
        }
    })
    console.log('flag', flag);
    return flag;
}*/

const getCurrentValue = () => {
    emptyArray(currentValue);
    const range = $('.range-slider__range');
    range.each(function () {
        const id = $(this).data('distribute-id');
        const model = { ...distributeElement, id, budget: $(this).val() }
        currentValue.push(model);
    });
}

export const generateSlider = (data) => {
    const processArray = data.processBudgets;
    const max = data.budgetExpense;
    maxValue = max;
    const body = $('#distribute-body');
    body.empty();
    $('#distribute-max').text(`Spending in month: $${max}`);
    $.each(processArray, (index, element) => {
        body.append(distributeRange(element, max));
    });
    rangeSlider();
}