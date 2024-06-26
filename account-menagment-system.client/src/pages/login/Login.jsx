import Button from "../../components/button/Button";
import styles from "./Login.module.css";
import Error from "../../components/errorCom/Error";
import { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
import Loader from "../../components/loader/Loader";

function Login() {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const { loginFun, isLoading } = useAuth();

  // const navigate = useNavigate();

  function handleSubmit(e) {
    e.preventDefault();
    if (!login || !password) return;

    loginFun(login, password);
    setLogin("");
    setPassword("");
  }

  return (
    <div className={styles.login}>
      {isLoading && <Loader />}
      <form className={styles.loginForm} onSubmit={handleSubmit}>
        <h1>Login</h1>

        <div className={styles.row}>
          <label htmlFor="username">Username</label>
          <input
            value={login}
            onChange={(e) => setLogin(e.target.value)}
            placeholder={`Type your username`}
            id="username"
            type="text"
          />
        </div>

        <div className={styles.row}>
          <label htmlFor="password">Password</label>
          <input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Type your password"
            id="password"
            type="password"
          />
        </div>

        <Error />

        <Button type="login">Login</Button>
      </form>
    </div>
  );
}

export default Login;
