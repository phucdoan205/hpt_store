import { Link } from "react-router-dom";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import SearchIcon from "@mui/icons-material/Search";
import PersonIcon from "@mui/icons-material/Person";

export default function Navbar() {
  return (
    <nav className="fixed top-0 left-0 right-0 z-50 bg-white shadow">
      <div className="h-16 max-w-7xl mx-auto px-8 flex items-center justify-between">

        {/* Logo */}
        <Link to="/" className="font-bold text-2xl text-blue-600">
          HPT STORE
        </Link>

        {/* Menu */}
        <ul className="flex gap-8 font-medium text-blue-600">
          <li className="hover:text-blue-700 cursor-pointer">
            HÀNG MỚI
          </li>

          <li className="group relative">
            <Link to="/products" className="hover:text-blue-700">
              SẢN PHẨM
            </Link>

            <div className="absolute top-full left-0 hidden group-hover:block bg-white shadow-lg w-40 text-black">
              <p className="p-2 hover:bg-gray-100">Áo Nam</p>
              <p className="p-2 hover:bg-gray-100">Quần Nam</p>
            </div>
          </li>

          <li className="hover:text-blue-700 cursor-pointer">
            BẢN TIN
          </li>
        </ul>

        {/* Search + User */}
        <div className="flex items-center gap-5 text-blue-600">
          <div className="relative">
            <input
              type="text"
              placeholder="Tìm kiếm..."
              className="border rounded-full px-4 py-1 focus:outline-none"
            />
            <SearchIcon className="absolute right-2 top-1 text-blue-600" />
          </div>

          <Link to="/login" className="flex items-center gap-1 hover:text-blue-700">
            <PersonIcon />
            Đăng nhập
          </Link>

          <Link to="/register" className="hover:text-blue-700">
            Đăng ký
          </Link>

          <div className="relative text-blue-600">
            <ShoppingCartIcon />
            <span className="absolute -top-2 -right-2 bg-red-500 text-white rounded-full text-xs px-1.5">
              0
            </span>
          </div>
        </div>

      </div>
    </nav>
  );
}
