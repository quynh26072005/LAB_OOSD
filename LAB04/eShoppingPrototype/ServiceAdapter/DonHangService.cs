using System;
using System.Linq;
using eShoppingPrototype.Models;
using eShoppingPrototype.Data;

namespace eShoppingPrototype.ServiceAdapter
{
    public class DonHangService
    {
        private const decimal FREE_EXPRESS_THRESHOLD = 1000000m;
        private const decimal FREE_SAME_DAY_THRESHOLD = 5000000m;

        private readonly DonHangDAO _orderDAO;
        private readonly KhuVucDAO _regionDAO;

        public DonHangService()
        {
            _orderDAO = new DonHangDAO();
            _regionDAO = new KhuVucDAO();
        }

        public decimal CalculateTotalProductAmount(DonHang donHang)
        {
            return donHang.ChiTietDonHang.Sum(ct => ct.ThanhTien);
        }

        public decimal CalculateShippingFee(string loaiGiaoHang, decimal tongTriGiaSanPham, int maKhuVuc)
        {
            var khuVuc = _regionDAO.GetRegionById(maKhuVuc);
            if (khuVuc == null)
                return 0;

            decimal baseFee = 0;

            switch (loaiGiaoHang)
            {
                case "Thường":
                    baseFee = khuVuc.PhiGiaoHangThuong;
                    break;

                case "Chuyển Phát Nhanh":
                    if (tongTriGiaSanPham >= FREE_EXPRESS_THRESHOLD)
                        return 0; // Free shipping
                    baseFee = khuVuc.PhiGiaoHangNhanh;
                    break;

                case "Chuyển Phát Nhanh Trong Ngày":
                    if (tongTriGiaSanPham >= FREE_SAME_DAY_THRESHOLD)
                        return 0; // Free same-day
                    if (tongTriGiaSanPham >= FREE_EXPRESS_THRESHOLD)
                        baseFee = khuVuc.PhiGiaoHangTrongNgay - khuVuc.PhiGiaoHangNhanh;
                    else
                        baseFee = khuVuc.PhiGiaoHangTrongNgay;
                    break;

                default:
                    baseFee = khuVuc.PhiGiaoHangThuong;
                    break;
            }

            return baseFee;
        }

        public decimal CalculateCardFee(string loaiThe, decimal tongTriGia)
        {
            // Card transaction fees (typically percentage of total)
            decimal feeRate = 0;

            switch (loaiThe)
            {
                case "VISA":
                    feeRate = 0.015m; // 1.5%
                    break;
                case "Master":
                    feeRate = 0.015m; // 1.5%
                    break;
                case "Discover":
                    feeRate = 0.02m; // 2%
                    break;
                case "American Express":
                    feeRate = 0.025m; // 2.5%
                    break;
                default:
                    feeRate = 0.02m; // Default 2%
                    break;
            }

            return Math.Round(tongTriGia * feeRate, 0);
        }

        public void CalculateOrderTotals(DonHang donHang)
        {
            // Calculate product total
            donHang.TongTriGiaSanPham = CalculateTotalProductAmount(donHang);

            // Calculate shipping fee based on region and delivery type
            donHang.PhiGiaoHang = CalculateShippingFee(
                donHang.LoaiGiaoHang, 
                donHang.TongTriGiaSanPham, 
                donHang.NguoiNhan.MaKhuVuc
            );

            // Calculate card transaction fee
            donHang.LePhi = CalculateCardFee(donHang.LoaiThe, donHang.TongTriGiaSanPham + donHang.PhiGiaoHang);

            // Calculate grand total
            donHang.TongTriGiaHoaDon = donHang.TongTriGiaSanPham + donHang.PhiGiaoHang + donHang.LePhi;
        }

        public int SaveOrder(DonHang donHang)
        {
            return _orderDAO.InsertDonHang(donHang);
        }
    }
}
