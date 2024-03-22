import { BUDGETPAGE } from "../const/api.js";
import { fetchOnGet, fetchOnPost } from "./fetchInstance.js";
export const onGetData = async () => {
    const getDataUrl = new URL(BUDGETPAGE.GETDATA);
    return await fetchOnGet(getDataUrl)
}