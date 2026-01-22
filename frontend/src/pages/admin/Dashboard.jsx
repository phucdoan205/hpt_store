export default function Dashboard() {
  return (
    <>
      <h2>Tổng quan</h2>
      <div style={{ display: "flex", gap: 20 }}>
        <Card title="Đơn hàng" value="120" />
        <Card title="Doanh thu" value="50.000.000đ" />
        <Card title="Nhân viên" value="8" />
      </div>
    </>
  );
}

function Card({ title, value }) {
  return (
    <div style={{
      border: "1px solid #ccc",
      padding: 20,
      borderRadius: 8,
      width: 200
    }}>
      <h4>{title}</h4>
      <strong>{value}</strong>
    </div>
  );
}
