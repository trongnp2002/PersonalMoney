
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

export const adjustModel = {
    spend: 0,
    earn:0,
}

export const distributeModel = {
    distributeList:[],
}
export const distributeElement = {
    id: '',
    budget: 0,
};
export const originDistributeModel = {
    data: {}
};