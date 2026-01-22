import { useEffect } from "react";

export default function ChatBot() {
  useEffect(() => {
    if (window.Tawk_API) return;

    const s1 = document.createElement("script");
    const s0 = document.getElementsByTagName("script")[0];

    s1.async = true;
    s1.src = "https://embed.tawk.to/6969b1b454ac551981db872f/1jf2dsato"; // 🔴 LINK ĐÚNG
    s1.charset = "UTF-8";
    s1.setAttribute("crossorigin", "*");

    s0.parentNode.insertBefore(s1, s0);
  }, []);

  return null;
}
