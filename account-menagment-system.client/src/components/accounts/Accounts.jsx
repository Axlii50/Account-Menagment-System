import { useDashboard } from "../../contexts/DashboardContext";

import Account from "./account/Account";
import LoaderDashboard from "../../components/loader/LoaderDashboard";

import styles from "./Accounts.module.css";

function Accounts() {
  const { accounts, isLoading, changeStatus, changeStatusBot } = useDashboard();

  if (isLoading) return <LoaderDashboard />;

  if (!accounts || !accounts.length) return;

  return (
    <ul className={styles.accounts}>
      {accounts?.map((account) => (
        <Account
          key={account.id}
          account={account}
          changeStatus={changeStatus}
          changeStatusBot={changeStatusBot}
        />
      ))}
    </ul>
  );
}

export default Accounts;
