# Bài 6 - Quản lý công ty du lịch Văn Hóa Việt

- **Sinh viên:** Nguyễn Sĩ Quỳnh
- **MSSV:** 1250080157
- **Công nghệ:** C# WinForms, .NET Framework 4.7.2, SQL Server
- **Kiến trúc:** UI (Forms) → Services → Data/Db.cs → SQL Server

## Chạy thử
1. Mở SSMS, chạy file `Database/QuanLyCongTyDuLich.sql` (F5).
2. Mở `QuanLyCongTyDuLich/App.config`, chọn đúng chuỗi kết nối.
3. Mở `QuanLyCongTyDuLich.sln` bằng Visual Studio 2022, Rebuild Solution rồi nhấn F5.

## Cấu trúc
- `Data/Db.cs`: truy cập SQL Server
- `Services/`: Models.cs và 8 Service (DanhMuc, Tour, ChuyenLe, DangKyLe, DangKyDoan, PhanCong, KetThuc, ThongKe)
- `Forms/`: FormHelper.cs và 9 Form (.cs, .Designer.cs)

## Lỗi thường gặp
- **Không kết nối được CSDL:** kiểm tra đã chạy file SQL chưa và `Data Source` trong `App.config` có đúng tên server không.
- **Lỗi "filtered index" khi chạy bằng sqlcmd:** thêm tham số `-I`.
- **Không mở được Designer:** Build Solution trước rồi mở lại Form.
- **Ngày mẫu trong script là năm 2026:** nếu chạy muộn hơn, hãy dời ngày của dữ liệu đăng ký đoàn và chuyến mới.