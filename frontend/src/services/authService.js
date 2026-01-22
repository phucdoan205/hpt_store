export const loginApi = async (email, password) => {
  if (email === "admin@gmail.com") return { email, role: "admin" };
  if (email === "staff@gmail.com") return { email, role: "staff" };
  return { email, role: "user" };
};

export const registerApi = async (data) => {
  console.log("Register data:", data);

  // MOCK – backend gắn sau
  return {
    success: true,
    message: "Đăng ký thành công"
  };
};

