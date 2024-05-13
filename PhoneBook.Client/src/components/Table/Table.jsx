import DataTable from "react-data-table-component"
import "./Table.css"
import { colums } from "./tableData"

export default function Table({data}) {

    console.log(data)
    // const tableData = data.map(record => {
    //     return {
    //         ...record,
    //         fullname: `${record.lastname} ${record.firstname} ${record.middlename}`,
    //     }
    // })

    return (
            <div className="table-container">

                <DataTable columns={colums} fixedHeader/>
            </div>
    )
}