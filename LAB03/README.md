Lab 3: Hệ Thống Quản Lý Khách Sạn

1. Thông tin sinh viên

Họ và tên: Nguyễn Sĩ Quỳnh

MSSV: 1250080157

2. Tên bài Lab

Lab 3 - Xây dựng Ứng dụng Quản lý Khách sạn với Windows Forms và SQL Server theo kiến trúc phân lớp.

3. Môi trường & Version

Ngôn ngữ/Nền tảng: C# / Windows Forms (.NET Framework 4.7.2 / .NET 6+)
Cơ sở dữ liệu: SQL Server 2022

Công cụ: Visual Studio 2022, SQL Server Management Studio (SSMS) v21.6.17

4. Nội dung đã thực hiện

Thiết kế và khởi tạo cơ sở dữ liệu cho hệ thống khách sạn.

Xây dựng kiến trúc phân lớp: WinForms UI, Service/Business Logic, Data (Db).

Hoàn thiện các chức năng Quản lý danh mục (Phòng, Nhân viên, Tiện nghi, Dịch vụ).

Xây dựng quy trình Đặt phòng, Nhận phòng, Cập nhật dịch vụ/tiện nghi.

Hoàn thiện quy trình Trả phòng, tính tiền đền bù (nếu có) và Thanh toán (hỗ trợ chia đợt).

Thiết kế chức năng Thống kê báo cáo doanh thu theo thời gian.

5. Kết quả

Ứng dụng kết nối thành công với cơ sở dữ liệu.

Thực hiện trơn tru luồng nghiệp vụ từ lúc khách đặt phòng đến khi thanh toán.

Giao diện thân thiện, báo cáo thống kê hiển thị chính xác dữ liệu.

6. Lỗi gặp phải & Cách khắc phục

Lỗi: Xung đột khi cập nhật dữ liệu tiện nghi/dịch vụ hoặc lỗi hiển thị danh sách trên DataGridView.

Cách khắc phục: Cập nhật lại logic trong tầng Service, sử dụng BindingSource để đồng bộ dữ liệu giao diện và bắt Exception để tránh văng app. 7. Hướng dẫn kiểm tra & chạy chương trình

Mở SQL Server Management Studio (SSMS), chạy file script Database.sql (hoặc restore file .bak) đính kèm để khởi tạo cơ sở dữ liệu.

Mở Solution bằng Visual Studio.

Mở file App.config (hoặc file chứa cấu hình kết nối trong tầng Data). Sửa lại ConnectionString sao cho khớp với tên Server Name trên máy của thầy

Clean và Rebuild lại Solution.

Nhấn Start (F5) để chạy ứng dụng. Dùng tài khoản admin mặc định (nếu có) để đăng nhập và kiểm tra các chức năng.
