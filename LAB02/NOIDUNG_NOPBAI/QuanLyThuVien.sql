/* =========================================================================
   HE THONG QUAN LY THU VIEN - SCRIPT TAO CO SO DU LIEU
   CSDL: QuanLyThuVienDB
   Chay tren SQL Server LocalDB / SQL Server Express
   Gom: DDL (tao 9 bang, rang buoc PK/FK/CHECK/UNIQUE) + DML (du lieu mau)
   ========================================================================= */

-- =========================================================================
-- 1. TAO DATABASE
-- =========================================================================
IF DB_ID(N'QuanLyThuVienDB') IS NULL
    CREATE DATABASE QuanLyThuVienDB;
GO

USE QuanLyThuVienDB;
GO

-- =========================================================================
-- 2. XOA BANG NEU DA TON TAI (dung thu tu de khong vi pham khoa ngoai)
-- =========================================================================
IF OBJECT_ID('dbo.PhieuPhat', 'U') IS NOT NULL DROP TABLE dbo.PhieuPhat;
IF OBJECT_ID('dbo.ChiTietPhieuMuon', 'U') IS NOT NULL DROP TABLE dbo.ChiTietPhieuMuon;
IF OBJECT_ID('dbo.PhieuMuon', 'U') IS NOT NULL DROP TABLE dbo.PhieuMuon;
IF OBJECT_ID('dbo.TheDocGia', 'U') IS NOT NULL DROP TABLE dbo.TheDocGia;
IF OBJECT_ID('dbo.DocGia', 'U') IS NOT NULL DROP TABLE dbo.DocGia;
IF OBJECT_ID('dbo.DauSach', 'U') IS NOT NULL DROP TABLE dbo.DauSach;
IF OBJECT_ID('dbo.NhaXuatBan', 'U') IS NOT NULL DROP TABLE dbo.NhaXuatBan;
IF OBJECT_ID('dbo.TheLoai', 'U') IS NOT NULL DROP TABLE dbo.TheLoai;
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO

-- =========================================================================
-- 3. DDL - TAO BANG VA RANG BUOC
-- =========================================================================

-- 3.1. NhanVien -----------------------------------------------------------
CREATE TABLE dbo.NhanVien (
    MaNhanVien      NVARCHAR(20)  NOT NULL PRIMARY KEY,
    Ho              NVARCHAR(50)  NOT NULL,
    Ten             NVARCHAR(50)  NOT NULL,
    Phai            NVARCHAR(10)  NOT NULL,
    NgaySinh        DATE          NOT NULL,
    ChucVu          NVARCHAR(80)  NOT NULL,
    SoDienThoai     NVARCHAR(20)  NULL
);
GO

-- 3.2. TheLoai --------------------------------------------------------------
CREATE TABLE dbo.TheLoai (
    MaTheLoai       NVARCHAR(20)  NOT NULL PRIMARY KEY,
    TenTheLoai      NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- 3.3. NhaXuatBan -----------------------------------------------------------
CREATE TABLE dbo.NhaXuatBan (
    MaNhaXuatBan    NVARCHAR(20)  NOT NULL PRIMARY KEY,
    DiaChi          NVARCHAR(250) NULL,
    SoDienThoai     NVARCHAR(20)  NULL
);
GO

-- 3.4. DauSach (BR07, BR12, BR13) --------------------------------------------
CREATE TABLE dbo.DauSach (
    MaDauSach       NVARCHAR(20)  NOT NULL PRIMARY KEY,
    TenSach         NVARCHAR(200) NOT NULL,
    NamXuatBan      INT           NOT NULL,
    SoLuongHienCo   INT           NOT NULL
        CONSTRAINT CK_DauSach_SoLuong CHECK (SoLuongHienCo >= 0),
    MaTheLoai       NVARCHAR(20)  NOT NULL,
    MaNhaXuatBan    NVARCHAR(20)  NOT NULL,
    CONSTRAINT FK_DauSach_TheLoai FOREIGN KEY (MaTheLoai)
        REFERENCES dbo.TheLoai(MaTheLoai),
    CONSTRAINT FK_DauSach_NXB FOREIGN KEY (MaNhaXuatBan)
        REFERENCES dbo.NhaXuatBan(MaNhaXuatBan)
);
GO

-- 3.5. DocGia -----------------------------------------------------------------
CREATE TABLE dbo.DocGia (
    MaDocGia        NVARCHAR(20)  NOT NULL PRIMARY KEY,
    Ho              NVARCHAR(50)  NOT NULL,
    Ten             NVARCHAR(50)  NOT NULL,
    NgaySinh        DATE          NOT NULL,
    Phai            NVARCHAR(10)  NOT NULL,
    SoDienThoai     NVARCHAR(20)  NULL,
    DiaChi          NVARCHAR(250) NOT NULL,
    Email           NVARCHAR(150) NOT NULL,
    Anh3x4          NVARCHAR(260) NULL
);
GO

-- 3.6. TheDocGia (BR01, BR02, BR03) --------------------------------------------
-- MaThe la khoa ky thuat bo sung de luu lich su cap/gia han the.
CREATE TABLE dbo.TheDocGia (
    MaThe           NVARCHAR(30)  NOT NULL PRIMARY KEY,
    MaDocGia        NVARCHAR(20)  NOT NULL,
    NgayCap         DATE          NOT NULL,
    HanSuDung       DATE          NOT NULL,
    DaDongLePhi     BIT           NOT NULL,
    TrangThai       BIT           NOT NULL
        CONSTRAINT DF_TheDocGia_TrangThai DEFAULT (1),
    CONSTRAINT CK_TheDocGia_Han CHECK (HanSuDung >= NgayCap),
    CONSTRAINT FK_TheDocGia_DocGia FOREIGN KEY (MaDocGia)
        REFERENCES dbo.DocGia(MaDocGia)
);
GO

-- UNIQUE INDEX loc: moi doc gia chi co MOT the dang hoat dong (TrangThai=1) - bao ve BR02
CREATE UNIQUE INDEX UX_TheDocGia_MotTheHoatDong
    ON dbo.TheDocGia(MaDocGia) WHERE TrangThai = 1;
GO

-- 3.7. PhieuMuon (BR08) ---------------------------------------------------------
CREATE TABLE dbo.PhieuMuon (
    MaPhieuMuon     NVARCHAR(30)  NOT NULL PRIMARY KEY,
    MaDocGia        NVARCHAR(20)  NOT NULL,
    MaNhanVien      NVARCHAR(20)  NOT NULL,
    NgayMuon        DATE          NOT NULL,
    NgayHenTra      DATE          NOT NULL,
    CONSTRAINT CK_PhieuMuon_Ngay CHECK (NgayHenTra >= NgayMuon),
    CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia)
        REFERENCES dbo.DocGia(MaDocGia),
    CONSTRAINT FK_PhieuMuon_NhanVien FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien)
);
GO

