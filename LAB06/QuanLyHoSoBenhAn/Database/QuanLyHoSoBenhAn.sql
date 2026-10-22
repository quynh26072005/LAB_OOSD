/* ============================================================
   BÀI 5 - QUẢN LÝ HỒ SƠ BỆNH ÁN
   Script tạo CSDL SQL Server theo tài liệu phân tích thiết kế
   (mục 4. THIẾT KẾ CƠ SỞ DỮ LIỆU)

   Thứ tự tạo bảng theo mục 4.5:
   NhanVien, BacSi, Khoa, BenhNhan, HoSoBenhAn, LePhiNam,
   LichKhamDinhKy, PhieuKham, XetNghiem, ChiDinhXetNghiem,
   KetQuaXetNghiem, Thuoc, ToaThuoc, ChiTietToaThuoc,
   HoaDonVienPhi, ChiTietVienPhi, ThanhToan, NhatKyTruyCap

   Chạy toàn bộ file này trong SSMS / Azure Data Studio
   bằng tài khoản có quyền tạo database.
   ============================================================ */

SET NOCOUNT ON;
GO

IF DB_ID(N'QuanLyHoSoBenhAn') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyHoSoBenhAn SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyHoSoBenhAn;
END
GO

CREATE DATABASE QuanLyHoSoBenhAn
COLLATE Vietnamese_CI_AS;
GO

USE QuanLyHoSoBenhAn;
GO

/* ============================================================
   1. NhanVien
   ============================================================ */
CREATE TABLE NhanVien
(
    MaNhanVien   INT IDENTITY(1,1) NOT NULL,
    HoTen        NVARCHAR(100)     NOT NULL,
    DienThoai    NVARCHAR(20)      NULL,
    Email        NVARCHAR(100)     NULL,
    TenDangNhap  NVARCHAR(50)      NOT NULL,
    MatKhauHash  NVARCHAR(256)     NOT NULL,   -- SHA-256 hex, xem Data/DbHelper.cs
    VaiTro       NVARCHAR(30)      NOT NULL,
    TrangThai    NVARCHAR(20)      NOT NULL CONSTRAINT DF_NhanVien_TrangThai DEFAULT (N'DangLamViec'),
    NgayTao      DATETIME          NOT NULL CONSTRAINT DF_NhanVien_NgayTao DEFAULT (GETDATE()),

    CONSTRAINT PK_NhanVien PRIMARY KEY (MaNhanVien),
    CONSTRAINT UQ_NhanVien_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT CK_NhanVien_VaiTro CHECK (VaiTro IN
        (N'LeTan', N'BacSi', N'XetNghiem', N'DuocSi', N'VienPhi', N'QuanLy', N'QuanTri')),
    CONSTRAINT CK_NhanVien_TrangThai CHECK (TrangThai IN (N'DangLamViec', N'NghiViec'))
);
GO

/* ============================================================
   2. BacSi  (chuyên biệt hoá từ NhanVien - 1-1)
   ============================================================ */
CREATE TABLE BacSi
(
    MaNhanVien   INT             NOT NULL,
    SoChungChi   NVARCHAR(50)    NOT NULL,
    ChuyenMon    NVARCHAR(100)   NULL,

    CONSTRAINT PK_BacSi PRIMARY KEY (MaNhanVien),
    CONSTRAINT FK_BacSi_NhanVien FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien (MaNhanVien),
    CONSTRAINT UQ_BacSi_SoChungChi UNIQUE (SoChungChi)
);
GO

/* ============================================================
   3. Khoa
   ============================================================ */
CREATE TABLE Khoa
(
    MaKhoa     INT IDENTITY(1,1) NOT NULL,
    TenKhoa    NVARCHAR(100)     NOT NULL,
    DienThoai  NVARCHAR(20)      NULL,
    ViTri      NVARCHAR(100)     NULL,

    CONSTRAINT PK_Khoa PRIMARY KEY (MaKhoa),
    CONSTRAINT UQ_Khoa_TenKhoa UNIQUE (TenKhoa)
);
GO

