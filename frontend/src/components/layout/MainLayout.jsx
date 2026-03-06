import React from "react";
import { Outlet, useLocation } from "react-router-dom";
import Navbar from "../Navbar";
import Footer from "../Footer";
import ChatBot from "../ChatBot"; // Nếu bạn đã có file này

const MainLayout = () => {
  const location = useLocation();

  // Kiểm tra xem người dùng có đang ở trang Quản trị (Admin) hay Nhân viên (Staff) không
  // Nếu đường dẫn bắt đầu bằng /admin hoặc /staff thì sẽ ẩn Chatbot
  const isManagementPage =
    location.pathname.startsWith("/admin") ||
    location.pathname.startsWith("/staff");
  return (
    <>
      <div className="flex flex-col min-h-screen">
        {/* Header cố định phía trên */}
        <Navbar />

        {/* Phần nội dung chính: 
        - pt-[128px]: Padding-top để không bị Navbar (fixed) che mất nội dung.
        - flex-grow: Đẩy Footer xuống đáy trang nếu nội dung ngắn.
      */}
        <main className="flex-grow pt-[100px] lg:pt-[136px]">
          <Outlet />
        </main>

        {/* Nút Chat hoặc Hotline lơ lửng (nếu có) */}
        <ChatBot />

        {/* Footer ở cuối cùng */}
        <Footer />
      </div>

      {/* Chỉ hiển thị Chatbot nếu KHÔNG PHẢI là trang quản lý */}
      {!isManagementPage && <ChatBot />}
    </>
  );
};

export default MainLayout;
