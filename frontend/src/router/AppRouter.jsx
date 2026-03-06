import { Routes, Route } from "react-router-dom";
import Home from "../pages/home/Home";
import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";
import ProductDetail from "../pages/product/ProductDetail";

import AdminLayout from "../pages/admin/AdminLayout";
import Dashboard from "../pages/admin/Dashboard";
import Employees from "../pages/admin/Employees";
import Revenue from "../pages/admin/Revenue";

import StaffLayout from "../pages/staff/StaffLayout";
import Orders from "../pages/staff/Orders";
import Products from "../pages/staff/Products";
import Posts from "../pages/staff/Posts";

import MainLayout from "../components/layout/MainLayout";

export default function AppRouter() {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route path="/" element={<Home />} />
        {/* Thêm dòng này */}
        <Route path="/product/:id" element={<ProductDetail />} />
      </Route>
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />

      <Route path="/admin" element={<AdminLayout />}>
        <Route index element={<Dashboard />} />
        <Route path="employees" element={<Employees />} />
        <Route path="revenue" element={<Revenue />} />
      </Route>

      <Route path="/staff" element={<StaffLayout />}>
        <Route path="orders" element={<Orders />} />
        <Route path="products" element={<Products />} />
        <Route path="posts" element={<Posts />} />
      </Route>
    </Routes>
  );
}