-- 3.8. ChiTietPhieuMuon (BR06) ----------------------------------------------------
-- Moi dong tuong ung mot dau sach; UNIQUE bao dam khong co hai sach cung dau sach trong mot phieu.
CREATE TABLE dbo.ChiTietPhieuMuon (
    MaChiTiet       NVARCHAR(35)  NOT NULL PRIMARY KEY,
    MaPhieuMuon     NVARCHAR(30)  NOT NULL,
    MaDauSach       NVARCHAR(20)  NOT NULL,
    NgayTraThucTe   DATE          NULL,
    TinhTrangTra    NVARCHAR(50)  NULL,
    CONSTRAINT UQ_CTPM_Phieu_DauSach UNIQUE (MaPhieuMuon, MaDauSach),
    CONSTRAINT FK_CTPM_PhieuMuon FOREIGN KEY (MaPhieuMuon)
        REFERENCES dbo.PhieuMuon(MaPhieuMuon),
    CONSTRAINT FK_CTPM_DauSach FOREIGN KEY (MaDauSach)
        REFERENCES dbo.DauSach(MaDauSach)
);
GO

-- 3.9. PhieuPhat (BR09, BR10) -----------------------------------------------------
CREATE TABLE dbo.PhieuPhat (
    MaPhieuPhat     NVARCHAR(35)  NOT NULL PRIMARY KEY,
    MaChiTiet       NVARCHAR(35)  NOT NULL,
    MaNhanVien      NVARCHAR(20)  NOT NULL,
    NgayPhat        DATE          NOT NULL,
    LyDo            NVARCHAR(250) NOT NULL,
    PhiPhat         DECIMAL(18,0) NOT NULL
        CONSTRAINT CK_PhieuPhat_Phi CHECK (PhiPhat >= 0),
    CONSTRAINT FK_PhieuPhat_CTPM FOREIGN KEY (MaChiTiet)
        REFERENCES dbo.ChiTietPhieuMuon(MaChiTiet),
    CONSTRAINT FK_PhieuPhat_NhanVien FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien)
);
GO

-- =========================================================================
-- 4. INDEX BO SUNG (toi uu truy van thuong dung)
-- =========================================================================
CREATE INDEX IX_PhieuMuon_DocGia       ON dbo.PhieuMuon(MaDocGia);
CREATE INDEX IX_PhieuMuon_NgayHenTra   ON dbo.PhieuMuon(NgayHenTra);
CREATE INDEX IX_CTPM_MaDauSach         ON dbo.ChiTietPhieuMuon(MaDauSach);
CREATE INDEX IX_PhieuPhat_NgayPhat     ON dbo.PhieuPhat(NgayPhat);
GO

-- =========================================================================
-- 5. DML - DU LIEU MAU (day du cho ca 9 bang, san sang de test)
-- =========================================================================

