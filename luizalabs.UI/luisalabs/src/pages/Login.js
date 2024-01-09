import { useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";

import { useState } from "react";
import axios from "axios";

const Login = () => {
    const { setToken } = useAuth();
    const [registerName, setRegisterName] = useState("")
    const [registerEmail, setRegisterEmail] = useState("")
    const [registerPassword, setRegisterPassword] = useState("")
    const [loginEmail, setLoginEmail] = useState("")
    const [loginPassword, setLoginPassword] = useState("")
    const [emailError, setEmailError] = useState("")
    const [passwordError, setPasswordError] = useState("")

    const navigate = useNavigate();

    const onLoginClick = async () => {
        const data = { email: loginEmail, password: loginPassword };
        const response = await axios.post('http://localhost:54971/users/authenticate', data);
        setToken(response.data.jwtToken);
        navigate("/", { replace: true });
    }

    const onNewUserClick = async () => {
        const data = { name: registerName, email: registerEmail, password: registerPassword };
        const response = await axios.post('http://localhost:54971/users/create', data);
        console.log(response);
        if (response.status === 200) {
            setToken(response.data.jwtToken);
            navigate("/", { replace: true });
        } else {
            console.log("error");
        }
    }

    return (
        <div>
            <div className="LoginPage-title">Identificação</div>
            <div className={"mainContainer"}>
                <div className="LoginPage-leftContainer">
                    <div className={"titleContainer"}>
                        <div>Quero criar uma conta</div>
                    </div>
                    <div className={"inputContainer"}>
                        <label className="FormGroup-label">Nome</label>
                        <input
                            value={registerName}
                            onChange={ev => setRegisterName(ev.target.value)}
                            className={"FormGroup-input"} />
                        <label className="errorLabel">{emailError}</label>
                    </div>
                    <div className={"inputContainer"}>
                        <label className="FormGroup-label">E-mail</label>
                        <input
                            value={registerEmail}
                            onChange={ev => setRegisterEmail(ev.target.value)}
                            className={"FormGroup-input"} />
                        <label className="errorLabel">{emailError}</label>
                    </div>
                    <div className={"inputContainer"}>
                        <label className="FormGroup-label">Senha</label>
                        <input
                            value={registerPassword}
                            onChange={ev => setRegisterPassword(ev.target.value)}
                            className={"FormGroup-input"} />
                        <label className="errorLabel">{passwordError}</label>
                    </div>
                    <div className={"inputContainer"}>
                        <input
                            className={"LoginBox-form-continue"}
                            type="button"
                            onClick={onNewUserClick}
                            value={"Continuar"} />
                    </div>
                </div>
                <div className="LoginPage-rightContainer">
                    <div className={"titleContainer"}>
                        <div>Já sou cliente</div>
                    </div>
                    <div className={"inputContainer"}>
                        <label className="FormGroup-label">E-mail</label>
                        <input
                            value={loginEmail}
                            onChange={ev => setLoginEmail(ev.target.value)}
                            className={"FormGroup-input"} />
                        <label className="errorLabel">{emailError}</label>
                    </div>
                    <div className={"inputContainer"}>
                        <label className="FormGroup-label">Senha</label>
                        <input
                            value={loginPassword}
                            onChange={ev => setLoginPassword(ev.target.value)}
                            className={"FormGroup-input"} />
                        <label className="errorLabel">{passwordError}</label>
                    </div>

                    <div className={"inputContainer"}>
                        <input
                            className={"LoginBox-form-continue"}
                            type="button"
                            onClick={onLoginClick}
                            value={"Continuar"} />
                    </div>
                </div>
            </div>
        </div>
        // <form onSubmit={handleLogin}>
        //     <input type="text" placeholder="Username" onChange={e => setUsername(e.target.value)} />
        //     <input type="password" placeholder="Password" onChange={e => setPassword(e.target.value)} />
        //     <button type="submit">Login</button>
        // </form>
    );
};

export default Login;