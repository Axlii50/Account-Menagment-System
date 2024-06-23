import { useEffect } from "react";
import { useAuth } from "../../contexts/AuthContext";
import { useDashboard } from "../../contexts/DashboardContext";

import Account from "./account/Account";
import LoaderDashboard from "../../components/loader/LoaderDashboard";

import styles from "./Accounts.module.css";

function Accounts() {
  const { user } = useAuth();
  const { accounts, fetchAccounts, isLoading } = useDashboard();

  useEffect(function () {
    fetchAccounts(user.id);
  }, []);

  if (isLoading) return <LoaderDashboard />;

  if (!accounts || !accounts.length) return;

  return (
    <ul className={styles.accounts}>
      {accounts.map((account) => (
        <Account key={account.id} account={account} />
      ))}
    </ul>
  );
}

export default Accounts;
