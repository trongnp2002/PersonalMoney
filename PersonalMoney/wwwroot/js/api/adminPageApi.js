import { fetchOnGet, fetchOnPost } from "./fetchInstance.js";
import { ADMINPAGE } from "../const/api.js";
export const onGetData = async ({ search, pageNum, size }) => {
    const getDataUrl = new URL(ADMINPAGE.GETDATA);
    getDataUrl.searchParams.append('search', search);
    getDataUrl.searchParams.append('pageNum', pageNum);
    getDataUrl.searchParams.append('size', size);
    return await fetchOnGet(getDataUrl)
}

export const onUpdateLockout = async(data) =>{
    const updateLockoutUrl = new URL(ADMINPAGE.POSTUPDATELOCKOUT);
    console.log("OnpostData>>>", data);
    return await fetchOnPost(updateLockoutUrl,data);
}
