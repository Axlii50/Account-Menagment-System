import {
  createContext,
  useCallback,
  useContext,
  useReducer,
  useState,
} from "react";

const DashboardContext = createContext();

const initialState = {
  accounts: [],
};

function reducer(state, action) {
  switch (action.type) {
    case "fetchedAccounts":
      return { ...state, accounts: action.payload };
    case "changeStatus":
      return {
        ...state,
        accounts: state.accounts.map((account) => {
          if (account.id == action.payload.id) {
            return { ...account, isActive: !account.isActive };
          } else {
            return account;
          }
        }),
      };
    case "changeStatusBot":
      return {
        ...state,
        accounts: state.accounts.map((account) => {
          if (account.id == action.payload.id) {
            return { ...account, BotState: !account.BotState };
          } else {
            return account;
          }
        }),
      };
    default:
      throw new Error("Action unknown");
  }
}

function DashboardProvider({ children }) {
  const [{ accounts }, dispatch] = useReducer(reducer, initialState);
  const [isLoading, setIsLoading] = useState(false);

  const fetchAccounts = useCallback(async function fetchAccounts(id) {
    try {
      setIsLoading(true);
      const res = await fetch("/Accounts/GetAccountsData", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ ID: id }),
      });
      const data = await res.json();
      dispatch({ type: "fetchedAccounts", payload: data });
    } catch (err) {
      err.message;
    } finally {
      setIsLoading(false);
    }
  }, []);

  async function changeStatus(id, status) {
    try {
      setIsLoading(true);
      const res = await fetch("/Accounts/ChangeStateAccount", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ ID: id, State: status }),
      });
      const data = await res.json();

      dispatch({ type: "changeStatus", payload: data });
    } catch (err) {
      err.message;
    } finally {
      setIsLoading(false);
    }
  }

  async function changeStatusBot(id, status) {
    try {
      setIsLoading(true);
      const res = await fetch("/Accounts/ChangeStateBot", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ ID: id, State: status }),
      });
      const data = await res.json();
      console.log(data);

      dispatch({ type: "changeStatusBot", payload: data });
    } catch (err) {
      err.message;
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <DashboardContext.Provider
      value={{
        accounts,
        fetchAccounts,
        isLoading,
        changeStatus,
        changeStatusBot,
      }}
    >
      {children}
    </DashboardContext.Provider>
  );
}

function useDashboard() {
  const value = useContext(DashboardContext);
  if (value === undefined)
    throw new Error("AuthContext was used outside the AuthProvider");
  return value;
}

export { DashboardProvider, useDashboard };
