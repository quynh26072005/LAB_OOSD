/* =====================================================================
   BÀI 7 - QUẢN LÝ NHÂN SỰ (Công ty Sơn Đông Á)
   CSDL SQL Server nhất quán với mô hình lớp (Class Diagram)
   Phiên bản: .NET Framework 4.7.2 / SQL Server 2019+
   ===================================================================== */

IF DB_ID('QuanLyNhanSu') IS NULL
    CREATE DATABASE QuanLyNhanSu;
GO

USE QuanLyNhanSu;
GO

-- Xóa theo thứ tự phụ thuộc (cho phép chạy lại script)
IF OBJECT_ID('dbo.ChiTietBangLuong','U') IS NOT NULL DROP TABLE dbo.ChiTietBangLuong;
IF OBJECT_ID('dbo.BangLuong','U') IS NOT NULL DROP TABLE dbo.BangLuong;
IF OBJECT_ID('dbo.ChiTietPhuTroi','U') IS NOT NULL DROP TABLE dbo.ChiTietPhuTroi;
IF OBJECT_ID('dbo.PhieuPhuTroi','U') IS NOT NULL DROP TABLE dbo.PhieuPhuTroi;
IF OBJECT_ID('dbo.ChiTietChamCong','U') IS NOT NULL DROP TABLE dbo.ChiTietChamCong;
IF OBJECT_ID('dbo.BangChamCong','U') IS NOT NULL DROP TABLE dbo.BangChamCong;
IF OBJECT_ID('dbo.ChiTietPhanCong','U') IS NOT NULL DROP TABLE dbo.ChiTietPhanCong;
IF OBJECT_ID('dbo.KeHoachPhanCong','U') IS NOT NULL DROP TABLE dbo.KeHoachPhanCong;
IF OBJECT_ID('dbo.CaLamViec','U') IS NOT NULL DROP TABLE dbo.CaLamViec;
IF OBJECT_ID('dbo.BaoHiem','U') IS NOT NULL DROP TABLE dbo.BaoHiem;
IF OBJECT_ID('dbo.HopDong','U') IS NOT NULL DROP TABLE dbo.HopDong;
IF OBJECT_ID('dbo.PhieuThuNopPhat','U') IS NOT NULL DROP TABLE dbo.PhieuThuNopPhat;
IF OBJECT_ID('dbo.PhieuChiKhenThuong','U') IS NOT NULL DROP TABLE dbo.PhieuChiKhenThuong;
IF OBJECT_ID('dbo.QD_KyLuat','U') IS NOT NULL DROP TABLE dbo.QD_KyLuat;
IF OBJECT_ID('dbo.QD_KhenThuong','U') IS NOT NULL DROP TABLE dbo.QD_KhenThuong;
IF OBJECT_ID('dbo.QD_Luong','U') IS NOT NULL DROP TABLE dbo.QD_Luong;
IF OBJECT_ID('dbo.QD_ChucVu','U') IS NOT NULL DROP TABLE dbo.QD_ChucVu;
IF OBJECT_ID('dbo.QD_TuyenDung','U') IS NOT NULL DROP TABLE dbo.QD_TuyenDung;
IF OBJECT_ID('dbo.QuyetDinh','U') IS NOT NULL DROP TABLE dbo.QuyetDinh;
IF OBJECT_ID('dbo.NhanVien','U') IS NOT NULL DROP TABLE dbo.NhanVien;
IF OBJECT_ID('dbo.ChucVu','U') IS NOT NULL DROP TABLE dbo.ChucVu;
IF OBJECT_ID('dbo.PhongBan','U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO

/* =========================== 1. Danh mục =========================== */
CREATE TABLE dbo.PhongBan (
    MaPB  NVARCHAR(20)  NOT NULL CONSTRAINT PK_PhongBan PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    MoTa  NVARCHAR(500) NULL
);

CREATE TABLE dbo.ChucVu (
    MaCV         NVARCHAR(20)  NOT NULL CONSTRAINT PK_ChucVu PRIMARY KEY,
    TenCV        NVARCHAR(100) NOT NULL,
    PhuCapChucVu DECIMAL(18,2) NOT NULL CONSTRAINT DF_ChucVu_PhuCap DEFAULT 0
);

CREATE TABLE dbo.CaLamViec (
    MaCa       NVARCHAR(20) NOT NULL CONSTRAINT PK_CaLamViec PRIMARY KEY,
    TenCa      NVARCHAR(100) NOT NULL,
    GioBatDau  TIME(0)      NOT NULL,
    GioKetThuc TIME(0)      NOT NULL
);

/* =========================== 2. Nhân viên =========================== */
CREATE TABLE dbo.NhanVien (
    MaNV     NVARCHAR(20)  NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
    HoTen    NVARCHAR(100) NOT NULL,
    NgaySinh DATE          NULL,
    GioiTinh NVARCHAR(10)  NULL,
    DiaChi   NVARCHAR(200) NULL,
    SDT      NVARCHAR(20)  NULL,
    CCCD     NVARCHAR(20)  NULL,
    MaPB     NVARCHAR(20)  NULL CONSTRAINT FK_NhanVien_PhongBan REFERENCES dbo.PhongBan(MaPB),
    MaCV     NVARCHAR(20)  NULL CONSTRAINT FK_NhanVien_ChucVu   REFERENCES dbo.ChucVu(MaCV)
);

/* ============ 3. Quyết định (lớp cha - kế thừa TPT) ================ */
CREATE TABLE dbo.QuyetDinh (
    SoQD        NVARCHAR(30)  NOT NULL CONSTRAINT PK_QuyetDinh PRIMARY KEY,
    LoaiQD      NVARCHAR(20)  NOT NULL,   -- TuyenDung | ChucVu | Luong | KhenThuong | KyLuat
    NgayLap     DATE          NOT NULL,
    NgayHieuLuc DATE          NULL,
    NoiDung     NVARCHAR(1000) NULL,
    NguoiKy     NVARCHAR(100) NULL
);

CREATE TABLE dbo.QD_TuyenDung (
    SoQD             NVARCHAR(30)   NOT NULL CONSTRAINT PK_QD_TuyenDung PRIMARY KEY
        CONSTRAINT FK_QD_TuyenDung_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD),
    ThoiGianThuViec  INT            NOT NULL,          -- số tháng thử việc
    LuongThuViec     DECIMAL(18,2)  NOT NULL,
    DieuKhoan        NVARCHAR(1000) NULL,
    MaNV             NVARCHAR(20)   NOT NULL CONSTRAINT FK_QD_TuyenDung_NhanVien REFERENCES dbo.NhanVien(MaNV),
    MaPB             NVARCHAR(20)   NOT NULL CONSTRAINT FK_QD_TuyenDung_PhongBan REFERENCES dbo.PhongBan(MaPB)
);

