import "./SideBar.css"
import pickTable from "/selectedTableSvg.svg"
import unpickTable from "/unselectedTableSvg.svg"
import syncEnable from "/sync-en.svg"
import syncDisable from "/sync-diseb.svg"

export default function SideBar({ changeLocation, currentLocation }) {

    return (
        <aside>
            <div className="sidebar-item" onClick={() => changeLocation("table")}>
                <img src={
                    currentLocation === "table"
                        ? pickTable
                        : unpickTable} alt="table" />
            </div>
            <div className="sidebar-item" onClick={() => changeLocation("sync")}>
                <img src={
                    currentLocation === "sync"
                        ? syncEnable
                        : syncDisable} alt="sync" />
            </div>
        </aside>
    )
}