using System.Data;
using System.Windows.Forms;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Forms
{
    /// <summary>Ham dung chung cho cac Form de khong lap code.</summary>
    public static class UiHelper
    {
        public static void NapCombo(ComboBox cbo, DataTable dt, string hienThi, string giaTri)
        {
            cbo.DataSource = dt;
            cbo.DisplayMember = hienThi;
            cbo.ValueMember = giaTri;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static string GiaTri(ComboBox cbo)
        {
            return cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        }

        public static string Cell(DataGridView dgv, string cot)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Index < 0) return "";
            object v = dgv.CurrentRow.Cells[cot].Value;
            return v == null ? "" : v.ToString();
        }

        /// <summary>Hien thong bao ket qua; tra ve true neu thanh cong.</summary>
        public static bool ThongBao(KetQuaXuLy kq)
        {
            MessageBox.Show(kq.ThongBao, "Quản lý cao ốc",
                MessageBoxButtons.OK, kq.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            return kq.ThanhCong;
        }
    }
}
