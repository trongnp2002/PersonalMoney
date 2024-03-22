"use strict";
import { onPostCreate, onPostUpdate, onGetData, onPostDelete } from "./api/rolePageApi.js";
import { statusMessage } from "./component/statusMessage.js";
import { ClosePopUp } from "./helper/popupHelper.js";
import { roleUpdate, roleModel } from "./const/model.js";
import { addOnclickUpdate, regenerateTableBody, addOnClickDelete } from "./helper/roleHelper.js";
import { pageInitial } from "./component/pagination.js";
import { wsServer } from "./const/socket-api.js";
$(document).ready(
    function () {
        wsServer.on("ReloadRoles", async () => {
            await reloadPage();
        });


        $('#role_btnCreate').on('click', async () => {
            const inputCreate = $('#role_create_inputRoleName');
            const roleName = inputCreate.val();
            await onPostCreate({ name: roleName })
                .then(
                     (data) => {
                        if (data.success) {
                            ClosePopUp('#popupRoleCreate')
                            createStatusMessageBox(data, '#role__table', 5000);
                        } else {
                            createStatusMessageBox(data, '#role_form-create', 5000);
                        }
                    }
                ).finally( () => {
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
                        roleUpdate.OnRefresh();
                    } else {
                        createStatusMessageBox(data, '#role_form-update', 5000);
                    }
                }
            )
                .finally( () => {
                    inputRoleName.val('');
                })


        });

        $('#role_btnDelete').on('click', async () => {
            await onPostDelete(roleModel)
                .then( (data) => {
                    ClosePopUp('#popupRoleDelete');
                    createStatusMessageBox(data, '#role__table', 5000);
                }
                ).finally(() => roleModel.OnRefresh());
        });

        const createStatusMessageBox = (data, id, time) => {
            if ($(id).find('div.alert').length > 0) {
                $(id).find('div.alert').remove();
            }
            const alertDiv = statusMessage(data.success, data.message)
            $(id).prepend(alertDiv);
            setTimeout(function () {
                alertDiv.remove();
            }, time);
        }
        const reloadPage = async () => {
            $('#role__loader').removeClass('loader--hidden');
            await onGetData().then(response => {
                regenerateTableBody(response.data);       
            }
            ).finally(() => {
                setTimeout(()=>$('#role__loader').addClass('loader--hidden'),300);
            });
        }

        const onLoadPage = () =>{
            addOnclickUpdate('.role__popup__update');
            addOnClickDelete('.role__popup__delete');
            pageInitial();
        }
        onLoadPage();
    }

)

