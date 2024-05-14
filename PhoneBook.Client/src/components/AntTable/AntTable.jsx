import { Table, Input } from "antd";
import searchIcon from "/IconSearch.png"
import "./AntTable.css"

export default function AntTable({ data }) {

    const filters = data.map(record => {
        return {
            text: record.subdivision,
            value: record.subdivision
        }
    })
    const tableRecords = data.map(record => {
        return {
            ...record,
            key: record.id,
            fullname: `${record.lastname} ${record.firstname} ${record.middlename}`,
            // phonenumbers: phonenumbersToString(record.phonenumbers),
            organization: record.organization
        }
    })


    const colums = [
        {
            title: "ID",
            dataIndex: "id",
            key: "id",
            width: 150
        },
        {
            title: "ФИО",
            dataIndex: "fullname",
            key: "fullname",
            width: 270
        },
        {
            title: "Телефон",
            width: 300,
            dataIndex: "phonenumbers",
            key: "phonenumbers",
            render: (nums) => (
                <ul
                    style={{ listStyle: "none" }}
                >
                    {nums.map((num, index) => {
                        return (
                            <li key={index} style={{ whiteSpace: "nowrap" }}>
                                {`${num.type}: ${num.number}`}
                            </li>
                        )
                    })}
                </ul>),
        },
        {
            title: "Должность",
            width: 250,
            dataIndex: "post",
            key: "post",
        },
        {
            title: "Адрес",
            width: 250,
            dataIndex: "address",
            key: "address",
        },
        {
            title: "Организация",
            width: 250,
            dataIndex: "organization",
            key: "organization",
        },
        {
            title: "Подразделение",
            width: 250,
            dataIndex: "subdivision",
            key: "subdivision",
            filters:  filters,
            onFilter: (value, item) => item.subdivision.includes(value) 
            
        }
    ]
    console.log(filters)

    return (
        <div className="main-container shadow">
            <Input
                className="search"
                prefix={<img src={searchIcon} />}
                inputMode="text"
                placeholder="Введите текс"
                size="large"
            />
            <Table
                className="atable"
                columns={colums}
                pagination={{
                    position: ["bottomRight"],
                    defaultPageSize: '10',
                    showSizeChanger: true,
                    pageSizeOptions: [
                        10, 25, 50, 100
                    ],
                }}
                dataSource={tableRecords}
                scroll={{ x: 800, y: 500 }}
                tableLayout="auto"
                ellipsis={false}
            // size="small"
            />
        </div>
    )
}