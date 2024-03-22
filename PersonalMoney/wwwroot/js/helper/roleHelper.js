
import { buttonTogglePopup } from "../atoms/buttonCustom.js";
import { roleModel,roleUpdate } from "../const/model.js";
export const addOnclickUpdate = (className) => {
    $(className).click(function () {
        const roleId = $(this).data('role-id');
        const roleName = $(this).data('role-name');
        $('#role_update_inputRoleName').val(roleName);
        roleUpdate.id = roleId;
        roleUpdate.name = roleName;
    });
}

export const addOnClickDelete = (className)=>{
    $(className).click(function(){
        const roleName = $(this).data('role-name');
        roleModel.name = roleName;
        $('#popup-delete__model-type').text('Role');
        $('#popup-delete_model-value').text(roleName);
    })
}

export const regenerateTableBody = (data) => {
 
    const tableBody = $('#role__table-body');
    const updateClass = ['btn', 'btn-warning', 'role__popup__update'];
    const deleteClass = ['btn', 'btn-danger','role__popup__delete']
    tableBody.empty();
    $.each(data, (index, element) => {
        const tr = $('<tr></tr>');
        const roleName = $('<td></td>');
        const iconUpdate = $('<i></i>').addClass('fas fa-cogs');
        const iconDelete = $('<i></i>').addClass('fas fa-trash-alt');
        const actionCol = $('<td></td>');
        const dataArrUpdate = {
            'data-role-id': element.id,
            'data-role-name': element.name
        }
        const dataArrDelete={
            'data-role-name': element.name
        }
        const btnUpdate = createButtonWithDataAttr(updateClass, '#popupRoleUpdate', dataArrUpdate);
        const btnDelete = createButtonWithDataAttr(deleteClass, `#popupRoleDelete`,dataArrDelete).css({'margin-left':'3px'});

        roleName.text(element.name);
        roleName.appendTo(tr);

        iconUpdate.appendTo(btnUpdate);
        iconDelete.appendTo(btnDelete)

        btnUpdate.append('&nbsp;Update');
        btnDelete.append('&nbsp;Delete');

        btnUpdate.appendTo(actionCol);
        btnDelete.appendTo(actionCol);
        actionCol.appendTo(tr);
        tr.appendTo(tableBody);
    });
    addOnclickUpdate('.role__popup__update');
    addOnClickDelete('.role__popup__delete');
}


const createButtonWithDataAttr = (classes, dataTarget, dataAttr) => {
    const button = buttonTogglePopup(classes,'' ,dataTarget);
    if (dataAttr) {
        for (const [key, value] of Object.entries(dataAttr)) {
            button.attr(key, value);
          }
    }
    return button;
}
