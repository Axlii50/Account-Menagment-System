import { useAuth } from "../../../contexts/AuthContext";
import styles from "./ManagementHeader.module.css";

function ManagementHeader() {
  const { accounts } = useAuth();

  if (!accounts || !accounts.length) return;

  return (
    <div className={styles.titles}>
      &nbsp;
      <h2>Nazwa</h2>
      <h2>&nbsp;</h2>
      <h2 className={styles.actions}>Akcje</h2>
    </div>
  );
}

export default ManagementHeader;
