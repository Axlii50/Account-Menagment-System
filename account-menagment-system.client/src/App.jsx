import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext";
import { DashboardProvider } from "./contexts/DashboardContext";
import { Suspense, lazy } from "react";

import ProtectedRoute from "./pages/protectedRoute/ProtectedRoute";
import LoaderDashboard from "./components/loader/LoaderDashboard";

// import Login from "./pages/login/Login";
// import Dashboard from "./pages/dashboard/Dashboard";
// import PageNotFound from "./pages/notFound/PageNotFound";
// import AccountManagement from "./components/management/AccountManagement";

const Login = lazy(() => import("./pages/login/Login"));
const Dashboard = lazy(() => import("./pages/dashboard/Dashboard"));
const PageNotFound = lazy(() => import("./pages/notFound/PageNotFound"));
const AccountManagement = lazy(() =>
  import("./components/management/AccountManagement")
);

export default function App() {
  return (
    <BrowserRouter>
      <DashboardProvider>
        <AuthProvider>
          <Suspense fallback={<LoaderDashboard />}>
            <Routes>
              <Route index element={<Login />} />

              <Route
                path="dashboard"
                element={
                  <ProtectedRoute>
                    <Dashboard />
                  </ProtectedRoute>
                }
              >
                <Route index element={<Navigate replace to="management" />} />
                <Route path="management" element={<AccountManagement />} />
              </Route>

              <Route path="*" element={<PageNotFound />} />
            </Routes>
          </Suspense>
        </AuthProvider>
      </DashboardProvider>
    </BrowserRouter>
  );
}
