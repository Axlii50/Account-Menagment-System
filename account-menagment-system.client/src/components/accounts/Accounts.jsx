import styles from "./Accounts.module.css";
import Account from "./account/Account";

function Accounts() {
  return (
    <ul className={styles.accounts}>
      <Account />
      <Account />
    </ul>
  );
}

export default Accounts;
