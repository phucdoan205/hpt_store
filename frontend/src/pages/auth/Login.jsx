import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginApi } from "../../services/authService";
import { useAuth } from "../../auth/AuthContext";
import GoogleLoginButton from "../../components/common/GoogleLoginButton";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useAuth();
  const navigate = useNavigate();

  const submit = async (e) => {
    e.preventDefault();
    const user = await loginApi(email, password);
    login(user);

    if (user.role === "admin") navigate("/admin");
    else if (user.role === "staff") navigate("/staff/orders");
    else navigate("/");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      {/* KHUNG */}
      <div className="w-full max-w-md bg-white border-2 border-blue-700 p-8 rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300">
        {/* TIÊU ĐỀ */}
        <h2 className="text-2xl font-bold text-center mb-6 text-blue-700">
          ĐĂNG NHẬP
        </h2>

        <form onSubmit={submit} className="flex flex-col gap-4">
          {/* EMAIL */}
          <input
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
              hover:bg-blue-50
              transition-colors
            "
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          {/* PASSWORD */}
          <input
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
              hover:bg-blue-50
              transition-colors
            "
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          {/* LOGIN BUTTON */}
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
              border-2
              border-blue-700
              hover:bg-blue-800
              hover:border-blue-800
              transition-colors
            "
          >
            Đăng nhập
          </button>
        </form>
        {/* GOOGLE LOGIN */}
        <div className="mt-4">
          <GoogleLoginButton />
        </div>
      </div>
    </div>
  );
}
