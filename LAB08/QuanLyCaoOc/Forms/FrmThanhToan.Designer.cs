namespace QuanLyCaoOc.Forms
{
    partial class FrmThanhToan
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboHopDong = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.tabThu = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvKy = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSoPhieuKy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numSoTienKy = new System.Windows.Forms.NumericUpDown();
            this.btnThuThue = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPhongDV = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSoHDDV = new System.Windows.Forms.TextBox();
            this.btnLapHDDV = new System.Windows.Forms.Button();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvHDDV = new System.Windows.Forms.DataGridView();
            this.dgvCTDV = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSoPhieuDV = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numSoTienDV = new System.Windows.Forms.NumericUpDown();
            this.btnThuDV = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienKy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDV)).BeginInit();
            this.tabThu.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Text = "Hợp đồng:";
            this.label1.TabIndex = 1;
            // 
            // cboHopDong
            // 
            this.cboHopDong.Location = new System.Drawing.Point(80, 8);
            this.cboHopDong.Name = "cboHopDong";
            this.cboHopDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHopDong.FormattingEnabled = true;
            this.cboHopDong.Size = new System.Drawing.Size(120, 23);
            this.cboHopDong.TabIndex = 2;
            this.cboHopDong.SelectedIndexChanged += new System.EventHandler(this.cboHopDong_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 12);
            this.label2.Name = "label2";
            this.label2.Text = "NV thu:";
            this.label2.TabIndex = 3;
            // 
            // cboNV
            // 
            this.cboNV.Location = new System.Drawing.Point(270, 8);
            this.cboNV.Name = "cboNV";
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Size = new System.Drawing.Size(170, 23);
            this.cboNV.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 12);
            this.label3.Name = "label3";
            this.label3.Text = "Hình thức:";
            this.label3.TabIndex = 5;
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.Location = new System.Drawing.Point(530, 8);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.FormattingEnabled = true;
            this.cboHinhThuc.Size = new System.Drawing.Size(140, 23);
            this.cboHinhThuc.TabIndex = 6;
            // 
            // tabThu
            // 
            this.tabThu.Location = new System.Drawing.Point(10, 38);
            this.tabThu.Name = "tabThu";
            this.tabThu.Controls.Add(this.tabPage1);
            this.tabThu.Controls.Add(this.tabPage2);
            this.tabThu.SelectedIndex = 0;
            this.tabThu.Size = new System.Drawing.Size(1098, 552);
            this.tabThu.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Controls.Add(this.dgvKy);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtSoPhieuKy);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.numSoTienKy);
            this.tabPage1.Controls.Add(this.btnThuThue);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Text = "Tiền thuê";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.cboPhongDV);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.numThang);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numNam);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtSoHDDV);
            this.tabPage2.Controls.Add(this.btnLapHDDV);
            this.tabPage2.Controls.Add(this.dgvDichVu);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.dgvHDDV);
            this.tabPage2.Controls.Add(this.dgvCTDV);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtSoPhieuDV);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.numSoTienDV);
            this.tabPage2.Controls.Add(this.btnThuDV);
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Text = "Hóa đơn dịch vụ cuối tháng";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvKy
            // 
            this.dgvKy.Location = new System.Drawing.Point(11, 13);
            this.dgvKy.Name = "dgvKy";
            this.dgvKy.AllowUserToAddRows = false;
            this.dgvKy.AllowUserToDeleteRows = false;
            this.dgvKy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKy.BackgroundColor = System.Drawing.Color.White;
            this.dgvKy.MultiSelect = false;
            this.dgvKy.ReadOnly = true;
            this.dgvKy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKy.Size = new System.Drawing.Size(1066, 330);
            this.dgvKy.TabIndex = 8;
            this.dgvKy.SelectionChanged += new System.EventHandler(this.dgvKy_SelectionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 363);
            this.label4.Name = "label4";
            this.label4.Text = "Số phiếu thu:";
            this.label4.TabIndex = 9;
            // 
            // txtSoPhieuKy
            // 
            this.txtSoPhieuKy.Location = new System.Drawing.Point(95, 359);
            this.txtSoPhieuKy.Name = "txtSoPhieuKy";
            this.txtSoPhieuKy.Size = new System.Drawing.Size(110, 22);
            this.txtSoPhieuKy.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 363);
            this.label5.Name = "label5";
            this.label5.Text = "Số tiền thu:";
            this.label5.TabIndex = 11;
            // 
            // numSoTienKy
            // 
            this.numSoTienKy.Location = new System.Drawing.Point(300, 359);
            this.numSoTienKy.Name = "numSoTienKy";
            this.numSoTienKy.Size = new System.Drawing.Size(130, 23);
            this.numSoTienKy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSoTienKy.ThousandsSeparator = true;
            this.numSoTienKy.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            this.numSoTienKy.TabIndex = 12;
            // 
            // btnThuThue
            // 
            this.btnThuThue.Location = new System.Drawing.Point(455, 357);
            this.btnThuThue.Name = "btnThuThue";
            this.btnThuThue.Size = new System.Drawing.Size(150, 28);
            this.btnThuThue.Text = "Thu tiền thuê";
            this.btnThuThue.UseVisualStyleBackColor = true;
            this.btnThuThue.TabIndex = 13;
            this.btnThuThue.Click += new System.EventHandler(this.btnThuThue_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 403);
            this.label6.Name = "label6";
            this.label6.Text = "Gợi ý: số tiền tự điền bằng phần còn nợ của kỳ đang chọn; không cho thu vượt quá còn nợ.";
            this.label6.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 13);
            this.label7.Name = "label7";
            this.label7.Text = "Phòng:";
            this.label7.TabIndex = 15;
            // 
            // cboPhongDV
            // 
            this.cboPhongDV.Location = new System.Drawing.Point(60, 9);
            this.cboPhongDV.Name = "cboPhongDV";
            this.cboPhongDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongDV.FormattingEnabled = true;
            this.cboPhongDV.Size = new System.Drawing.Size(100, 23);
            this.cboPhongDV.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(180, 13);
            this.label8.Name = "label8";
            this.label8.Text = "Tháng:";
            this.label8.TabIndex = 17;
            // 
            // numThang
            // 
            this.numThang.Location = new System.Drawing.Point(225, 9);
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(50, 23);
            this.numThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.numThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThang.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(290, 13);
            this.label9.Name = "label9";
            this.label9.Text = "Năm:";
            this.label9.TabIndex = 19;
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(325, 9);
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(70, 23);
            this.numNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numNam.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numNam.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            this.numNam.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(415, 13);
            this.label10.Name = "label10";
            this.label10.Text = "Số HĐ dịch vụ:";
            this.label10.TabIndex = 21;
            // 
            // txtSoHDDV
            // 
            this.txtSoHDDV.Location = new System.Drawing.Point(500, 9);
            this.txtSoHDDV.Name = "txtSoHDDV";
            this.txtSoHDDV.Size = new System.Drawing.Size(100, 22);
            this.txtSoHDDV.TabIndex = 22;
            // 
            // btnLapHDDV
            // 
            this.btnLapHDDV.Location = new System.Drawing.Point(625, 7);
            this.btnLapHDDV.Name = "btnLapHDDV";
            this.btnLapHDDV.Size = new System.Drawing.Size(160, 28);
            this.btnLapHDDV.Text = "Lập hóa đơn dịch vụ";
            this.btnLapHDDV.UseVisualStyleBackColor = true;
            this.btnLapHDDV.TabIndex = 23;
            this.btnLapHDDV.Click += new System.EventHandler(this.btnLapHDDV_Click);
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.Location = new System.Drawing.Point(11, 45);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.ReadOnly = false;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(520, 130);
            this.dgvDichVu.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(545, 51);
            this.label11.Name = "label11";
            this.label11.Text = "Nhập số lượng sử dụng; hạn nộp = ngày cuối tháng.";
            this.label11.TabIndex = 25;
            // 
            // dgvHDDV
            // 
            this.dgvHDDV.Location = new System.Drawing.Point(11, 193);
            this.dgvHDDV.Name = "dgvHDDV";
            this.dgvHDDV.AllowUserToAddRows = false;
            this.dgvHDDV.AllowUserToDeleteRows = false;
            this.dgvHDDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHDDV.BackgroundColor = System.Drawing.Color.White;
            this.dgvHDDV.MultiSelect = false;
            this.dgvHDDV.ReadOnly = true;
            this.dgvHDDV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHDDV.Size = new System.Drawing.Size(1066, 130);
            this.dgvHDDV.TabIndex = 26;
            this.dgvHDDV.SelectionChanged += new System.EventHandler(this.dgvHDDV_SelectionChanged);
            // 
            // dgvCTDV
            // 
            this.dgvCTDV.Location = new System.Drawing.Point(11, 338);
            this.dgvCTDV.Name = "dgvCTDV";
            this.dgvCTDV.AllowUserToAddRows = false;
            this.dgvCTDV.AllowUserToDeleteRows = false;
            this.dgvCTDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCTDV.BackgroundColor = System.Drawing.Color.White;
            this.dgvCTDV.MultiSelect = false;
            this.dgvCTDV.ReadOnly = true;
            this.dgvCTDV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCTDV.Size = new System.Drawing.Size(520, 90);
            this.dgvCTDV.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(555, 343);
            this.label12.Name = "label12";
            this.label12.Text = "Số phiếu thu:";
            this.label12.TabIndex = 28;
            // 
            // txtSoPhieuDV
            // 
            this.txtSoPhieuDV.Location = new System.Drawing.Point(635, 339);
            this.txtSoPhieuDV.Name = "txtSoPhieuDV";
            this.txtSoPhieuDV.Size = new System.Drawing.Size(100, 22);
            this.txtSoPhieuDV.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(555, 375);
            this.label13.Name = "label13";
            this.label13.Text = "Số tiền thu:";
            this.label13.TabIndex = 30;
            // 
            // numSoTienDV
            // 
            this.numSoTienDV.Location = new System.Drawing.Point(635, 371);
            this.numSoTienDV.Name = "numSoTienDV";
            this.numSoTienDV.Size = new System.Drawing.Size(130, 23);
            this.numSoTienDV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSoTienDV.ThousandsSeparator = true;
            this.numSoTienDV.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            this.numSoTienDV.TabIndex = 31;
            // 
            // btnThuDV
            // 
            this.btnThuDV.Location = new System.Drawing.Point(785, 369);
            this.btnThuDV.Name = "btnThuDV";
            this.btnThuDV.Size = new System.Drawing.Size(150, 28);
            this.btnThuDV.Text = "Thu tiền dịch vụ";
            this.btnThuDV.UseVisualStyleBackColor = true;
            this.btnThuDV.TabIndex = 32;
            this.btnThuDV.Click += new System.EventHandler(this.btnThuDV_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1010, 598);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(94, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 33;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboHopDong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboNV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboHinhThuc);
            this.Controls.Add(this.tabThu);
            this.Controls.Add(this.btnDong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thu tiền thuê - Hóa đơn dịch vụ";
            this.Load += new System.EventHandler(this.FrmThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienKy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDV)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabThu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHopDong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboHinhThuc;
        private System.Windows.Forms.TabControl tabThu;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvKy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSoPhieuKy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSoTienKy;
        private System.Windows.Forms.Button btnThuThue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPhongDV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSoHDDV;
        private System.Windows.Forms.Button btnLapHDDV;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvHDDV;
        private System.Windows.Forms.DataGridView dgvCTDV;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSoPhieuDV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numSoTienDV;
        private System.Windows.Forms.Button btnThuDV;
        private System.Windows.Forms.Button btnDong;
    }
}
