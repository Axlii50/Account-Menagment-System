import Button from "../../button/Button";

import styles from "./Account.module.css";

export default function Account({ account }) {
  return (
    <li className={styles.account}>
      <span
        className={`${account.isActive ? styles.active : styles.inActive}`}
      ></span>

      <p>{account.login}</p>

      <p>&nbsp;</p>

      <div className={styles.btns}>
        <Button type="action">{account.isActive ? "Wyłącz" : "Włącz"}</Button>
      </div>
    </li>
  );
}
