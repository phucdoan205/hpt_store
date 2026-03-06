import React from "react";
import ShoppingBagOutlinedIcon from "@mui/icons-material/ShoppingBagOutlined";
import { Link } from "react-router-dom";

const Home = () => {
  // Dữ liệu mẫu dựa trên ảnh bạn gửi
  const products = [
    {
      id: 1,
      name: "Quần Short Kaki Nam Túi Hộp ICONDENIM Utility Cargo",
      price: 350000,
      image:
        "https://cbhbhhyucmziqupasuqf.supabase.co/storage/v1/object/public/products/ao_hoodie/ao_hoodie_banana/vn-11134207-7ras8-m2287h84n9i6ec.webp", // Thay bằng link ảnh thật của bạn
      isNew: true,
      voucher: "Voucher 20K",
    },
    {
      id: 2,
      name: "Áo Sơ Mi Nam ICONDENIM Steel Line",
      price: 379000,
      image:
        "https://cbhbhhyucmziqupasuqf.supabase.co/storage/v1/object/public/products/ao_khoac/ao_khoac_jean/vn-11134207-820l4-mgx5wpp63pxl0b.webp",
      isNew: true,
      voucher: "Voucher 20K",
    },
    {
      id: 3,
      name: "Thắt Lưng Nam ICONDENIM Vincent",
      price: 399000,
      image:
        "https://cbhbhhyucmziqupasuqf.supabase.co/storage/v1/object/public/products/ao_khoac/ao_khoac_jean/vn-11134207-820l4-mgx5wpp9njm4d2.webp",
      isNew: true,
      voucher: "Voucher 20K",
    },
    {
      id: 4,
      name: "Quần Jeans Nam ICONDENIM Elevyn",
      price: 599000,
      image:
        "https://cbhbhhyucmziqupasuqf.supabase.co/storage/v1/object/public/products/ao_somi/ao_somi_ongrong/sg-11134201-7qvdm-lk8zlrnc3xcn8a.webp",
      isNew: true,
      voucher: "Voucher 20K",
    },
  ];

  return (
    <main className="min-h-screen bg-white">
      {/* Hero Section */}
      <section className="relative h-[60vh] lg:h-[80vh] bg-gray-200">
        <img
          src="https://insieutoc.vn/wp-content/uploads/2021/02/mau-banner-quang-cao-khuyen-mai.jpg"
          className="w-full h-full object-cover"
          alt="Banner"
        />
        <div className="absolute inset-0 flex flex-col justify-center items-start px-6 lg:px-24 bg-black/10">
          <h2 className="text-white text-4xl lg:text-6xl font-black mb-4 uppercase tracking-tighter">
            New Collection <br /> 2024
          </h2>
          <button className="bg-white text-black px-10 py-4 font-bold hover:bg-black hover:text-white transition-all duration-300 transform hover:scale-105">
            MUA NGAY
          </button>
        </div>
      </section>

      {/* Product List Section */}
      <section className="max-w-[1400px] mx-auto py-16 px-4">
        <h2 className="text-2xl font-black text-center mb-12 uppercase tracking-[0.3em] text-gray-900">
          Sản phẩm mới nhất
        </h2>

        {/* Grid hiển thị sản phẩm */}
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-x-4 gap-y-10">
          {products.map((product) => (
            <div key={product.id} className="group cursor-pointer">
              <Link
                to={`/product/${product.id}`}
                key={product.id}
                className="group cursor-pointer block"
              >
                {/* Giữ nguyên phần UI Image và Info của bạn ở đây */}
                <div className="relative overflow-hidden bg-gray-100 mb-4 aspect-[3/4]">
                  <img
                    src={product.image}
                    className="w-full h-full object-cover"
                  />
                </div>
                <h3 className="text-[13px] font-medium">{product.name}</h3>
                <p className="font-black">{product.price.toLocaleString()}₫</p>
              </Link>

              {/* Product Info */}
              <div className="space-y-2 px-1">
                <h3 className="text-[13px] font-medium text-gray-800 leading-tight line-clamp-2 h-8">
                  {product.name}
                </h3>

                {/* Tags */}
                <div className="flex flex-wrap gap-2">
                  {product.isNew && (
                    <span className="text-[10px] border border-gray-300 px-2 py-0.5 rounded-sm font-bold uppercase">
                      Hàng Mới
                    </span>
                  )}
                  {product.voucher && (
                    <span className="text-[10px] bg-yellow-400 px-2 py-0.5 rounded-sm font-black uppercase">
                      {product.voucher}
                    </span>
                  )}
                </div>

                {/* Price */}
                <p className="text-sm font-black text-black">
                  {product.price.toLocaleString("vi-VN")}₫
                </p>
              </div>
            </div>
          ))}
        </div>
      </section>
    </main>
  );
};

export default Home;