CREATE TABLE dbo.QD_ChucVu (
    SoQD          NVARCHAR(30)  NOT NULL CONSTRAINT PK_QD_ChucVu PRIMARY KEY
        CONSTRAINT FK_QD_ChucVu_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD),
    MaNV          NVARCHAR(20)  NOT NULL CONSTRAINT FK_QD_ChucVu_NhanVien REFERENCES dbo.NhanVien(MaNV),
    MaCV          NVARCHAR(20)  NOT NULL CONSTRAINT FK_QD_ChucVu_ChucVu   REFERENCES dbo.ChucVu(MaCV),
    PhuCapChucVu  DECIMAL(18,2) NOT NULL CONSTRAINT DF_QD_ChucVu_PhuCap DEFAULT 0
);

CREATE TABLE dbo.QD_Luong (
    SoQD          NVARCHAR(30)  NOT NULL CONSTRAINT PK_QD_Luong PRIMARY KEY
        CONSTRAINT FK_QD_Luong_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD),
    MaNV          NVARCHAR(20)  NOT NULL CONSTRAINT FK_QD_Luong_NhanVien REFERENCES dbo.NhanVien(MaNV),
    LuongCoBan    DECIMAL(18,2) NOT NULL,
    PhuCapLuong   DECIMAL(18,2) NOT NULL CONSTRAINT DF_QD_Luong_PhuCapLuong DEFAULT 0,
    PhuCapDocHai  DECIMAL(18,2) NOT NULL CONSTRAINT DF_QD_Luong_PhuCapDocHai DEFAULT 0
);

CREATE TABLE dbo.QD_KhenThuong (
    SoQD           NVARCHAR(30)  NOT NULL CONSTRAINT PK_QD_KhenThuong PRIMARY KEY
        CONSTRAINT FK_QD_KhenThuong_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD),
    LoaiKhenThuong NVARCHAR(100) NOT NULL,   -- thưởng lễ, cuối năm, lương tháng 13...
    SoTien         DECIMAL(18,2) NOT NULL,
    DoiTuong       NVARCHAR(200) NOT NULL    -- cá nhân (MaNV) hoặc tập thể (MaPB)
);

