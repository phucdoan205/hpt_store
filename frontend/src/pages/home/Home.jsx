import React from "react";

const Home = () => {
  return (
    <>
      <main className="min-h-screen">
        {/* Hero Section */}
        <section className="relative h-[80vh] bg-gray-200">
          <img
            src="https://insieutoc.vn/wp-content/uploads/2021/02/mau-banner-quang-cao-khuyen-mai.jpg"
            className="w-full h-full object-cover"
            alt="Banner"
          />
          <div className="absolute inset-0 flex flex-col justify-center items-start px-12 bg-black/20">
            <h2 className="text-white text-5xl font-bold mb-4 uppercase">
              New Collection 2024
            </h2>
            <button className="bg-white text-black px-8 py-3 font-bold hover:bg-black hover:text-white transition">
              MUA NGAY
            </button>
          </div>
        </section>

        {/* Product List Section */}
        <section className="container mx-auto py-16 px-4">
          <h2 className="text-2xl font-bold text-center mb-10 uppercase tracking-widest">
            Sản phẩm mới nhất
          </h2>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
            {/* Map ProductCard here */}
          </div>
        </section>
      </main>
    </>
  );
};
export default Home;
