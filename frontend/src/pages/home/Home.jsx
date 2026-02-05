import Navbar from "../../components/Navbar";
import ChatBot from "../../components/ChatBot";

export default function Home() {
  return (
    <>
      <Navbar />
      <ChatBot />

        <main className="pt-16">
        <div className="max-w-7xl mx-auto p-6">
          <h1 className="text-gray-600 mt-2 font-bold">HPT STORE</h1>
          <p className="text-gray-600 mt-2">
            Trang bán hàng cho khách
          </p>
        </div>
      </main>
    </>
  );
}

