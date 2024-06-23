import { useEffect } from "react";
import { useAuth } from "../../contexts/AuthContext";
import { useDashboard } from "../../contexts/DashboardContext";

import Account from "./account/Account";
import LoaderDashboard from "../../components/loader/LoaderDashboard";

import styles from "./Accounts.module.css";

function Accounts() {
  const { accounts, isLoading, changeStatus } = useDashboard();

  if (isLoading) return <LoaderDashboard />;

  if (!accounts || !accounts.length) return;

  return (
    <ul className={styles.accounts}>
      {accounts?.map((account) => (
        <Account
          key={account.id}
          account={account}
          changeStatus={changeStatus}
        />
      ))}
    </ul>
  );
}

export default Accounts;
