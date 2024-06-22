import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import Login from "./pages/login/Login";
import Dashboard from "./pages/dashboard/Dashboard";
import PageNotFound from "./pages/notFound/PageNotFound";
import AccountManagement from "./components/management/AccountManagement";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<Login />} />

        <Route path="dashboard" element={<Dashboard />}>
          <Route index element={<Navigate replace to="accountManagement" />} />
          <Route path="accountManagement" element={<AccountManagement />} />
        </Route>

        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