/* ============================================================
   4. BenhNhan
   ============================================================ */
CREATE TABLE BenhNhan
(
    MaBenhNhan  INT IDENTITY(1,1) NOT NULL,
    HoTen       NVARCHAR(100)     NOT NULL,
    NgaySinh    DATE              NOT NULL,
    GioiTinh    NVARCHAR(10)      NOT NULL,
    DiaChi      NVARCHAR(200)     NULL,
    DienThoai   NVARCHAR(20)      NULL,
    Email       NVARCHAR(100)     NULL,
    SoGiayTo    NVARCHAR(30)      NULL,         -- khóa tra cứu, không phải khóa kỹ thuật
    Anh         NVARCHAR(260)     NULL,         -- đường dẫn/URI ảnh
    NgayTao     DATETIME          NOT NULL CONSTRAINT DF_BenhNhan_NgayTao DEFAULT (GETDATE()),

    CONSTRAINT PK_BenhNhan PRIMARY KEY (MaBenhNhan),
    CONSTRAINT CK_BenhNhan_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác'))
);
GO
-- SoGiayTo chỉ unique khi có giá trị (not null) -> dùng filtered index (BR01/TC04)
CREATE UNIQUE INDEX UQ_BenhNhan_SoGiayTo ON BenhNhan (SoGiayTo)
    WHERE SoGiayTo IS NOT NULL;
GO
CREATE INDEX IX_BenhNhan_HoTen ON BenhNhan (HoTen);
GO

/* ============================================================
   5. HoSoBenhAn  (chỉ tạo SAU khi lệ phí năm đầu thu đủ - BR02)
   ============================================================ */
CREATE TABLE HoSoBenhAn
(
    MaHoSo      INT IDENTITY(1,1) NOT NULL,
    MaBenhNhan  INT               NOT NULL,
    NgayCap     DATETIME          NOT NULL CONSTRAINT DF_HoSoBenhAn_NgayCap DEFAULT (GETDATE()),
    TrangThai   NVARCHAR(20)      NOT NULL CONSTRAINT DF_HoSoBenhAn_TrangThai DEFAULT (N'DangMo'),

    CONSTRAINT PK_HoSoBenhAn PRIMARY KEY (MaHoSo),
    CONSTRAINT FK_HoSoBenhAn_BenhNhan FOREIGN KEY (MaBenhNhan)
        REFERENCES BenhNhan (MaBenhNhan),
    CONSTRAINT UQ_HoSoBenhAn_BenhNhan UNIQUE (MaBenhNhan),   -- tối đa 1 hồ sơ chính (BR01)
    CONSTRAINT CK_HoSoBenhAn_TrangThai CHECK (TrangThai IN (N'DangMo', N'DaDong'))
);
GO

/* ============================================================
   6. LePhiNam
   ============================================================ */
CREATE TABLE LePhiNam
(
    MaLePhi      INT IDENTITY(1,1) NOT NULL,
    MaBenhNhan   INT               NOT NULL,
    NamApDung    INT               NOT NULL,
    SoTien       DECIMAL(18,2)     NOT NULL,
    NgayThu      DATETIME          NOT NULL CONSTRAINT DF_LePhiNam_NgayThu DEFAULT (GETDATE()),
    TrangThai    NVARCHAR(20)      NOT NULL CONSTRAINT DF_LePhiNam_TrangThai DEFAULT (N'DaThu'),

    CONSTRAINT PK_LePhiNam PRIMARY KEY (MaLePhi),
    CONSTRAINT FK_LePhiNam_BenhNhan FOREIGN KEY (MaBenhNhan)
        REFERENCES BenhNhan (MaBenhNhan),
    CONSTRAINT UQ_LePhiNam_BenhNhan_Nam UNIQUE (MaBenhNhan, NamApDung),  -- BR03/TC03
    CONSTRAINT CK_LePhiNam_SoTien CHECK (SoTien > 0),
    CONSTRAINT CK_LePhiNam_Nam CHECK (NamApDung BETWEEN 2000 AND 2100),
    CONSTRAINT CK_LePhiNam_TrangThai CHECK (TrangThai IN (N'DaThu', N'ChuaDu'))
);
GO

