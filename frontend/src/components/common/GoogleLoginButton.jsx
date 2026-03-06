export default function GoogleLoginButton() {
  const handleGoogleLogin = () => {
    alert("Google login (gắn Firebase / Supabase Auth sau)");
  };

  return (
    <button
      type="button"
      onClick={handleGoogleLogin}
      className="
        mt-4
        w-full
        flex items-center justify-center gap-3
        py-2
        rounded-lg
        border-2 border-gray-300
        bg-white
        text-gray-700
        font-medium
        shadow-sm
        hover:bg-gray-100
        transition
      "
    >
      {/* ICON GOOGLE */}
      <img
        src="https://www.svgrepo.com/show/475656/google-color.svg"
        alt="google"
        className="w-5 h-5"
      />
      Đăng nhập bằng Google
    </button>
  );
}
