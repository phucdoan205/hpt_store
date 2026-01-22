import { Link, Outlet } from "react-router-dom";

export default function StaffLayout() {
  return (
    <div className="flex min-h-screen">
      <aside className="w-56 bg-blue-900 text-white p-4">
        <Link to="/staff/orders">Đơn hàng</Link>
        <Link to="/staff/products">Sản phẩm</Link>
        <Link to="/staff/posts">Bài viết</Link>
      </aside>
      <main className="flex-1 p-6">
        <Outlet />
      </main>
    </div>
  );
}
