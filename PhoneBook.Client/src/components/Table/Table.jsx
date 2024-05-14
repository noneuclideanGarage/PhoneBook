import DataTable from "react-data-table-component"
import "./Table.css"
import { colums, tableStyles } from "./tableColums"


export default function Table({ data, isPending, changePendingStatus }) {


    function phonenumbersToString(phonenumbers) {
        let result = ""
        phonenumbers.map(pn => {
            result = result + `${pn.type.toLowerCase()}: ${pn.number}\n`
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

    console.log(changePendingStatus, isPending)
    console.log(tableRecords)

    return (
        <div className="main-container shadow">
            <div className="table-component"></div>
            <DataTable
                style={tableStyles}
                columns={colums}
                fixedHeader
                pagination
                data={tableRecords}
            />
        </div>
    )
}