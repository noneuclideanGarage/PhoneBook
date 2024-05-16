import { message } from "antd"

const apiUrl = 'backend/' //http://localhost:5888/api/

export async function loginApi(username, password) {
    try {
        const data = await fetch(apiUrl + "account/login",
            {
                method: 'POST',
                body: JSON.stringify({
                    "username": username,
                    "password": password
                }),
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            }
        )

        return data
    } catch (error) {
        message.error("Сервер не отвечает")
    }
}