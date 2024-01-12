import "react-toastify/dist/ReactToastify.css";
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
    const [nameError, setNameError] = useState("")
    const [loginEmailError, setLoginEmailError] = useState("")
    const [registerEmailError, setRegisterEmailError] = useState("")
    const [passwordError, setPasswordError] = useState("")
    const [loginValidationError, setLoginValidationError] = useState("")
    const [newUserValidationError, setNewUserValidationError] = useState("")

    const navigate = useNavigate();

    const validateEmail = (email) => {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    const validatePassword = (password) => {
        var hasUpperCase = /[A-Z]/.test(password);
        var hasNumber = /\d/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        return hasUpperCase && hasNumber && hasLowerCase;
    }

    const onLoginClick = async () => {
        if (!validateEmail(loginEmail)) {
            setLoginEmailError("O E-mail não foi digitado corretamente.");
            return;
        }

        const data = { email: loginEmail, password: loginPassword };

        try {
            const response = await axios.post('http://localhost:8080/users/authenticate', data);
            localStorage.setItem("id", response.data.id)
            localStorage.setItem("name", response.data.name)
            setToken(response.data.jwtToken);
            navigate("/", { replace: true });
        } catch (error) {
            if (error.response !== undefined && error.response.status !== 200) {
                setLoginValidationError(error.response.data.message);
                return;
            }
            setLoginValidationError("Não foi possível realizar o login. Erro interno do servidor!");
            return;
        }
    }

    const onNewUserClick = async () => {
        var validForm = true;
        setRegisterEmailError("");
        setNameError("");
        setPasswordError("");

        if (!validateEmail(registerEmail)) {
            setRegisterEmailError("O E-mail não foi digitado corretamente.");
            validForm = false;
        }

        if (!registerName) {
            setNameError("Preencha um nome válido.");
            validForm = false;
        }

        if (!validatePassword(registerPassword)) {
            setPasswordError("Senha deve conter ao menos uma letra maiúscula, uma letra minúscula e um número.");
            validForm = false;
        }

        if (!validForm) {
            return;
        }

        const data = { name: registerName, email: registerEmail, password: registerPassword };

        try {
            const response = await axios.post('http://localhost:8080/users/create', data);
            if (response.status === 200) {
                localStorage.setItem("id", response.data.id)
                localStorage.setItem("name", response.data.name)
                setToken(response.data.jwtToken);
                navigate("/", { replace: true });
            }
        } catch (error) {
            if (error.response !== undefined && error.response.status < 500) {
                setNewUserValidationError(error.response.data.message);
                return;
            }

            setNewUserValidationError("Não foi possível realizar o login. Erro interno do servidor!");
            return;
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
                    <div className="FormGroup">
                        <div className={"inputContainer"}>
                            <label className="FormGroup-label">Nome</label>
                            <input
                                value={registerName}
                                onChange={ev => setRegisterName(ev.target.value)}
                                className={"FormGroup-input"} />
                        </div>
                        <label className="errorLabel">{nameError}</label>
                    </div>
                    <div className="FormGroup">
                        <div className={"inputContainer"}>
                            <label className="FormGroup-label">E-mail</label>
                            <input
                                value={registerEmail}
                                onChange={ev => setRegisterEmail(ev.target.value)}
                                className={"FormGroup-input"} />
                        </div>
                        <label className="errorLabel">{registerEmailError}</label>
                    </div>
                    <div className="FormGroup">
                        <div className={"inputContainer"}>
                            <label className="FormGroup-label">Senha</label>
                            <input
                                type="password"
                                value={registerPassword}
                                onChange={ev => setRegisterPassword(ev.target.value)}
                                className={"FormGroup-input"} />
                        </div>
                        <label className="errorLabel">{passwordError}</label>
                    </div>
                    <div className={"inputContainer"}>
                        <input
                            className={"LoginBox-form-continue"}
                            type="button"
                            onClick={onNewUserClick}
                            value={"Continuar"} />
                    </div>
                    <div className="FormGroup">
                        <label className="errorLabel">{newUserValidationError}</label>
                    </div>
                </div>
                <div className="LoginPage-rightContainer">
                    <div className={"titleContainer"}>
                        <div>Já sou cliente</div>
                    </div>
                    <div className="FormGroup">
                        <div className={"inputContainer"}>
                            <label className="FormGroup-label">E-mail</label>
                            <input
                                value={loginEmail}
                                onChange={ev => setLoginEmail(ev.target.value)}
                                className={"FormGroup-input"} />
                        </div>
                        <label className="errorLabel">{loginEmailError}</label>
                    </div>
                    <div className="FormGroup">
                        <div className={"inputContainer"}>
                            <label className="FormGroup-label">Senha</label>
                            <input
                                type="password"
                                value={loginPassword}
                                onChange={ev => setLoginPassword(ev.target.value)}
                                className={"FormGroup-input"} />
                        </div>
                    </div>
                    <div className={"inputContainer"}>
                        <input
                            className={"LoginBox-form-continue"}
                            type="button"
                            onClick={onLoginClick}
                            value={"Continuar"} />
                    </div>
                    <div className="FormGroup">
                        <label className="errorLabel">{loginValidationError}</label>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;