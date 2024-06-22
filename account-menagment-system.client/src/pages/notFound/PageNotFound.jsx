import styles from "./PageNotFound.module.css";

function PageNotFound() {
  return (
    <div className={styles.pageNotFound}>
      <p className={styles.text}>Page not found 🙁</p>
    </div>
  );
}

export default PageNotFound;
