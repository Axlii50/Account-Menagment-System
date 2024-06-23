import styles from "./LoaderDashboard.module.css";

function LoaderDashboard() {
  return (
    <div className={styles.loaderBG}>
      <div className={styles.loader}></div>
    </div>
  );
}

export default LoaderDashboard;
