namespace QuanLyKhachSan.UI
{
    partial class FrmPhong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTabPhong = new System.Windows.Forms.Label();
            this.lblTabTienNghi = new System.Windows.Forms.Label();
            this.lblTabLapDat = new System.Windows.Forms.Label();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.cmbKhuVuc = new System.Windows.Forms.ComboBox();
            this.lblSucChua = new System.Windows.Forms.Label();
            this.txtSucChua = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.pnlLapDat = new System.Windows.Forms.Panel();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblTienNghi = new System.Windows.Forms.Label();
            this.cmbTienNghi = new System.Windows.Forms.ComboBox();
            this.lblPhongLapDat = new System.Windows.Forms.Label();
            this.cmbPhongLapDat = new System.Windows.Forms.ComboBox();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlLapDat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTabPhong
            // 
            this.lblTabPhong.AutoSize = true;
            this.lblTabPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTabPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTabPhong.ForeColor = System.Drawing.Color.Blue;
            this.lblTabPhong.Location = new System.Drawing.Point(30, 20);
            this.lblTabPhong.Name = "lblTabPhong";
            this.lblTabPhong.Size = new System.Drawing.Size(78, 23);
            this.lblTabPhong.TabIndex = 0;
            this.lblTabPhong.Text = "[Phòng]";
            this.lblTabPhong.Click += new System.EventHandler(this.Tab_Click);
            // 
            // lblTabTienNghi
            // 
            this.lblTabTienNghi.AutoSize = true;
            this.lblTabTienNghi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTabTienNghi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTabTienNghi.ForeColor = System.Drawing.Color.Gray;
            this.lblTabTienNghi.Location = new System.Drawing.Point(125, 20);
            this.lblTabTienNghi.Name = "lblTabTienNghi";
            this.lblTabTienNghi.Size = new System.Drawing.Size(96, 23);
            this.lblTabTienNghi.TabIndex = 1;
            this.lblTabTienNghi.Text = "[Tiện nghi]";
            this.lblTabTienNghi.Click += new System.EventHandler(this.Tab_Click);
            // 
            // lblTabLapDat
            // 
            this.lblTabLapDat.AutoSize = true;
            this.lblTabLapDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTabLapDat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTabLapDat.ForeColor = System.Drawing.Color.Gray;
            this.lblTabLapDat.Location = new System.Drawing.Point(235, 20);
            this.lblTabLapDat.Name = "lblTabLapDat";
            this.lblTabLapDat.Size = new System.Drawing.Size(185, 23);
            this.lblTabLapDat.TabIndex = 2;
            this.lblTabLapDat.Text = "[Lắp đặt / luân chuyển]";
            this.lblTabLapDat.Click += new System.EventHandler(this.Tab_Click);
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Location = new System.Drawing.Point(30, 65);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(69, 16);
            this.lblSoPhong.TabIndex = 3;
            this.lblSoPhong.Text = "Số phòng:";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Location = new System.Drawing.Point(105, 62);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.Size = new System.Drawing.Size(100, 22);
            this.txtSoPhong.TabIndex = 4;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Location = new System.Drawing.Point(220, 65);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(57, 16);
            this.lblKhuVuc.TabIndex = 5;
            this.lblKhuVuc.Text = "Khu vực:";
            // 
            // cmbKhuVuc
            // 
            this.cmbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhuVuc.FormattingEnabled = true;
            this.cmbKhuVuc.Location = new System.Drawing.Point(283, 61);
            this.cmbKhuVuc.Name = "cmbKhuVuc";
            this.cmbKhuVuc.Size = new System.Drawing.Size(110, 24);
            this.cmbKhuVuc.TabIndex = 6;
            // 
            // lblSucChua
            // 
            this.lblSucChua.AutoSize = true;
            this.lblSucChua.Location = new System.Drawing.Point(405, 65);
            this.lblSucChua.Name = "lblSucChua";
            this.lblSucChua.Size = new System.Drawing.Size(98, 16);
            this.lblSucChua.TabIndex = 7;
            this.lblSucChua.Text = "Số người tối đa:";
            // 
            // txtSucChua
            // 
            this.txtSucChua.Location = new System.Drawing.Point(509, 62);
            this.txtSucChua.Name = "txtSucChua";
            this.txtSucChua.Size = new System.Drawing.Size(70, 22);
            this.txtSucChua.TabIndex = 8;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(595, 65);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(92, 16);
            this.lblDonGia.TabIndex = 9;
            this.lblDonGia.Text = "Đơn giá/ngày:";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(693, 62);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(120, 22);
            this.txtDonGia.TabIndex = 10;
            // 
            // dgvData
            // 
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(30, 100);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(890, 310);
            this.dgvData.TabIndex = 11;
            // 
            // pnlLapDat
            // 
            this.pnlLapDat.Controls.Add(this.lblMaPhieu);
            this.pnlLapDat.Controls.Add(this.txtMaPhieu);
            this.pnlLapDat.Controls.Add(this.lblTienNghi);
            this.pnlLapDat.Controls.Add(this.cmbTienNghi);
            this.pnlLapDat.Controls.Add(this.lblPhongLapDat);
            this.pnlLapDat.Controls.Add(this.cmbPhongLapDat);
            this.pnlLapDat.Controls.Add(this.lblTinhTrang);
            this.pnlLapDat.Controls.Add(this.txtTinhTrang);
            this.pnlLapDat.Controls.Add(this.btnLapPhieu);
            this.pnlLapDat.Location = new System.Drawing.Point(30, 420);
            this.pnlLapDat.Name = "pnlLapDat";
            this.pnlLapDat.Size = new System.Drawing.Size(890, 50);
            this.pnlLapDat.TabIndex = 12;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(0, 15);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(88, 16);
            this.lblMaPhieu.TabIndex = 0;
            this.lblMaPhieu.Text = "Phiếu lắp đặt:";
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Location = new System.Drawing.Point(94, 12);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(100, 22);
            this.txtMaPhieu.TabIndex = 1;
            // 
            // lblTienNghi
            // 
            this.lblTienNghi.AutoSize = true;
            this.lblTienNghi.Location = new System.Drawing.Point(210, 15);
            this.lblTienNghi.Name = "lblTienNghi";
            this.lblTienNghi.Size = new System.Drawing.Size(65, 16);
            this.lblTienNghi.TabIndex = 2;
            this.lblTienNghi.Text = "Tiện nghi:";
            // 
            // cmbTienNghi
            // 
            this.cmbTienNghi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTienNghi.FormattingEnabled = true;
            this.cmbTienNghi.Location = new System.Drawing.Point(281, 11);
            this.cmbTienNghi.Name = "cmbTienNghi";
            this.cmbTienNghi.Size = new System.Drawing.Size(120, 24);
            this.cmbTienNghi.TabIndex = 3;
            // 
            // lblPhongLapDat
            // 
            this.lblPhongLapDat.AutoSize = true;
            this.lblPhongLapDat.Location = new System.Drawing.Point(420, 15);
            this.lblPhongLapDat.Name = "lblPhongLapDat";
            this.lblPhongLapDat.Size = new System.Drawing.Size(49, 16);
            this.lblPhongLapDat.TabIndex = 4;
            this.lblPhongLapDat.Text = "Phòng:";
            // 
            // cmbPhongLapDat
            // 
            this.cmbPhongLapDat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhongLapDat.FormattingEnabled = true;
            this.cmbPhongLapDat.Location = new System.Drawing.Point(475, 11);
            this.cmbPhongLapDat.Name = "cmbPhongLapDat";
            this.cmbPhongLapDat.Size = new System.Drawing.Size(100, 24);
            this.cmbPhongLapDat.TabIndex = 5;
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Location = new System.Drawing.Point(595, 15);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(69, 16);
            this.lblTinhTrang.TabIndex = 6;
            this.lblTinhTrang.Text = "Tình trạng:";
            // 
            // txtTinhTrang
            // 
            this.txtTinhTrang.Location = new System.Drawing.Point(670, 12);
            this.txtTinhTrang.Name = "txtTinhTrang";
            this.txtTinhTrang.Size = new System.Drawing.Size(100, 22);
            this.txtTinhTrang.TabIndex = 7;
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.Location = new System.Drawing.Point(785, 9);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(100, 28);
            this.btnLapPhieu.TabIndex = 8;
            this.btnLapPhieu.Text = "Lập phiếu";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(830, 59);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 28);
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Thêm phòng";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // FrmPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 485);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.pnlLapDat);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.txtSucChua);
            this.Controls.Add(this.lblSucChua);
            this.Controls.Add(this.cmbKhuVuc);
            this.Controls.Add(this.lblKhuVuc);
            this.Controls.Add(this.txtSoPhong);
            this.Controls.Add(this.lblSoPhong);
            this.Controls.Add(this.lblTabLapDat);
            this.Controls.Add(this.lblTabTienNghi);
            this.Controls.Add(this.lblTabPhong);
            this.Name = "FrmPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phòng - Tiện nghi - Phiếu lắp đặt";
            this.Load += new System.EventHandler(this.FrmPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlLapDat.ResumeLayout(false);
            this.pnlLapDat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTabPhong;
        private System.Windows.Forms.Label lblTabTienNghi;
        private System.Windows.Forms.Label lblTabLapDat;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.ComboBox cmbKhuVuc;
        private System.Windows.Forms.Label lblSucChua;
        private System.Windows.Forms.TextBox txtSucChua;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel pnlLapDat;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblTienNghi;
        private System.Windows.Forms.ComboBox cmbTienNghi;
        private System.Windows.Forms.Label lblPhongLapDat;
        private System.Windows.Forms.ComboBox cmbPhongLapDat;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.Button btnLuu;
    }
}