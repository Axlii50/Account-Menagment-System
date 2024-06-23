import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import Login from "./pages/login/Login";
import Dashboard from "./pages/dashboard/Dashboard";
import PageNotFound from "./pages/notFound/PageNotFound";
import AccountManagement from "./components/management/AccountManagement";
import { AuthProvider } from "./contexts/AuthContext";
import ProtectedRoute from "./pages/protectedRoute/ProtectedRoute";
import { DashboardProvider } from "./contexts/DashboardContext";

export default function App() {
  return (
    <BrowserRouter>
      <DashboardProvider>
        <AuthProvider>
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
        </AuthProvider>
      </DashboardProvider>
    </BrowserRouter>
  );
}
