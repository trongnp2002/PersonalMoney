import { onGetData, onUpdateLockout } from "../api/adminPageApi.js";
import { pageModel } from "../const/model.js";
import { createPagination} from "../component/pagination.js";

export const addOnClickToPage = () => {
    $('.btn-pagination-page').click(function () {
        var text = $(this).text();
        handlerOnClickToPage(text);
    });
    $('.btnNext').click(function(){
        handlerOnClickToPage(pageModel.pageNum+1);
    })
    $('.btnPrev').click(function(){
        handlerOnClickToPage(pageModel.pageNum-1);
    })
    console.log(pageModel.pageNum);
}

export const addOnChangeLockout = () =>{
    $('.admin_switch').on("change",async function(){
        const checkbox = $(this);
        const userId = checkbox.data('userid');
        const isChecked = checkbox.prop('checked'); 
        const data = {id:userId};
        console.log("OnchangeData >>",data)
        await onUpdateLockout(data).then(
            response =>{
                if(response.success){

                }else{
                   
                    setTimeout(()=> checkbox.prop('checked', !isChecked),1000)
                }
            }
        );
    })
}

const handlerOnClickToPage = async (page) => {
    pageModel.pageNum = page;
    onHandleChange();
}

export const onHandleChange = async () => {
    await onGetData(pageModel).then(
        response => {
            if (response.success) {
                console.log(response);
                pageModel.pageNum = response.data.pageNumber;
                pageModel.totalPage = response.data.totalPages;
                reGenerateTableBody(response.data.content);
                createPagination();
                addOnClickToPage();
                addOnChangeLockout();                    
            }
        }
    )
}




const reGenerateTableBody = (data) => {
    console.log(data)
    const tbody = $('#admin_table-body')
    tbody.empty();
    $.each(data, function (index, user) {
        const tr = $('<tr></tr>').addClass("tr-shadow");

        //col image and name

        const imageUrl = user.avatarUrl ? user.avatarUrl : "/images/icon/avatar-big-01.jpg"
        const fullName = `${user.firstName ? user.firstName : ''} ${user.lastName ? user.lastName : ''}`;
        const imageAndName = $('<td></td>');
        const image = `<img class="image img-cir img-40" src=${imageUrl}></img>`;
        imageAndName.append(image);
        imageAndName.append(` &nbsp; ${fullName} `)
        imageAndName.appendTo(tr);
        //col email
        const emailTd = $('<td></td>');
        const emailSpan = $('<span></span>').addClass("block-email").append(user.email);
        emailSpan.appendTo(emailTd);
        emailTd.appendTo(tr);

        //col address
        const address = $('<td></td>').addClass("desc").append(user.address);
        address.appendTo(tr);

        //col lockoutend
        const lockoutEnd = $('<td></td>').append(user.lockoutEnd);
        lockoutEnd.appendTo(tr);

        //col access failed count
        const accessFailedCount = $('<td></td>').addClass("text-center").append(user.accessFailedCount);
        accessFailedCount.appendTo(tr);

        //col switch
        const switchEnable = $('<td></td>');
        switchEnable.append(
            `             
        <label class="switch switch-3d switch-info mr-3">
            <input type="checkbox" class="switch-input admin_switch" data-userid="${user.id}" checked="${user.lockoutEnd}">
            <span class="switch-label"></span>
            <span class="switch-handle"></span>
        </label>`
        )
        switchEnable.appendTo(tr);

        //col action
        const action = $('<td></td>');
        action.append(`
        <div class="table-data-feature">
            <button class="item" data-toggle="tooltip" data-placement="top" title="Send">
                <i class="zmdi zmdi-mail-send"></i>
            </button>
            <button class="item" data-toggle="tooltip" data-placement="top" title="Edit">
                <i class="zmdi zmdi-edit"></i>
            </button>
            <button class="item" data-toggle="tooltip" data-placement="top" title="Delete">
                <i class="zmdi zmdi-delete"></i>
            </button>
            <button class="item" data-toggle="tooltip" data-placement="top" title="More">
                <i class="zmdi zmdi-more"></i>
            </button>
        </div>`
        )
        action.appendTo(tr);
        tr.appendTo(tbody);
    });
 
}