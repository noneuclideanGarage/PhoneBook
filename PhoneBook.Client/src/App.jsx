import Header from './components/Header/Header'
import { useEffect, useState } from 'react'
import SideBar from './components/SideBar/SideBar'
// import Table from './components/Table/Table'
import AntTable from './components/AntTable/AntTable'

function App() {

  const [rawData, setRawData] = useState([])
  const [location, setLocation] = useState("table")
  // const [pending, SetPending] = useState(true)

  useEffect(() => {
    async function sendReq() {
      const res = await fetch("http://localhost:5888/api/phonebook")
      if (res.status !== 200) {
        // console.log("Status code: ",res.status)
        console.log("Server doesn't respond")
      } else {
        const data = await res.json()
        setRawData(data)
      }
    }
    sendReq()
  }, [])

  console.log(rawData)

  return (
    <>
      <Header />
      <main>
        <SideBar changeLocation={setLocation} currentLocation={location} />
        <AntTable data={rawData}/>
        {/* <Table data={rawData} isPending={pending} changePendingStatus={SetPending}/> */}
      </main>
    </>
  )
}

export default App
