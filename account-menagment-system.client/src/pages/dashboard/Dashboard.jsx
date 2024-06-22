import { Outlet } from "react-router-dom";

import DashboardNav from "../../components/dashboardNav/DashboardNav";

import styles from "./Dashboard.module.css";

function Dashboard() {
  return (
    <main className={styles.dashboard}>
      <DashboardNav />
      <Outlet />
    </main>
  );
}

export default Dashboard;
