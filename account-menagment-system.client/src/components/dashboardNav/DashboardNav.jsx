import { NavLink } from "react-router-dom";
import styles from "./DashboardNav.module.css";
import Button from "../button/Button";

function DashboardNav() {
  return (
    <nav className={styles.nav}>
      <ul>
        <li>
          <NavLink to="accountManagement">Management</NavLink>
        </li>

        <li>
          <NavLink to="accountManag">Management</NavLink>
        </li>

        <li>
          <Button>Logout</Button>
        </li>
      </ul>
    </nav>
  );
}

export default DashboardNav;
