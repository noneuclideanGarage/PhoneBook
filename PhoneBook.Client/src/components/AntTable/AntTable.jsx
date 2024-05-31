import { useEffect, useState } from "react";
import { Table, Input } from "antd";
import searchIcon from "/IconSearch.png"
import "./AntTable.css"

export default function AntTable({ data }) {

    const [searchValue, setSearchValue] = useState('')
    const [searchResult, setSearchResult] = useState()
    const [filters, setFilters] = useState([])

    useEffect(() => {
        const rawFilters = data.map(record => {
            return {
                text: record.subdivision,
                value: record.subdivision
            }
        })

        setFilters(rawFilters.filter(filter => filter.value))

        setSearchResult(mappingRecords(data))
    }, [data])


    useEffect(() => {

        if (searchValue.length === 0) {
            setSearchResult(mappingRecords(data))
        } else {
            setSearchResult(prev => {
                const filteredData = prev.filter(row => {
                    const searchTerms = searchValue.toLowerCase().split(' ');
                    return searchTerms.some(term => {
                        return (
                            row.fullname.toLowerCase().includes(term) ||
                            row.post.toLowerCase().includes(term) ||
                            row.address.toLowerCase().includes(term) ||
                            row.organization.toLowerCase().includes(term) ||
                            row.subdivision.toLowerCase().includes(term) ||
                            row.email.toLowerCase().includes(term)
                        );
                    });
                })

                return filteredData
            })
        }
    }, [data, searchValue])

    function mappingRecords(raw) {
        return raw.map(record => {
            return {
                ...record,
                key: record.id,
                fullname: `${record.lastname} ${record.firstname} ${record.middlename}`,
            }
        })
    }

    const colums = [
        {
            title: "ФИО",
            dataIndex: "fullname",
            key: "fullname",
            width: 300
        },
        {
            title: "Телефон",
            width: 350,
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
            title: "Email",
            width: 250,
            dataIndex: "email",
            key: "email"
        },
        {
            title: "Должность",
            width: 300,
            dataIndex: "post",
            key: "post",
        },
        {
            title: "Адрес",
            width: 300,
            dataIndex: "address",
            key: "address",
        },
        {
            title: "Организация",
            width: 300,
            dataIndex: "organization",
            key: "organization",
        },
        {
            title: "Подразделение",
            width: 300,
            dataIndex: "subdivision",
            key: "subdivision",
            filters: filters,
            onFilter: (value, item) => item.subdivision.includes(value)

        }
    ]

    return (
        <div className="main-container shadow">
            <Input
                className="search"
                prefix={<img src={searchIcon} />}
                inputMode="text"
                placeholder="Введите текс"
                size="large"
                value={searchValue}
                onChange={(e) => setSearchValue(e.target.value)}
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
                dataSource={searchResult}
                scroll={{ x: 800, y: 500 }}
                // tableLayout="auto"
                ellipsis={false}
            // size="small"
            />
        </div>
    )
}