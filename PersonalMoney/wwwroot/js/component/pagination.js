
import { createLiCustom } from "../atoms/liTagsCustom.js";
import { pageModel } from "../const/model.js";
export const pageInitial =() => {
    pageModel.pageNum = Number($('#pageNum').val());
    pageModel.size = Number($('#size').val());
    pageModel.totalPage = Number($('#totalPage').val());
    pageModel.search = $('#txtSearch').val();
}


export const createPagination = ()=> {
    let ul = $('<ul></ul>').addClass('pagination_ul')
    let pageCutLow = pageModel.pageNum - 1;
    let pageCutHigh = pageModel.pageNum + 1;
    // Show the Previous button only if you are on a page other than the first
    let liClass = "pagination_li";

    if (pageModel.pageNum > 1) {
        const prevBtn = prevButton();
        prevBtn.appendTo(ul);
    }
    // Show all the pagination elements if there are less than 6 pages total
    if (pageModel.totalPage < 6) {
        for (let p = 1; p <= pageModel.totalPage; p++) {
            liClass = pageModel.pageNum == p ? liClass+" active": liClass;
            const pageBtn = pageButton(liClass,p);
            pageBtn.appendTo(ul);
            liClass = "pagination_li";
        }
    }
    // Use "..." to collapse pages outside of a certain range
    else {
        // Show the very first page followed by a "..." at the beginning of the
        // pagination section (after the Previous button)
        if (pageModel.pageNum > 2) {
            const pageOne = pageButton("no page-item pagination_li",1);
            pageOne.appendTo(ul);
            if (pageModel.pageNum > 3) {
                const pageBtn = pageButton("out-of-range pagination_li","...");
                pageBtn.appendTo(ul);
            }
        }
        if (pageModel.pageNum === 1) {
            pageCutHigh += 2;
        } else if (pageModel.pageNum === 2) {
            pageCutHigh += 1;
        }
        if (pageModel.pageNum === pageModel.totalPage) {
            pageCutLow -= 2;
        } else if (pageModel.pageNum === pageModel.totalPage - 1) {
            pageCutLow -= 1;
        }
        liClass = liClass +" page-item";
        for (let p = pageCutLow; p <= pageCutHigh; p++) {
            if (p === 0) {
                p += 1;
            }
            if (p > pageModel.totalPage) {
                continue
            }
            liClass = pageModel.pageNum == p ? liClass+" active": liClass;
            const pageBtn = pageButton(liClass,p);
            pageBtn.appendTo(ul);
            liClass = "pagination_li";
        }

        if (pageModel.pageNum < pageModel.totalPage - 1) {
            if (pageModel.pageNum < pageModel.totalPage - 2) {
                const pageBtn = pageButton('out-of-range pagination_li', "...");
                pageBtn.appendTo(ul);
            }
            const lastPageBtn = pageButton('page-item no pagination_li',pageModel.totalPage);
            lastPageBtn.appendTo(ul);
        }
    }
    if (pageModel.pageNum < pageModel.totalPage) {
        const nextBtn = nextButton();
        nextBtn.appendTo(ul);
    }
    $('#pagination').empty();
    $('#pagination').append(ul);
}



const createATag = (text,classes)=>{
    const a = $('<a></a>');
    a.addClass(classes)
    a.text(text);
    return a;
}

const pageButton = (classes,page) =>{
    const li = createLiCustom(classes);
    const a = createATag(page,"btn-pagination-page");
    a.appendTo(li);
    return li;
}



const prevButton = ()=>{
    const li = createLiCustom('page-item previous no pagination_li');
    const a = createATag('Previous',"btnPrev");
    a.appendTo(li);
    return li;
}

const nextButton = ()=>{
    const li = createLiCustom('page-item next no pagination_li');
    const a = createATag('Next',"btnNext");
    a.appendTo(li);
    return li;
}