/* ============================================================
   7. LichKhamDinhKy
   ============================================================ */
CREATE TABLE LichKhamDinhKy
(
    MaLichHen    INT IDENTITY(1,1) NOT NULL,
    MaBenhNhan   INT               NOT NULL,
    Nam          INT               NOT NULL,
    Quy          TINYINT           NOT NULL,
    ThoiGianHen  DATETIME          NOT NULL,
    TrangThai    NVARCHAR(20)      NOT NULL CONSTRAINT DF_LichKhamDinhKy_TrangThai DEFAULT (N'ChoXacNhan'),

    CONSTRAINT PK_LichKhamDinhKy PRIMARY KEY (MaLichHen),
    CONSTRAINT FK_LichKhamDinhKy_BenhNhan FOREIGN KEY (MaBenhNhan)
        REFERENCES BenhNhan (MaBenhNhan),
    CONSTRAINT CK_LichKhamDinhKy_Quy CHECK (Quy BETWEEN 1 AND 4),
    CONSTRAINT CK_LichKhamDinhKy_TrangThai CHECK (TrangThai IN
        (N'ChoXacNhan', N'DaXacNhan', N'DaTiepNhan', N'HoanTat', N'Huy', N'TuChoi'))
);
GO
-- Mỗi bệnh nhân tối đa 1 suất quý còn hiệu lực (BR09/BR10, TC06-TC08)
-- Hủy/từ chối không tính vào ràng buộc -> cho phép đặt lại.
CREATE UNIQUE INDEX UQ_LichKhamDinhKy_Active
    ON LichKhamDinhKy (MaBenhNhan, Nam, Quy)
    WHERE TrangThai <> N'Huy' AND TrangThai <> N'TuChoi';
GO

/* ============================================================
   8. PhieuKham
   ============================================================ */
CREATE TABLE PhieuKham
(
    MaPhieuKham  INT IDENTITY(1,1) NOT NULL,
    MaHoSo       INT               NOT NULL,
    MaKhoa       INT               NOT NULL,
    MaNhanVien   INT               NOT NULL,   -- bác sĩ phụ trách (FK -> BacSi.MaNhanVien)
    MaLichHen    INT               NULL,       -- nếu phát sinh từ lịch khám định kỳ
    ThoiGian     DATETIME          NOT NULL CONSTRAINT DF_PhieuKham_ThoiGian DEFAULT (GETDATE()),
    LoaiKham     NVARCHAR(30)      NOT NULL CONSTRAINT DF_PhieuKham_LoaiKham DEFAULT (N'ThuongQuy'),
    TrieuChung   NVARCHAR(500)     NULL,
    SinhHieu     NVARCHAR(200)     NULL,
    ChanDoan     NVARCHAR(500)     NULL,
    TrangThai    NVARCHAR(30)      NOT NULL CONSTRAINT DF_PhieuKham_TrangThai DEFAULT (N'ChoKham'),

    CONSTRAINT PK_PhieuKham PRIMARY KEY (MaPhieuKham),
    CONSTRAINT FK_PhieuKham_HoSoBenhAn FOREIGN KEY (MaHoSo)
        REFERENCES HoSoBenhAn (MaHoSo),
    CONSTRAINT FK_PhieuKham_Khoa FOREIGN KEY (MaKhoa)
        REFERENCES Khoa (MaKhoa),
    CONSTRAINT FK_PhieuKham_BacSi FOREIGN KEY (MaNhanVien)
        REFERENCES BacSi (MaNhanVien),
    CONSTRAINT FK_PhieuKham_LichKhamDinhKy FOREIGN KEY (MaLichHen)
        REFERENCES LichKhamDinhKy (MaLichHen),
    CONSTRAINT CK_PhieuKham_LoaiKham CHECK (LoaiKham IN (N'ThuongQuy', N'DinhKy', N'CapCuu')),
    CONSTRAINT CK_PhieuKham_TrangThai CHECK (TrangThai IN
        (N'ChoTiepNhan', N'ChoKham', N'DangKham', N'ChoXetNghiem', N'ChoVienPhi', N'HoanTat', N'Huy'))
);
GO
CREATE INDEX IX_PhieuKham_HoSo ON PhieuKham (MaHoSo, ThoiGian);
CREATE INDEX IX_PhieuKham_BacSi ON PhieuKham (MaNhanVien, ThoiGian);
GO

