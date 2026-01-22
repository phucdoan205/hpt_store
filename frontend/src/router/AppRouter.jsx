import { Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Cart from "../pages/Cart";
import Checkout from "../pages/Checkout";

import AdminLayout from "../pages/admin/AdminLayout";
import Dashboard from "../pages/admin/Dashboard";
import Employees from "../pages/admin/Employees";
import Revenue from "../pages/admin/Revenue";

import StaffLayout from "../pages/staff/StaffLayout";
import Orders from "../pages/staff/Orders";
import Products from "../pages/staff/Products";
import Posts from "../pages/staff/Posts";

export default function AppRouter() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/cart" element={<Cart />} />
      <Route path="/checkout" element={<Checkout />} />

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
