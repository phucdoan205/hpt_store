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
      {/* KHUNG */}
      <div className="w-full max-w-md border-2 border-blue-700 p-8 rounded-xl">
        
        {/* TIÊU ĐỀ */}
        <h2 className="text-2xl font-bold text-center mb-6 text-blue-700">
          Đăng nhập
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
              !bg-blue-700
              !text-white
              font-semibold
              py-2
              rounded-lg
              border-2
              border-blue-700
            "
          >
            Đăng nhập
          </button>
        </form>

        {/* GOOGLE LOGIN */}
          <GoogleLoginButton />
      </div>
    </div>
  );
}
