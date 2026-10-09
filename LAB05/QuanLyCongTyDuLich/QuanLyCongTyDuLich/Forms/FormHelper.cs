using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;
 
namespace QuanLyCongTyDuLich.Forms
{
    /// <summary>Tiện ích dùng chung cho các Form.</summary>
    internal static class FormHelper
    {
        public static void Nap(ComboBox cbo, DataTable dt, string display, string value)
        {
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.DataSource = dt;
        }
 
        public static string Gia(ComboBox cbo)
        {
            return cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        }
 
        public static string O(DataGridView dgv, string cot)
        {
            return dgv.CurrentRow == null ? "" : System.Convert.ToString(dgv.CurrentRow.Cells[cot].Value);
        }
 
        /// <summary>Hiển thị kết quả; trả về true nếu thành công.</summary>
        public static bool Bao(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao, k.ThanhCong ? "Thông báo" : "Không thực hiện được",
                MessageBoxButtons.OK, k.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            return k.ThanhCong;
        }

        // ===== Định dạng hiển thị số / ngày trong bảng dữ liệu =====
        public static void ApplyTheme(Control root)
        {
            foreach (Control c in root.Controls)
            {
                var g = c as DataGridView;
                if (g != null) FormatGrid(g);
                ApplyTheme(c);
            }
        }

        private static void FormatGrid(DataGridView g)
        {
            g.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn col in g.Columns)
                {
                    if (col.ValueType == null) continue;
                    Type t = Nullable.GetUnderlyingType(col.ValueType) ?? col.ValueType;
                    if (t == typeof(decimal)) col.DefaultCellStyle.Format = "#,##0.##";
                    else if (t == typeof(DateTime)) col.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            };
        }
    }
}
