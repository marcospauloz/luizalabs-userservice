import { useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";

const Logout = () => {
  const { setToken } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    setToken();
    navigate("/", { replace: true });
  };

  return (
    <div className={"inputContainer"}>
      <button className={"LoginBox-form-continue"} onClick={handleLogout}>Sair</button>
    </div>);
};

export default Logout;