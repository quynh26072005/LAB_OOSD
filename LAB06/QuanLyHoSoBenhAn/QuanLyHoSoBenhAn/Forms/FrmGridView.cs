using System.Data;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>Form tiện ích dùng chung để hiển thị nhanh một DataTable (modal), tránh lặp code ở nhiều form.</summary>
    public partial class FrmGridView : Form
    {
        private readonly DataTable _data;

        public FrmGridView(string tieuDe, DataTable data)
        {
            _data = data;
            Text = tieuDe;
            InitializeComponent();
        }
    }
}