/* ============================================================
   9. XetNghiem  (danh mục)
   ============================================================ */
CREATE TABLE XetNghiem
(
    MaXetNghiem   INT IDENTITY(1,1) NOT NULL,
    TenXetNghiem  NVARCHAR(100)     NOT NULL,
    DonVi         NVARCHAR(30)      NULL,
    MoTa          NVARCHAR(200)     NULL,
    DonGia        DECIMAL(18,2)     NOT NULL,

    CONSTRAINT PK_XetNghiem PRIMARY KEY (MaXetNghiem),
    CONSTRAINT UQ_XetNghiem_Ten UNIQUE (TenXetNghiem),
    CONSTRAINT CK_XetNghiem_DonGia CHECK (DonGia >= 0)
);
GO

/* ============================================================
   10. ChiDinhXetNghiem
   ============================================================ */
CREATE TABLE ChiDinhXetNghiem
(
    MaChiDinh     INT IDENTITY(1,1) NOT NULL,
    MaPhieuKham   INT               NOT NULL,
    MaXetNghiem   INT               NOT NULL,
    MaNhanVien    INT               NOT NULL,  -- bác sĩ chỉ định
    NgayChiDinh   DATETIME          NOT NULL CONSTRAINT DF_ChiDinhXetNghiem_Ngay DEFAULT (GETDATE()),
    TrangThai     NVARCHAR(20)      NOT NULL CONSTRAINT DF_ChiDinhXetNghiem_TrangThai DEFAULT (N'ChoThucHien'),
    GhiChu        NVARCHAR(200)     NULL,

    CONSTRAINT PK_ChiDinhXetNghiem PRIMARY KEY (MaChiDinh),
    CONSTRAINT FK_ChiDinh_PhieuKham FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham (MaPhieuKham),
    CONSTRAINT FK_ChiDinh_XetNghiem FOREIGN KEY (MaXetNghiem)
        REFERENCES XetNghiem (MaXetNghiem),
    CONSTRAINT FK_ChiDinh_BacSi FOREIGN KEY (MaNhanVien)
        REFERENCES BacSi (MaNhanVien),
    CONSTRAINT CK_ChiDinhXetNghiem_TrangThai CHECK (TrangThai IN
        (N'ChoThucHien', N'DaCoKetQua', N'Huy'))
);
GO
CREATE INDEX IX_ChiDinh_PhieuKham ON ChiDinhXetNghiem (MaPhieuKham);
GO

/* ============================================================
   11. KetQuaXetNghiem
   ============================================================ */
CREATE TABLE KetQuaXetNghiem
(
    MaKetQua           INT IDENTITY(1,1) NOT NULL,
    MaChiDinh          INT               NOT NULL,
    MaNhanVien         INT               NOT NULL,  -- nhân viên xét nghiệm nhập kết quả
    ThoiGian           DATETIME          NOT NULL CONSTRAINT DF_KetQuaXetNghiem_ThoiGian DEFAULT (GETDATE()),
    KetQua             NVARCHAR(200)     NOT NULL,
    DonVi              NVARCHAR(30)      NULL,
    KhoangThamChieu    NVARCHAR(100)     NULL,
    NhanXet            NVARCHAR(300)     NULL,

    CONSTRAINT PK_KetQuaXetNghiem PRIMARY KEY (MaKetQua),
    CONSTRAINT FK_KetQua_ChiDinh FOREIGN KEY (MaChiDinh)
        REFERENCES ChiDinhXetNghiem (MaChiDinh),
    CONSTRAINT FK_KetQua_NhanVien FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien (MaNhanVien),
    CONSTRAINT UQ_KetQuaXetNghiem_ChiDinh UNIQUE (MaChiDinh)   -- kết quả hiện hành (1-0..1, TC11)
);
GO

