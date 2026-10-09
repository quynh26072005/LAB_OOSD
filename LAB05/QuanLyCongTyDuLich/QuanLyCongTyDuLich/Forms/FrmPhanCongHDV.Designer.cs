namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmPhanCongHDV
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
            this.lblCap_txtMaPC = new System.Windows.Forms.Label();
            this.txtMaPC = new System.Windows.Forms.TextBox();
            this.lblCap_cboHDV = new System.Windows.Forms.Label();
            this.cboHDV = new System.Windows.Forms.ComboBox();
            this.lblCap_cboLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblCap_cboDoiTuong = new System.Windows.Forms.Label();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();
            this.lblCap_numThuLao = new System.Windows.Forms.Label();
            this.numThuLao = new System.Windows.Forms.NumericUpDown();
            this.btnPhanCong = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.SuspendLayout();
            this.pnlHeader.BackColor = System.Drawing.SystemColors.Window;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(900, 56);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitle.Text = "PHÂN CÔNG HƯỚNG DẪN VIÊN";
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
            this.lblCap_txtMaPC.Text = "Mã phân công:";
            this.lblCap_txtMaPC.AutoSize = true;
            this.lblCap_txtMaPC.Location = new System.Drawing.Point(14, 17);
            this.lblCap_txtMaPC.TabIndex = 3;
            this.txtMaPC.Location = new System.Drawing.Point(164, 12);
            this.txtMaPC.Size = new System.Drawing.Size(246, 23);
            this.txtMaPC.TabIndex = 4;
            this.lblCap_cboHDV.Text = "Hướng dẫn viên:";
            this.lblCap_cboHDV.AutoSize = true;
            this.lblCap_cboHDV.Location = new System.Drawing.Point(434, 17);
            this.lblCap_cboHDV.TabIndex = 5;
            this.cboHDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHDV.FormattingEnabled = true;
            this.cboHDV.Location = new System.Drawing.Point(584, 12);
            this.cboHDV.Size = new System.Drawing.Size(246, 23);
            this.cboHDV.TabIndex = 6;
            this.lblCap_cboLoai.Text = "Loại:";
            this.lblCap_cboLoai.AutoSize = true;
            this.lblCap_cboLoai.Location = new System.Drawing.Point(14, 51);
            this.lblCap_cboLoai.TabIndex = 7;
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(164, 46);
            this.cboLoai.Size = new System.Drawing.Size(246, 23);
            this.cboLoai.TabIndex = 8;
            this.lblCap_cboDoiTuong.Text = "Chuyến / đoàn:";
            this.lblCap_cboDoiTuong.AutoSize = true;
            this.lblCap_cboDoiTuong.Location = new System.Drawing.Point(434, 51);
            this.lblCap_cboDoiTuong.TabIndex = 9;
            this.cboDoiTuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTuong.FormattingEnabled = true;
            this.cboDoiTuong.Location = new System.Drawing.Point(584, 46);
            this.cboDoiTuong.Size = new System.Drawing.Size(246, 23);
            this.cboDoiTuong.TabIndex = 10;
            this.lblCap_numThuLao.Text = "Thù lao tour:";
            this.lblCap_numThuLao.AutoSize = true;
            this.lblCap_numThuLao.Location = new System.Drawing.Point(14, 85);
            this.lblCap_numThuLao.TabIndex = 11;
            this.numThuLao.Minimum = new decimal(new int[] {0, 0, 0, 0});
            this.numThuLao.Maximum = new decimal(new int[] {2000000000, 0, 0, 0});
            this.numThuLao.Increment = new decimal(new int[] {100000, 0, 0, 0});
            this.numThuLao.ThousandsSeparator = true;
            this.numThuLao.Value = new decimal(new int[] {0, 0, 0, 0});
            this.numThuLao.Location = new System.Drawing.Point(164, 80);
            this.numThuLao.Size = new System.Drawing.Size(180, 23);
            this.numThuLao.TabIndex = 12;
            this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard1.Location = new System.Drawing.Point(16, 70);
            this.pnlCard1.Size = new System.Drawing.Size(868, 122);
            this.pnlCard1.TabIndex = 13;
            this.pnlCard1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnPhanCong.Text = "Phân công";
            this.btnPhanCong.UseVisualStyleBackColor = true;
            this.btnPhanCong.Location = new System.Drawing.Point(16, 204);
            this.btnPhanCong.Size = new System.Drawing.Size(120, 36);
            this.btnPhanCong.TabIndex = 14;
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
            this.dgv.TabIndex = 15;
            this.dgv.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnDong);
            this.Controls.Add(this.pnlHeader);
            this.pnlCard1.Controls.Add(this.lblCap_txtMaPC);
            this.pnlCard1.Controls.Add(this.txtMaPC);
            this.pnlCard1.Controls.Add(this.lblCap_cboHDV);
            this.pnlCard1.Controls.Add(this.cboHDV);
            this.pnlCard1.Controls.Add(this.lblCap_cboLoai);
            this.pnlCard1.Controls.Add(this.cboLoai);
            this.pnlCard1.Controls.Add(this.lblCap_cboDoiTuong);
            this.pnlCard1.Controls.Add(this.cboDoiTuong);
            this.pnlCard1.Controls.Add(this.lblCap_numThuLao);
            this.pnlCard1.Controls.Add(this.numThuLao);
            this.Controls.Add(this.pnlCard1);
            this.Controls.Add(this.btnPhanCong);
            this.Controls.Add(this.dgv);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ClientSize = new System.Drawing.Size(900, 438);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPhanCongHDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân công hướng dẫn viên";
            this.AutoScroll = true;
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);
            this.btnPhanCong.Click += new System.EventHandler(this.btnPhanCong_Click);
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            this.Load += new System.EventHandler(this.FrmPhanCongHDV_Load);
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
        private System.Windows.Forms.Label lblCap_txtMaPC;
        private System.Windows.Forms.TextBox txtMaPC;
        private System.Windows.Forms.Label lblCap_cboHDV;
        private System.Windows.Forms.ComboBox cboHDV;
        private System.Windows.Forms.Label lblCap_cboLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblCap_cboDoiTuong;
        private System.Windows.Forms.ComboBox cboDoiTuong;
        private System.Windows.Forms.Label lblCap_numThuLao;
        private System.Windows.Forms.NumericUpDown numThuLao;
        private System.Windows.Forms.Button btnPhanCong;
        private System.Windows.Forms.DataGridView dgv;
    }
}
