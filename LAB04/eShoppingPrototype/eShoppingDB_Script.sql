USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'eShoppingDB')
BEGIN
    ALTER DATABASE eShoppingDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE eShoppingDB;
END
GO

CREATE DATABASE eShoppingDB;
GO

USE eShoppingDB;
GO

CREATE TABLE NhomSanPham (
    MaNhom INT PRIMARY KEY IDENTITY(1,1),
    TenNhom NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)
);
GO

CREATE TABLE SanPham (
    MaSP INT PRIMARY KEY IDENTITY(1,1),
    TenSP NVARCHAR(200) NOT NULL,
    MaNhom INT,
    NhaSanXuat NVARCHAR(100),
    GiaBan DECIMAL(18,0),
    SoLuong INT,
    TinhTrang NVARCHAR(50),
    MoTa NVARCHAR(500),
    HinhAnh NVARCHAR(255),
    FOREIGN KEY (MaNhom) REFERENCES NhomSanPham(MaNhom)
);
GO

CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    DienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    DiaChi NVARCHAR(255),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE KhuVuc (
    MaKhuVuc INT PRIMARY KEY IDENTITY(1,1),
    TenKhuVuc NVARCHAR(100) NOT NULL,
    PhiGiaoHangThuong DECIMAL(18,0) NOT NULL,
    PhiGiaoHangNhanh DECIMAL(18,0) NOT NULL,
    PhiGiaoHangTrongNgay DECIMAL(18,0) NOT NULL
);
GO

CREATE TABLE NguoiNhan (
    MaNguoiNhan INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NOT NULL,
    DienThoai NVARCHAR(20) NOT NULL,
    MaKhuVuc INT,
    FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc)
);
GO

CREATE TABLE DonHang (
    MaDonHang INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT,
    NgayDatHang DATETIME DEFAULT GETDATE(),
    MaNguoiNhan INT,
    TongTienHang DECIMAL(18,0),
    LoaiGiaoHang NVARCHAR(50),
    PhiGiaoHang DECIMAL(18,0),
    PhiThanhToan DECIMAL(18,0),
    TongTriGiaHoaDon DECIMAL(18,0),
    SoThe NVARCHAR(20),
    LoaiThe NVARCHAR(20),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNguoiNhan) REFERENCES NguoiNhan(MaNguoiNhan)
);
GO

CREATE TABLE ChiTietDonHang (
    MaChiTiet INT PRIMARY KEY IDENTITY(1,1),
    MaDonHang INT,
    MaSP INT,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    ThanhTien DECIMAL(18,0),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
GO

INSERT INTO NhomSanPham (TenNhom, MoTa) VALUES
(N'May anh ky thuat so', N'Camera va phu kien chup anh'),
(N'Thiet bi dien gia dung', N'Tivi, tu lanh, may giat'),
(N'Thiet bi may tinh', N'Laptop, PC, phu kien'),
(N'Do choi', N'Do choi tre em va nguoi lon');
GO

INSERT INTO SanPham (TenSP, MaNhom, NhaSanXuat, GiaBan, SoLuong, TinhTrang, MoTa, HinhAnh) VALUES
(N'Canon EOS R6', 1, N'Canon', 2200000, 15, N'Con hang', N'May anh khong guong lat full-frame', NULL),
(N'Nikon Z6 II', 1, N'Nikon', 1800000, 12, N'Con hang', N'May anh mirrorless chuyen nghiep', NULL),
(N'Samsung 55 QLED TV', 2, N'Samsung', 2800000, 8, N'Con hang', N'TV QLED 4K 55 inch', NULL),
(N'LG 65 OLED TV', 2, N'LG', 5500000, 5, N'Con hang', N'TV OLED 4K 65 inch', NULL),
(N'Sony Alpha A7 III Camera', 1, N'Sony', 1500000, 10, N'Con hang', N'May anh full-frame cao cap', NULL),
(N'Sony 50 Bravia TV', 2, N'Sony', 1900000, 12, N'Con hang', N'TV LED 4K 50 inch', NULL),
(N'Dell XPS 15 Laptop', 3, N'Dell', 3200000, 7, N'Con hang', N'Laptop cao cap cho do hoa', NULL),
(N'MacBook Pro 14 M1 Pro', 3, N'Apple', 4500000, 6, N'Con hang', N'Laptop chuyen nghiep voi chip M1 Pro', NULL),
(N'Lenovo ThinkPad X1 Carbon', 3, N'Lenovo', 2900000, 9, N'Con hang', N'Laptop doanh nhan mong nhe', NULL),
(N'ASUS ROG Zephyrus G14', 3, N'ASUS', 3500000, 5, N'Con hang', N'Laptop gaming hieu nang cao', NULL),
(N'LEGO Star Wars Millennium Falcon', 4, N'LEGO', 1200000, 20, N'Con hang', N'Bo lap rap LEGO Star Wars', NULL),
(N'Nintendo Switch OLED', 4, N'Nintendo', 850000, 15, N'Con hang', N'May choi game cam tay', NULL);
GO

INSERT INTO KhachHang (HoTen, DienThoai, Email, DiaChi, TenDangNhap, MatKhau) VALUES
(N'Nguyen Van A', N'0901234567', N'nguyenvana@email.com', N'123 Duong ABC, Quan 1, TP.HCM', N'nguyenvana', N'password123'),
(N'Tran Thi B', N'0912345678', N'tranthib@email.com', N'456 Duong XYZ, Quan 3, TP.HCM', N'tranthib', N'password456'),
(N'Le Van C', N'0923456789', N'levanc@email.com', N'789 Duong MNO, Ha Noi', N'levanc', N'password789');
GO

INSERT INTO KhuVuc (TenKhuVuc, PhiGiaoHangThuong, PhiGiaoHangNhanh, PhiGiaoHangTrongNgay) VALUES
(N'Noi thanh TP.HCM', 20000, 30000, 50000),
(N'Ngoai thanh TP.HCM', 35000, 50000, 80000),
(N'Cac tinh mien Nam', 50000, 75000, 120000),
(N'Noi thanh Ha Noi', 20000, 30000, 50000),
(N'Ngoai thanh Ha Noi', 35000, 50000, 80000),
(N'Cac tinh mien Bac', 50000, 75000, 120000),
(N'Cac tinh mien Trung', 60000, 90000, 150000),
(N'Vung xa, hai dao', 100000, 150000, 250000);
GO
