import Button from "../../button/Button";

import styles from "./Account.module.css";

export default function Account({ account, changeStatus, changeStatusBot }) {
  const activeAccount = !account.isActive;
  const activeBot = !account.BotStatus;

  return (
    <li className={styles.account}>
      <span
        className={`${account.isActive ? styles.active : styles.inActive}`}
      ></span>

      <p>{account.login}</p>

      <p>&nbsp;</p>

      <div className={styles.btns}>
        <div className={styles.botBtns}>
          <span
            className={`${account.BotStatus ? styles.active : styles.inActive}`}
          ></span>
          <Button
            onClick={() => changeStatusBot(account.id, activeAccount)}
            type="action"
          >
            {account.BotStatus ? "Wyłącz bota" : "Włącz bota"}
          </Button>
        </div>
        <Button
          onClick={() => changeStatus(account.id, activeAccount)}
          type="action"
        >
          {account.isActive ? "Wyłącz" : "Włącz"}
        </Button>
      </div>
    </li>
  );
}
