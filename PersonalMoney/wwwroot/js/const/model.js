
export const roleUpdate = {
    id: '',
    name: '',

    OnRefresh() {
        this.id = '';
        this.name = '';
    }
}

export const updateModel = {
    id: '',
    value: {},
    OnRefresh() {
        this.id = '';
        this.value = {};
    }
}

export const roleModel = {
    name: '',
    OnRefresh() {
        this.name = '';
    }
}

export const pageModel = {
    pageNum: 1,
    totalPage: 1,
    size: 10,
    search: '',
}

