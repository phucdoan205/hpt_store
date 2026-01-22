import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginApi } from "../../services/authService";
import { useAuth } from "../../auth/AuthContext";
import GoogleLoginButton from "../../components/GoogleLoginButton";

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
    <div className="min-h-screen flex items-center justify-center bg-white">
      <div className="w-full max-w-md border border-[#0A1F44] p-8 rounded-xl">
        <h2 className="text-2xl font-bold text-center mb-6 text-[#0A1F44]">
          Đăng nhập
        </h2>

        <form onSubmit={submit} className="flex flex-col gap-4">
          {/* EMAIL */}
          <input
            type="email"
            placeholder="Email"
            className="w-full border border-[#0A1F44] rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0A1F44]"
            onChange={(e) => setEmail(e.target.value)}
          />

          {/* PASSWORD */}
          <input
            type="password"
            placeholder="Mật khẩu"
            className="w-full border border-[#0A1F44] rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0A1F44]"
            onChange={(e) => setPassword(e.target.value)}
          />

          {/* BUTTON LOGIN */}
          <button
            type="submit"
            className="w-full mt-2 bg-[#0A1F44] hover:bg-[#081833] text-white font-semibold py-2 rounded-lg transition"
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
