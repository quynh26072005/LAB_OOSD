# Bài 4 – Quản lý cho thuê văn phòng ở cao ốc (LAB 08)

- Họ tên: Nguyễn Sĩ Quỳnh
- MSSV: 1250080157
- Môn: Thực hành Phân tích thiết kế hướng đối tượng 

## Giới thiệu
Ứng dụng WinForms quản lý việc cho thuê văn phòng trong cao ốc: tầng – phòng, hợp đồng thuê, thu tiền thuê, hóa đơn dịch vụ, gia hạn, nhân viên công ty và thẻ giữ xe, kiểm tra phòng và phạt, thống kê.

## Công nghệ
- C# WinForms, .NET Framework 4.7.2
- SQL Server (ADO.NET)
- Visual Studio 2022

## Cấu trúc thư mục
- `Data/` – kết nối CSDL (Db.cs) và lớp kết quả
- `Services/` – xử lý nghiệp vụ
- `Forms/` – các Form giao diện
- `Database/QuanLyCaoOc.sql` – script tạo CSDL và dữ liệu mẫu
- `App.config` – chuỗi kết nối

## Cách chạy
1. Mở SQL Server Management Studio, chạy file `Database/QuanLyCaoOc.sql`.
2. Mở `App.config`, sửa chuỗi kết nối `QuanLyCaoOcDB` cho đúng máy (Data Source, tài khoản/mật khẩu).
3. Mở file `QuanLyCaoOc.sln` bằng Visual Studio 2022.
4. Bấm F5 để chạy.

## Quy tắc nghiệp vụ đã cài
- Thuê tối thiểu 6 tháng, một phòng không được thuê trùng thời gian.
- Trả trước tiền thuê 6 tháng đầu; từ tháng thứ 7 trả đầu mỗi tháng.
- Tiền dịch vụ thanh toán vào cuối tháng.
- Gia hạn tối đa bằng 1/2 thời gian thuê và chỉ khi không còn nợ tiền thuê, tiền dịch vụ.
- Mỗi nhân viên công ty có một thẻ giữ xe.
- Kiểm tra phòng 1 năm/lần; phạt khi tự ý thay đổi kết cấu.
