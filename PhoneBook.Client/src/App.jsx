import Header from './components/Header/Header'
import { useEffect, useState } from 'react'

function App() {

  const [rawData, setRawData] = useState([])


  useEffect(() => {
    console.log("Effect!!!")

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
        
      </main>
    </>
  )
}

export default App
