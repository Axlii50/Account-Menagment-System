import { createContext, useContext, useEffect, useReducer } from "react";
import { Router, useNavigate } from "react-router-dom";

const AuthContext = createContext();

const initialState = {
  user: null,
  error: "",
  isAuth: false,
  isLoading: false,
};

function reducer(state, action) {
  switch (action.type) {
    case "login":
      return { ...state, user: action.payload, isAuth: true, error: "" };
    case "logout":
      return { ...initialState };
    case "rejected":
      return { ...state, error: action.payload };
    default:
      throw new Error("Action unknown");
  }
}

function AuthProvider({ children }) {
  const [{ user, isAuth, error }, dispatch] = useReducer(reducer, initialState);
  const navigate = useNavigate();

  async function login(login, password) {
    try {
      const res = await fetch(`Accounts/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ userName: login, password: password }),
      });
      const data = await res.json();
      if (login === data.login && data.isAdmin === true) {
        dispatch({ type: "login", payload: data });
        navigate("/dashboard", { replace: true });
      }
    } catch (err) {
      dispatch({
        type: "rejected",
        payload: "Zła nazwa użytkownika lub hasło",
      });
    }
  }

  function logout() {
    dispatch({ type: "logout" });
    navigate[-1] = [];
  }

  return (
    <AuthContext.Provider
      value={{ user, isAuth, error, loginFun: login, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
}

function useAuth() {
  const value = useContext(AuthContext);
  if (value === undefined)
    throw new Error("AuthContext was used outside the AuthProvider");
  return value;
}

export { AuthProvider, useAuth };
