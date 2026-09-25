IF DB_ID(N'QuanLyKhachSan') IS NULL CREATE DATABASE QuanLyKhachSan;
GO
USE QuanLyKhachSan;
GO

-- XÓA BẢNG NẾU ĐÃ TỒN TẠI (Theo thứ tự từ con đến cha để không lỗi khóa ngoại)
IF OBJECT_ID('ThanhToan','U') IS NOT NULL DROP TABLE ThanhToan;
IF OBJECT_ID('HoaDon','U') IS NOT NULL DROP TABLE HoaDon;
IF OBJECT_ID('ChiTietPhieuDenBu','U') IS NOT NULL DROP TABLE ChiTietPhieuDenBu;
IF OBJECT_ID('PhieuDenBu','U') IS NOT NULL DROP TABLE PhieuDenBu;
IF OBJECT_ID('QuyDinhDenBu','U') IS NOT NULL DROP TABLE QuyDinhDenBu;
IF OBJECT_ID('ChiTietPhieuSuDungDV','U') IS NOT NULL DROP TABLE ChiTietPhieuSuDungDV;
IF OBJECT_ID('PhieuSuDungDV','U') IS NOT NULL DROP TABLE PhieuSuDungDV;
IF OBJECT_ID('DichVu','U') IS NOT NULL DROP TABLE DichVu;
IF OBJECT_ID('NguoiLuuTru','U') IS NOT NULL DROP TABLE NguoiLuuTru;
IF OBJECT_ID('ChiTietDatPhong','U') IS NOT NULL DROP TABLE ChiTietDatPhong;
IF OBJECT_ID('PhieuDatPhong','U') IS NOT NULL DROP TABLE PhieuDatPhong;
IF OBJECT_ID('KhachHang','U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID('PhieuLapDat','U') IS NOT NULL DROP TABLE PhieuLapDat;
IF OBJECT_ID('TienNghi','U') IS NOT NULL DROP TABLE TienNghi;
IF OBJECT_ID('LoaiTienNghi','U') IS NOT NULL DROP TABLE LoaiTienNghi;
IF OBJECT_ID('Phong','U') IS NOT NULL DROP TABLE Phong;
IF OBJECT_ID('KhuVuc','U') IS NOT NULL DROP TABLE KhuVuc;
IF OBJECT_ID('NhanVien','U') IS NOT NULL DROP TABLE NhanVien;
GO

-- 1. BẢNG NHÂN VIÊN
CREATE TABLE NhanVien(
    MaNV varchar(20) NOT NULL PRIMARY KEY,
    HoTen nvarchar(120) NOT NULL,
    VaiTro nvarchar(50) NOT NULL,
    SoDienThoai varchar(20) NULL
);

-- 2. BẢNG KHU VỰC
CREATE TABLE KhuVuc(
    MaKhuVuc varchar(20) NOT NULL PRIMARY KEY,
    TenKhuVuc nvarchar(100) NOT NULL UNIQUE
);

-- 3. BẢNG PHÒNG
CREATE TABLE Phong(
    SoPhong varchar(20) NOT NULL PRIMARY KEY,
    MaKhuVuc varchar(20) NOT NULL,
    SoNguoiToiDa int NOT NULL CHECK(SoNguoiToiDa > 0),
    DonGiaNgay decimal(18,2) NOT NULL CHECK(DonGiaNgay >= 0),
    TrangThai nvarchar(30) NOT NULL DEFAULT N'Trống',
    CONSTRAINT CK_Phong_TrangThai CHECK(TrangThai IN (N'Trống',N'Đã đặt',N'Đang ở',N'Bảo trì')),
    CONSTRAINT FK_Phong_KhuVuc FOREIGN KEY(MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc)
);

-- 4. BẢNG LOẠI TIỆN NGHI
CREATE TABLE LoaiTienNghi(
    MaLoaiTN varchar(20) NOT NULL PRIMARY KEY,
    TenLoaiTN nvarchar(100) NOT NULL UNIQUE
);

-- 5. BẢNG TIỆN NGHI
CREATE TABLE TienNghi(
    MaTienNghi varchar(30) NOT NULL PRIMARY KEY,
    MaLoaiTN varchar(20) NOT NULL,
    SoThuTu int NOT NULL,
    TinhTrangHienTai nvarchar(100) NULL,
    CONSTRAINT UQ_TienNghi_Loai_STT UNIQUE(MaLoaiTN,SoThuTu),
    CONSTRAINT FK_TienNghi_Loai FOREIGN KEY(MaLoaiTN) REFERENCES LoaiTienNghi(MaLoaiTN)
);

-- 6. BẢNG PHIẾU LẮP ĐẶT
CREATE TABLE PhieuLapDat(
    SoPhieuLapDat varchar(30) NOT NULL PRIMARY KEY,
    MaTienNghi varchar(30) NOT NULL,
    SoPhong varchar(20) NOT NULL,
    NgayLap date NOT NULL,
    TinhTrang nvarchar(100) NOT NULL,
    MaNV varchar(20) NOT NULL,
    GhiChu nvarchar(250) NULL,
    CONSTRAINT UQ_PhieuLapDat_ThietBi_Ngay UNIQUE(MaTienNghi,NgayLap),
    CONSTRAINT FK_PhieuLapDat_TienNghi FOREIGN KEY(MaTienNghi) REFERENCES TienNghi(MaTienNghi),
    CONSTRAINT FK_PhieuLapDat_Phong FOREIGN KEY(SoPhong) REFERENCES Phong(SoPhong),
    CONSTRAINT FK_PhieuLapDat_NV FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

-- 7. BẢNG KHÁCH HÀNG
CREATE TABLE KhachHang(
    MaKhach varchar(20) NOT NULL PRIMARY KEY,
    HoTen nvarchar(120) NOT NULL,
    SoCMND varchar(30) NOT NULL UNIQUE,
    QuocTich nvarchar(80) NOT NULL,
    SoDienThoai varchar(20) NULL
);

-- 8. BẢNG PHIẾU ĐẶT PHÒNG
CREATE TABLE PhieuDatPhong(
    SoPhieuDat varchar(30) NOT NULL PRIMARY KEY,
    MaKhach varchar(20) NOT NULL,
    MaNVLeTan varchar(20) NOT NULL,
    NgayLap datetime NOT NULL,
    NgayNhan date NOT NULL,
    NgayTraDuKien date NOT NULL,
    TienCoc decimal(18,2) NOT NULL DEFAULT 0 CHECK(TienCoc >= 0),
    KenhDat nvarchar(20) NOT NULL,
    TrangThai nvarchar(30) NOT NULL DEFAULT N'Đã đặt',
    NgayNhanThucTe datetime NULL,
    NgayTraThucTe datetime NULL,
    CONSTRAINT CK_PhieuDat_Ngay CHECK(NgayTraDuKien >= NgayNhan),
    CONSTRAINT CK_PhieuDat_Kenh CHECK(KenhDat IN (N'Điện thoại',N'Website',N'Trực tiếp')),
    CONSTRAINT CK_PhieuDat_TrangThai CHECK(TrangThai IN (N'Đã đặt',N'Đang ở',N'Đã trả',N'No-show',N'Hủy')),
    CONSTRAINT FK_PhieuDat_Khach FOREIGN KEY(MaKhach) REFERENCES KhachHang(MaKhach),
    CONSTRAINT FK_PhieuDat_NV FOREIGN KEY(MaNVLeTan) REFERENCES NhanVien(MaNV)
);

-- 9. BẢNG CHI TIẾT ĐẶT PHÒNG
CREATE TABLE ChiTietDatPhong(
    SoPhieuDat varchar(30) NOT NULL,
    SoPhong varchar(20) NOT NULL,
    SoNguoi int NOT NULL CHECK(SoNguoi > 0),
    PRIMARY KEY(SoPhieuDat,SoPhong),
    CONSTRAINT FK_CTDat_Phieu FOREIGN KEY(SoPhieuDat) REFERENCES PhieuDatPhong(SoPhieuDat),
    CONSTRAINT FK_CTDat_Phong FOREIGN KEY(SoPhong) REFERENCES Phong(SoPhong)
);

-- 10. BẢNG NGƯỜI LƯU TRÚ
CREATE TABLE NguoiLuuTru(
    MaNguoiLT int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SoPhieuDat varchar(30) NOT NULL,
    SoPhong varchar(20) NOT NULL,
    HoTen nvarchar(120) NOT NULL,
    SoCMND varchar(30) NOT NULL,
    QuocTich nvarchar(80) NOT NULL,
    CONSTRAINT FK_NguoiLT_CTDat FOREIGN KEY(SoPhieuDat,SoPhong) REFERENCES ChiTietDatPhong(SoPhieuDat,SoPhong)
);

-- 11. BẢNG DỊCH VỤ
CREATE TABLE DichVu(
    MaDV varchar(20) NOT NULL PRIMARY KEY,
    TenDV nvarchar(120) NOT NULL,
    DonViTinh nvarchar(40) NOT NULL,
    DonGia decimal(18,2) NOT NULL CHECK(DonGia >= 0)
);

-- 12. BẢNG PHIẾU SỬ DỤNG DỊCH VỤ
CREATE TABLE PhieuSuDungDV(
    SoPhieuSDDV varchar(30) NOT NULL PRIMARY KEY,
    SoPhieuDat varchar(30) NOT NULL,
    SoPhong varchar(20) NOT NULL,
    NgaySuDung date NOT NULL,
    MaNV varchar(20) NOT NULL,
    CONSTRAINT UQ_PhieuSDDV_PhongNgay UNIQUE(SoPhieuDat,SoPhong,NgaySuDung),
    CONSTRAINT FK_PhieuSDDV_CTDat FOREIGN KEY(SoPhieuDat,SoPhong) REFERENCES ChiTietDatPhong(SoPhieuDat,SoPhong),
    CONSTRAINT FK_PhieuSDDV_NV FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

-- 13. BẢNG CHI TIẾT PHIẾU SỬ DỤNG DỊCH VỤ
CREATE TABLE ChiTietPhieuSuDungDV(
    SoPhieuSDDV varchar(30) NOT NULL,
    MaDV varchar(20) NOT NULL,
    SoLuong int NOT NULL CHECK(SoLuong > 0),
    DonGia decimal(18,2) NOT NULL CHECK(DonGia >= 0),
    ThanhTien AS (CONVERT(decimal(18,2),SoLuong * DonGia)) PERSISTED,
    PRIMARY KEY(SoPhieuSDDV,MaDV),
    CONSTRAINT FK_CTSDDV_Phieu FOREIGN KEY(SoPhieuSDDV) REFERENCES PhieuSuDungDV(SoPhieuSDDV),
    CONSTRAINT FK_CTSDDV_DV FOREIGN KEY(MaDV) REFERENCES DichVu(MaDV)
);

-- 14. BẢNG QUY ĐỊNH ĐỀN BÙ
CREATE TABLE QuyDinhDenBu(
    MaQuyDinh varchar(30) NOT NULL PRIMARY KEY,
    MaLoaiTN varchar(20) NOT NULL,
    MucDoThietHai nvarchar(80) NOT NULL,
    MucDenBu decimal(18,2) NOT NULL CHECK(MucDenBu >= 0),
    CONSTRAINT UQ_QDDB_Loai_MucDo UNIQUE(MaLoaiTN,MucDoThietHai),
    CONSTRAINT FK_QDDB_Loai FOREIGN KEY(MaLoaiTN) REFERENCES LoaiTienNghi(MaLoaiTN)
);

-- 15. BẢNG PHIẾU ĐỀN BÙ
CREATE TABLE PhieuDenBu(
    SoPhieuDenBu varchar(30) NOT NULL PRIMARY KEY,
    SoPhieuDat varchar(30) NOT NULL,
    SoPhong varchar(20) NOT NULL,
    NgayLap datetime NOT NULL,
    MaNV varchar(20) NOT NULL,
    TongTien decimal(18,2) NOT NULL DEFAULT 0 CHECK(TongTien >= 0),
    CONSTRAINT FK_PhieuDB_CTDat FOREIGN KEY(SoPhieuDat,SoPhong) REFERENCES ChiTietDatPhong(SoPhieuDat,SoPhong),
    CONSTRAINT FK_PhieuDB_NV FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

-- 16. BẢNG CHI TIẾT PHIẾU ĐỀN BÙ
CREATE TABLE ChiTietPhieuDenBu(
    SoPhieuDenBu varchar(30) NOT NULL,
    MaTienNghi varchar(30) NOT NULL,
    MucDoThietHai nvarchar(80) NOT NULL,
    SoTien decimal(18,2) NOT NULL CHECK(SoTien >= 0),
    PRIMARY KEY(SoPhieuDenBu,MaTienNghi),
    CONSTRAINT FK_CTDB_Phieu FOREIGN KEY(SoPhieuDenBu) REFERENCES PhieuDenBu(SoPhieuDenBu),
    CONSTRAINT FK_CTDB_TienNghi FOREIGN KEY(MaTienNghi) REFERENCES TienNghi(MaTienNghi)
);

-- 17. BẢNG HÓA ĐƠN (Phần bị thiếu trong tài liệu)
CREATE TABLE HoaDon(
    SoHoaDon varchar(30) NOT NULL PRIMARY KEY,
    SoPhieuDat varchar(30) NOT NULL UNIQUE,
    NgayLap datetime NOT NULL,
    MaNV varchar(20) NOT NULL,
    SoNgayTinhTien int NOT NULL CHECK(SoNgayTinhTien > 0),
    TienPhong decimal(18,2) NOT NULL CHECK(TienPhong >= 0),
    TienDichVu decimal(18,2) NOT NULL DEFAULT 0 CHECK(TienDichVu >= 0),
    TongTien AS (CONVERT(decimal(18,2), TienPhong + TienDichVu)) PERSISTED,
    CONSTRAINT FK_HoaDon_PhieuDat FOREIGN KEY(SoPhieuDat) REFERENCES PhieuDatPhong(SoPhieuDat),
    CONSTRAINT FK_HoaDon_NV FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

-- 18. BẢNG THANH TOÁN (Phần bị thiếu trong tài liệu)
CREATE TABLE ThanhToan(
    MaThanhToan varchar(30) NOT NULL PRIMARY KEY,
    SoHoaDon varchar(30) NOT NULL,
    NgayThanhToan datetime NOT NULL,
    HinhThuc varchar(50) NOT NULL,
    SoTien decimal(18,2) NOT NULL CHECK(SoTien > 0),
    CONSTRAINT CK_ThanhToan_HinhThuc CHECK(HinhThuc IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ', N'Ví điện tử')),
    CONSTRAINT FK_ThanhToan_HoaDon FOREIGN KEY(SoHoaDon) REFERENCES HoaDon(SoHoaDon)
);
GO


ALTER TABLE Phong
ADD SucChua INT,
    DonGia FLOAT;

    USE QuanLyKhachSan;
GO

USE QuanLyKhachSan;
GO

-- 1. XÓA SẠCH DỮ LIỆU CŨ THEO THỨ TỰ TỪ BẢNG CON ĐẾN BẢNG CHA
DELETE FROM ThanhToan;
DELETE FROM HoaDon;
DELETE FROM ChiTietPhieuDenBu;
DELETE FROM PhieuDenBu;
DELETE FROM QuyDinhDenBu;
DELETE FROM ChiTietPhieuSuDungDV;
DELETE FROM PhieuSuDungDV;
DELETE FROM DichVu;
DELETE FROM NguoiLuuTru;
DELETE FROM ChiTietDatPhong;
DELETE FROM PhieuDatPhong;
DELETE FROM KhachHang;
DELETE FROM PhieuLapDat;
DELETE FROM TienNghi;
DELETE FROM LoaiTienNghi;
DELETE FROM Phong;
DELETE FROM KhuVuc;
DELETE FROM NhanVien;
GO

-- 2. THỰC HIỆN NẠP LẠI DỮ LIỆU MẪU
INSERT INTO NhanVien (MaNV, HoTen, VaiTro, SoDienThoai) VALUES
('NV01', N'Nguyễn Văn A', N'Lễ tân', '0901234567'),
('NV02', N'Trần Thị B', N'Quản lý', '0902345678'),
('NV03', N'Lê Văn C', N'Kỹ thuật', '0903456789');

INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc) VALUES
('KV01', N'Khu A - Tầng 1'),
('KV02', N'Khu B - Tầng 2');

INSERT INTO Phong (SoPhong, MaKhuVuc, SoNguoiToiDa, DonGiaNgay, TrangThai, SucChua, DonGia) VALUES
('P101', 'KV01', 2, 500000.00, N'Đang ở', 2, 500000),
('P102', 'KV01', 4, 800000.00, N'Trống', 4, 800000),
('P201', 'KV02', 2, 600000.00, N'Trống', 2, 600000);

INSERT INTO LoaiTienNghi (MaLoaiTN, TenLoaiTN) VALUES
('LTN01', N'Tivi'),
('LTN02', N'Điều hòa'),
('LTN03', N'Tủ lạnh');

INSERT INTO TienNghi (MaTienNghi, MaLoaiTN, SoThuTu, TinhTrangHienTai) VALUES
('TN01', 'LTN01', 1, N'Tốt'),
('TN02', 'LTN02', 1, N'Tốt'),
('TN03', 'LTN03', 1, N'Tốt');

INSERT INTO PhieuLapDat (SoPhieuLapDat, MaTienNghi, SoPhong, NgayLap, TinhTrang, MaNV, GhiChu) VALUES
('PLD01', 'TN01', 'P101', '2026-01-10', N'Hoạt động tốt', 'NV03', N'Lắp mới'),
('PLD02', 'TN02', 'P101', '2026-01-10', N'Hoạt động tốt', 'NV03', N'Lắp mới');

INSERT INTO KhachHang (MaKhach, HoTen, SoCMND, QuocTich, SoDienThoai) VALUES
('KH01', N'Phạm Văn D', '123456789', N'Việt Nam', '0987654321'),
('KH02', N'John Smith', '987654321', N'Mỹ', '0123456789');

INSERT INTO PhieuDatPhong (SoPhieuDat, MaKhach, MaNVLeTan, NgayLap, NgayNhan, NgayTraDuKien, TienCoc, KenhDat, TrangThai) VALUES
('PDP01', 'KH01', 'NV01', '2026-09-20 08:00:00', '2026-09-24', '2026-09-27', 200000.00, N'Trực tiếp', N'Đang ở');

INSERT INTO ChiTietDatPhong (SoPhieuDat, SoPhong, SoNguoi) VALUES
('PDP01', 'P101', 2);

INSERT INTO NguoiLuuTru (SoPhieuDat, SoPhong, HoTen, SoCMND, QuocTich) VALUES
('PDP01', 'P101', N'Phạm Văn D', '123456789', N'Việt Nam');

INSERT INTO DichVu (MaDV, TenDV, DonViTinh, DonGia) VALUES
('DV01', N'Nước suối', N'Chai', 15000.00),
('DV02', N'Giặt ủi', N'Bộ', 30000.00);

INSERT INTO PhieuSuDungDV (SoPhieuSDDV, SoPhieuDat, SoPhong, NgaySuDung, MaNV) VALUES
('PSDV01', 'PDP01', 'P101', '2026-09-25', 'NV01');

INSERT INTO ChiTietPhieuSuDungDV (SoPhieuSDDV, MaDV, SoLuong, DonGia) VALUES
('PSDV01', 'DV01', 2, 15000.00);

INSERT INTO QuyDinhDenBu (MaQuyDinh, MaLoaiTN, MucDoThietHai, MucDenBu) VALUES
('QD01', 'LTN01', N'Hỏng điều khiển', 200000.00);

INSERT INTO PhieuDenBu (SoPhieuDenBu, SoPhieuDat, SoPhong, NgayLap, MaNV, TongTien) VALUES
('PDB01', 'PDP01', 'P101', '2026-09-25 10:00:00', 'NV01', 200000.00);

INSERT INTO ChiTietPhieuDenBu (SoPhieuDenBu, MaTienNghi, MucDoThietHai, SoTien) VALUES
('PDB01', 'TN01', N'Hỏng điều khiển', 200000.00);

INSERT INTO HoaDon (SoHoaDon, SoPhieuDat, NgayLap, MaNV, SoNgayTinhTien, TienPhong, TienDichVu) VALUES
('HD01', 'PDP01', '2026-09-25 10:30:00', 'NV01', 3, 1500000.00, 30000.00);

INSERT INTO ThanhToan (MaThanhToan, SoHoaDon, NgayThanhToan, HinhThuc, SoTien) VALUES
('TT01', 'HD01', '2026-09-25 10:35:00', N'Tiền mặt', 1530000.00);
GO


UPDATE Phong SET TrangThai = N'Bảo trì' WHERE SoPhong = 'A101';

USE QuanLyKhachSan;
GO

UPDATE Phong 
SET TrangThai = N'Bảo trì' 
WHERE SoPhong = 'A101';

USE QuanLyKhachSan;
GO

UPDATE Phong 
SET TrangThai = N'Bảo trì' 
WHERE SoPhong = 'P101';