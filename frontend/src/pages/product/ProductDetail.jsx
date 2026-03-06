import React, { useState } from "react";
import { useParams } from "react-router-dom";
import AddIcon from "@mui/icons-material/Add";
import RemoveIcon from "@mui/icons-material/Remove";
import StarIcon from "@mui/icons-material/Star";

const ProductDetail = () => {
  const { id } = useParams(); // Lấy ID từ URL
  const [quantity, setQuantity] = useState(1);
  const [selectedSize, setSelectedSize] = useState("S");

  // Giả lập dữ liệu (Thực tế bạn sẽ fetch từ API theo id)
  const product = {
    name: "Quần Short Kaki Nam Túi Hộp ICONDENIM Utility Cargo",
    price: 350000,
    status: "Còn Hàng",
    sku: "QSID0180-01",
    rating: 112,
    colors: ["Be", "Rêu", "Đen"],
    sizes: ["S", "M", "L", "XL"],
    images: ["https://bizweb.dktcdn.net/100/287/440/products/ao-thun-local-brand-dep-mau-den-1.jpg?v=1648723476070"],
  };

  const vouchers = [
    { code: "MAR20", text: "GIẢM 20K đơn từ 299K" },
    { code: "MAR50", text: "GIẢM 50K đơn từ 699K" },
    { code: "MAR80", text: "GIẢM 80K đơn từ 999K" },
  ];

  return (
    <div className="max-w-[1200px] mx-auto px-4 py-10">
      {/* Breadcrumb */}
      <div className="text-xs text-gray-500 mb-6 uppercase tracking-widest">
        Trang chủ / Tất cả sản phẩm / {product.name}
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-12">
        {/* Bên trái: Hình ảnh */}
        <div className="space-y-4">
          <div className="aspect-[3/4] bg-gray-100 overflow-hidden rounded-sm">
            <img
              src={product.images[0]}
              alt=""
              className="w-full h-full object-cover"
            />
          </div>
        </div>

        {/* Bên phải: Thông tin */}
        <div className="flex flex-col">
          <h1 className="text-2xl font-bold uppercase mb-2">{product.name}</h1>

          <div className="flex items-center gap-4 mb-4">
            <span className="bg-green-100 text-green-700 text-[10px] font-bold px-2 py-1 rounded">
              {product.status}
            </span>
            <div className="flex items-center text-yellow-400">
              {[...Array(5)].map((_, i) => (
                <StarIcon key={i} sx={{ fontSize: 16 }} />
              ))}
              <span className="text-gray-400 text-xs ml-2">
                ({product.rating} đánh giá)
              </span>
            </div>
          </div>

          <div className="text-sm text-gray-500 mb-4">
            Loại: <span className="text-black font-bold">Quần Short</span> |
            MSP: <span className="text-black font-bold">{product.sku}</span>
          </div>

          <div className="text-2xl font-black text-black mb-6">
            {product.price.toLocaleString()}₫
          </div>

          {/* Khuyến mãi Box */}
          <div className="border-2 border-dashed border-gray-200 p-4 rounded-md mb-6 bg-gray-50">
            <div className="flex items-center gap-2 font-bold text-sm mb-3">
              🎁 KHUYẾN MÃI - ONLY ONLINE
            </div>
            <div className="space-y-2">
              {vouchers.map((v, i) => (
                <div key={i} className="text-xs flex items-center gap-2">
                  <span className="font-bold text-red-600">
                    Nhập mã {v.code}
                  </span>{" "}
                  {v.text}
                </div>
              ))}
            </div>
          </div>

          {/* Chọn Size */}
          <div className="mb-6">
            <div className="text-sm font-bold mb-3 uppercase">
              Kích thước: {selectedSize}
            </div>
            <div className="flex gap-2">
              {product.sizes.map((size) => (
                <button
                  key={size}
                  onClick={() => setSelectedSize(size)}
                  className={`w-10 h-10 border flex items-center justify-center text-xs font-bold transition-all
                    ${selectedSize === size ? "border-black bg-black text-white" : "border-gray-300 hover:border-black"}`}
                >
                  {size}
                </button>
              ))}
            </div>
          </div>

          {/* Số lượng và Nút mua */}
          <div className="flex flex-col gap-4 mt-auto">
            <div className="flex items-center border border-gray-300 w-fit">
              <button
                onClick={() => setQuantity(Math.max(1, quantity - 1))}
                className="p-2 border-r"
              >
                <RemoveIcon sx={{ fontSize: 16 }} />
              </button>
              <span className="px-6 font-bold">{quantity}</span>
              <button
                onClick={() => setQuantity(quantity + 1)}
                className="p-2 border-l"
              >
                <AddIcon sx={{ fontSize: 16 }} />
              </button>
            </div>

            <div className="grid grid-cols-2 gap-4">
              <button className="bg-black text-white py-4 font-bold uppercase hover:bg-zinc-800 transition-all">
                Thêm vào giỏ
              </button>
              <button className="border-2 border-black py-4 font-bold uppercase hover:bg-black hover:text-white transition-all">
                Mua ngay
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductDetail;