CREATE TABLE dbo.QD_KyLuat (
    SoQD           NVARCHAR(30)  NOT NULL CONSTRAINT PK_QD_KyLuat PRIMARY KEY
        CONSTRAINT FK_QD_KyLuat_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD),
    HinhThucKyLuat NVARCHAR(100) NOT NULL,
    SoTienPhat     DECIMAL(18,2) NOT NULL,
    DoiTuong       NVARCHAR(200) NOT NULL
);

/* =========================== 4. Hợp đồng - Bảo hiểm =========================== */
CREATE TABLE dbo.HopDong (
    SoHD        NVARCHAR(30)  NOT NULL CONSTRAINT PK_HopDong PRIMARY KEY,
    LoaiHD      NVARCHAR(20)  NOT NULL,       -- 'Dài hạn' | 'Ngắn hạn'
    NgayKy      DATE          NOT NULL,
    NgayBatDau  DATE          NOT NULL,
    NgayKetThuc DATE          NULL,
    MaNV        NVARCHAR(20)  NOT NULL CONSTRAINT FK_HopDong_NhanVien REFERENCES dbo.NhanVien(MaNV)
);

CREATE TABLE dbo.BaoHiem (
    MaBH       NVARCHAR(30)  NOT NULL CONSTRAINT PK_BaoHiem PRIMARY KEY,
    LoaiBH     NVARCHAR(20)  NOT NULL,        -- 'BHYT' | 'BHXH'
    SoSo       NVARCHAR(30)  NULL,
    MucDong    DECIMAL(18,2) NOT NULL,        -- mức đóng
    NgayBatDau DATE          NULL,
    SoHD       NVARCHAR(30)  NOT NULL CONSTRAINT FK_BaoHiem_HopDong REFERENCES dbo.HopDong(SoHD)
);

/* ===================== 5. Phân công - Chấm công ========================= */
CREATE TABLE dbo.KeHoachPhanCong (
    MaKH      NVARCHAR(30)  NOT NULL CONSTRAINT PK_KeHoachPhanCong PRIMARY KEY,
    Tuan      NVARCHAR(20)  NOT NULL,         -- tuần trong năm '2026-W43'
    MaPB      NVARCHAR(20)  NOT NULL CONSTRAINT FK_KeHoachPhanCong_PhongBan REFERENCES dbo.PhongBan(MaPB),
    NguoiLap  NVARCHAR(100) NULL
);

CREATE TABLE dbo.ChiTietPhanCong (
    MaKH       NVARCHAR(30) NOT NULL CONSTRAINT FK_ChiTietPhanCong_KeHoach REFERENCES dbo.KeHoachPhanCong(MaKH),
    MaNV       NVARCHAR(20) NOT NULL CONSTRAINT FK_ChiTietPhanCong_NhanVien REFERENCES dbo.NhanVien(MaNV),
    Ngay       DATE         NOT NULL,
    MaCa       NVARCHAR(20) NOT NULL CONSTRAINT FK_ChiTietPhanCong_Ca      REFERENCES dbo.CaLamViec(MaCa),
    CoPhuTroi  BIT          NOT NULL CONSTRAINT DF_ChiTietPhanCong_CoPhuTroi DEFAULT 0,
    CONSTRAINT PK_ChiTietPhanCong PRIMARY KEY (MaKH, MaNV, Ngay)
);

CREATE TABLE dbo.BangChamCong (
    MaCC  NVARCHAR(30) NOT NULL CONSTRAINT PK_BangChamCong PRIMARY KEY,
    Thang INT          NOT NULL,
    Nam   INT          NOT NULL,
    MaPB  NVARCHAR(20) NOT NULL CONSTRAINT FK_BangChamCong_PhongBan REFERENCES dbo.PhongBan(MaPB)
);

CREATE TABLE dbo.ChiTietChamCong (
    MaCC     NVARCHAR(30) NOT NULL CONSTRAINT FK_ChiTietChamCong_Bang REFERENCES dbo.BangChamCong(MaCC),
    MaNV     NVARCHAR(20) NOT NULL CONSTRAINT FK_ChiTietChamCong_NhanVien REFERENCES dbo.NhanVien(MaNV),
    Ngay     DATE         NOT NULL,
    MaCa     NVARCHAR(20) NOT NULL CONSTRAINT FK_ChiTietChamCong_Ca REFERENCES dbo.CaLamViec(MaCa),
    TrangThai NVARCHAR(30) NULL,              -- 'Đi làm' | 'Vắng' | 'Nghỉ phép'...
    CONSTRAINT PK_ChiTietChamCong PRIMARY KEY (MaCC, MaNV, Ngay)
);

