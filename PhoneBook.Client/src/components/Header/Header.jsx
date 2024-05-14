import gasLogo from "/gas-logo.png"
import "./Header.css"

export default function Header() {
    return (
        <>
            <header id="header">
            <img className="app-logo" src={gasLogo} alt="app-logo"/>
            </header>
        </>
    )
}