-- 5.1. NhanVien
INSERT INTO dbo.NhanVien (MaNhanVien, Ho, Ten, Phai, NgaySinh, ChucVu, SoDienThoai) VALUES
(N'NV001', N'Nguyễn', N'An',   N'Nam', '1990-02-15', N'Thủ thư',                 N'0901000001'),
(N'NV002', N'Trần',   N'Bình', N'Nữ',  '1992-08-20', N'Nhân viên quản lý sách',  N'0901000002'),
(N'NV003', N'Lê',     N'Cường',N'Nam', '1995-11-05', N'Thủ thư',                 N'0901000003');
GO

-- 5.2. TheLoai
INSERT INTO dbo.TheLoai (MaTheLoai, TenTheLoai) VALUES
(N'TL001', N'Tin học'),
(N'TL002', N'Tiểu thuyết'),
(N'TL003', N'Anh văn'),
(N'TL004', N'Truyện ngắn');
GO

-- 5.3. NhaXuatBan
INSERT INTO dbo.NhaXuatBan (MaNhaXuatBan, DiaChi, SoDienThoai) VALUES
(N'NXB001', N'Quận 1, TP.HCM',        N'0283000001'),
(N'NXB002', N'Quận Cầu Giấy, Hà Nội', N'0243000002');
GO

-- 5.4. DauSach
INSERT INTO dbo.DauSach (MaDauSach, TenSach, NamXuatBan, SoLuongHienCo, MaTheLoai, MaNhaXuatBan) VALUES
(N'S001', N'Lập trình C# căn bản',    2025, 5, N'TL001', N'NXB001'),
(N'S002', N'Cơ sở dữ liệu',           2024, 4, N'TL001', N'NXB001'),
(N'S003', N'Mạng máy tính',           2023, 3, N'TL001', N'NXB002'),
(N'S004', N'Tiếng Anh chuyên ngành',  2024, 2, N'TL003', N'NXB002'),
(N'S005', N'Số đỏ',                   2020, 0, N'TL002', N'NXB001'),
(N'S006', N'Chí Phèo và các truyện ngắn khác', 2019, 6, N'TL004', N'NXB001');
GO

-- 5.5. DocGia
INSERT INTO dbo.DocGia (MaDocGia, Ho, Ten, NgaySinh, Phai, SoDienThoai, DiaChi, Email, Anh3x4) VALUES
(N'DG001', N'Lê',    N'Minh', '2003-05-12', N'Nam', N'0911000001', N'TP.HCM', N'minh@example.com', NULL),
(N'DG002', N'Phạm',  N'Lan',  '2002-10-23', N'Nữ',  N'0911000002', N'TP.HCM', N'lan@example.com',  NULL),
(N'DG003', N'Hoàng', N'Nam',  '2001-03-08', N'Nam', N'0911000003', N'Hà Nội', N'nam@example.com',  NULL);
GO

-- 5.6. TheDocGia (moi doc gia mot the dang hoat dong)
INSERT INTO dbo.TheDocGia (MaThe, MaDocGia, NgayCap, HanSuDung, DaDongLePhi, TrangThai) VALUES
(N'THE_DG001_2026', N'DG001', '2026-01-01', '2026-12-31', 1, 1),
(N'THE_DG002_2026', N'DG002', '2026-01-01', '2026-12-31', 1, 1),
(N'THE_DG003_2025', N'DG003', '2025-01-01', '2025-12-31', 1, 0);  -- the cu, da bi thay the (TrangThai=0)
GO

-- 5.7. PhieuMuon (DG002 dang muon 1 cuon, con han)
INSERT INTO dbo.PhieuMuon (MaPhieuMuon, MaDocGia, MaNhanVien, NgayMuon, NgayHenTra) VALUES
(N'PM20260101001', N'DG002', N'NV001', '2026-01-10', '2026-01-24');
GO

-- 5.8. ChiTietPhieuMuon
INSERT INTO dbo.ChiTietPhieuMuon (MaChiTiet, MaPhieuMuon, MaDauSach, NgayTraThucTe, TinhTrangTra) VALUES
(N'PM20260101001_01', N'PM20260101001', N'S002', NULL, NULL);
GO

-- 5.9. PhieuPhat (vi du: chua co phat sinh phat trong du lieu mau ban dau)
-- Bang PhieuPhat de trong luc khoi tao; du lieu se duoc sinh ra khi thuc hien
-- nghiep vu tra sach tre han / mat / hu hong qua chuong trinh (xem Muc 3.3 bao cao).

-- =========================================================================
-- 6. KIEM TRA NHANH SAU KHI CHAY SCRIPT
-- =========================================================================
-- SELECT * FROM dbo.NhanVien;
-- SELECT * FROM dbo.DauSach;
-- SELECT * FROM dbo.TheDocGia;
-- SELECT * FROM dbo.PhieuMuon;
-- SELECT * FROM dbo.ChiTietPhieuMuon;
GO
