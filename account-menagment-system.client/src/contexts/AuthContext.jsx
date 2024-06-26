import { createContext, useContext, useReducer, useState } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

const authDataGlobal = JSON.parse(localStorage.getItem("authData")) || {
  user: null,
  isAuth: false,
};

const initialState = {
  user: authDataGlobal.user,
  error: "",
  isAuth: authDataGlobal.isAuth,
};

function reducer(state, action) {
  switch (action.type) {
    case "login":
      return {
        ...state,
        user: action.payload,
        isAuth: true,
        error: "",
      };
    case "logout":
      return { ...initialState, isAuth: false, user: null };
    case "rejected":
      return { ...state, error: action.payload };
    default:
      throw new Error("Action unknown");
  }
}

function AuthProvider({ children }) {
  const [{ user, isAuth, error, accounts }, dispatch] = useReducer(
    reducer,
    initialState
  );
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  async function login(login, password) {
    try {
      setIsLoading(true);

      const res = await fetch(`Accounts/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ userName: login, password: password }),
      });

      if (!res.ok) {
        if (res.status === 404) {
          throw new Error("Zła nazwa użytkownika lub hasło");
        }

        throw new Error(
          "Coś poszło nie tak, skontaktuj się z administratorem strony"
        );
      }
      const data = await res.json();

      if (login === data.login && data.isAdmin === true) {
        const authData = {
          user: data,
          isAuth: true,
        };

        localStorage.setItem("authData", JSON.stringify(authData));
        dispatch({ type: "login", payload: data });
        navigate("/dashboard", { replace: true });
      }
    } catch (err) {
      dispatch({
        type: "rejected",
        payload: err.message,
      });
    } finally {
      setIsLoading(false);
    }
  }

  function logout() {
    localStorage.removeItem("authData");
    dispatch({ type: "logout" });
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        isAuth,
        error,
        loginFun: login,
        logout,
        accounts,
        isLoading,
      }}
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