/* ============================================================
   12. Thuoc  (danh mục)
   ============================================================ */
CREATE TABLE Thuoc
(
    MaThuoc     INT IDENTITY(1,1) NOT NULL,
    TenThuoc    NVARCHAR(150)     NOT NULL,
    HamLuong    NVARCHAR(50)      NULL,
    DonViTinh   NVARCHAR(30)      NULL,
    DonGia      DECIMAL(18,2)     NOT NULL,
    TrangThai   NVARCHAR(20)      NOT NULL CONSTRAINT DF_Thuoc_TrangThai DEFAULT (N'ConKinhDoanh'),

    CONSTRAINT PK_Thuoc PRIMARY KEY (MaThuoc),
    CONSTRAINT CK_Thuoc_DonGia CHECK (DonGia >= 0),
    CONSTRAINT CK_Thuoc_TrangThai CHECK (TrangThai IN (N'ConKinhDoanh', N'NgungKinhDoanh'))
);
GO

/* ============================================================
   13. ToaThuoc
   ============================================================ */
CREATE TABLE ToaThuoc
(
    MaToaThuoc   INT IDENTITY(1,1) NOT NULL,
    MaPhieuKham  INT               NOT NULL,
    MaNhanVien   INT               NOT NULL,   -- bác sĩ kê toa
    NgayKe       DATETIME          NOT NULL CONSTRAINT DF_ToaThuoc_NgayKe DEFAULT (GETDATE()),
    LoiDan       NVARCHAR(300)     NULL,
    TrangThai    NVARCHAR(20)      NOT NULL CONSTRAINT DF_ToaThuoc_TrangThai DEFAULT (N'DaXacNhan'),

    CONSTRAINT PK_ToaThuoc PRIMARY KEY (MaToaThuoc),
    CONSTRAINT FK_ToaThuoc_PhieuKham FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham (MaPhieuKham),
    CONSTRAINT FK_ToaThuoc_BacSi FOREIGN KEY (MaNhanVien)
        REFERENCES BacSi (MaNhanVien),
    CONSTRAINT UQ_ToaThuoc_PhieuKham UNIQUE (MaPhieuKham),   -- tối đa 1 toa chính (BR08)
    CONSTRAINT CK_ToaThuoc_TrangThai CHECK (TrangThai IN (N'Nhap', N'DaXacNhan', N'Huy'))
);
GO

/* ============================================================
   14. ChiTietToaThuoc
   ============================================================ */
CREATE TABLE ChiTietToaThuoc
(
    MaChiTietToa  INT IDENTITY(1,1) NOT NULL,
    MaToaThuoc    INT               NOT NULL,
    MaThuoc       INT               NOT NULL,
    SoLuong       INT               NOT NULL,
    LieuDung      NVARCHAR(100)     NULL,
    TanSuat       NVARCHAR(50)      NULL,
    SoNgay        INT               NOT NULL,
    CachDung      NVARCHAR(200)     NULL,

    CONSTRAINT PK_ChiTietToaThuoc PRIMARY KEY (MaChiTietToa),
    CONSTRAINT FK_ChiTietToa_ToaThuoc FOREIGN KEY (MaToaThuoc)
        REFERENCES ToaThuoc (MaToaThuoc),
    CONSTRAINT FK_ChiTietToa_Thuoc FOREIGN KEY (MaThuoc)
        REFERENCES Thuoc (MaThuoc),
    CONSTRAINT CK_ChiTietToaThuoc_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietToaThuoc_SoNgay CHECK (SoNgay > 0)
);
GO

/* ============================================================
   15. HoaDonVienPhi
   ============================================================ */
