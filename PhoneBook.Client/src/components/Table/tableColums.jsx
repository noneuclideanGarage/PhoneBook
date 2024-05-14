const colums = [
    {
        name: "#",
        selector: row => row.id,
        sortable: true
    },
    {
        name: "ФИО",
        selector: row => row.fullname,
        sortable: true
    },
    {
        name: "Телефон",
        selector: row => row.phonenumbers,
        sortable: false
    },
    {
        name: "Должность",
        selector: row => row.post,
        sortable: true
    },
    {
        name: "Адрес",
        selector: row => row.address,
        sortable: false
    },
    {
        name: "Организация",
        selector: row => row.organization,
        sortable: true
    },
    {
        name: "Подразделение",
        selector: row => row.subdivision,
        sortable: true
    }
]

const tableStyles = {
    row: {
        style: {
            minHeight: "48px",
            minWidth: "140px"
        }
    }
}

export {
    colums,
    tableStyles
}