import {
    onInputAdjust, onHandleConfirm, onHandleChangeDate,
    onHandleGetAdjust, onHandleGetData,
    onHandleConfirmDistribute, onHandleRefreshDistribute
} from "./helper/budgetHelper.js";

$(document).ready(
    function () {
        const onLoadBudgetPage = onHandleGetData
        onLoadBudgetPage();

        $('#txtSpendMonth').on('input',onInputAdjust);
        $('#txtEarnMonth').on('input', onInputAdjust);


        $('#btnAdjustConfirm').click(onHandleConfirm);
        $('#btnAdjust').click(onHandleGetAdjust);
        $('#btnDistribute').click(onHandleRefreshDistribute);
        $('#btnDistributeConfirm').click(onHandleConfirmDistribute);

        $('.datepicker').datepicker({
            clearBtn: true,
            format: "mm/yyyy",
            minViewMode: "months"

        });


        // FOR DEMO PURPOSE
        $('#reservationDate').on('change', onHandleChangeDate);
      
    }
)