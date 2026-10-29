# Quản lý nhân sự

Bài thực hành LAB07 xây dựng ứng dụng quản lý nhân sự cho Công ty Sơn Đông Á.

**Sinh viên:** Nguyễn Sĩ Quỳnh  
**MSSV:** 1250080157

## Công nghệ

C# Windows Forms, .NET Framework 4.7.2, SQL Server và ADO.NET. Mã nguồn chia thành ba tầng:

- `QuanLyNhanSu.UI`: giao diện người dùng.
- `QuanLyNhanSu.Service`: xử lý nghiệp vụ.
- `QuanLyNhanSu.Data`: các lớp dữ liệu và truy cập cơ sở dữ liệu.

## Chức năng chính

- Quản lý nhân viên, phòng ban, chức vụ và ca làm việc.
- Lập quyết định tuyển dụng, chức vụ, lương, khen thưởng và kỷ luật.
- Quản lý hợp đồng, BHYT và BHXH.
- Phân công ca, chấm công và ghi nhận phụ trội.
- Tính lương tháng và xem chi tiết thực lãnh.

## Cách chạy

1. Chuẩn bị Visual Studio với workload **.NET desktop development**, .NET Framework 4.7.2 Developer Pack và SQL Server.
2. Chạy [Script_QuanLyNhanSu.sql](Database/Script_QuanLyNhanSu.sql) trong SSMS để tạo cơ sở dữ liệu và dữ liệu mẫu.
3. Sửa `Data Source` trong [App.config](QuanLyNhanSu.UI/App.config) theo tên SQL Server trên máy. Mặc định là `.`, dùng xác thực Windows; nếu dùng instance `SQLEXPRESS`, đổi thành `.\SQLEXPRESS`.
4. Mở `QuanLyNhanSu.sln`, đặt `QuanLyNhanSu.UI` làm **Startup Project**, rồi nhấn **F5**.

**Lưu ý:** chạy lại script SQL sẽ xóa các bảng hiện có của ứng dụng và nạp lại dữ liệu mẫu.

## Tính lương

```text
Thực lãnh = Lương cơ bản + Phụ cấp + Tiền phụ trội + Tiền thưởng
            - Tiền phạt - BHYT - BHXH
```

Chỉ nhân viên có quyết định lương mới được tính lương. Bản hiện tại dùng 22 ngày công, 8 giờ/ngày để tính đơn giá giờ và chưa điều chỉnh lương theo số ngày chấm công thực tế.
