import { NavLink } from "react-router-dom";
import styles from "./DashboardNav.module.css";
import Button from "../button/Button";
import { useAuth } from "../../contexts/AuthContext";

function DashboardNav() {
  const { logout } = useAuth();

  return (
    <nav className={styles.nav}>
      <ul>
        <li>
          <NavLink to="management">Management</NavLink>
        </li>

        <li>
          <Button onClick={() => logout()}>Logout</Button>
        </li>
      </ul>
    </nav>
  );
}

export default DashboardNav;
