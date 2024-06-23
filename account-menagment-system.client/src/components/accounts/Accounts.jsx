import { useAuth } from "../../contexts/AuthContext";
import styles from "./Accounts.module.css";
import Account from "./account/Account";

function Accounts() {
  const { accounts } = useAuth();

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
