LAB 02: HỆ THỐNG QUẢN LÝ THƯ VIỆN

1. GIỚI THIỆU
   Đây là dự án thực hành (Lab 02) với mục tiêu đi trọn chu trình từ mô tả nghiệp vụ đến việc xây dựng một ứng dụng Windows Forms C# có kết nối cơ sở dữ liệu. Ứng dụng này giúp tin học hóa khâu quản lý thông tin sách, độc giả và các nghiệp vụ mượn, trả sách tại thư viện.

2. CÔNG NGHỆ VÀ CÔNG CỤ SỬ DỤNG

- Môi trường phát triển: Visual Studio 2022 (Workload .NET desktop development)
- Ngôn ngữ lập trình: C#
- Nền tảng: .NET Framework 4.7.2
- Giao diện người dùng: Windows Forms (WinForms)
- Hệ quản trị CSDL: SQL Server (LocalDB hoặc SQL Server Express)
- Truy xuất dữ liệu: ADO.NET

3. CÁC TÍNH NĂNG CHÍNH

- Quản lý danh mục: Thêm, sửa, xóa thông tin Nhân viên, Thể loại và Nhà xuất bản.
- Quản lý đầu sách: Quản lý thông tin sách, số lượng tồn kho và tìm kiếm đầu sách.
- Quản lý độc giả và Thẻ thư viện: Lưu trữ thông tin người mượn, thực hiện cấp mới và gia hạn thẻ thư viện.
- Mượn - Trả sách:
  - Kiểm tra điều kiện và lập phiếu mượn (tối đa 3 cuốn / độc giả).
  - Nhận trả sách và lập phiếu phạt đối với các trường hợp trả trễ hạn, làm mất hoặc hư hỏng rách nát.
- Thống kê: Báo cáo định kỳ hàng tháng về số lượt mượn sách, sách quá hạn, mất, hư hỏng và tổng số tiền phạt.

4. HƯỚNG DẪN CÀI ĐẶT VÀ KHỞI CHẠY
   Bước 1: Khởi tạo Cơ sở dữ liệu
1. Mở Visual Studio và chọn View -> SQL Server Object Explorer.
1. Mở file script Database/QuanLyThuVien.sql.
1. Chạy (Execute) toàn bộ script này để hệ thống tự động tạo cơ sở dữ liệu QuanLyThuVienDB, các bảng và dữ liệu mẫu.

Bước 2: Cấu hình kết nối (Connection String)

1. Mở file App.config nằm trong thư mục gốc của project.
2. Tìm thẻ connectionStrings và kiểm tra thuộc tính connectionString.
3. Nếu đang dùng SQL Server Express thay vì LocalDB, hãy điều chỉnh phần Data Source cho phù hợp với máy (giữ nguyên Initial Catalog = QuanLyThuVienDB).

Bước 3: Biên dịch và chạy

1. Chọn Build -> Rebuild Solution để biên dịch ứng dụng.
2. Nhấn F5 hoặc nút Start để chạy chương trình. Giao diện FrmMain sẽ hiển thị đầu tiên.

3. THÔNG TIN SINH VIÊN THỰC HIỆN

- Họ và tên: Nguyễn Sĩ Quỳnh
- MSSV: 1250080157
- Lớp: K12_CNPM1
