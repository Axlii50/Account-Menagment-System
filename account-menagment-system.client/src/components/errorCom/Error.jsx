import { useAuth } from "../../contexts/AuthContext";
import styles from "./Error.module.css";

function Error() {
  const { error } = useAuth();

  return <p className={styles.error}>{error}</p>;
}

export default Error;
