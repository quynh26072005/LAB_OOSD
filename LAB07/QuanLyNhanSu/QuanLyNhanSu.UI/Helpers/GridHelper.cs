using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.UI
{
    /// <summary>Tiện ích gắn danh sách thực thể và đặt tiêu đề cột tiếng Việt từ [Label].</summary>
    public static class GridHelper
    {
        public static void Bind<T>(DataGridView grid, IList<T> items)
        {
            grid.DataSource = null;
            grid.AutoGenerateColumns = true;
            grid.DataSource = items;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (string.IsNullOrEmpty(col.DataPropertyName)) continue;
                PropertyInfo prop = typeof(T).GetProperty(col.DataPropertyName);
                LabelAttribute label = prop?.GetCustomAttribute<LabelAttribute>();
                if (label != null && !string.IsNullOrEmpty(label.Text))
                    col.HeaderText = label.Text;
            }
            grid.AutoResizeColumns();
        }

        public static object SelectedKey(DataGridView grid, string column)
        {
            if (grid.CurrentRow == null) return null;
            DataGridViewCell cell = grid.CurrentRow.Cells[column];
            if (cell == null || cell.Value == null || cell.Value is System.DBNull) return null;
            return cell.Value;
        }

        /// <summary>True khi đang chạy trong Visual Studio Designer (tránh truy cập CSDL lúc thiết kế).</summary>
        public static bool IsDesignTime =>
            System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime;
    }
}