/* ========================= 6. Phụ trội =========================== */
CREATE TABLE dbo.PhieuPhuTroi (
    SoPhieu      NVARCHAR(30) NOT NULL CONSTRAINT PK_PhieuPhuTroi PRIMARY KEY,
    NgayPhuTroi  DATE         NOT NULL,
    SoGioPhuTroi FLOAT(53)    NOT NULL,
    HinhThuc     NVARCHAR(30) NOT NULL,       -- 'Tăng ca' | 'Ngày lễ' | 'Ngày nghỉ'
    HeSo         FLOAT(53)    NOT NULL CONSTRAINT DF_PhieuPhuTroi_HeSo DEFAULT 1.5
);

CREATE TABLE dbo.ChiTietPhuTroi (
    SoPhieu NVARCHAR(30) NOT NULL CONSTRAINT FK_ChiTietPhuTroi_Phieu REFERENCES dbo.PhieuPhuTroi(SoPhieu),
    MaNV    NVARCHAR(20) NOT NULL CONSTRAINT FK_ChiTietPhuTroi_NhanVien REFERENCES dbo.NhanVien(MaNV),
    SoGio   FLOAT(53)    NOT NULL,
    CONSTRAINT PK_ChiTietPhuTroi PRIMARY KEY (SoPhieu, MaNV)
);

/* ========================= 7. Bảng lương =========================== */
CREATE TABLE dbo.BangLuong (
    MaBL    NVARCHAR(30) NOT NULL CONSTRAINT PK_BangLuong PRIMARY KEY,
    Thang   INT          NOT NULL,
    Nam     INT          NOT NULL,
    NgayLap DATE         NOT NULL
);

CREATE TABLE dbo.ChiTietBangLuong (
    MaBL         NVARCHAR(30)  NOT NULL CONSTRAINT FK_ChiTietBangLuong_Bang  REFERENCES dbo.BangLuong(MaBL),
    MaNV         NVARCHAR(20)  NOT NULL CONSTRAINT FK_ChiTietBangLuong_NhanVien REFERENCES dbo.NhanVien(MaNV),
    LuongCB      DECIMAL(18,2) NOT NULL,
    PhuCap       DECIMAL(18,2) NOT NULL,
    TienPhuTroi  DECIMAL(18,2) NOT NULL,
    TienThuong   DECIMAL(18,2) NOT NULL,
    TienPhat     DECIMAL(18,2) NOT NULL,
    BHYT         DECIMAL(18,2) NOT NULL,
    BHXH         DECIMAL(18,2) NOT NULL,
    ThucLanh     DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_ChiTietBangLuong PRIMARY KEY (MaBL, MaNV)
);

/* ================= 8. Phiếu chi khen thưởng / Phiếu thu nộp phạt ============ */
CREATE TABLE dbo.PhieuChiKhenThuong (
    SoPhieuChi NVARCHAR(30)  NOT NULL CONSTRAINT PK_PhieuChiKhenThuong PRIMARY KEY,
    NgayChi    DATE          NOT NULL,
    SoTien     DECIMAL(18,2) NOT NULL,
    SoQD       NVARCHAR(30)  NOT NULL CONSTRAINT FK_PhieuChiKhenThuong_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD)
);

CREATE TABLE dbo.PhieuThuNopPhat (
    SoPhieuThu NVARCHAR(30)  NOT NULL CONSTRAINT PK_PhieuThuNopPhat PRIMARY KEY,
    NgayThu    DATE          NOT NULL,
    SoTien     DECIMAL(18,2) NOT NULL,
    HinhThuc   NVARCHAR(50)  NULL,            -- 'Thu tiền mặt' | 'Trừ lương'
    SoQD       NVARCHAR(30)  NOT NULL CONSTRAINT FK_PhieuThuNopPhat_QuyetDinh REFERENCES dbo.QuyetDinh(SoQD)
);
GO

/* =====================================================================
   DỮ LIỆU MẪU (seed)
   ===================================================================== */
-- Phòng ban
INSERT dbo.PhongBan(MaPB, TenPB, MoTa) VALUES
    ('PB01', N'Phòng Tài chính - Kế toán', N'Kế toán, tài chính công ty'),
    ('PB02', N'Phòng Kỹ thuật',             N'Kỹ thuật sản xuất và R&D'),
    ('PB03', N'Phòng Sản xuất',             N'Nhà máy sản xuất sơn'),
    ('PB04', N'Phòng Nhân sự',              N'Tuyển dụng và hành chính');

