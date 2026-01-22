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
      <div className="w-full max-w-md border p-6 rounded shadow">
        <h2 className="text-2xl font-bold mb-4 text-center">
          Đăng ký tài khoản
        </h2>

        <form onSubmit={submit} className="space-y-3">
          <input
            name="username"
            placeholder="Tên người dùng"
            className="w-full border p-2 rounded"
            onChange={handleChange}
            required
          />

          <input
            name="email"
            type="email"
            placeholder="Email"
            className="w-full border p-2 rounded"
            onChange={handleChange}
            required
          />

          <input
            name="password"
            type="password"
            placeholder="Mật khẩu"
            className="w-full border p-2 rounded"
            onChange={handleChange}
            required
          />

          <input
            name="confirmPassword"
            type="password"
            placeholder="Nhập lại mật khẩu"
            className="w-full border p-2 rounded"
            onChange={handleChange}
            required
          />

          <textarea
            name="address"
            placeholder="Địa chỉ"
            className="w-full border p-2 rounded"
            rows={2}
            onChange={handleChange}
          />

          <button className="w-full bg-black text-white py-2 rounded hover:opacity-90">
            Đăng ký
          </button>
        </form>

        <p className="text-center mt-4">
          Đã có tài khoản?{" "}
          <Link to="/login" className="text-blue-600">
            Đăng nhập
          </Link>
        </p>
      </div>
    </div>
  );
}
