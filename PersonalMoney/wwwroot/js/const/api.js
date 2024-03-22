const host = "https://localhost:7015/"
export const ROLEPAGE = {
    GETDATA:`${host}admin/roles?handler=Data`,
    POSTCREATE: `${host}admin/roles/?handler=Create`,
    POSTUPDATE: `${host}admin/roles/?handler=Update`,
    POSTDELETE: `${host}admin/roles/?handler=Delete`,
}

export const ADMINPAGE = {
    GETDATA: `${host}admin/?handler=Data`,
    POSTUPDATELOCKOUT: `${host}admin/?handler=UpdateLockout`,
}

export const BUDGETPAGE = {
    GETDATA: `${host}budget/index/?handler=Data`,
    ADJUST: `${host}budget/index/?handler=Adjust`,
    DISTRIBUTE: `${host}budget/index/?handler=Distribute`,
}