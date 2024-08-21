import Button from "../../button/Button";

import styles from "./Account.module.css";

export default function Account({
  account,
  changeStatus,
  changeStatusBot,
  extendActiveState,
  extendBotActiveState,
}) {
  const activeAccount = !account.isActive;
  const activeBot = !account.isBotActive;

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
            className={`${
              account.isBotActive ? styles.active : styles.inActive
            }`}
          ></span>
          <Button
            onClick={() => changeStatusBot(account.id, activeBot)}
            type="action"
          >
            {account.isBotActive ? "Wyłącz bota" : "Włącz bota"}
          </Button>
        </div>
        <Button
          onClick={() => changeStatus(account.id, activeAccount)}
          type="action"
        >
          {account.isActive ? "Wyłącz" : "Włącz"}
        </Button>

        <Button onClick={() => extendActiveState(account.id)} type="action">
          Przedłuż konto
        </Button>
        <Button onClick={() => extendBotActiveState(account.id)} type="action">
          Przedłuż bota
        </Button>
      </div>
    </li>
  );
}
