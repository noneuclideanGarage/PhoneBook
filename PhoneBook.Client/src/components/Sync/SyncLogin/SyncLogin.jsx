import "./SyncLogin.css"
import { Button, Form, Input, Flex } from 'antd';

export default function SyncLogin({ changeAuth }) {

    const onFinish = (values) => {
        console.log('Success:', values);
        changeAuth(prev => !prev)
    };
    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
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
                            style={{height: 40}}
                            type="primary"
                            htmlType="submit">
                            Submit
                        </Button>
                    </Form.Item>
                </Form>
            </Flex>

        </div>

    )
}