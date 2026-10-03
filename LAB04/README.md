# LAB04 - Hệ thống e-SHOPPING

**Môn học**: Phương pháp phát triển phần mềm hướng đối tượng  
**Sinh viên**: Nguyễn Sĩ Quỳnh  
**MSSV**: 1250080157

## Mô tả

Hệ thống mua sắm trực tuyến với các tính năng:
- Quản lý tài khoản (Đăng nhập/Đăng ký)
- Duyệt và tìm kiếm sản phẩm
- Giỏ hàng mua sắm
- Thanh toán với tính phí giao hàng và phí thẻ

**Công nghệ**: .NET Framework 4.7.2, Windows Forms, SQL Server  
**Kiến trúc**: 3-layer (UI - Service/Adapter - Data)

## Cấu trúc thư mục

```
LAB04/
|-- eShoppingDB_Script.sql      # Script tạo database
|-- Bai_9_GOC.docx              # Yêu cầu đề bài
|-- MUC TIEU.docx               # Mục tiêu bài tập
+-- eShoppingPrototype/         # Source code
    |-- Models/                 # 8 classes
    |-- Data/                   # 4 DAOs
    |-- ServiceAdapter/         # 6 services
    +-- UI/                     # 6 forms
```

## Hướng dẫn chạy

### 1. Tạo database
```bash
sqlcmd -S localhost -i eShoppingDB_Script.sql
```

### 2. Build và chạy project
```bash
cd eShoppingPrototype
dotnet build
dotnet run
```

### 3. Đăng nhập test
```
Username: nguyenvana
Password: password123
```

## Tính năng chính

- Đăng nhập/Đăng ký tài khoản
- Xem danh sách sản phẩm theo nhóm
- Tìm kiếm sản phẩm
- Thêm vào giỏ hàng
- Thanh toán với nhiều hình thức giao hàng
- Tự động tính phí giao hàng và phí thanh toán

## Lưu ý

- Giao diện được tạo 100% bằng code (không dùng Designer)
- Sử dụng SqlTransaction để đảm bảo tính nhất quán
- Hỗ trợ tiếng Việt với NVARCHAR
