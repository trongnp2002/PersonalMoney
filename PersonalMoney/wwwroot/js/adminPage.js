import { createPagination, pageInitial } from "./component/pagination.js";
import { addOnChangeLockout, addOnClickToPage, onHandleChange } from "./helper/adminHelper.js";
import { pageModel } from "./const/model.js";
$(document).ready(
    function () {
        pageInitial();
        createPagination();
        addOnClickToPage();
        addOnChangeLockout();
        $('#btnSearch').click(() => {
            pageModel.search = $('#txtSearch').val();
            onHandleChange();
        })
    })