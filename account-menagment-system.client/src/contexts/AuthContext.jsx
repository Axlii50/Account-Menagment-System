import { createContext, useContext, useEffect, useReducer } from "react";

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
      return { ...state, user: action.payload, isAuth: true };
    case "logout":
      return { ...initialState };
    default:
      throw new Error("Action unknown");
  }
}

function AuthProvider({ children }) {
  const [{ user, isAuth }, dispatch] = useReducer(reducer, initialState);

  async function login(login, password) {
    try {
      const res = await fetch(`/Accounts/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ userName: login, password: password }),
      });
      const data = await res.json();
      console.log(data);
      dispatch({ type: "login", payload: data });
    } catch (err) {
      console.log(err.message);
    }
  }

  return (
    <AuthContext.Provider value={{ user, isAuth, loginFun: login }}>
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
