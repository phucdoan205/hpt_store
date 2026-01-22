export default function Orders() {
  return (
    <div>
      <h2>Đơn hàng</h2>
      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>Mã đơn</th>
            <th>Khách</th>
            <th>Trạng thái</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>#DH001</td>
            <td>Trần B</td>
            <td>Chờ xử lý</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}
