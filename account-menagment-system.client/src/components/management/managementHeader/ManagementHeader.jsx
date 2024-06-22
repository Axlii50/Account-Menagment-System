import styles from "./ManagementHeader.module.css";

function ManagementHeader() {
  return (
    <div className={styles.titles}>
      &nbsp;
      <h2>Nazwa</h2>
      <h2>Ważność</h2>
      <h2 className={styles.actions}>Akcje</h2>
    </div>
  );
}

export default ManagementHeader;
