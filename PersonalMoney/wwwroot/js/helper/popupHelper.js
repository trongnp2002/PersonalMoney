import { statusMessage } from "../component/statusMessage.js";

export const ClosePopUp = (id) => {
    $(id).find('.close').click();

}
export const createStatusMessageBox = (data, id, time) => {
    if ($(id).find('div.alert').length > 0) {
        $(id).find('div.alert').remove();
    }
    const alertDiv = statusMessage(data.success, data.message)
    $(id).prepend(alertDiv);
    setTimeout(function () {
        alertDiv.remove();
    }, time);
}