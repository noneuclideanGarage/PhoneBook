import "./SideBar.css"
import pickTable from "/selectedTableSvg.svg"
import unpickTable from "/unselectedTableSvg.svg"

export default function SideBar({ changeLocation, currentLocation }) {

    return (
        <aside>
            <div className="sidebar-item" onClick={() => changeLocation("table")}>
                <img src={
                    currentLocation === "table"
                        ? pickTable
                        : unpickTable} alt="" />
            </div>
        </aside>
    )
}