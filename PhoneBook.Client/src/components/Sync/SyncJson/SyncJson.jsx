import { Flex } from 'antd';
import SyncForm from '../SyncForm/SyncForm';
import SyncLogin from '../SyncLogin/SyncLogin';
export default function SyncJson({ isAuth = false, authorize }) {
    return (
        <div className="main-container shadow">
            <Flex justify='center' align='center'>
                {isAuth
                    ? <SyncForm />
                    : <SyncLogin changeAuth={authorize} />}
            </Flex>
        </div>
    )
}