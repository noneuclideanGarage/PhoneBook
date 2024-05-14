import Header from './components/Header/Header'
import { useEffect, useState } from 'react'
import SideBar from './components/SideBar/SideBar'
import AntTable from './components/AntTable/AntTable'
import { Col, Row, Layout } from 'antd';

const { Sider, Content } = Layout;

function App() {

  const [rawData, setRawData] = useState(() => [])
  const [location, setLocation] = useState("table")

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
              <AntTable data={rawData}/>
            </Content>
          </Layout>
        </Row>
        {/* <Table data={rawData} isPending={pending} changePendingStatus={SetPending}/> */}
      </main>
    </>
  )
}

export default App
