using System;
using System.Collections.Generic;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    /// <summary>
    /// Lập quyết định khen thưởng / kỷ luật (cá nhân hoặc tập thể phòng ban)
    /// kèm phiếu chi khen thưởng / phiếu thu nộp phạt trong cùng một giao dịch.
    /// </summary>
    public class KhenThuongKyLuatService
    {
        private readonly Repository<PhieuChiKhenThuong> _phieuChi = new Repository<PhieuChiKhenThuong>();
        private readonly Repository<PhieuThuNopPhat> _phieuThu = new Repository<PhieuThuNopPhat>();

        public void LapQuyetDinhKhenThuong(QuyetDinh qd, QdKhenThuong detail, PhieuChiKhenThuong phieu)
        {
            if (qd == null || detail == null || phieu == null)
                throw new ArgumentNullException();
            qd.LoaiQD = LoaiQuyetDinh.KhenThuong;

            var commands = new List<SqlCommandSpec>
            {
                QuyetDinhService.BuildInsertBase(qd),
                new QuyetDinhService().BuildInsertKhenThuong(detail),
                new SqlCommandSpec(
                    @"INSERT INTO PhieuChiKhenThuong (SoPhieuChi, NgayChi, SoTien, SoQD)
                      VALUES (@SoPhieuChi, @NgayChi, @SoTien, @SoQD)",
                    new[]
                    {
                        SqlHelper.P("@SoPhieuChi", phieu.SoPhieuChi),
                        SqlHelper.P("@NgayChi", phieu.NgayChi),
                        SqlHelper.P("@SoTien", phieu.SoTien),
                        SqlHelper.P("@SoQD", phieu.SoQD)
                    })
            };
            SqlHelper.ExecuteTransaction(commands);
        }

        public void LapQuyetDinhKyLuat(QuyetDinh qd, QdKyLuat detail, PhieuThuNopPhat phieu)
        {
            if (qd == null || detail == null || phieu == null)
                throw new ArgumentNullException();
            qd.LoaiQD = LoaiQuyetDinh.KyLuat;

            var commands = new List<SqlCommandSpec>
            {
                QuyetDinhService.BuildInsertBase(qd),
                new QuyetDinhService().BuildInsertKyLuat(detail),
                new SqlCommandSpec(
                    @"INSERT INTO PhieuThuNopPhat (SoPhieuThu, NgayThu, SoTien, HinhThuc, SoQD)
                      VALUES (@SoPhieuThu, @NgayThu, @SoTien, @HinhThuc, @SoQD)",
                    new[]
                    {
                        SqlHelper.P("@SoPhieuThu", phieu.SoPhieuThu),
                        SqlHelper.P("@NgayThu", phieu.NgayThu),
                        SqlHelper.P("@SoTien", phieu.SoTien),
                        SqlHelper.P("@HinhThuc", phieu.HinhThuc),
                        SqlHelper.P("@SoQD", phieu.SoQD)
                    })
            };
            SqlHelper.ExecuteTransaction(commands);
        }

        public List<PhieuChiKhenThuong> GetPhieuChi() => _phieuChi.GetAll("NgayChi DESC");
        public List<PhieuThuNopPhat> GetPhieuThu() => _phieuThu.GetAll("NgayThu DESC");
    }
}