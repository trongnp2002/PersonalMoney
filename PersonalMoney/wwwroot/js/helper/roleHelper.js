
import { buttonTogglePopup } from "../atoms/buttonCustom.js";
import { roleUpdate } from "../const/model.js";
export const addOnclickForClasses = (className) => {
    $(className).click(function () {
        const roleId = $(this).data('role-id');
        const roleName = $(this).data('role-name');
        $('#role_update_inputRoleName').val(roleName);
        roleUpdate.id = roleId;
        roleUpdate.name = roleName;
    });
}

export const regenerateTableBody = (data) => {
 
    const tableBody = $('#role__table-body');
    const updateClass = ['btn', 'btn-warning', 'role__popup__update'];
    const deleteClass = ['btn', 'btn-danger']
    tableBody.empty();
    $.each(data, (index, element) => {
        const tr = $('<tr></tr>');
        const roleName = $('<td></td>');
        const iconUpdate = $('<i></i>').addClass('fas fa-cogs');
        const iconDelete = $('<i></i>').addClass('fas fa-trash-alt');
        const actionCol = $('<td></td>');
        const dataArr = {
            'data-role-id': element.id,
            'data-role-name': element.name
        }
        const btnUpdate = createButtonWithDataAttr(updateClass, '#popupRoleUpdate', dataArr);
        const btnDelete = createButtonWithDataAttr(deleteClass, `#deleteModel${element.id}`).css({'margin-left':'3px'});

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
