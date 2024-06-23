import { Outlet } from "react-router-dom";

import DashboardNav from "../../components/dashboardNav/DashboardNav";

import styles from "./Dashboard.module.css";
import { useAuth } from "../../contexts/AuthContext";
import { useEffect } from "react";
import { useDashboard } from "../../contexts/DashboardContext";

function Dashboard() {
  const { user } = useAuth();
  const { fetchAccounts } = useDashboard();

  useEffect(function () {
    fetchAccounts(user.id);
  }, []);

  return (
    <main className={styles.dashboard}>
      <DashboardNav />
      <Outlet />
    </main>
  );
}

export default Dashboard;
