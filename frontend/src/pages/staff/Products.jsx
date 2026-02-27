import { useState } from "react";

export default function Products() {
  const [open, setOpen] = useState(false);
  const [images, setImages] = useState([]);

  const [hasColor, setHasColor] = useState(false);
  const [hasSize, setHasSize] = useState(false);

  const sizes = ["S", "M", "L", "XL", "XXL"];
  const [selectedSizes, setSelectedSizes] = useState([]);

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
          <div className="bg-white w-[900px] rounded-2xl p-8 shadow-xl">

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
                  className="mb-4"
                />

                <div className="grid grid-cols-3 gap-3">
                  {images.map((img, index) => (
                    <div key={index} className="relative">
                      <img
                        src={img.url}
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
                    className="w-full border rounded-lg p-2 mt-1"
                  />
                </div>

                {/* PRICE */}
                <div>
                  <label className="block font-semibold text-gray-700">
                    Giá
                  </label>
                  <input
                    type="number"
                    className="w-full border rounded-lg p-2 mt-1"
                  />
                </div>

                {/* COLOR */}
                <label className="flex items-center gap-2">
                  <input
                    type="checkbox"
                    checked={hasColor}
                    onChange={() => setHasColor(!hasColor)}
                  />
                  Có màu sắc
                </label>

                {hasColor && (
                  <input
                    placeholder="Nhập màu (vd: Đen, Trắng...)"
                    className="w-full border rounded-lg p-2"
                  />
                )}

                {/* SIZE */}
                <label className="flex items-center gap-2">
                  <input
                    type="checkbox"
                    checked={hasSize}
                    onChange={() => setHasSize(!hasSize)}
                  />
                  Có size
                </label>

                {hasSize && (
                  <div className="flex gap-2 flex-wrap">
                    {sizes.map((size) => (
                      <button
                        key={size}
                        onClick={() => toggleSize(size)}
                        type="button"
                        className={`px-3 py-1 rounded-lg border
                        ${
                          selectedSizes.includes(size)
                            ? "bg-blue-600 text-white"
                            : "bg-gray-100"
                        }`}
                      >
                        {size}
                      </button>
                    ))}
                  </div>
                )}

                {/* DESCRIPTION */}
                <div>
                  <label className="block font-semibold text-gray-700">
                    Mô tả sản phẩm
                  </label>
                  <textarea
                    rows={4}
                    className="w-full border rounded-lg p-2 mt-1"
                  />
                </div>

              </div>
            </div>

            {/* BUTTONS */}
            <div className="flex justify-end gap-4 mt-8">

              {/* SAVE */}
              <button
                className="bg-green-600 hover:bg-green-700 text-white px-8 py-3 rounded-xl font-semibold"
              >
                Lưu
              </button>

              {/* CANCEL */}
              <button
                onClick={() => setOpen(false)}
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