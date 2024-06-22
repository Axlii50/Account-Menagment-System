import { createContext, useContext, useReducer } from "react";

const AuthContext = createContext();

const initialState = {
  id: null,
  isLogged: false,
};

function reducer(action, state) {
  switch (action.type) {
    case "login":
      return { ...state, isLogged: true, id: action.payload };
    case "logout":
      return { ...initialState };
    default:
      throw new Error("Action unknown");
  }
}

function AuthProvider({ children }) {
  const [{ id, isLogged }, dispatch] = useReducer(reducer, initialState);

  async function login(login, password) {
    try {
      const res = await fetch(`/Accounts/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ userName: login, password: password }),
      });
      console.log(res);
      const data = await res.json();
      console.log(res);
      console.log(data);
      dispatch({ type: "login" });
    } catch (err) {
      console.log(err.message);
    }
  }

  return (
    <AuthContext.Provider value={{ id, isLogged, loginFun: login }}>
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
