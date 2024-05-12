import gasLogo from "/gas-logo.png"
import "./Header.css"

export default function Header() {
    return (
        <>
            <nav>
            <img className="app-logo" src={gasLogo} alt="app-logo"/>
            </nav>
        </>
    )
}