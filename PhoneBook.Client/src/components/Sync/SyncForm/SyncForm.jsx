import { message, Upload } from 'antd';
import { InboxOutlined } from '@ant-design/icons';
const { Dragger } = Upload;

const props = {
    name: 'files' , //it should be `files` for post request
    multiple: true,
    action: 'http://localhost:5888/api/sync-json',
    onChange(info) {
        const { status } = info.file;
        if (status === 'done') {
            message.success(`${info.file.name} успешно загружен.`);
        } else if (status === 'error') {
            message.error(`${info.file.name} загрузка не удалась.`);
        }
    },
    onDrop(e) {
        console('Dropped files', e.dataTransfer.files);
    },
};


export default function SyncForm({ token }) {
    console.log(token)
    return (
        <div className="sync-form" style={{ marginTop: 200 }}>
            <Dragger {...props} headers={{
                "Authorization": 'Bearer ' + token
            }} accept='.json'>
                <p className="ant-upload-drag-icon">
                    <InboxOutlined />
                </p>
                <p className="ant-upload-text">Щелкните или перетащите файл в эту область для загрузки</p>
                <p className="ant-upload-hint">
                    Поддерживается одиночная или массовая загрузка. Строго запрещено загружать данные компании или другие запрещенные файлы.
                </p>
            </Dragger>
        </div>
    )
}