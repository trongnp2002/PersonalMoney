import { getDataModelBudget, getDataModelProcess, getDataModelSpent } from "../helper/budgetHelper.js";
import { formatDateUSText } from "../utils/dateUtils.js";
import { pieChart, polarChart, doughutChart } from "./chartCustom.js";



const budgetJunGoalsTableBodyElement = (goal) =>{
    const total = goal?.total > 0 ? goal?.total : 0;
    const bud = goal?.budget > 0 ? goal?.budget : total > 0 ? total : 1;
    const process = 100 * (total / bud);
    return `<tr>
        <td class="px-3">
            <p class="p-0 text-dark font-weight-bold">${goal?.name}</p>
            <span class="text-black-50 text-opacity-25">Update ${formatDateUSText(goal?.lastUpdate)}</span>
        </td>
   
        <td class="px-2">
            <p class="text-dark font-weight-bold">$${goal.total}</p>
            <span>${goal.isIncome ? "Spent" : "Expense"}</span>
        </td>
        <td class="px-2">
            <p class="text-dark font-weight-bold">$${goal.budget}</p>
            <span>Set Goal</span>
        </td>
        <td class="px-2">
            <p class="text-dark font-weight-bold">${goal.count}</p>
            <span>Transactions</span>
        </td>
        <td class="px-3" style="min-width:150px">
            <div class=" d-flex justify-content-between">
                <p class="float-right d-inline m-r-10">Progress</p>
                <p class="float-left d-inline ">
                    $${goal.total}/${goal.budget}
                </p>
            </div>
            <div class="progress">
                <div class=" progress-bar ${process < 25 ? 'bg-success' : process < 50 ? 'bg-primary' : process < 75 ? 'bg-warning':'bg-danger'} progress-bar-striped progress-bar-animated" role="progressbar" style="width: ${process.toFixed(3)}%" ) aria-valuenow="25"
                     aria-valuemin="0" aria-valuemax="100">
                    ${process.toString().includes('.') ? process.toFixed(1) : process}%
                </div>
            </div>
        </td>
    </tr>`
}


export const generateBudgetJunGoalsTableBody = (data) =>{
    const table = $('#budget-process__table');
    table.empty();
    $.each(data.processBudgets, (index, element) => {
        const row = budgetJunGoalsTableBodyElement(element);
        table.append(row);
    });
    generateChartBudget(data.processBudgets);
    generateChartProcess(data.processBudgets);
    generateChartSpent(data)
    const report = $("#budget_status_report");
    report.empty();
    report.append(`<b>$${data.totalExpense}</b> out of <b>$${data.budgetExpense}</b>`);
}

export const generateChartBudget = (data) => {
    const div = $('#budget_chart_div');
    div.empty();
    const canva = $('<canvas></canvas>')
    canva.attr('id', 'budget_chart');
    div.append(canva);
    const model = getDataModelBudget(data);
    pieChart("budget_chart", 273, model);
}


export const generateChartProcess = (data) => {
    const div = $('#budget-process-chart-div');
    div.empty();
    const canva = $('<canvas></canvas>')
    canva.attr('id', 'budget-process-chart');
    div.append(canva);
    const model = getDataModelProcess(data);
    console.log("process| ", model)

    polarChart("budget-process-chart", 273, model);
}

export const generateChartSpent = (data) => {
    const div = $('#budget-process__spent-div');
    div.empty();
    const canva = $('<canvas></canvas>')
    canva.attr('id', 'budget-process__spent');
    div.append(canva);
    const model = getDataModelSpent(data);
    console.log("spent| ", model)
    doughutChart("budget-process__spent",333,model);
}