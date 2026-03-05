import { Link } from "react-router-dom";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCartOutlined";
import SearchIcon from "@mui/icons-material/Search";
import PersonOutlineIcon from "@mui/icons-material/PersonOutline";
import LocationOnOutlinedIcon from "@mui/icons-material/LocationOnOutlined";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import logo from "../assets/logo.jpg"; // Đảm bảo logo của bạn là bản màu đen trắng

export default function Navbar() {
  return (
    <nav className="fixed top-0 left-0 right-0 z-50 bg-white">
      {/* Top Bar - Chứa Logo, Search và Icons */}
      <div className="bg-black text-white h-24">
        <div className="max-w-[1400px] mx-auto h-full px-8 flex items-center justify-between">
          {/* 1. Logo */}
          <Link to="/" className="flex items-center">
            <img src={logo} alt="HPT Store" className="h-25" />
            {/* Thêm 'invert' nếu file gốc là màu đen để biến thành trắng */}
          </Link>

          {/* 2. Search Box */}
          <div className="flex-1 max-w-2xl mx-10 relative">
            <input
              type="text"
              placeholder="Bạn đang tìm gì..."
              className="w-full bg-white text-black py-3 px-5 rounded-sm focus:outline-none placeholder:text-gray-400"
            />
            <button className="absolute right-0 top-0 bottom-0 bg-black border border-white px-6 flex items-center justify-center hover:bg-gray-800 transition-colors">
              <SearchIcon className="text-white scale-125" />
            </button>
          </div>

          {/* 3. Action Icons */}
          <div className="flex items-center gap-8">
            <div className="flex flex-col items-center cursor-pointer group">
              <LocationOnOutlinedIcon className="scale-110" />
              <span className="text-xs mt-1 font-medium group-hover:underline">
                Cửa hàng
              </span>
            </div>

            <Link
              to="/login"
              className="flex flex-col items-center cursor-pointer group text-white"
            >
              <PersonOutlineIcon className="scale-110" />
              <span className="text-xs mt-1 font-medium group-hover:underline">
                Đăng nhập
              </span>
            </Link>

            <div className="flex flex-col items-center cursor-pointer relative group">
              <ShoppingCartIcon className="scale-110" />
              <span className="text-xs mt-1 font-medium group-hover:underline">
                Giỏ hàng
              </span>
              <span className="absolute -top-1 -right-1 bg-white text-black font-bold rounded-full text-[10px] w-4 h-4 flex items-center justify-center">
                0
              </span>
            </div>
          </div>
        </div>
      </div>

      {/* Bottom Bar - Menu điều hướng */}
      <div className="bg-white border-b border-gray-200">
        <div className="max-w-[1400px] mx-auto px-8">
          <ul className="flex items-center justify-center gap-8 py-3 text-[13px] font-bold text-black uppercase tracking-wider">
            {/* Hàng mới với nhãn New */}
            <li className="relative cursor-pointer hover:text-red-600 flex items-center gap-1">
              🔍 HÀNG MỚI
              <span className="absolute -top-3 -right-3 text-[10px] text-red-600 font-bold italic">
                New
              </span>
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              SẢN PHẨM <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              ÁO NAM <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              QUẦN NAM <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              PHỤ KIỆN <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="cursor-pointer hover:text-red-600">ĐỒ CÔNG SỞ</li>

            {/* Outlet với nhãn -50% */}
            <li className="relative cursor-pointer text-red-600 font-extrabold">
              OUTLET
              <span className="absolute -top-4 left-0 w-full text-center text-[10px] text-red-600">
                -50%
              </span>
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              COLLECTION <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="group cursor-pointer hover:text-red-600 flex items-center">
              JEANS <KeyboardArrowDownIcon fontSize="small" />
            </li>

            <li className="cursor-pointer hover:text-red-600">
              TIN THỜI TRANG
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}
