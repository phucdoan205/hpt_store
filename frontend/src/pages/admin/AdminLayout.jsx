import { Link, Outlet } from "react-router-dom";

export default function AdminLayout() {
  return (
    <div className="flex min-h-screen">
      <aside className="w-56 bg-gray-800 text-white p-4">
        <Link to="/admin">Tổng quan</Link>
        <Link to="/admin/employees">Nhân viên</Link>
        <Link to="/admin/revenue">Doanh thu</Link>
      </aside>
      <main className="flex-1 p-6">
        <Outlet />
      </main>
    </div>
  );
}
