import { json, useNavigate } from "react-router-dom";
import Button from "../../components/button/Button";
import styles from "./Login.module.css";
import Error from "../../components/errorCom/Error";
import { useEffect, useState } from "react";

async function LoginFun(login, password) {
    let json = JSON.stringify({ userName: login, password: password });
    console.log(json);
  try {
      const res = await fetch(`Accounts/Login/LoginForm`, {
          headers: {
              'Accept': '*/*',
              'Content-Type': 'application/json'
          },
      method: "POST",
      body: json,
      });

      

    console.log(res);
    const data = await res.json();
    console.log(res);
    console.log(data);
  } catch (err) {
    console.log(err.message);
  }
}

function Login() {
  const [login, setLogin] = useState("Maciek");
  const [password, setPassword] = useState("Test123");
  const navigate = useNavigate();

  function handleSubmit(e) {
    e.preventDefault();
    if (!login || !password) return;

    LoginFun(login, password);
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
