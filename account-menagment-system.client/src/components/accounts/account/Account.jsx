import Button from "../../button/Button";

import styles from "./Account.module.css";

export default function Account() {
  return (
    <li className={styles.account}>
      <span className={styles.active}></span>

      <p>Account name</p>

      <p>Account date</p>

      <div className={styles.btns}>
        <Button type="action">Edytuj</Button>
        <Button type="action">Wyłącz</Button>
        <Button type="action">Usuń</Button>
        <Button type="action">Dodaj</Button>
      </div>
    </li>
  );
}
