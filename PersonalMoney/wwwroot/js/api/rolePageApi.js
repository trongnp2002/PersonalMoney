import { fetchOnGet, fetchOnPost } from "./fetchInstance.js";
import { ROLEPAGE } from "../const/api.js";
export const onPostCreate = async (data)=>{
    const postUrl = new URL(ROLEPAGE.POSTCREATE);
    return await fetchOnPost(postUrl,data);
}
export const onPostUpdate = async(data)=>{
    const updateUrl = new URL(ROLEPAGE.POSTUPDATE);
    return await fetchOnPost(updateUrl,data);
}

export const onGetData = async()=>{
    const getDataUrl = new URL(ROLEPAGE.GETDATA);
    return await fetchOnGet(getDataUrl)
}

export const onPostDelete = async(data) =>{
    const deleteUrl = new URL(ROLEPAGE.POSTDELETE);
    return await fetchOnPost(deleteUrl,data);
}