-- Chức vụ
INSERT dbo.ChucVu(MaCV, TenCV, PhuCapChucVu) VALUES
    ('CV01', N'Nhân viên',      0),
    ('CV02', N'Trưởng phòng',   3000000),
    ('CV03', N'Phó phòng',      1500000),
    ('CV04', N'Giám đốc',       5000000);

-- Ca làm việc
INSERT dbo.CaLamViec(MaCa, TenCa, GioBatDau, GioKetThuc) VALUES
    ('CA1', N'Ca sáng',  '08:00', '12:00'),
    ('CA2', N'Ca chiều', '13:00', '17:00'),
    ('CA3', N'Ca tối',   '18:00', '22:00');

-- Nhân viên
INSERT dbo.NhanVien(MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SDT, CCCD, MaPB, MaCV) VALUES
    ('NV001', N'Nguyễn Văn An',   '1990-05-12', N'Nam',   N'TP.HCM', '0901000001', '079090000001', 'PB04', 'CV04'),
    ('NV002', N'Trần Thị Bình',   '1995-08-20', N'Nữ',    N'TP.HCM', '0901000002', '079090000002', 'PB01', 'CV02'),
    ('NV003', N'Lê Văn Cường',    '1998-01-15', N'Nam',   N'TP.HCM', '0901000003', '079090000003', 'PB02', 'CV03'),
    ('NV004', N'Phạm Thị Dung',   '2000-03-09', N'Nữ',    N'TP.HCM', '0901000004', '079090000004', 'PB03', 'CV01'),
    ('NV005', N'Võ Hoàng Em',     '1999-11-30', N'Nam',   N'TP.HCM', '0901000005', '079090000005', 'PB03', 'CV01');

-- Quyết định (lớp cha)
INSERT dbo.QuyetDinh(SoQD, LoaiQD, NgayLap, NgayHieuLuc, NoiDung, NguoiKy) VALUES
    ('QD/TD/NV004', N'TuyenDung', '2026-01-05', '2026-01-10', N'Quyết định tuyển dụng thử việc', N'Giám đốc'),
    ('QD/TD/NV005', N'TuyenDung', '2026-02-03', '2026-02-08', N'Quyết định tuyển dụng thử việc', N'Giám đốc'),
    ('QD/LU/NV004', N'Luong',     '2026-04-10', '2026-05-01', N'Quyết định lương chính thức',    N'Giám đốc'),
    ('QD/CV/NV004', N'ChucVu',    '2026-04-10', '2026-05-01', N'Bổ nhiệm chức vụ',               N'Giám đốc'),
    ('QD/LU/NV002', N'Luong',     '2025-01-01', '2025-01-01', N'Quyết định lương',               N'Giám đốc'),
    ('QD/LU/NV003', N'Luong',     '2025-01-01', '2025-01-01', N'Quyết định lương',               N'Giám đốc'),
    ('QD/KT/NV003', N'KhenThuong','2026-10-01', '2026-10-01', N'Thưởng cá nhân xuất sắc quý III', N'Giám đốc'),
    ('QD/KL/NV005', N'KyLuat',    '2026-09-15', '2026-09-15', N'Kỷ luật vi phạm nội quy',        N'Giám đốc');

-- Chi tiết quyết định
INSERT dbo.QD_TuyenDung(SoQD, ThoiGianThuViec, LuongThuViec, DieuKhoan, MaNV, MaPB) VALUES
    ('QD/TD/NV004', 3, 6000000, N'Thử việc 3 tháng theo điều khoản hai bên', 'NV004', 'PB03'),
    ('QD/TD/NV005', 2, 5500000, N'Thử việc 2 tháng theo điều khoản hai bên', 'NV005', 'PB03');

INSERT dbo.QD_Luong(SoQD, MaNV, LuongCoBan, PhuCapLuong, PhuCapDocHai) VALUES
    ('QD/LU/NV002', 'NV002', 12000000, 2000000, 0),
    ('QD/LU/NV003', 'NV003', 10000000, 1500000, 0),
    ('QD/LU/NV004', 'NV004', 8000000,  500000,  500000);

INSERT dbo.QD_ChucVu(SoQD, MaNV, MaCV, PhuCapChucVu) VALUES
    ('QD/CV/NV004', 'NV004', 'CV02', 3000000);

