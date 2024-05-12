import Header from './components/Header'
import { useEffect, useState } from 'react'

function App() {

  const [rawData, setRawData] = useState([])

  // console.log(rawData)

  useEffect(() => {
    console.log("Effect!!!")

    async function sendReq() {
      const res = await fetch("http://localhost:5888/api/phonebook")
      const data = await res.json()
      console.log("Data:")
      setRawData(data)
    }
    sendReq()
  }, [])

  console.log(rawData)

  return (
    <header>
      <Header />
    </header>
  )
}

export default App
