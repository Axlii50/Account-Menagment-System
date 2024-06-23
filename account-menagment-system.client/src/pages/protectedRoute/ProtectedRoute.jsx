import { useEffect } from "react";
import { useAuth } from "../../contexts/AuthContext";
import { useNavigate } from "react-router-dom";

function ProtectedRoute({ children }) {
  const { isAuth, user } = useAuth();
  const navigate = useNavigate();

  useEffect(
    function () {
      if (!isAuth && !user?.isAdmin) navigate("/", { replace: true });
    },
    [isAuth, navigate, user]
  );

  return isAuth && user.isAdmin ? children : null;
}

export default ProtectedRoute;
