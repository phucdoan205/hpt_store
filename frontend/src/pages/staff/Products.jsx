import { useState } from "react";
import axios from "axios";

export default function Products() {
  const [open, setOpen] = useState(false);
  const [images, setImages] = useState([]);

  const [hasColor, setHasColor] = useState(false);
  const [hasSize, setHasSize] = useState(false);

  const sizes = ["S", "M", "L", "XL", "XXL"];
  const [selectedSizes, setSelectedSizes] = useState([]);

  /* ================= PRODUCT FORM ================= */

  const [form, setForm] = useState({
    name: "",
    description: "",
    price: "",
    categoryId: "",
    color: "",
  });

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  /* ================= RESET ================= */

  const resetForm = () => {
    setImages([]);
    setHasColor(false);
    setHasSize(false);
    setSelectedSizes([]);
    setForm({
      name: "",
      description: "",
      price: "",
      categoryId: "",
      color: "",
    });
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

  /* ================= SAVE PRODUCT ================= */

  const handleSave = async () => {
    try {
      const formData = new FormData();

      formData.append("Name", form.name);
      formData.append(
        "Slug",
        form.name.toLowerCase().replace(/\s+/g, "-")
      );
      formData.append("Description", form.description);
      formData.append("Price", form.price);
      formData.append("CategoryId", form.categoryId);

      // chỉ lấy ảnh đầu làm thumbnail
      if (images.length > 0) {
        formData.append("ThumbnailFile", images[0].file);
      }

      await axios.post(
        "https://localhost:5001/api/products",
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      alert("✅ Tạo sản phẩm thành công");

      resetForm();
      setOpen(false);
    } catch (err) {
      console.error(err);
      alert("❌ Lỗi tạo sản phẩm");
    }
  };

  /* ================= STYLE ================= */

  const inputStyle =
    "w-full border-2 border-gray-300 rounded-lg p-2 mt-1 text-black bg-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-600 focus:border-blue-600";

  const checkboxStyle = "w-4 h-4 accent-blue-600 cursor-pointer";

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

      {/* DEMO LIST */}
      <ul className="bg-white rounded-xl shadow p-4 space-y-2">
        <li>Áo thun</li>
        <li>Giày</li>
      </ul>

      {/* ================= OVERLAY ================= */}
      {open && (
        <div className="fixed inset-0 bg-black/50 flex justify-center items-center z-50">
          <div className="bg-white w-[900px] max-h-[90vh] overflow-y-auto rounded-2xl p-8 shadow-xl">

            <div className="grid grid-cols-2 gap-8">

              {/* IMAGE */}
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

                <div className="grid grid-cols-3 gap-3 max-h-[350px] overflow-y-auto">
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

              {/* FORM */}
              <div className="space-y-4">

                <div>
                  <label className="font-semibold text-gray-700">
                    Tên sản phẩm
                  </label>
                  <input
                    name="name"
                    value={form.name}
                    onChange={handleChange}
                    className={inputStyle}
                  />
                </div>

                <div>
                  <label className="font-semibold text-gray-700">
                    Giá
                  </label>
                  <input
                    type="number"
                    name="price"
                    value={form.price}
                    onChange={handleChange}
                    className={inputStyle}
                  />
                </div>

                <div>
                  <label className="font-semibold text-gray-700">
                    CategoryId
                  </label>
                  <input
                    type="number"
                    name="categoryId"
                    value={form.categoryId}
                    onChange={handleChange}
                    className={inputStyle}
                  />
                </div>

                {/* COLOR */}
                <label className="flex gap-2 text-gray-700">
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
                    name="color"
                    value={form.color}
                    onChange={handleChange}
                    className={inputStyle}
                  />
                )}

                {/* SIZE */}
                <label className="flex gap-2 text-gray-700">
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
                          className={`px-4 py-1 rounded-lg border-2 ${
                            active
                              ? "bg-blue-600 text-white border-blue-600"
                              : "bg-gray-100 text-gray-700"
                          }`}
                        >
                          {size}
                        </button>
                      );
                    })}
                  </div>
                )}

                <div>
                  <label className="font-semibold text-gray-700">
                    Mô tả
                  </label>
                  <textarea
                    rows={4}
                    name="description"
                    value={form.description}
                    onChange={handleChange}
                    className={inputStyle}
                  />
                </div>

              </div>
            </div>

            {/* BUTTON */}
            <div className="flex justify-end gap-4 mt-8">
              <button
                onClick={handleSave}
                className="bg-green-600 hover:bg-green-700 text-white px-8 py-3 rounded-xl font-semibold"
              >
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