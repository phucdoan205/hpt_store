export default function GoogleLoginButton() {
  const handleGoogleLogin = () => {
    alert("Google login (gắn Firebase / Supabase Auth sau)");
  };

  return (
    <button
      onClick={handleGoogleLogin}
      className="mt-4 w-full border py-2 rounded"
    >
      Đăng nhập bằng Google
    </button>
  );
}
