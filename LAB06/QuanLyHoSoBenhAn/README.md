# Quản lý Hồ sơ Bệnh án — WinForms + SQL Server

Bài tập lớn môn **Phân tích & Thiết kế hướng đối tượng (OOSD)** — **LAB 06**

|                  |                                                           |
| ---------------- | --------------------------------------------------------- |
| Họ tên sinh viên | Nguyễn Sĩ Quỳnh                                           |
| MSSV             | 1250080157                                                |
| Lớp              | 12_CNPM1                                                  |
| Công nghệ        | C# WinForms (.NET Framework 4.7.2) + SQL Server (ADO.NET) |

---

## 1. Giới thiệu

Dự án **Quản lý Hồ sơ Bệnh án** phục vụ quy trình khám chữa bệnh nội bộ: đăng ký
bệnh nhân, thu lệ phí năm, cấp hồ sơ bệnh án, đặt lịch khám định kỳ, tiếp nhận/khám
bệnh, chỉ định và trả kết quả xét nghiệm, lập toa thuốc, lập hóa đơn viện phí và
hoàn tất đợt khám.

Gồm 2 phần chính:

- **Tài liệu phân tích - thiết kế** (`LAB6_NguyenSiQuynh_1250080157.docx`) kèm bộ
  20 sơ đồ UML trong thư mục `drawio/`.
- **Bản triển khai chương trình** (`QuanLyHoSoBenhAn/`) gồm script SQL và ứng dụng
  WinForms phân lớp 3 tầng.

---

## 2. Các sơ đồ phân tích thiết kế

| #    | Sơ đồ                              |
| ---- | ---------------------------------- |
| P.1  | Use Case tổng quát                 |
| P.2  | Phân rã Use Case đăng ký & hồ sơ   |
| P.3  | Phân rã Use Case khám & xét nghiệm |
| P.4  | Phân rã Use Case viện phí          |
| P.5  | Biểu đồ lớp phân tích              |
| P.6  | Trạng thái hồ sơ bệnh án           |
| P.7  | Trạng thái phiếu khám              |
| P.8  | Trạng thái hóa đơn                 |
| P.9  | Sequence đăng ký & cấp hồ sơ       |
| P.10 | Sequence tiếp nhận khám            |
| P.11 | Sequence chỉ định & trả kết quả XN |
| P.12 | Sequence lập toa thuốc             |
| P.13 | Sequence viện phí & hoàn tất       |
| P.14 | Biểu đồ lớp chi tiết               |
| P.15 | Thiết kế lớp module đăng ký        |
| P.16 | Thiết kế lớp module khám           |
| P.17 | Thiết kế lớp module viện phí       |
| P.18 | Activity quy trình khám            |
| P.19 | Activity lập toa thuốc             |
| P.20 | ERD triển khai SQL Server          |

> Toàn bộ 20 sơ đồ cũng được gộp sẵn trong `drawio/QuanLyHoSoBenhAn.drawio`
> (mỗi sơ đồ là một tab).

---

## 3. Thiết lập CSDL

1. Mở **SQL Server Management Studio** (hoặc Azure Data Studio), kết nối tới
   instance SQL Server (LocalDB / SQLEXPRESS / SQL Server đầy đủ).
2. Mở `Database/QuanLyHoSoBenhAn.sql` và **Execute** toàn bộ (F5).
3. Script tự tạo database `QuanLyHoSoBenhAn` với **18 bảng**, các ràng buộc
   PK/FK/UNIQUE/CHECK, và chèn dữ liệu mẫu: 8 tài khoản nhân viên, 4 khoa,
   4 loại xét nghiệm, 4 loại thuốc.

> Script sẽ **DROP** database nếu đã tồn tại trước khi tạo lại — chỉ chạy một lần.

---

## 4. Chạy ứng dụng WinForms

1. Mở `QuanLyHoSoBenhAn.sln` bằng **Visual Studio 2022** (workload
   ".NET desktop development").
2. Sửa `connectionString` trong `QuanLyHoSoBenhAn/App.config` cho đúng SQL Server
   (mặc định trỏ tới `.\SQLEXPRESS`):
   ```xml
   <add name="QuanLyHoSoBenhAn"
        connectionString="Server=.\SQLEXPRESS;Database=QuanLyHoSoBenhAn;Integrated Security=True;TrustServerCertificate=True;" />
   ```
3. Nhấn **F5** để build và chạy. Màn hình đăng nhập hiện ra.

---

## 5. Tài khoản mẫu

Mật khẩu mọi tài khoản mẫu đều là `123456`.

| Tên đăng nhập      | Vai trò                               |
| ------------------ | ------------------------------------- |
| `admin`            | Quản trị hệ thống (thấy toàn bộ menu) |
| `quanly`           | Quản lý (thống kê)                    |
| `letan`            | Lễ tân / CSKH                         |
| `bacsi1`, `bacsi2` | Bác sĩ                                |
| `xetnghiem`        | Nhân viên xét nghiệm                  |
| `duocsi`           | Dược sĩ                               |
| `vienphi`          | Nhân viên viện phí                    |

---

## 6. Luồng demo nghiệp vụ UC01

1. Đăng nhập `letan` → **Bệnh nhân / Đăng ký**: đăng ký bệnh nhân mới.
2. **Lệ phí & Hồ sơ**: thu lệ phí năm → hồ sơ được cấp tự động (BR12).
3. **Tiếp nhận khám**: chọn khoa/bác sĩ → tạo phiếu khám.
4. Đăng nhập `bacsi1` → **Khám bệnh**: nhập triệu chứng/chẩn đoán → (tuỳ chọn)
   chỉ định xét nghiệm / lập toa → **Hoàn tất chuyên môn**.
5. Nếu có chỉ định: đăng nhập `xetnghiem` → **Xét nghiệm** → nhập kết quả.
6. Đăng nhập `vienphi` → **Viện phí**: lập hóa đơn, ghi nhận thanh toán → khi đủ
   tiền, phiếu khám tự chuyển **Hoàn tất** (BR11).
7. Đăng nhập `admin` → **Tài khoản & Nhật ký**: xem lịch sử thao tác (BR15).

---

## 7. Ghi chú kỹ thuật

- Kiến trúc **3 tầng UI → Services → Data (Db) → SQL Server**; không có SQL nằm
  trực tiếp trong code-behind của Form.
- Nghiệp vụ nhiều bảng được bọc trong `SqlTransaction`, rollback toàn bộ khi lỗi
  (mục 4.4, BR12/BR13).
- Mật khẩu được băm **SHA-256** trước khi lưu (`DbHelper.HashPassword`).
- Phân quyền theo vai trò: mỗi actor chỉ thấy menu trong phạm vi nghiệp vụ của mình.

---

_thực hiện bởi Nguyễn Sĩ Quỳnh — 1250080157 — Lớp 12_CNPM1._
