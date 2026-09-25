namespace QuanLyKhachSan.UI
{
    partial class FrmDanhMuc
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
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblTienNghi = new System.Windows.Forms.Label();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.lblDenBu = new System.Windows.Forms.Label();
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblExtra = new System.Windows.Forms.Label();
            this.txtExtra = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKhuVuc.ForeColor = System.Drawing.Color.Gray;
            this.lblKhuVuc.Location = new System.Drawing.Point(40, 25);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(80, 23);
            this.lblKhuVuc.TabIndex = 0;
            this.lblKhuVuc.Text = "[ Khu vực ]";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNhanVien.ForeColor = System.Drawing.Color.Gray;
            this.lblNhanVien.Location = new System.Drawing.Point(140, 25);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(95, 23);
            this.lblNhanVien.TabIndex = 1;
            this.lblNhanVien.Text = "[ Nhân viên ]";
            // 
            // lblTienNghi
            // 
            this.lblTienNghi.AutoSize = true;
            this.lblTienNghi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienNghi.ForeColor = System.Drawing.Color.Gray;
            this.lblTienNghi.Location = new System.Drawing.Point(250, 25);
            this.lblTienNghi.Name = "lblTienNghi";
            this.lblTienNghi.Size = new System.Drawing.Size(120, 23);
            this.lblTienNghi.TabIndex = 2;
            this.lblTienNghi.Text = "[ Loại tiện nghi ]";
            // 
            // lblDichVu
            // 
            this.lblDichVu.AutoSize = true;
            this.lblDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDichVu.ForeColor = System.Drawing.Color.Blue;
            this.lblDichVu.Location = new System.Drawing.Point(385, 25);
            this.lblDichVu.Name = "lblDichVu";
            this.lblDichVu.Size = new System.Drawing.Size(88, 23);
            this.lblDichVu.TabIndex = 3;
            this.lblDichVu.Text = "[ Dịch vụ ]";
            // 
            // lblDenBu
            // 
            this.lblDenBu.AutoSize = true;
            this.lblDenBu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDenBu.ForeColor = System.Drawing.Color.Gray;
            this.lblDenBu.Location = new System.Drawing.Point(490, 25);
            this.lblDenBu.Name = "lblDenBu";
            this.lblDenBu.Size = new System.Drawing.Size(140, 23);
            this.lblDenBu.TabIndex = 4;
            this.lblDenBu.Text = "[ Quy định đền bù ]";
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Location = new System.Drawing.Point(40, 75);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(30, 16);
            this.lblMa.TabIndex = 5;
            this.lblMa.Text = "Mã:";
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(75, 72);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(110, 22);
            this.txtMa.TabIndex = 6;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(205, 75);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(34, 16);
            this.lblTen.TabIndex = 7;
            this.lblTen.Text = "Tên:";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(245, 72);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(180, 22);
            this.txtTen.TabIndex = 8;
            // 
            // lblExtra
            // 
            this.lblExtra.AutoSize = true;
            this.lblExtra.Location = new System.Drawing.Point(445, 75);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(90, 16);
            this.lblExtra.TabIndex = 9;
            this.lblExtra.Text = "Đơn vị / Vai trò:";
            // 
            // txtExtra
            // 
            this.txtExtra.Location = new System.Drawing.Point(545, 72);
            this.txtExtra.Name = "txtExtra";
            this.txtExtra.Size = new System.Drawing.Size(140, 22);
            this.txtExtra.TabIndex = 10;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(700, 69);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 28);
            this.btnThem.TabIndex = 11;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvData
            // 
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(40, 115);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(740, 320);
            this.dgvData.TabIndex = 12;
            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 460);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtExtra);
            this.Controls.Add(this.lblExtra);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.lblMa);
            this.Controls.Add(this.lblDenBu);
            this.Controls.Add(this.lblDichVu);
            this.Controls.Add(this.lblTienNghi);
            this.Controls.Add(this.lblNhanVien);
            this.Controls.Add(this.lblKhuVuc);
            this.Name = "FrmDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục khách sạn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblTienNghi;
        private System.Windows.Forms.Label lblDichVu;
        private System.Windows.Forms.Label lblDenBu;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblExtra;
        private System.Windows.Forms.TextBox txtExtra;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvData;
    }
}