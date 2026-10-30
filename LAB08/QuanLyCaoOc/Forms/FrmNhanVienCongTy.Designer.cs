namespace QuanLyCaoOc.Forms
{
    partial class FrmNhanVienCongTy
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaNVCT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHoTenNV = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCongTy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtVaoLam = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHinhAnh = new System.Windows.Forms.TextBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnNghi = new System.Windows.Forms.Button();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNVChon = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaThe = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboLoaiXe = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBienSo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNhanHieu = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMauSac = new System.Windows.Forms.TextBox();
            this.btnCapThe = new System.Windows.Forms.Button();
            this.btnThuHoi = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.dgvNVCT = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.dgvThe = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNVCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBox1
            // 
            this.grpBox1.Location = new System.Drawing.Point(12, 18);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(560, 250);
            this.grpBox1.Text = "Báo cáo nhân viên mới của công ty thuê";
            this.grpBox1.TabStop = false;
            this.grpBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 44);
            this.label1.Name = "label1";
            this.label1.Text = "Mã NV:";
            this.label1.TabIndex = 2;
            // 
            // txtMaNVCT
            // 
            this.txtMaNVCT.Location = new System.Drawing.Point(90, 40);
            this.txtMaNVCT.Name = "txtMaNVCT";
            this.txtMaNVCT.Size = new System.Drawing.Size(90, 22);
            this.txtMaNVCT.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 44);
            this.label2.Name = "label2";
            this.label2.Text = "Họ tên:";
            this.label2.TabIndex = 4;
            // 
            // txtHoTenNV
            // 
            this.txtHoTenNV.Location = new System.Drawing.Point(245, 40);
            this.txtHoTenNV.Name = "txtHoTenNV";
            this.txtHoTenNV.Size = new System.Drawing.Size(200, 22);
            this.txtHoTenNV.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 76);
            this.label3.Name = "label3";
            this.label3.Text = "Công ty:";
            this.label3.TabIndex = 6;
            // 
            // cboCongTy
            // 
            this.cboCongTy.Location = new System.Drawing.Point(90, 72);
            this.cboCongTy.Name = "cboCongTy";
            this.cboCongTy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCongTy.FormattingEnabled = true;
            this.cboCongTy.Size = new System.Drawing.Size(230, 23);
            this.cboCongTy.TabIndex = 7;
            this.cboCongTy.SelectedIndexChanged += new System.EventHandler(this.cboCongTy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 76);
            this.label4.Name = "label4";
            this.label4.Text = "Phòng:";
            this.label4.TabIndex = 8;
            // 
            // cboPhong
            // 
            this.cboPhong.Location = new System.Drawing.Point(380, 72);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhong.FormattingEnabled = true;
            this.cboPhong.Size = new System.Drawing.Size(90, 23);
            this.cboPhong.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 108);
            this.label5.Name = "label5";
            this.label5.Text = "Chức vụ:";
            this.label5.TabIndex = 10;
            // 
            // txtChucVu
            // 
            this.txtChucVu.Location = new System.Drawing.Point(90, 104);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(150, 22);
            this.txtChucVu.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 108);
            this.label6.Name = "label6";
            this.label6.Text = "Ngày vào:";
            this.label6.TabIndex = 12;
            // 
            // dtVaoLam
            // 
            this.dtVaoLam.Location = new System.Drawing.Point(315, 104);
            this.dtVaoLam.Name = "dtVaoLam";
            this.dtVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtVaoLam.Size = new System.Drawing.Size(120, 23);
            this.dtVaoLam.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 140);
            this.label7.Name = "label7";
            this.label7.Text = "Ảnh:";
            this.label7.TabIndex = 14;
            // 
            // txtHinhAnh
            // 
            this.txtHinhAnh.Location = new System.Drawing.Point(90, 136);
            this.txtHinhAnh.Name = "txtHinhAnh";
            this.txtHinhAnh.Size = new System.Drawing.Size(280, 22);
            this.txtHinhAnh.ReadOnly = true;
            this.txtHinhAnh.TabIndex = 15;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(380, 134);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(70, 26);
            this.btnChonAnh.Text = "Chọn...";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.TabIndex = 16;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 172);
            this.label8.Name = "label8";
            this.label8.Text = "NV tiếp nhận:";
            this.label8.TabIndex = 17;
            // 
            // cboNV
            // 
            this.cboNV.Location = new System.Drawing.Point(110, 168);
            this.cboNV.Name = "cboNV";
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Size = new System.Drawing.Size(170, 23);
            this.cboNV.TabIndex = 18;
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Location = new System.Drawing.Point(300, 166);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(110, 28);
            this.btnBaoCao.Text = "Báo cáo NV mới";
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.TabIndex = 19;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnNghi
            // 
            this.btnNghi.Location = new System.Drawing.Point(426, 166);
            this.btnNghi.Name = "btnNghi";
            this.btnNghi.Size = new System.Drawing.Size(130, 28);
            this.btnNghi.Text = "Đánh dấu nghỉ việc";
            this.btnNghi.UseVisualStyleBackColor = true;
            this.btnNghi.TabIndex = 20;
            this.btnNghi.Click += new System.EventHandler(this.btnNghi_Click);
            // 
            // picAnh
            // 
            this.picAnh.Location = new System.Drawing.Point(460, 38);
            this.picAnh.Name = "picAnh";
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.Size = new System.Drawing.Size(100, 120);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabStop = false;
            this.picAnh.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 210);
            this.label9.Name = "label9";
            this.label9.Text = "Tầng của phòng được suy ra từ phòng đang thuê; chỉ chọn được phòng công ty còn hợp đồng.";
            this.label9.TabIndex = 22;
            // 
            // grpBox2
            // 
            this.grpBox2.Location = new System.Drawing.Point(586, 18);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(560, 250);
            this.grpBox2.Text = "Cấp thẻ giữ xe cho nhân viên đã chọn";
            this.grpBox2.TabStop = false;
            this.grpBox2.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(600, 44);
            this.label10.Name = "label10";
            this.label10.Text = "NV đã chọn:";
            this.label10.TabIndex = 24;
            // 
            // txtNVChon
            // 
            this.txtNVChon.Location = new System.Drawing.Point(680, 40);
            this.txtNVChon.Name = "txtNVChon";
            this.txtNVChon.Size = new System.Drawing.Size(90, 22);
            this.txtNVChon.ReadOnly = true;
            this.txtNVChon.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(790, 44);
            this.label11.Name = "label11";
            this.label11.Text = "Mã thẻ:";
            this.label11.TabIndex = 26;
            // 
            // txtMaThe
            // 
            this.txtMaThe.Location = new System.Drawing.Point(840, 40);
            this.txtMaThe.Name = "txtMaThe";
            this.txtMaThe.Size = new System.Drawing.Size(90, 22);
            this.txtMaThe.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(600, 76);
            this.label12.Name = "label12";
            this.label12.Text = "Loại xe:";
            this.label12.TabIndex = 28;
            // 
            // cboLoaiXe
            // 
            this.cboLoaiXe.Location = new System.Drawing.Point(660, 72);
            this.cboLoaiXe.Name = "cboLoaiXe";
            this.cboLoaiXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiXe.FormattingEnabled = true;
            this.cboLoaiXe.Size = new System.Drawing.Size(110, 23);
            this.cboLoaiXe.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(790, 76);
            this.label13.Name = "label13";
            this.label13.Text = "Biển số:";
            this.label13.TabIndex = 30;
            // 
            // txtBienSo
            // 
            this.txtBienSo.Location = new System.Drawing.Point(840, 72);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.Size = new System.Drawing.Size(120, 22);
            this.txtBienSo.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(600, 108);
            this.label14.Name = "label14";
            this.label14.Text = "Nhãn hiệu:";
            this.label14.TabIndex = 32;
            // 
            // txtNhanHieu
            // 
            this.txtNhanHieu.Location = new System.Drawing.Point(670, 104);
            this.txtNhanHieu.Name = "txtNhanHieu";
            this.txtNhanHieu.Size = new System.Drawing.Size(100, 22);
            this.txtNhanHieu.TabIndex = 33;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(790, 108);
            this.label15.Name = "label15";
            this.label15.Text = "Màu sắc:";
            this.label15.TabIndex = 34;
            // 
            // txtMauSac
            // 
            this.txtMauSac.Location = new System.Drawing.Point(850, 104);
            this.txtMauSac.Name = "txtMauSac";
            this.txtMauSac.Size = new System.Drawing.Size(90, 22);
            this.txtMauSac.TabIndex = 35;
            // 
            // btnCapThe
            // 
            this.btnCapThe.Location = new System.Drawing.Point(600, 150);
            this.btnCapThe.Name = "btnCapThe";
            this.btnCapThe.Size = new System.Drawing.Size(110, 28);
            this.btnCapThe.Text = "Cấp thẻ";
            this.btnCapThe.UseVisualStyleBackColor = true;
            this.btnCapThe.TabIndex = 36;
            this.btnCapThe.Click += new System.EventHandler(this.btnCapThe_Click);
            // 
            // btnThuHoi
            // 
            this.btnThuHoi.Location = new System.Drawing.Point(720, 150);
            this.btnThuHoi.Name = "btnThuHoi";
            this.btnThuHoi.Size = new System.Drawing.Size(110, 28);
            this.btnThuHoi.Text = "Thu hồi thẻ";
            this.btnThuHoi.UseVisualStyleBackColor = true;
            this.btnThuHoi.TabIndex = 37;
            this.btnThuHoi.Click += new System.EventHandler(this.btnThuHoi_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label16.Location = new System.Drawing.Point(14, 282);
            this.label16.Name = "label16";
            this.label16.Text = "Nhân viên công ty";
            this.label16.TabIndex = 38;
            // 
            // dgvNVCT
            // 
            this.dgvNVCT.Location = new System.Drawing.Point(14, 300);
            this.dgvNVCT.Name = "dgvNVCT";
            this.dgvNVCT.AllowUserToAddRows = false;
            this.dgvNVCT.AllowUserToDeleteRows = false;
            this.dgvNVCT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNVCT.BackgroundColor = System.Drawing.Color.White;
            this.dgvNVCT.MultiSelect = false;
            this.dgvNVCT.ReadOnly = true;
            this.dgvNVCT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNVCT.Size = new System.Drawing.Size(1132, 150);
            this.dgvNVCT.TabIndex = 39;
            this.dgvNVCT.SelectionChanged += new System.EventHandler(this.dgvNVCT_SelectionChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label17.Location = new System.Drawing.Point(14, 464);
            this.label17.Name = "label17";
            this.label17.Text = "Thẻ giữ xe";
            this.label17.TabIndex = 40;
            // 
            // dgvThe
            // 
            this.dgvThe.Location = new System.Drawing.Point(14, 482);
            this.dgvThe.Name = "dgvThe";
            this.dgvThe.AllowUserToAddRows = false;
            this.dgvThe.AllowUserToDeleteRows = false;
            this.dgvThe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThe.BackgroundColor = System.Drawing.Color.White;
            this.dgvThe.MultiSelect = false;
            this.dgvThe.ReadOnly = true;
            this.dgvThe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThe.Size = new System.Drawing.Size(1132, 150);
            this.dgvThe.TabIndex = 41;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1046, 650);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 42;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmNhanVienCongTy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 700);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaNVCT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHoTenNV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCongTy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboPhong);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtChucVu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtVaoLam);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtHinhAnh);
            this.Controls.Add(this.btnChonAnh);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboNV);
            this.Controls.Add(this.btnBaoCao);
            this.Controls.Add(this.btnNghi);
            this.Controls.Add(this.picAnh);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNVChon);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMaThe);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboLoaiXe);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtBienSo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtNhanHieu);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtMauSac);
            this.Controls.Add(this.btnCapThe);
            this.Controls.Add(this.btnThuHoi);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dgvNVCT);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dgvThe);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.grpBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmNhanVienCongTy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhân viên công ty - Thẻ giữ xe";
            this.Load += new System.EventHandler(this.FrmNhanVienCongTy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNVCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaNVCT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHoTenNV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCongTy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboPhong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtVaoLam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHinhAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnNghi;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNVChon;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMaThe;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboLoaiXe;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBienSo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNhanHieu;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMauSac;
        private System.Windows.Forms.Button btnCapThe;
        private System.Windows.Forms.Button btnThuHoi;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dgvNVCT;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dgvThe;
        private System.Windows.Forms.Button btnDong;
    }
}
