import { NavLink, Outlet } from "react-router-dom";

export default function AdminLayout() {
  const menuItem =
    "block px-4 py-3 transition-all duration-200";

  return (
    <div className="flex min-h-screen bg-gray-100">
      {/* SIDEBAR */}
      <aside className="w-64 bg-gray-800 text-white flex flex-col border-r border-gray-300">
        
        {/* LOGO */}
        <div className="text-2xl font-bold text-center py-6 text-blue-500">
          DASHBOARD
        </div>

        {/* MENU */}
        <nav className="flex flex-col">
          <NavLink
            to="/admin"
            end
            className={({ isActive }) =>
              `${menuItem} ${
                isActive
                  ? "bg-white text-blue-700 font-semibold"
                  : "text-white"
              }`
            }
          >
            Tổng quan
          </NavLink>

          <NavLink
            to="/admin/employees"
            className={({ isActive }) =>
              `${menuItem} ${
                isActive
                  ? "bg-white text-blue-700 font-semibold"
                  : "text-white"
              }`
            }
          >
            Nhân viên
          </NavLink>

          <NavLink
            to="/admin/revenue"
            className={({ isActive }) =>
              `${menuItem} ${
                isActive
                  ? "bg-white text-blue-700 font-semibold"
                  : "text-white"
              }`
            }
          >
            Doanh thu
          </NavLink>
        </nav>
      </aside>

      {/* MAIN CONTENT */}
      <main className="flex-1 p-6">
        <Outlet />
      </main>
    </div>
  );
}