CREATE TABLE HoaDonVienPhi
(
    MaHoaDon     INT IDENTITY(1,1) NOT NULL,
    MaPhieuKham  INT               NOT NULL,
    NgayLap      DATETIME          NOT NULL CONSTRAINT DF_HoaDonVienPhi_NgayLap DEFAULT (GETDATE()),
    TongTien     DECIMAL(18,2)     NOT NULL CONSTRAINT DF_HoaDonVienPhi_TongTien DEFAULT (0),
    TrangThai    NVARCHAR(20)      NOT NULL CONSTRAINT DF_HoaDonVienPhi_TrangThai DEFAULT (N'ChuaThanhToan'),

    CONSTRAINT PK_HoaDonVienPhi PRIMARY KEY (MaHoaDon),
    CONSTRAINT FK_HoaDonVienPhi_PhieuKham FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham (MaPhieuKham),
    CONSTRAINT UQ_HoaDonVienPhi_PhieuKham UNIQUE (MaPhieuKham),  -- 1 hóa đơn/lượt khám
    CONSTRAINT CK_HoaDonVienPhi_TongTien CHECK (TongTien >= 0),
    CONSTRAINT CK_HoaDonVienPhi_TrangThai CHECK (TrangThai IN
        (N'ChuaThanhToan', N'MotPhan', N'DaThanhToan', N'Huy'))
);
GO

/* ============================================================
   16. ChiTietVienPhi
   ============================================================ */
CREATE TABLE ChiTietVienPhi
(
    MaChiTietVP  INT IDENTITY(1,1) NOT NULL,
    MaHoaDon     INT               NOT NULL,
    NoiDung      NVARCHAR(150)     NOT NULL,
    SoLuong      INT               NOT NULL CONSTRAINT DF_ChiTietVienPhi_SoLuong DEFAULT (1),
    DonGia       DECIMAL(18,2)     NOT NULL,
    ThanhTien    AS (SoLuong * DonGia) PERSISTED,

    CONSTRAINT PK_ChiTietVienPhi PRIMARY KEY (MaChiTietVP),
    CONSTRAINT FK_ChiTietVienPhi_HoaDon FOREIGN KEY (MaHoaDon)
        REFERENCES HoaDonVienPhi (MaHoaDon),
    CONSTRAINT CK_ChiTietVienPhi_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietVienPhi_DonGia CHECK (DonGia >= 0)
);
GO

/* ============================================================
   17. ThanhToan
   ============================================================ */
CREATE TABLE ThanhToan
(
    MaThanhToan     INT IDENTITY(1,1) NOT NULL,
    MaHoaDon        INT               NOT NULL,
    NgayThanhToan   DATETIME          NOT NULL CONSTRAINT DF_ThanhToan_Ngay DEFAULT (GETDATE()),
    SoTien          DECIMAL(18,2)     NOT NULL,
    PhuongThuc      NVARCHAR(30)      NOT NULL,
    MaGiaoDich      NVARCHAR(50)      NULL,

    CONSTRAINT PK_ThanhToan PRIMARY KEY (MaThanhToan),
    CONSTRAINT FK_ThanhToan_HoaDon FOREIGN KEY (MaHoaDon)
        REFERENCES HoaDonVienPhi (MaHoaDon),
    CONSTRAINT CK_ThanhToan_SoTien CHECK (SoTien > 0),
    CONSTRAINT CK_ThanhToan_PhuongThuc CHECK (PhuongThuc IN (N'TienMat', N'ChuyenKhoan', N'The'))
);
GO

/* ============================================================
   18. NhatKyTruyCap  (audit log - BR15)
   ============================================================ */
CREATE TABLE NhatKyTruyCap
(
    MaNhatKy    INT IDENTITY(1,1) NOT NULL,
    MaNhanVien  INT               NULL,
    ThoiGian    DATETIME          NOT NULL CONSTRAINT DF_NhatKyTruyCap_ThoiGian DEFAULT (GETDATE()),
    HanhDong    NVARCHAR(100)     NOT NULL,
    DoiTuong    NVARCHAR(50)      NULL,
    MaDoiTuong  INT               NULL,

    CONSTRAINT PK_NhatKyTruyCap PRIMARY KEY (MaNhatKy),
    CONSTRAINT FK_NhatKyTruyCap_NhanVien FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien (MaNhanVien)
);
GO
CREATE INDEX IX_NhatKyTruyCap_ThoiGian ON NhatKyTruyCap (ThoiGian);
GO

