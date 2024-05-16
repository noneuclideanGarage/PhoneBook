import Header from './components/Header/Header'
import { useEffect, useState } from 'react'
import SideBar from './components/SideBar/SideBar'
import AntTable from './components/AntTable/AntTable'
import { Col, Row, Layout } from 'antd';
import SyncJson from './components/Sync/SyncJson/SyncJson';

const { Sider, Content } = Layout;

function App() {

  const [rawData, setRawData] = useState(() => [])
  const [location, setLocation] = useState("table")
  const [user, setUser] = useState({
    username: "",
    token: "",
    isAuth: false
  })

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

    const username = localStorage.getItem("user")
    const token = localStorage.getItem("token")

    if (username && token) {
      setUser({
        [username]: username,
        [token]: token,
        isAuth: true
      }
      )
    }
  }, [])

  return (
    <>
      <Row>
        <Col span={24}>
          <Header />
        </Col>
      </Row>
      <main>
        <Row >
          <Layout>
            <Sider
              width={150}
              style={{
                backgroundColor: 'transparent'
              }}>
              <SideBar changeLocation={setLocation} currentLocation={location} />
            </Sider>
            <Content style={{
              backgroundColor: 'white',
              padding: '10px',
              margin: '10px 10px',
              borderRadius: '8px',
              boxShadow: '0px 0px 2px 0px #00000049'
            }}>
              {location === "table"
                ? <AntTable data={rawData} />
                : <SyncJson
                  isAuth={user.isAuth}
                  authorize={setUser}
                  token={user.token} />}
            </Content>
          </Layout>
        </Row>
      </main>
    </>
  )
}

export default App
