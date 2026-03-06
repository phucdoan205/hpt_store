import React from "react";
import PhoneInTalkIcon from "@mui/icons-material/PhoneInTalk";
import EmailIcon from "@mui/icons-material/Email";
import AccessTimeIcon from "@mui/icons-material/AccessTime";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import HeadphonesIcon from "@mui/icons-material/Headphones";
import { Link } from "react-router-dom";

const Footer = () => {
  return (
    <footer className="bg-black text-white pt-12 pb-6 mt-20">
      <div className="container mx-auto px-4 lg:px-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-10 border-b border-zinc-800 pb-12">
          {/* Cột 1: Giới thiệu */}
          <div>
            <h3 className="font-bold text-lg mb-6 uppercase tracking-wider">
              Giới thiệu
            </h3>
            <p className="text-sm font-medium mb-4 leading-relaxed">
              160STORE - Chuỗi Phân Phối Thời Trang Nam Chuẩn Hiệu
            </p>
            <ul className="space-y-4 text-sm text-gray-300">
              <li className="flex items-center gap-3">
                <PhoneInTalkIcon sx={{ fontSize: 20 }} /> 02871006789
              </li>
              <li className="flex items-center gap-3">
                <EmailIcon sx={{ fontSize: 20 }} /> cs@160store.com
              </li>
              <li className="flex items-center gap-3">
                <AccessTimeIcon sx={{ fontSize: 20 }} /> Giờ mở cửa : 08:30 -
                22:00
              </li>
              <li className="flex items-center gap-3 leading-tight">
                <HeadphonesIcon sx={{ fontSize: 20 }} />
                <span>
                  Nhân viên tư vấn phản hồi tin nhắn đến 24:00 (Mỗi ngày)
                </span>
              </li>
            </ul>
            <div className="mt-6 flex gap-3">
              <img
                src="https://pub-8438186178824133.r2.dev/da-thong-bao.png"
                alt="Bộ Công Thương"
                className="h-12"
              />
              <img
                src="https://pub-8438186178824133.r2.dev/dmca.png"
                alt="DMCA"
                className="h-12"
              />
            </div>
          </div>

          {/* Cột 2: Chính sách */}
          <div>
            <h3 className="font-bold text-lg mb-6 uppercase tracking-wider">
              Chính sách
            </h3>
            <ul className="space-y-4 text-sm font-medium text-gray-300 uppercase italic">
              <li>
                <Link
                  to="/"
                  className="hover:text-red-600 tracking-tighter transition-colors underline decoration-zinc-700 underline-offset-4"
                >
                  • Hướng dẫn đặt hàng
                </Link>
              </li>
              <li>
                <Link
                  to="/"
                  className="hover:text-red-600 tracking-tighter transition-colors underline decoration-zinc-700 underline-offset-4"
                >
                  • Chính sách
                </Link>
              </li>
            </ul>
          </div>

          {/* Cột 3: Địa chỉ cửa hàng */}
          <div>
            <h3 className="font-bold text-lg mb-6 uppercase tracking-wider">
              Địa chỉ cửa hàng (22 CH)
            </h3>
            <div className="space-y-6">
              <div className="relative">
                <div className="flex items-start gap-2">
                  <LocationOnIcon
                    className="mt-1 text-gray-400"
                    sx={{ fontSize: 20 }}
                  />
                  <div>
                    <p className="font-bold text-sm uppercase">
                      Hồ Chí Minh (11 CH){" "}
                      <span className="text-red-600 italic text-[10px] ml-1">
                        New
                      </span>
                    </p>
                    <p className="text-xs text-gray-400 mt-1">
                      274 Ba Cu, Phường Vũng Tàu, TP.HCM
                    </p>
                  </div>
                </div>
              </div>

              <div className="flex items-start gap-2">
                <LocationOnIcon
                  className="mt-1 text-gray-400"
                  sx={{ fontSize: 20 }}
                />
                <div>
                  <p className="font-bold text-sm uppercase">Hà Nội (2 CH)</p>
                  <p className="text-xs text-gray-400 mt-1">
                    Số 26 Phố Lê Đại Hành, Phường Hai Bà Trưng, TP.Hà Nội
                  </p>
                </div>
              </div>

              <div className="flex items-start gap-2">
                <LocationOnIcon
                  className="mt-1 text-gray-400"
                  sx={{ fontSize: 20 }}
                />
                <div>
                  <p className="font-bold text-sm uppercase">Cần Thơ (2 CH)</p>
                  <p className="text-xs text-gray-400 mt-1">
                    Số 35 Trần Phú, Phường Ninh Kiều, TP.Cần Thơ
                  </p>
                </div>
              </div>

              <button className="text-xs font-black uppercase underline underline-offset-4 hover:text-red-600 transition-colors">
                Xem tất cả cửa hàng
              </button>
            </div>
          </div>

          {/* Cột 4: Phương thức thanh toán */}
          <div>
            <h3 className="font-bold text-lg mb-6 uppercase tracking-wider">
              Phương thức thanh toán
            </h3>
            <div className="flex flex-wrap gap-4 items-center grayscale opacity-80 hover:grayscale-0 hover:opacity-100 transition-all">
              <span className="bg-white px-2 py-1 rounded text-black font-black italic text-xs">
                SPay
              </span>
              <span className="bg-white px-2 py-1 rounded text-blue-700 font-black text-xs">
                VNPAY
              </span>
              <span className="bg-white px-2 py-1 rounded text-black font-black text-xs">
                COD
              </span>
            </div>

            {/* Phone Floating Icon (Optional - as seen in photo) */}
            <div className="mt-16 flex justify-end">
              <div className="w-12 h-12 bg-white rounded-full flex items-center justify-center cursor-pointer shadow-lg animate-bounce">
                <PhoneInTalkIcon className="text-black" />
              </div>
            </div>
          </div>
        </div>

        {/* Bản quyền */}
        <div className="text-center mt-8">
          <p className="text-[10px] lg:text-xs font-bold uppercase tracking-[0.2em] text-gray-500">
            Bản quyền thuộc về 160store
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
