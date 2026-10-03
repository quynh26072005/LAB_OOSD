using System;
using System.Collections.Generic;
using System.Linq;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.ServiceAdapter
{
    public class ShoppingCartService
    {
        private List<GioHangItem> _cartItems;

        public ShoppingCartService()
        {
            _cartItems = new List<GioHangItem>();
        }

        public void AddProduct(SanPham sanPham, int soLuong = 1)
        {
            var existingItem = _cartItems.FirstOrDefault(x => x.SanPham.MaSP == sanPham.MaSP);

            if (existingItem != null)
            {
                existingItem.SoLuong += soLuong;
            }
            else
            {
                _cartItems.Add(new GioHangItem
                {
                    SanPham = sanPham,
                    SoLuong = soLuong
                });
            }
        }

        public void RemoveProduct(int maSP)
        {
            _cartItems.RemoveAll(x => x.SanPham.MaSP == maSP);
        }

        public void UpdateQuantity(int maSP, int soLuong)
        {
            var item = _cartItems.FirstOrDefault(x => x.SanPham.MaSP == maSP);
            if (item != null)
            {
                if (soLuong <= 0)
                    RemoveProduct(maSP);
                else
                    item.SoLuong = soLuong;
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }

        public List<GioHangItem> GetCartItems()
        {
            return _cartItems;
        }

        public decimal GetTotalAmount()
        {
            return _cartItems.Sum(x => x.ThanhTien);
        }

        public int GetTotalItems()
        {
            return _cartItems.Sum(x => x.SoLuong);
        }

        public bool IsEmpty()
        {
            return _cartItems.Count == 0;
        }

        public List<ChiTietDonHang> ConvertToOrderDetails()
        {
            return _cartItems.Select(item => new ChiTietDonHang
            {
                MaSP = item.SanPham.MaSP,
                TenSanPham = item.SanPham.TenSP,
                SoLuong = item.SoLuong,
                DonGia = item.SanPham.GiaBan
            }).ToList();
        }
    }
}
