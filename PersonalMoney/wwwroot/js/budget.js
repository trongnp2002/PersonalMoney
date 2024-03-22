import { generateBudgetJunGoalsTableBody, generateChartSpent } from "./component/budgetComponent.js"
import { onGetData } from "./api/budgetPageApi.js"

$(document).ready(
    function () {
        const onLoadBudgetPage = async ()=>{
            await onGetData().then(response => {
                console.log(response.data);
                generateBudgetJunGoalsTableBody(response.data);
            })
        }
        onLoadBudgetPage();

    }
)