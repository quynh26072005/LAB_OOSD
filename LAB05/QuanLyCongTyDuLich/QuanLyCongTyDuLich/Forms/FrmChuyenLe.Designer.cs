namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmChuyenLe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlCard1 = new System.Windows.Forms.Panel();
            this.lblCap_txtMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblCap_cboTour = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.lblCap_dtDi = new System.Windows.Forms.Label();
            this.dtDi = new System.Windows.Forms.DateTimePicker();
            this.lblCap_lblNgayVe = new System.Windows.Forms.Label();
            this.lblNgayVe = new System.Windows.Forms.Label();
            this.lblCap_txtDon = new System.Windows.Forms.Label();
            this.txtDon = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnDongDK = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.SuspendLayout();
            this.pnlHeader.BackColor = System.Drawing.SystemColors.Window;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(900, 56);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitle.Text = "LỊCH CHUYẾN KHÁCH LẺ";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.lblTitle.Location = new System.Drawing.Point(16, 13);
            this.lblTitle.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Location = new System.Drawing.Point(784, 12);
            this.btnDong.Size = new System.Drawing.Size(100, 32);
            this.btnDong.TabIndex = 2;
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblCap_txtMa.Text = "Mã chuyến:";
            this.lblCap_txtMa.AutoSize = true;
            this.lblCap_txtMa.Location = new System.Drawing.Point(14, 17);
            this.lblCap_txtMa.TabIndex = 3;
            this.txtMa.Location = new System.Drawing.Point(164, 12);
            this.txtMa.Size = new System.Drawing.Size(246, 23);
            this.txtMa.TabIndex = 4;
            this.lblCap_cboTour.Text = "Tour:";
            this.lblCap_cboTour.AutoSize = true;
            this.lblCap_cboTour.Location = new System.Drawing.Point(434, 17);
            this.lblCap_cboTour.TabIndex = 5;
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.FormattingEnabled = true;
            this.cboTour.Location = new System.Drawing.Point(584, 12);
            this.cboTour.Size = new System.Drawing.Size(246, 23);
            this.cboTour.TabIndex = 6;
            this.lblCap_dtDi.Text = "Ngày đi:";
            this.lblCap_dtDi.AutoSize = true;
            this.lblCap_dtDi.Location = new System.Drawing.Point(14, 51);
            this.lblCap_dtDi.TabIndex = 7;
            this.dtDi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDi.Location = new System.Drawing.Point(164, 46);
            this.dtDi.Size = new System.Drawing.Size(180, 23);
            this.dtDi.TabIndex = 8;
            this.lblCap_lblNgayVe.Text = "Ngày về:";
            this.lblCap_lblNgayVe.AutoSize = true;
            this.lblCap_lblNgayVe.Location = new System.Drawing.Point(434, 51);
            this.lblCap_lblNgayVe.TabIndex = 9;
            this.lblNgayVe.Text = "-";
            this.lblNgayVe.AutoSize = true;
            this.lblNgayVe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblNgayVe.Location = new System.Drawing.Point(584, 50);
            this.lblNgayVe.TabIndex = 10;
            this.lblCap_txtDon.Text = "Địa điểm đón:";
            this.lblCap_txtDon.AutoSize = true;
            this.lblCap_txtDon.Location = new System.Drawing.Point(14, 85);
            this.lblCap_txtDon.TabIndex = 11;
            this.txtDon.Location = new System.Drawing.Point(164, 80);
            this.txtDon.Size = new System.Drawing.Size(690, 23);
            this.txtDon.TabIndex = 12;
            this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard1.Location = new System.Drawing.Point(16, 70);
            this.pnlCard1.Size = new System.Drawing.Size(868, 122);
            this.pnlCard1.TabIndex = 13;
            this.pnlCard1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnThem.Text = "Tạo chuyến";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Location = new System.Drawing.Point(16, 204);
            this.btnThem.Size = new System.Drawing.Size(122, 36);
            this.btnThem.TabIndex = 14;
            this.btnDongDK.Text = "Đóng đăng ký";
            this.btnDongDK.UseVisualStyleBackColor = true;
            this.btnDongDK.Location = new System.Drawing.Point(148, 204);
            this.btnDongDK.Size = new System.Drawing.Size(140, 36);
            this.btnDongDK.TabIndex = 15;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.MultiSelect = false;
            this.dgv.RowHeadersVisible = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.Location = new System.Drawing.Point(16, 252);
            this.dgv.Size = new System.Drawing.Size(868, 170);
            this.dgv.TabIndex = 16;
            this.dgv.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnDong);
            this.Controls.Add(this.pnlHeader);
            this.pnlCard1.Controls.Add(this.lblCap_txtMa);
            this.pnlCard1.Controls.Add(this.txtMa);
            this.pnlCard1.Controls.Add(this.lblCap_cboTour);
            this.pnlCard1.Controls.Add(this.cboTour);
            this.pnlCard1.Controls.Add(this.lblCap_dtDi);
            this.pnlCard1.Controls.Add(this.dtDi);
            this.pnlCard1.Controls.Add(this.lblCap_lblNgayVe);
            this.pnlCard1.Controls.Add(this.lblNgayVe);
            this.pnlCard1.Controls.Add(this.lblCap_txtDon);
            this.pnlCard1.Controls.Add(this.txtDon);
            this.Controls.Add(this.pnlCard1);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnDongDK);
            this.Controls.Add(this.dgv);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ClientSize = new System.Drawing.Size(900, 438);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmChuyenLe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch chuyến khách lẻ";
            this.AutoScroll = true;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.TinhNgayVe);
            this.dtDi.ValueChanged += new System.EventHandler(this.TinhNgayVe);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnDongDK.Click += new System.EventHandler(this.btnDongDK_Click);
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            this.Load += new System.EventHandler(this.FrmChuyenLe_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlCard1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Label lblCap_txtMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblCap_cboTour;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.Label lblCap_dtDi;
        private System.Windows.Forms.DateTimePicker dtDi;
        private System.Windows.Forms.Label lblCap_lblNgayVe;
        private System.Windows.Forms.Label lblNgayVe;
        private System.Windows.Forms.Label lblCap_txtDon;
        private System.Windows.Forms.TextBox txtDon;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnDongDK;
        private System.Windows.Forms.DataGridView dgv;
    }
}
