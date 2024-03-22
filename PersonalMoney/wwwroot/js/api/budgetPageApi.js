import { BUDGETPAGE } from "../const/api.js";
import { fetchOnGet, fetchOnPost } from "./fetchInstance.js";
export const onGetData = async (month = '', year = '') => {
    const getDataUrl = new URL(BUDGETPAGE.GETDATA);
    getDataUrl.searchParams.append('month', month);
    getDataUrl.searchParams.append('year', year);
    return await fetchOnGet(getDataUrl)
}

export const onPostAdjust = async (data) => {
    const postDataUrl = new URL(BUDGETPAGE.ADJUST);
    return await fetchOnPost(postDataUrl, data);
}
export const onGetAdjust = async () => {
    const postDataUrl = new URL(BUDGETPAGE.ADJUST);
    return await fetchOnGet(postDataUrl);
}
export const onPostDistribute = async (data) => {
    const postDataUrl = new URL(BUDGETPAGE.DISTRIBUTE);
    return await fetchOnPost(postDataUrl, data);
}