/* ============================================================
   DỮ LIỆU MẪU
   Mật khẩu mặc định cho mọi tài khoản mẫu: 123456
   (SHA-256 hex, xem WinForm Data/DbHelper.cs -> HashPassword)
   ============================================================ */
DECLARE @HashMacDinh NVARCHAR(256) = N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92';

INSERT INTO NhanVien (HoTen, DienThoai, Email, TenDangNhap, MatKhauHash, VaiTro) VALUES
(N'Nguyễn Văn Admin', N'0900000001', N'admin@benhvien.vn',    N'admin',    @HashMacDinh, N'QuanTri'),
(N'Trần Thị Quản Lý', N'0900000002', N'quanly@benhvien.vn',   N'quanly',   @HashMacDinh, N'QuanLy'),
(N'Lê Thị Lễ Tân',    N'0900000003', N'letan@benhvien.vn',    N'letan',    @HashMacDinh, N'LeTan'),
(N'Phạm Văn Bác Sĩ A',N'0900000004', N'bacsi.a@benhvien.vn',  N'bacsi1',   @HashMacDinh, N'BacSi'),
(N'Hoàng Thị Bác Sĩ B',N'0900000005',N'bacsi.b@benhvien.vn',  N'bacsi2',   @HashMacDinh, N'BacSi'),
(N'Đỗ Văn Xét Nghiệm', N'0900000006',N'xn@benhvien.vn',       N'xetnghiem',@HashMacDinh, N'XetNghiem'),
(N'Vũ Thị Dược Sĩ',    N'0900000007',N'duocsi@benhvien.vn',   N'duocsi',   @HashMacDinh, N'DuocSi'),
(N'Bùi Văn Viện Phí',  N'0900000008',N'vienphi@benhvien.vn',  N'vienphi',  @HashMacDinh, N'VienPhi');
GO

INSERT INTO BacSi (MaNhanVien, SoChungChi, ChuyenMon)
SELECT MaNhanVien, N'CCHN-00' + CAST(MaNhanVien AS NVARCHAR(10)), N'Đa khoa'
FROM NhanVien WHERE TenDangNhap IN (N'bacsi1', N'bacsi2');
GO

INSERT INTO Khoa (TenKhoa, DienThoai, ViTri) VALUES
(N'Khoa Nội',       N'0283000001', N'Tầng 1'),
(N'Khoa Ngoại',     N'0283000002', N'Tầng 2'),
(N'Khoa Nhi',       N'0283000003', N'Tầng 1'),
(N'Khoa Sản',       N'0283000004', N'Tầng 3');
GO

INSERT INTO XetNghiem (TenXetNghiem, DonVi, MoTa, DonGia) VALUES
(N'Công thức máu',     N'mẫu', N'Xét nghiệm huyết học tổng quát', 150000),
(N'Đường huyết',       N'mg/dL', N'Định lượng glucose máu',       80000),
(N'Chức năng gan',     N'mẫu', N'AST/ALT',                        180000),
(N'Tổng phân tích nước tiểu', N'mẫu', N'10 thông số',              60000);
GO

INSERT INTO Thuoc (TenThuoc, HamLuong, DonViTinh, DonGia) VALUES
(N'Paracetamol',  N'500mg', N'Viên', 1000),
(N'Amoxicillin',  N'500mg', N'Viên', 2500),
(N'Vitamin C',    N'500mg', N'Viên', 800),
(N'Omeprazole',   N'20mg',  N'Viên', 3000);
GO

PRINT N'Đã tạo CSDL QuanLyHoSoBenhAn và dữ liệu mẫu thành công.';
GO
