namespace QuanLyCaoOc.Forms
{
    partial class FrmTangPhong
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
            this.txtTangMa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTangTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numSoTang = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numDoCao = new System.Windows.Forms.NumericUpDown();
            this.btnThemTang = new System.Windows.Forms.Button();
            this.dgvTang = new System.Windows.Forms.DataGridView();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPhongMa = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTang = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numDienTich = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cboHuong = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numDonGia = new System.Windows.Forms.NumericUpDown();
            this.btnThemPhong = new System.Windows.Forms.Button();
            this.btnCapNhatGia = new System.Windows.Forms.Button();
            this.btnBaoTri = new System.Windows.Forms.Button();
            this.btnBoBaoTri = new System.Windows.Forms.Button();
            this.grpBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnPhongTrong = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDienTich)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBox1
            // 
            this.grpBox1.Location = new System.Drawing.Point(12, 18);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(340, 250);
            this.grpBox1.Text = "Tầng";
            this.grpBox1.TabStop = false;
            this.grpBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Text = "Mã tầng:";
            this.label1.TabIndex = 2;
            // 
            // txtTangMa
            // 
            this.txtTangMa.Location = new System.Drawing.Point(90, 36);
            this.txtTangMa.Name = "txtTangMa";
            this.txtTangMa.Size = new System.Drawing.Size(70, 22);
            this.txtTangMa.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 40);
            this.label2.Name = "label2";
            this.label2.Text = "Tên tầng:";
            this.label2.TabIndex = 4;
            // 
            // txtTangTen
            // 
            this.txtTangTen.Location = new System.Drawing.Point(240, 36);
            this.txtTangTen.Name = "txtTangTen";
            this.txtTangTen.Size = new System.Drawing.Size(100, 22);
            this.txtTangTen.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 70);
            this.label3.Name = "label3";
            this.label3.Text = "Số tầng:";
            this.label3.TabIndex = 6;
            // 
            // numSoTang
            // 
            this.numSoTang.Location = new System.Drawing.Point(90, 66);
            this.numSoTang.Name = "numSoTang";
            this.numSoTang.Size = new System.Drawing.Size(70, 23);
            this.numSoTang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSoTang.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.numSoTang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoTang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoTang.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 70);
            this.label4.Name = "label4";
            this.label4.Text = "Độ cao (m):";
            this.label4.TabIndex = 8;
            // 
            // numDoCao
            // 
            this.numDoCao.Location = new System.Drawing.Point(250, 66);
            this.numDoCao.Name = "numDoCao";
            this.numDoCao.Size = new System.Drawing.Size(70, 23);
            this.numDoCao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDoCao.DecimalPlaces = 1;
            this.numDoCao.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numDoCao.TabIndex = 9;
            // 
            // btnThemTang
            // 
            this.btnThemTang.Location = new System.Drawing.Point(24, 98);
            this.btnThemTang.Name = "btnThemTang";
            this.btnThemTang.Size = new System.Drawing.Size(90, 26);
            this.btnThemTang.Text = "Thêm tầng";
            this.btnThemTang.UseVisualStyleBackColor = true;
            this.btnThemTang.TabIndex = 10;
            this.btnThemTang.Click += new System.EventHandler(this.btnThemTang_Click);
            // 
            // dgvTang
            // 
            this.dgvTang.Location = new System.Drawing.Point(24, 132);
            this.dgvTang.Name = "dgvTang";
            this.dgvTang.AllowUserToAddRows = false;
            this.dgvTang.AllowUserToDeleteRows = false;
            this.dgvTang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTang.BackgroundColor = System.Drawing.Color.White;
            this.dgvTang.MultiSelect = false;
            this.dgvTang.ReadOnly = true;
            this.dgvTang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTang.Size = new System.Drawing.Size(316, 125);
            this.dgvTang.TabIndex = 11;
            // 
            // grpBox2
            // 
            this.grpBox2.Location = new System.Drawing.Point(366, 18);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(740, 250);
            this.grpBox2.Text = "Phòng";
            this.grpBox2.TabStop = false;
            this.grpBox2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 40);
            this.label5.Name = "label5";
            this.label5.Text = "Mã phòng:";
            this.label5.TabIndex = 13;
            // 
            // txtPhongMa
            // 
            this.txtPhongMa.Location = new System.Drawing.Point(450, 36);
            this.txtPhongMa.Name = "txtPhongMa";
            this.txtPhongMa.Size = new System.Drawing.Size(90, 22);
            this.txtPhongMa.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(560, 40);
            this.label6.Name = "label6";
            this.label6.Text = "Tầng:";
            this.label6.TabIndex = 15;
            // 
            // cboTang
            // 
            this.cboTang.Location = new System.Drawing.Point(600, 36);
            this.cboTang.Name = "cboTang";
            this.cboTang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTang.FormattingEnabled = true;
            this.cboTang.Size = new System.Drawing.Size(110, 23);
            this.cboTang.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 70);
            this.label7.Name = "label7";
            this.label7.Text = "Diện tích (m²):";
            this.label7.TabIndex = 17;
            // 
            // numDienTich
            // 
            this.numDienTich.Location = new System.Drawing.Point(470, 66);
            this.numDienTich.Name = "numDienTich";
            this.numDienTich.Size = new System.Drawing.Size(80, 23);
            this.numDienTich.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDienTich.DecimalPlaces = 2;
            this.numDienTich.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numDienTich.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(570, 70);
            this.label8.Name = "label8";
            this.label8.Text = "Hướng:";
            this.label8.TabIndex = 19;
            // 
            // cboHuong
            // 
            this.cboHuong.Location = new System.Drawing.Point(620, 66);
            this.cboHuong.Name = "cboHuong";
            this.cboHuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHuong.FormattingEnabled = true;
            this.cboHuong.Size = new System.Drawing.Size(90, 23);
            this.cboHuong.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(730, 70);
            this.label9.Name = "label9";
            this.label9.Text = "Đơn giá/tháng:";
            this.label9.TabIndex = 21;
            // 
            // numDonGia
            // 
            this.numDonGia.Location = new System.Drawing.Point(820, 66);
            this.numDonGia.Name = "numDonGia";
            this.numDonGia.Size = new System.Drawing.Size(130, 23);
            this.numDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDonGia.ThousandsSeparator = true;
            this.numDonGia.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            this.numDonGia.TabIndex = 22;
            // 
            // btnThemPhong
            // 
            this.btnThemPhong.Location = new System.Drawing.Point(378, 100);
            this.btnThemPhong.Name = "btnThemPhong";
            this.btnThemPhong.Size = new System.Drawing.Size(90, 26);
            this.btnThemPhong.Text = "Thêm phòng";
            this.btnThemPhong.UseVisualStyleBackColor = true;
            this.btnThemPhong.TabIndex = 23;
            this.btnThemPhong.Click += new System.EventHandler(this.btnThemPhong_Click);
            // 
            // btnCapNhatGia
            // 
            this.btnCapNhatGia.Location = new System.Drawing.Point(478, 100);
            this.btnCapNhatGia.Name = "btnCapNhatGia";
            this.btnCapNhatGia.Size = new System.Drawing.Size(110, 26);
            this.btnCapNhatGia.Text = "Cập nhật giá";
            this.btnCapNhatGia.UseVisualStyleBackColor = true;
            this.btnCapNhatGia.TabIndex = 24;
            this.btnCapNhatGia.Click += new System.EventHandler(this.btnCapNhatGia_Click);
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.Location = new System.Drawing.Point(598, 100);
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.Size = new System.Drawing.Size(90, 26);
            this.btnBaoTri.Text = "Bảo trì";
            this.btnBaoTri.UseVisualStyleBackColor = true;
            this.btnBaoTri.TabIndex = 25;
            this.btnBaoTri.Click += new System.EventHandler(this.btnBaoTri_Click);
            // 
            // btnBoBaoTri
            // 
            this.btnBoBaoTri.Location = new System.Drawing.Point(698, 100);
            this.btnBoBaoTri.Name = "btnBoBaoTri";
            this.btnBoBaoTri.Size = new System.Drawing.Size(100, 26);
            this.btnBoBaoTri.Text = "Bỏ bảo trì";
            this.btnBoBaoTri.UseVisualStyleBackColor = true;
            this.btnBoBaoTri.TabIndex = 26;
            this.btnBoBaoTri.Click += new System.EventHandler(this.btnBoBaoTri_Click);
            // 
            // grpBox3
            // 
            this.grpBox3.Location = new System.Drawing.Point(378, 140);
            this.grpBox3.Name = "grpBox3";
            this.grpBox3.Size = new System.Drawing.Size(716, 118);
            this.grpBox3.Text = "Tìm phòng trống theo thời gian";
            this.grpBox3.TabStop = false;
            this.grpBox3.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 168);
            this.label10.Name = "label10";
            this.label10.Text = "Từ ngày:";
            this.label10.TabIndex = 28;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.Location = new System.Drawing.Point(450, 164);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTuNgay.Size = new System.Drawing.Size(130, 23);
            this.dtTuNgay.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(600, 168);
            this.label11.Name = "label11";
            this.label11.Text = "Đến ngày:";
            this.label11.TabIndex = 30;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.Location = new System.Drawing.Point(665, 164);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDenNgay.Size = new System.Drawing.Size(130, 23);
            this.dtDenNgay.TabIndex = 31;
            // 
            // btnPhongTrong
            // 
            this.btnPhongTrong.Location = new System.Drawing.Point(815, 162);
            this.btnPhongTrong.Name = "btnPhongTrong";
            this.btnPhongTrong.Size = new System.Drawing.Size(130, 26);
            this.btnPhongTrong.Text = "Tìm phòng trống";
            this.btnPhongTrong.UseVisualStyleBackColor = true;
            this.btnPhongTrong.TabIndex = 32;
            this.btnPhongTrong.Click += new System.EventHandler(this.btnPhongTrong_Click);
            // 
            // btnTatCa
            // 
            this.btnTatCa.Location = new System.Drawing.Point(955, 162);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(110, 26);
            this.btnTatCa.Text = "Tất cả phòng";
            this.btnTatCa.UseVisualStyleBackColor = true;
            this.btnTatCa.TabIndex = 33;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(392, 205);
            this.label12.Name = "label12";
            this.label12.Text = "Phòng trống = không có hợp đồng nào chồng lấn khoảng thời gian đã chọn.";
            this.label12.TabIndex = 34;
            // 
            // dgvPhong
            // 
            this.dgvPhong.Location = new System.Drawing.Point(12, 285);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhong.MultiSelect = false;
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(1094, 300);
            this.dgvPhong.TabIndex = 35;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1006, 596);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 36;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmTangPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTangMa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTangTen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numSoTang);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numDoCao);
            this.Controls.Add(this.btnThemTang);
            this.Controls.Add(this.dgvTang);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPhongMa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTang);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numDienTich);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboHuong);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numDonGia);
            this.Controls.Add(this.btnThemPhong);
            this.Controls.Add(this.btnCapNhatGia);
            this.Controls.Add(this.btnBaoTri);
            this.Controls.Add(this.btnBoBaoTri);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtTuNgay);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.btnPhongTrong);
            this.Controls.Add(this.btnTatCa);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.grpBox2);
            this.Controls.Add(this.grpBox3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmTangPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý tầng - phòng";
            this.Load += new System.EventHandler(this.FrmTangPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoTang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDienTich)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTangMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTangTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSoTang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDoCao;
        private System.Windows.Forms.Button btnThemTang;
        private System.Windows.Forms.DataGridView dgvTang;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPhongMa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTang;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numDienTich;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboHuong;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.Button btnThemPhong;
        private System.Windows.Forms.Button btnCapNhatGia;
        private System.Windows.Forms.Button btnBaoTri;
        private System.Windows.Forms.Button btnBoBaoTri;
        private System.Windows.Forms.GroupBox grpBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.Button btnPhongTrong;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.Button btnDong;
    }
}
