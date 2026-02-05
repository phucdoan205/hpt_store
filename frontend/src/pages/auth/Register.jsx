import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { registerApi } from "../../services/authService";

export default function Register() {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    address: "",
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = async (e) => {
    e.preventDefault();

    if (form.password !== form.confirmPassword) {
      alert("Mật khẩu không khớp");
      return;
    }

    const res = await registerApi(form);

    if (res.success) {
      alert("Đăng ký thành công!");
      navigate("/login");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-white">
      {/* KHUNG NGOÀI */}
      <div className="w-full max-w-md border-2 border-blue-700 p-8 rounded-xl">

        {/* TITLE */}
        <h2 className="text-2xl font-bold text-center mb-6 text-blue-700">
          ĐĂNG KÝ TÀI KHOẢN
        </h2>

        <form onSubmit={submit} className="flex flex-col gap-4">

          {/* USERNAME */}
          <input
            name="username"
            placeholder="Tên người dùng"
            className="
              w-full
              border-2 border-blue-700
              rounded-lg
              px-4 py-2
              text-blue-700
              placeholder-blue-700/60
              focus:outline-none
              focus:ring-2
              focus:ring-blue-700
            "
            onChange={handleChange}
            required
          />

          {/* EMAIL */}
          <input
            name="email"
            type="email"
            placeholder="Email"
            className="
              w-full
              border-2 border-blue-700
              rounded-lg
              px-4 py-2
              text-blue-700
              placeholder-blue-700/60
              focus:outline-none
              focus:ring-2
              focus:ring-blue-700
            "
            onChange={handleChange}
            required
          />

          {/* PASSWORD */}
          <input
            name="password"
            type="password"
            placeholder="Mật khẩu"
            className="
              w-full
              border-2 border-blue-700
              rounded-lg
              px-4 py-2
              text-blue-700
              placeholder-blue-700/60
              focus:outline-none
              focus:ring-2
              focus:ring-blue-700
            "
            onChange={handleChange}
            required
          />

          {/* CONFIRM PASSWORD */}
          <input
            name="confirmPassword"
            type="password"
            placeholder="Nhập lại mật khẩu"
            className="
              w-full
              border-2 border-blue-700
              rounded-lg
              px-4 py-2
              text-blue-700
              placeholder-blue-700/60
              focus:outline-none
              focus:ring-2
              focus:ring-blue-700
            "
            onChange={handleChange}
            required
          />

          {/* ADDRESS */}
          <textarea
            name="address"
            placeholder="Địa chỉ"
            rows={2}
            className="
              w-full
              border-2 border-blue-700
              rounded-lg
              px-4 py-2
              text-blue-700
              placeholder-blue-700/60
              focus:outline-none
              focus:ring-2
              focus:ring-blue-700
            "
            onChange={handleChange}
          />

          {/* REGISTER BUTTON */}
          <button
            type="submit"
            className="
              w-full
              mt-2
              bg-blue-700
              text-white
              font-semibold
              py-2
              rounded-lg
            "
          >
            Đăng ký
          </button>
        </form>

        {/* LOGIN LINK */}
        <p className="text-center mt-4 text-blue-700">
          Đã có tài khoản?{" "}
          <Link to="/login" className="font-semibold underline">
            Đăng nhập
          </Link>
        </p>
      </div>
    </div>
  );
}
