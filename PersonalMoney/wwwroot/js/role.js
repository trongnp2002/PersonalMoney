import { onPostCreate, onPostUpdate, onGetData } from "./api/rolePageApi.js";
import { statusMessage } from "./component/statusMessage.js";
import { ClosePopUp } from "./helper/popupHelper.js";
import { roleUpdate } from "./const/model.js";
import { addOnclickForClasses, regenerateTableBody } from "./helper/roleHelper.js";
$(document).ready(
    function () {
        $('#role_btnCreate').on('click', async () => {
            const inputCreate = $('#role_create_inputRoleName');
            const roleName = inputCreate.val();
            await onPostCreate({ name: roleName })
                .then(
                    (data) => {
                        if (data.success) {
                            ClosePopUp('#popupRoleCreate')
                            createStatusMessageBox(data, '#role__table', 5000);
                            reloadPage();
                        } else {
                            createStatusMessageBox(data, '#role_form-create', 5000);
                        }


                    }
                ).finally(() => {
                    inputCreate.val('');
                })
            // .catch(error => console.log(error.message))
        });

        $('#role_btnUpdate').on('click', async () => {
            const inputRoleName = $('#role_update_inputRoleName');
            roleUpdate.name = inputRoleName.val();
            await onPostUpdate(roleUpdate).then(
                (data) => {
                    if (data.success) {
                        ClosePopUp('#popupRoleUpdate')
                        createStatusMessageBox(data, '#role__table', 5000);
                        reloadPage();
                    } else {
                        createStatusMessageBox(data, '#role_form-update', 5000);
                    }
                }
            )
                .finally(() => {
                    roleUpdate.OnRefresh();
                    inputRoleName.val('');
                    console.log(roleUpdate);
                })


        });
        addOnclickForClasses('.role__popup__update')
        const createStatusMessageBox = (data, id, time) => {
            const alertDiv = statusMessage(data.success, data.message)
            $(id).prepend(alertDiv);
            setTimeout(function () {
                alertDiv.remove();
            }, time);
        }
        const reloadPage = () => {
            onGetData().then(response => {
                console.log(response.data); regenerateTableBody(response.data); addOnclickForClasses('.role__popup__update');
            }
            );
        }
    }

)

