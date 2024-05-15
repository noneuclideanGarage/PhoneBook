import { loginApi } from "../../../Services/ApiService";
import "./SyncLogin.css"
import { Button, Form, Input, Flex, message } from 'antd';

export default function SyncLogin({ changeAuth }) {

    const onFinish = async (values) => {

        const data = await loginApi(values.username, values.password)

        if (data.status === 200) {
            const json = await data.json()


            localStorage.setItem("username", json.username)
            localStorage.setItem("token", json.token)

            changeAuth(prev => {
                return {
                    username: json.username,
                    token: json.token,
                    isAuth: !prev.isAuth
                }
            })
            console.log('Success:', values.username);
        } else {
            console.log("Неверное имя пользователя или пароль")
        }


    };
    const onFinishFailed = (errorInfo) => {
        message.error('Failed:', errorInfo);
    };

    return (
        <div className='login'>
            <Flex align="center" justify="center">
                <Form
                    layout="vertical"
                    name="basic"
                    labelCol={{
                        span: 10,
                    }}
                    wrapperCol={{
                        span: 20,
                    }}
                    style={{
                        maxWidth: 700,
                        maxHeight: 500,
                        marginTop: 200
                    }}
                    initialValues={{
                        remember: true,
                    }}
                    onFinish={onFinish}
                    onFinishFailed={onFinishFailed}
                    autoComplete="off"
                >
                    <Form.Item
                        label="Имя пользователя"
                        labelAlign="left"
                        name="username"
                        style={{ width: 400 }}
                        rules={[
                            {
                                required: true,
                                message: 'Please input your username!',
                            },
                        ]}
                    >
                        <Input style={{ height: 40 }} />
                    </Form.Item>

                    <Form.Item
                        label="Пароль"
                        name="password"
                        rules={[
                            {
                                required: true,
                                message: 'Please input your password!',
                            },

                        ]}
                    >
                        <Input.Password style={{ height: 40 }} />
                    </Form.Item>

                    <Form.Item
                        wrapperCol={{
                            offset: 8,
                            span: 16,
                        }}
                    >
                        <Button
                            type="primary"
                            htmlType="submit">
                            Войти
                        </Button>
                    </Form.Item>
                </Form>
            </Flex>

        </div>

    )
}