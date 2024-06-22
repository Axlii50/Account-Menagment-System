import { json, useNavigate } from "react-router-dom";
import Button from "../../components/button/Button";
import styles from "./Login.module.css";
import Error from "../../components/errorCom/Error";
import { useEffect, useState } from "react";

function Login() {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  useEffect(function () {
    async function Login() {
      try {
        const res = await fetch(`Accounts/Login`, {
          method: "POST",
          body: JSON.stringify({ UserName: login, Password: password }),
        });
        const data = await res.json();
        console.log(data);
      } catch (err) {
        console.log(err.message);
      }
    }
    Login();
  }, []);

  function handleSubmit(e) {
    e.preventDefault();
    if (!login || !password) return;

    navigate("/dashboard");
  }

  return (
    <div className={styles.login}>
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
