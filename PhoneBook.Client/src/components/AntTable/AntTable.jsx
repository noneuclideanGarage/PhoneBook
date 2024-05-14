import { Table } from "antd";


export default function AntTable({ data }) {

    const colums = [
        {
            title: "#",
            dataIndex: "id",
            key: "id",
        },
        {
            title: "ФИО",
            dataIndex: "fullname",
            key: "fullname",
        },
        {
            title: "Телефон",
            width: 300,
            dataIndex: "phonenumbers",
            key: "phonenumbers",


        },
        {
            title: "Должность",
            dataIndex: "post",
            key: "post",
        },
        {
            title: "Адрес",
            dataIndex: "address",
            key: "address",

        },
        {
            title: "Организация",
            dataIndex: "organization",
            key: "organization",
        },
        {
            title: "Подразделение",
            dataIndex: "subdivision",
            key: "subdivision",
        }
    ]



    function phonenumbersToString(phonenumbers) {
        let result = []
        phonenumbers.map(pn => {
            result.push(`${pn.type.toLowerCase()}: ${pn.number}\n`)
        })

        console.log(result)
        return result
    }

    const tableRecords = data.map(record => {
        return {
            ...record,
            fullname: `${record.lastname} ${record.firstname} ${record.middlename}`,
            phonenumbers: phonenumbersToString(record.phonenumbers),
            organization: record.organization
        }
    })



    return (
        <div className="main-container shadow">
            {/* <label>
                <input type="text" placeholder="Search"/>
            </label> */}
            <Table
                columns={colums}
                pagination={{ position: "top" }}
                dataSource={tableRecords}
                scroll={{ x: 100, y: 650 }}
            />
        </div>
    )
}