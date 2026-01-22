import { Routes, Route, useLocation } from "react-router-dom";
import Home from "./pages/home/Home";
import Login from "./pages/auth/Login";
import Register from "./pages/auth/Register";

import ProtectedRoute from "./auth/ProtectedRoute";

import AdminLayout from "./pages/admin/AdminLayout";
import AdminDashboard from "./pages/admin/Dashboard";
import Employees from "./pages/admin/Employees";
import Revenue from "./pages/admin/Revenue";

import StaffLayout from "./pages/staff/StaffLayout";
import Orders from "./pages/staff/Orders";
import Products from "./pages/staff/Products";
import Posts from "./pages/staff/Posts";

import Chatbot from "./components/ChatBot";

export default function App() {
  const location = useLocation();

  // Ẩn chatbot ở trang admin & staff
  const isManagementPage =
    location.pathname.startsWith("/admin") ||
    location.pathname.startsWith("/staff");

  return (
    <div className="min-h-screen bg-white">
      <Routes>
        {/* USER */}
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* ADMIN */}
        <Route
          path="/admin"
          element={
            <ProtectedRoute role="admin">
              <AdminLayout />
            </ProtectedRoute>
          }
        >
          <Route index element={<AdminDashboard />} />
          <Route path="employees" element={<Employees />} />
          <Route path="revenue" element={<Revenue />} />
        </Route>

        {/* STAFF */}
        <Route
          path="/staff"
          element={
            <ProtectedRoute role="staff">
              <StaffLayout />
            </ProtectedRoute>
          }
        >
          <Route path="orders" element={<Orders />} />
          <Route path="products" element={<Products />} />
          <Route path="posts" element={<Posts />} />
        </Route>
      </Routes>

      {/* Chatbot chỉ hiện ở USER */}
      {!isManagementPage && <Chatbot />}
    </div>
  );
}
