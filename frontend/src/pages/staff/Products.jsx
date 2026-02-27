import { useState } from "react";

export default function Products() {
  const [open, setOpen] = useState(false);
  const [images, setImages] = useState([]);

  const [hasColor, setHasColor] = useState(false);
  const [hasSize, setHasSize] = useState(false);

  const sizes = ["S", "M", "L", "XL", "XXL"];
  const [selectedSizes, setSelectedSizes] = useState([]);

  const resetForm = () => {
  setImages([]);
  setHasColor(false);
  setHasSize(false);
  setSelectedSizes([]);
};

  /* ================= IMAGE ================= */

  const handleUpload = (e) => {
    const files = Array.from(e.target.files);

    const preview = files.map((file) => ({
      file,
      url: URL.createObjectURL(file),
    }));

    setImages((prev) => [...prev, ...preview]);
  };

  const removeImage = (index) => {
    setImages(images.filter((_, i) => i !== index));
  };

  /* ================= SIZE ================= */

  const toggleSize = (size) => {
    setSelectedSizes((prev) =>
      prev.includes(size)
        ? prev.filter((s) => s !== size)
        : [...prev, size]
    );
  };

  /* ================= INPUT STYLE ================= */

  const inputStyle =
    "w-full border-2 border-gray-300 rounded-lg p-2 mt-1 " +
    "text-black bg-white placeholder-gray-400 " +
    "focus:outline-none focus:ring-2 focus:ring-blue-600 focus:border-blue-600";

  const checkboxStyle =
    "w-4 h-4 accent-blue-600 cursor-pointer";

  return (
    <div className="p-6">
      {/* HEADER */}
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-gray-800">
          Quản lý sản phẩm
        </h2>

        <button
          onClick={() => setOpen(true)}
          className="bg-blue-600 hover:bg-blue-700 text-white px-5 py-2 rounded-lg font-semibold"
        >
          + Thêm sản phẩm
        </button>
      </div>

      {/* LIST DEMO */}
      <ul className="bg-white rounded-xl shadow p-4 space-y-2">
        <li>Áo thun</li>
        <li>Giày</li>
      </ul>

      {/* ================= OVERLAY ================= */}
      {open && (
        <div className="fixed inset-0 bg-black/50 flex justify-center items-center z-50">

          {/* ✅ KHUNG CÓ SCROLL */}
          <div className="bg-white w-[900px] max-h-[90vh] overflow-y-auto rounded-2xl p-8 shadow-xl">

            <div className="grid grid-cols-2 gap-8">

              {/* LEFT — IMAGE */}
              <div>
                <h3 className="font-semibold mb-3 text-gray-700">
                  Hình ảnh
                </h3>

                <input
                  type="file"
                  multiple
                  onChange={handleUpload}
                  className="mb-4 border p-2 rounded-lg w-full text-black"
                />

                {/* ✅ KHUNG ẢNH CÓ SCROLL */}
                <div className="grid grid-cols-3 gap-3 max-h-[350px] overflow-y-auto pr-2">
                  {images.map((img, index) => (
                    <div key={index} className="relative">
                      <img
                        src={img.url}
                        alt=""
                        className="w-full h-24 object-cover rounded-lg border"
                      />

                      <button
                        onClick={() => removeImage(index)}
                        className="absolute top-1 right-1 bg-red-500 text-white text-xs px-2 rounded"
                      >
                        ✕
                      </button>
                    </div>
                  ))}
                </div>
              </div>

              {/* RIGHT — FORM */}
              <div className="space-y-4">

                {/* NAME */}
                <div>
                  <label className="block font-semibold text-gray-700">
                    Tên sản phẩm
                  </label>
                  <input
                    type="text"
                    placeholder="Nhập tên sản phẩm..."
                    className={inputStyle}
                  />
                </div>

                {/* PRICE */}
                <div>
                  <label className="block font-semibold text-gray-700">
                    Giá
                  </label>
                  <input
                    type="number"
                    placeholder="Nhập giá sản phẩm..."
                    className={inputStyle}
                  />
                </div>

                {/* COLOR */}
                <label className="flex items-center gap-2 text-gray-700 font-medium">
                  <input
                    type="checkbox"
                    checked={hasColor}
                    onChange={() => setHasColor(!hasColor)}
                    className={checkboxStyle}
                  />
                  Màu
                </label>

                {hasColor && (
                  <input
                    placeholder="Nhập màu (vd: Đen, Trắng...)"
                    className={inputStyle}
                  />
                )}

                {/* SIZE */}
                <label className="flex items-center gap-2 text-gray-700 font-medium">
                  <input
                    type="checkbox"
                    checked={hasSize}
                    onChange={() => setHasSize(!hasSize)}
                    className={checkboxStyle}
                  />
                  Size
                </label>

                {hasSize && (
                  <div className="flex gap-2 flex-wrap">
                    {sizes.map((size) => {
                      const active = selectedSizes.includes(size);

                      return (
                        <button
                          key={size}
                          type="button"
                          onClick={() => toggleSize(size)}
                          className={`px-4 py-1 rounded-lg border-2 transition
                          ${
                            active
                              ? "bg-blue-600 text-white border-blue-600"
                              : "bg-gray-100 text-gray-700 border-gray-300 hover:border-blue-400"
                          }`}
                        >
                          {size}
                        </button>
                      );
                    })}
                  </div>
                )}

                {/* DESCRIPTION */}
                <div>
                  <label className="block font-semibold text-gray-700">
                    Mô tả sản phẩm
                  </label>
                  <textarea
                    rows={4}
                    placeholder="Nhập mô tả sản phẩm..."
                    className={inputStyle}
                  />
                </div>

              </div>
            </div>

            {/* BUTTONS */}
            <div className="flex justify-end gap-4 mt-8">
              <button className="bg-green-600 hover:bg-green-700 text-white px-8 py-3 rounded-xl font-semibold">
                Lưu
              </button>

              <button
                onClick={() => {
                  resetForm();
                  setOpen(false);
                }}
                className="bg-red-600 hover:bg-red-700 text-white px-8 py-3 rounded-xl font-semibold"
              >
                Huỷ
              </button>
            </div>

          </div>
        </div>
      )}
    </div>
  );
}