INSERT dbo.QD_KhenThuong(SoQD, LoaiKhenThuong, SoTien, DoiTuong) VALUES
    ('QD/KT/NV003', N'Thưởng thành tích cá nhân', 2000000, 'NV003');

INSERT dbo.QD_KyLuat(SoQD, HinhThucKyLuat, SoTienPhat, DoiTuong) VALUES
    ('QD/KL/NV005', N'Vi phạm nội quy ca làm việc', 500000, 'NV005');

-- Hợp đồng
INSERT dbo.HopDong(SoHD, LoaiHD, NgayKy, NgayBatDau, NgayKetThuc, MaNV) VALUES
    ('HD/NV002', N'Dài hạn',  '2025-01-01', '2025-01-01', NULL,       'NV002'),
    ('HD/NV003', N'Ngắn hạn', '2025-01-01', '2025-01-01', '2025-12-31', 'NV003'),
    ('HD/NV004', N'Dài hạn',  '2026-05-01', '2026-05-01', NULL,       'NV004');

-- Bảo hiểm (chỉ hợp đồng dài hạn)
INSERT dbo.BaoHiem(MaBH, LoaiBH, SoSo, MucDong, NgayBatDau, SoHD) VALUES
    ('BH/NV002/BHYT', 'BHYT', 'DN00000001', 45000, '2025-01-01', 'HD/NV002'),
    ('BH/NV002/BHXH', 'BHXH', 'DN00000002', 1280000, '2025-01-01', 'HD/NV002'),
    ('BH/NV004/BHYT', 'BHYT', 'DN00000003', 45000, '2026-05-01', 'HD/NV004'),
    ('BH/NV004/BHXH', 'BHXH', 'DN00000004', 1050000, '2026-05-01', 'HD/NV004');

-- Kế hoạch phân công + chi tiết
INSERT dbo.KeHoachPhanCong(MaKH, Tuan, MaPB, NguoiLap) VALUES
    ('KH/2026-W43/PB03', '2026-W43', 'PB03', N'Trưởng phòng sản xuất');

INSERT dbo.ChiTietPhanCong(MaKH, MaNV, Ngay, MaCa, CoPhuTroi) VALUES
    ('KH/2026-W43/PB03', 'NV004', '2026-10-19', 'CA1', 0),
    ('KH/2026-W43/PB03', 'NV005', '2026-10-19', 'CA1', 1),
    ('KH/2026-W43/PB03', 'NV004', '2026-10-20', 'CA2', 0),
    ('KH/2026-W43/PB03', 'NV005', '2026-10-20', 'CA2', 0);

-- Bảng chấm công + chi tiết
INSERT dbo.BangChamCong(MaCC, Thang, Nam, MaPB) VALUES
    ('CC/2026/10/PB03', 10, 2026, 'PB03');

INSERT dbo.ChiTietChamCong(MaCC, MaNV, Ngay, MaCa, TrangThai) VALUES
    ('CC/2026/10/PB03', 'NV004', '2026-10-19', 'CA1', N'Đi làm'),
    ('CC/2026/10/PB03', 'NV005', '2026-10-19', 'CA1', N'Đi làm'),
    ('CC/2026/10/PB03', 'NV004', '2026-10-20', 'CA2', N'Vắng'),
    ('CC/2026/10/PB03', 'NV005', '2026-10-20', 'CA2', N'Đi làm');

-- Phiếu phụ trội + chi tiết
INSERT dbo.PhieuPhuTroi(SoPhieu, NgayPhuTroi, SoGioPhuTroi, HinhThuc, HeSo) VALUES
    ('PT/2026/10/001', '2026-10-19', 4, N'Tăng ca', 1.5);

INSERT dbo.ChiTietPhuTroi(SoPhieu, MaNV, SoGio) VALUES
    ('PT/2026/10/001', 'NV005', 4);

-- Phiếu chi khen thưởng / thu nộp phạt
INSERT dbo.PhieuChiKhenThuong(SoPhieuChi, NgayChi, SoTien, SoQD) VALUES
    ('PC/2026/10/001', '2026-10-05', 2000000, 'QD/KT/NV003');

INSERT dbo.PhieuThuNopPhat(SoPhieuThu, NgayThu, SoTien, HinhThuc, SoQD) VALUES
    ('PT/2026/10/002', '2026-09-20', 500000, N'Trừ lương', 'QD/KL/NV005');
GO

PRINT N'Tạo xong CSDL QuanLyNhanSu.';
GO