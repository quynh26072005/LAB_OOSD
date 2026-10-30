IF DB_ID(N'QuanLyCaoOc') IS NULL CREATE DATABASE QuanLyCaoOc;
GO
USE QuanLyCaoOc;
GO

/* ---------- Xoa bang cu (theo thu tu phu thuoc) ---------- */
IF OBJECT_ID('BienBanPhat','U') IS NOT NULL DROP TABLE BienBanPhat;
IF OBJECT_ID('ChiTietKiemTra','U') IS NOT NULL DROP TABLE ChiTietKiemTra;
IF OBJECT_ID('DotKiemTra','U') IS NOT NULL DROP TABLE DotKiemTra;
IF OBJECT_ID('QuyDinhPhat','U') IS NOT NULL DROP TABLE QuyDinhPhat;
IF OBJECT_ID('TheGiuXe','U') IS NOT NULL DROP TABLE TheGiuXe;
IF OBJECT_ID('NhanVienCongTy','U') IS NOT NULL DROP TABLE NhanVienCongTy;
IF OBJECT_ID('PhieuThu','U') IS NOT NULL DROP TABLE PhieuThu;
IF OBJECT_ID('ChiTietHoaDonDV','U') IS NOT NULL DROP TABLE ChiTietHoaDonDV;
IF OBJECT_ID('HoaDonDichVu','U') IS NOT NULL DROP TABLE HoaDonDichVu;
IF OBJECT_ID('DichVu','U') IS NOT NULL DROP TABLE DichVu;
IF OBJECT_ID('KyThanhToan','U') IS NOT NULL DROP TABLE KyThanhToan;
IF OBJECT_ID('PhieuGiaHan','U') IS NOT NULL DROP TABLE PhieuGiaHan;
IF OBJECT_ID('ChiTietHopDong','U') IS NOT NULL DROP TABLE ChiTietHopDong;
IF OBJECT_ID('HopDong','U') IS NOT NULL DROP TABLE HopDong;
IF OBJECT_ID('KhachHang','U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID('Phong','U') IS NOT NULL DROP TABLE Phong;
IF OBJECT_ID('Tang','U') IS NOT NULL DROP TABLE Tang;
IF OBJECT_ID('NhanVien','U') IS NOT NULL DROP TABLE NhanVien;
GO

/* ---------- Danh muc ---------- */
CREATE TABLE NhanVien(
    MaNV        varchar(20)   NOT NULL PRIMARY KEY,
    HoTen       nvarchar(120) NOT NULL,
    VaiTro      nvarchar(50)  NOT NULL,
    SoDienThoai varchar(20)   NULL
);

CREATE TABLE Tang(
    MaTang  varchar(20)   NOT NULL PRIMARY KEY,
    TenTang nvarchar(100) NOT NULL UNIQUE,
    SoTang  int           NOT NULL UNIQUE CHECK(SoTang > 0),
    DoCao   decimal(6,1)  NOT NULL CHECK(DoCao >= 0)
);

CREATE TABLE Phong(
    MaPhong     varchar(20)  NOT NULL PRIMARY KEY,
    MaTang      varchar(20)  NOT NULL,
    DienTich    decimal(8,2) NOT NULL CHECK(DienTich > 0),
    HuongPhong  nvarchar(30) NOT NULL,
    DonGiaThang decimal(18,2) NOT NULL CHECK(DonGiaThang >= 0),
    TrangThai   nvarchar(30) NOT NULL DEFAULT N'Trống',
    CONSTRAINT CK_Phong_TrangThai CHECK(TrangThai IN (N'Trống',N'Đang thuê',N'Bảo trì')),
    CONSTRAINT FK_Phong_Tang FOREIGN KEY(MaTang) REFERENCES Tang(MaTang)
);

CREATE TABLE KhachHang(
    MaKhach       varchar(20)   NOT NULL PRIMARY KEY,
    TenKhach      nvarchar(150) NOT NULL,
    SoGiayTo      varchar(30)   NOT NULL UNIQUE,   -- ma so thue / CCCD
    NguoiDaiDien  nvarchar(120) NOT NULL,
    SoDienThoai   varchar(20)   NULL,
    DiaChi        nvarchar(200) NULL
);

CREATE TABLE DichVu(
    MaDV       varchar(20)   NOT NULL PRIMARY KEY,
    TenDV      nvarchar(100) NOT NULL UNIQUE,
    DonViTinh  nvarchar(30)  NOT NULL,
    DonGia     decimal(18,2) NOT NULL CHECK(DonGia >= 0)
);

CREATE TABLE QuyDinhPhat(
    MaQuyDinh varchar(20)   NOT NULL PRIMARY KEY,
    NoiDung   nvarchar(200) NOT NULL,
    MucPhat   decimal(18,2) NOT NULL CHECK(MucPhat > 0)
);
GO

/* ---------- Hop dong thue ---------- */
CREATE TABLE HopDong(
    SoHopDong  varchar(30)   NOT NULL PRIMARY KEY,
    MaKhach    varchar(20)   NOT NULL,
    MaNV       varchar(20)   NOT NULL,
    NgayLap    date          NOT NULL,
    NoiDung    nvarchar(500) NULL,
    TrangThai  nvarchar(30)  NOT NULL DEFAULT N'Hiệu lực',
    CONSTRAINT CK_HopDong_TrangThai CHECK(TrangThai IN (N'Hiệu lực',N'Hết hạn')),
    CONSTRAINT FK_HopDong_KhachHang FOREIGN KEY(MaKhach) REFERENCES KhachHang(MaKhach),
    CONSTRAINT FK_HopDong_NhanVien  FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietHopDong(
    SoHopDong   varchar(30)   NOT NULL,
    MaPhong     varchar(20)   NOT NULL,
    NgayBatDau  date          NOT NULL,
    SoThang     int           NOT NULL CHECK(SoThang >= 6),          -- BR04: thue toi thieu 6 thang
    NgayKetThuc date          NOT NULL,
    DonGiaThue  decimal(18,2) NOT NULL CHECK(DonGiaThue >= 0),       -- gia chot theo thang
    CONSTRAINT PK_ChiTietHopDong PRIMARY KEY(SoHopDong,MaPhong),
    CONSTRAINT CK_CTHD_Ngay CHECK(NgayKetThuc > NgayBatDau),
    CONSTRAINT FK_CTHD_HopDong FOREIGN KEY(SoHopDong) REFERENCES HopDong(SoHopDong),
    CONSTRAINT FK_CTHD_Phong   FOREIGN KEY(MaPhong) REFERENCES Phong(MaPhong)
);

CREATE TABLE PhieuGiaHan(
    SoPhieuGH      varchar(30) NOT NULL PRIMARY KEY,
    SoHopDong      varchar(30) NOT NULL,
    MaPhong        varchar(20) NOT NULL,
    NgayLap        date        NOT NULL,
    SoThangGiaHan  int         NOT NULL CHECK(SoThangGiaHan > 0),
    NgayKetThucCu  date        NOT NULL,
    NgayKetThucMoi date        NOT NULL,
    MaNV           varchar(20) NOT NULL,
    CONSTRAINT CK_PhieuGiaHan_Ngay CHECK(NgayKetThucMoi > NgayKetThucCu),
    CONSTRAINT FK_PhieuGiaHan_CTHD FOREIGN KEY(SoHopDong,MaPhong) REFERENCES ChiTietHopDong(SoHopDong,MaPhong),
    CONSTRAINT FK_PhieuGiaHan_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

/* Moi ky: ky dau = thang 1-6 (tra truoc 6 thang); tu thang 7 moi thang 1 ky, han nop = dau thang */
CREATE TABLE KyThanhToan(
    MaKy       int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SoHopDong  varchar(30)   NOT NULL,
    MaPhong    varchar(20)   NOT NULL,
    TuThang    int           NOT NULL CHECK(TuThang >= 1),
    DenThang   int           NOT NULL,
    TuNgay     date          NOT NULL,
    DenNgay    date          NOT NULL,
    SoTien     decimal(18,2) NOT NULL CHECK(SoTien > 0),
    HanNop     date          NOT NULL,
    TrangThai  nvarchar(30)  NOT NULL DEFAULT N'Chưa thanh toán',
    CONSTRAINT UQ_KyThanhToan UNIQUE(SoHopDong,MaPhong,TuThang),
    CONSTRAINT CK_KyThanhToan_Thang CHECK(DenThang >= TuThang),
    CONSTRAINT CK_KyThanhToan_TT CHECK(TrangThai IN (N'Chưa thanh toán',N'Thanh toán một phần',N'Đã thanh toán')),
    CONSTRAINT FK_KyThanhToan_CTHD FOREIGN KEY(SoHopDong,MaPhong) REFERENCES ChiTietHopDong(SoHopDong,MaPhong)
);
GO

/* ---------- Dich vu cuoi thang ---------- */
CREATE TABLE HoaDonDichVu(
    SoHDDV     varchar(30)   NOT NULL PRIMARY KEY,
    SoHopDong  varchar(30)   NOT NULL,
    MaPhong    varchar(20)   NOT NULL,
    Thang      int           NOT NULL CHECK(Thang BETWEEN 1 AND 12),
    Nam        int           NOT NULL CHECK(Nam >= 2000),
    NgayLap    date          NOT NULL,
    HanNop     date          NOT NULL,                -- cuoi thang
    TongTien   decimal(18,2) NOT NULL DEFAULT 0,
    TrangThai  nvarchar(30)  NOT NULL DEFAULT N'Chưa thanh toán',
    MaNV       varchar(20)   NOT NULL,
    CONSTRAINT UQ_HoaDonDV UNIQUE(SoHopDong,MaPhong,Thang,Nam),
    CONSTRAINT CK_HoaDonDV_TT CHECK(TrangThai IN (N'Chưa thanh toán',N'Thanh toán một phần',N'Đã thanh toán')),
    CONSTRAINT FK_HoaDonDV_CTHD FOREIGN KEY(SoHopDong,MaPhong) REFERENCES ChiTietHopDong(SoHopDong,MaPhong),
    CONSTRAINT FK_HoaDonDV_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietHoaDonDV(
    SoHDDV    varchar(30)   NOT NULL,
    MaDV      varchar(20)   NOT NULL,
    SoLuong   decimal(18,2) NOT NULL CHECK(SoLuong > 0),
    DonGia    decimal(18,2) NOT NULL CHECK(DonGia >= 0),
    ThanhTien decimal(18,2) NOT NULL,
    CONSTRAINT PK_ChiTietHoaDonDV PRIMARY KEY(SoHDDV,MaDV),
    CONSTRAINT FK_CTHDDV_HoaDon FOREIGN KEY(SoHDDV) REFERENCES HoaDonDichVu(SoHDDV),
    CONSTRAINT FK_CTHDDV_DichVu FOREIGN KEY(MaDV) REFERENCES DichVu(MaDV)
);

/* Phieu thu: thu cho DUNG MOT trong hai doi tuong: ky tien thue hoac hoa don dich vu */
CREATE TABLE PhieuThu(
    SoPhieuThu varchar(30)   NOT NULL PRIMARY KEY,
    NgayThu    datetime      NOT NULL,
    SoTien     decimal(18,2) NOT NULL CHECK(SoTien > 0),
    HinhThuc   nvarchar(30)  NOT NULL,
    MaKy       int           NULL,
    SoHDDV     varchar(30)   NULL,
    MaNV       varchar(20)   NOT NULL,
    CONSTRAINT CK_PhieuThu_HinhThuc CHECK(HinhThuc IN (N'Tiền mặt',N'Chuyển khoản')),
    CONSTRAINT CK_PhieuThu_DoiTuong CHECK((MaKy IS NOT NULL AND SoHDDV IS NULL) OR (MaKy IS NULL AND SoHDDV IS NOT NULL)),
    CONSTRAINT FK_PhieuThu_Ky FOREIGN KEY(MaKy) REFERENCES KyThanhToan(MaKy),
    CONSTRAINT FK_PhieuThu_HoaDonDV FOREIGN KEY(SoHDDV) REFERENCES HoaDonDichVu(SoHDDV),
    CONSTRAINT FK_PhieuThu_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);
GO

/* ---------- Nhan vien cong ty va the giu xe ---------- */
CREATE TABLE NhanVienCongTy(
    MaNVCT      varchar(20)   NOT NULL PRIMARY KEY,
    HoTen       nvarchar(120) NOT NULL,
    HinhAnh     nvarchar(260) NULL,                  -- duong dan file anh
    ChucVu      nvarchar(80)  NOT NULL,
    MaKhach     varchar(20)   NOT NULL,              -- cong ty
    MaPhong     varchar(20)   NOT NULL,              -- ten phong -> tang suy ra tu Phong
    NgayVaoLam  date          NOT NULL,
    NgayBaoCao  date          NOT NULL,
    TrangThai   nvarchar(20)  NOT NULL DEFAULT N'Đang làm',
    MaNV        varchar(20)   NOT NULL,              -- nhan vien cao oc tiep nhan bao cao
    CONSTRAINT CK_NVCT_TrangThai CHECK(TrangThai IN (N'Đang làm',N'Đã nghỉ')),
    CONSTRAINT FK_NVCT_KhachHang FOREIGN KEY(MaKhach) REFERENCES KhachHang(MaKhach),
    CONSTRAINT FK_NVCT_Phong FOREIGN KEY(MaPhong) REFERENCES Phong(MaPhong),
    CONSTRAINT FK_NVCT_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE TheGiuXe(
    MaThe      varchar(20)  NOT NULL PRIMARY KEY,
    MaNVCT     varchar(20)  NOT NULL UNIQUE,         -- moi nhan vien mot the
    NgayCap    date         NOT NULL,
    LoaiXe     nvarchar(40) NOT NULL,
    BienSo     varchar(20)  NOT NULL UNIQUE,
    NhanHieu   nvarchar(60) NULL,
    MauSac     nvarchar(30) NULL,
    TrangThai  nvarchar(20) NOT NULL DEFAULT N'Hiệu lực',
    MaNV       varchar(20)  NOT NULL,
    CONSTRAINT CK_TheGiuXe_TT CHECK(TrangThai IN (N'Hiệu lực',N'Đã thu hồi')),
    CONSTRAINT FK_TheGiuXe_NVCT FOREIGN KEY(MaNVCT) REFERENCES NhanVienCongTy(MaNVCT),
    CONSTRAINT FK_TheGiuXe_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);
GO

/* ---------- Kiem tra phong va phat ---------- */
CREATE TABLE DotKiemTra(
    MaDot       varchar(20)  NOT NULL PRIMARY KEY,
    NgayKiemTra date         NOT NULL,
    GhiChu      nvarchar(250) NULL,
    MaNV        varchar(20)  NOT NULL,
    CONSTRAINT FK_DotKiemTra_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietKiemTra(
    MaDot          varchar(20)   NOT NULL,
    MaPhong        varchar(20)   NOT NULL,
    TinhTrang      nvarchar(100) NOT NULL,
    ThayDoiKetCau  bit           NOT NULL DEFAULT 0,
    KeHoachTuSua   nvarchar(250) NULL,
    CONSTRAINT PK_ChiTietKiemTra PRIMARY KEY(MaDot,MaPhong),
    CONSTRAINT FK_CTKT_Dot FOREIGN KEY(MaDot) REFERENCES DotKiemTra(MaDot),
    CONSTRAINT FK_CTKT_Phong FOREIGN KEY(MaPhong) REFERENCES Phong(MaPhong)
);

CREATE TABLE BienBanPhat(
    SoBienBan  varchar(30)   NOT NULL PRIMARY KEY,
    MaDot      varchar(20)   NOT NULL,
    MaPhong    varchar(20)   NOT NULL,
    MaQuyDinh  varchar(20)   NOT NULL,
    MaKhach    varchar(20)   NOT NULL,
    NgayLap    date          NOT NULL,
    SoTien     decimal(18,2) NOT NULL CHECK(SoTien > 0),
    TrangThai  nvarchar(20)  NOT NULL DEFAULT N'Chưa nộp',
    NgayNop    date          NULL,
    MaNV       varchar(20)   NOT NULL,
    CONSTRAINT CK_BienBanPhat_TT CHECK(TrangThai IN (N'Chưa nộp',N'Đã nộp')),
    CONSTRAINT FK_BBP_CTKT FOREIGN KEY(MaDot,MaPhong) REFERENCES ChiTietKiemTra(MaDot,MaPhong),
    CONSTRAINT FK_BBP_QuyDinh FOREIGN KEY(MaQuyDinh) REFERENCES QuyDinhPhat(MaQuyDinh),
    CONSTRAINT FK_BBP_KhachHang FOREIGN KEY(MaKhach) REFERENCES KhachHang(MaKhach),
    CONSTRAINT FK_BBP_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV)
);
GO

/* ---------- Du lieu mau ---------- */
INSERT INTO NhanVien VALUES
('NV01',N'Trần Minh Khoa',N'NV kinh doanh','0901000001'),
('NV02',N'Lê Thu Hà',N'NV kế toán','0901000002'),
('NV03',N'Phạm Quốc Bảo',N'NV an ninh','0901000003'),
('NV04',N'Võ Thanh Tùng',N'NV kỹ thuật','0901000004'),
('NV05',N'Nguyễn Sĩ Quỳnh',N'Quản lý','0901000005');

INSERT INTO Tang VALUES
('T01',N'Tầng 1',1,4.0),('T02',N'Tầng 2',2,8.0),('T03',N'Tầng 3',3,12.0),
('T04',N'Tầng 4',4,16.0),('T05',N'Tầng 5',5,20.0);

INSERT INTO Phong VALUES
('P0101',  'T01', 45.00,N'Đông',  22000000,N'Trống'),
('P0102',  'T01', 60.00,N'Tây',   27000000,N'Trống'),
('P0201',  'T02', 50.00,N'Đông',  24000000,N'Trống'),
('P0202',  'T02', 80.00,N'Nam',   36000000,N'Trống'),
('P0301',  'T03', 50.00,N'Đông',  26000000,N'Trống'),
('P0302',  'T03', 75.00,N'Bắc',   36000000,N'Trống'),
('P0401',  'T04', 55.00,N'Tây',   30000000,N'Trống'),
('P0402',  'T04', 90.00,N'Nam',   46000000,N'Trống'),
('P0501',  'T05', 60.00,N'Đông',  36000000,N'Trống'),
('P0502',  'T05',120.00,N'Nam',   66000000,N'Trống');

INSERT INTO KhachHang VALUES
('KH01',N'Công ty TNHH Phần mềm Sao Mai','0312345678',N'Nguyễn Văn An','0283800001',N'12 Nguyễn Huệ, Q1, TP.HCM'),
('KH02',N'Công ty CP Thương mại Hoàng Long','0398765432',N'Trần Thị Bích','0283800002',N'45 Lê Lợi, Q1, TP.HCM');

INSERT INTO DichVu VALUES
('DV01',N'Điện',N'kWh',3500),
('DV02',N'Nước',N'm3',18000),
('DV03',N'Internet',N'Tháng',1200000),
('DV04',N'Vệ sinh',N'Tháng',500000);

INSERT INTO QuyDinhPhat VALUES
('QD01',N'Tự ý thay đổi vách ngăn, kết cấu văn phòng',20000000),
('QD02',N'Tự ý khoan đục, đi lại hệ thống điện - nước',10000000);
GO
