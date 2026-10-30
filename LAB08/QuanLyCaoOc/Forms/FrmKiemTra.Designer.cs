namespace QuanLyCaoOc.Forms
{
    partial class FrmKiemTra
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
            this.dgvDenHan = new System.Windows.Forms.DataGridView();
            this.btnThemVaoDot = new System.Windows.Forms.Button();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaDot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtNgayKT = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnLuuDot = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvDot = new System.Windows.Forms.DataGridView();
            this.dgvCTDot = new System.Windows.Forms.DataGridView();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSoBB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboQuyDinh = new System.Windows.Forms.ComboBox();
            this.btnLapBienBan = new System.Windows.Forms.Button();
            this.btnNopPhat = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvBienBan = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDenHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBienBan)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Text = "Phòng đến hạn kiểm tra (≥ 1 năm/lần hoặc chưa kiểm tra lần nào)";
            this.label1.TabIndex = 1;
            // 
            // dgvDenHan
            // 
            this.dgvDenHan.Location = new System.Drawing.Point(14, 26);
            this.dgvDenHan.Name = "dgvDenHan";
            this.dgvDenHan.AllowUserToAddRows = false;
            this.dgvDenHan.AllowUserToDeleteRows = false;
            this.dgvDenHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDenHan.BackgroundColor = System.Drawing.Color.White;
            this.dgvDenHan.MultiSelect = false;
            this.dgvDenHan.ReadOnly = true;
            this.dgvDenHan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDenHan.Size = new System.Drawing.Size(560, 150);
            this.dgvDenHan.TabIndex = 2;
            // 
            // btnThemVaoDot
            // 
            this.btnThemVaoDot.Location = new System.Drawing.Point(590, 30);
            this.btnThemVaoDot.Name = "btnThemVaoDot";
            this.btnThemVaoDot.Size = new System.Drawing.Size(150, 28);
            this.btnThemVaoDot.Text = "Thêm vào đợt kiểm tra >";
            this.btnThemVaoDot.UseVisualStyleBackColor = true;
            this.btnThemVaoDot.TabIndex = 3;
            this.btnThemVaoDot.Click += new System.EventHandler(this.btnThemVaoDot_Click);
            // 
            // grpBox1
            // 
            this.grpBox1.Location = new System.Drawing.Point(756, 20);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(408, 150);
            this.grpBox1.Text = "Đợt kiểm tra";
            this.grpBox1.TabStop = false;
            this.grpBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(768, 46);
            this.label2.Name = "label2";
            this.label2.Text = "Mã đợt:";
            this.label2.TabIndex = 5;
            // 
            // txtMaDot
            // 
            this.txtMaDot.Location = new System.Drawing.Point(820, 42);
            this.txtMaDot.Name = "txtMaDot";
            this.txtMaDot.Size = new System.Drawing.Size(90, 22);
            this.txtMaDot.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(925, 46);
            this.label3.Name = "label3";
            this.label3.Text = "Ngày:";
            this.label3.TabIndex = 7;
            // 
            // dtNgayKT
            // 
            this.dtNgayKT.Location = new System.Drawing.Point(960, 42);
            this.dtNgayKT.Name = "dtNgayKT";
            this.dtNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgayKT.Size = new System.Drawing.Size(110, 23);
            this.dtNgayKT.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(768, 78);
            this.label4.Name = "label4";
            this.label4.Text = "Người kiểm tra:";
            this.label4.TabIndex = 9;
            // 
            // cboNV
            // 
            this.cboNV.Location = new System.Drawing.Point(860, 74);
            this.cboNV.Name = "cboNV";
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Size = new System.Drawing.Size(170, 23);
            this.cboNV.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(768, 110);
            this.label5.Name = "label5";
            this.label5.Text = "Ghi chú:";
            this.label5.TabIndex = 11;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(820, 106);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(330, 22);
            this.txtGhiChu.TabIndex = 12;
            // 
            // btnLuuDot
            // 
            this.btnLuuDot.Location = new System.Drawing.Point(1040, 136);
            this.btnLuuDot.Name = "btnLuuDot";
            this.btnLuuDot.Size = new System.Drawing.Size(110, 26);
            this.btnLuuDot.Text = "Lưu đợt";
            this.btnLuuDot.UseVisualStyleBackColor = true;
            this.btnLuuDot.TabIndex = 13;
            this.btnLuuDot.Click += new System.EventHandler(this.btnLuuDot_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(14, 186);
            this.label6.Name = "label6";
            this.label6.Text = "Kết quả kiểm tra từng phòng (nhập trực tiếp trên lưới)";
            this.label6.TabIndex = 14;
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.Location = new System.Drawing.Point(14, 204);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQua.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQua.MultiSelect = false;
            this.dgvKetQua.ReadOnly = false;
            this.dgvKetQua.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQua.Size = new System.Drawing.Size(1150, 120);
            this.dgvKetQua.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(14, 340);
            this.label7.Name = "label7";
            this.label7.Text = "Các đợt đã kiểm tra";
            this.label7.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(590, 340);
            this.label8.Name = "label8";
            this.label8.Text = "Chi tiết đợt đang chọn";
            this.label8.TabIndex = 17;
            // 
            // dgvDot
            // 
            this.dgvDot.Location = new System.Drawing.Point(14, 358);
            this.dgvDot.Name = "dgvDot";
            this.dgvDot.AllowUserToAddRows = false;
            this.dgvDot.AllowUserToDeleteRows = false;
            this.dgvDot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDot.BackgroundColor = System.Drawing.Color.White;
            this.dgvDot.MultiSelect = false;
            this.dgvDot.ReadOnly = true;
            this.dgvDot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDot.Size = new System.Drawing.Size(560, 110);
            this.dgvDot.TabIndex = 18;
            this.dgvDot.SelectionChanged += new System.EventHandler(this.dgvDot_SelectionChanged);
            // 
            // dgvCTDot
            // 
            this.dgvCTDot.Location = new System.Drawing.Point(590, 358);
            this.dgvCTDot.Name = "dgvCTDot";
            this.dgvCTDot.AllowUserToAddRows = false;
            this.dgvCTDot.AllowUserToDeleteRows = false;
            this.dgvCTDot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCTDot.BackgroundColor = System.Drawing.Color.White;
            this.dgvCTDot.MultiSelect = false;
            this.dgvCTDot.ReadOnly = true;
            this.dgvCTDot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCTDot.Size = new System.Drawing.Size(574, 110);
            this.dgvCTDot.TabIndex = 19;
            // 
            // grpBox2
            // 
            this.grpBox2.Location = new System.Drawing.Point(14, 486);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(1150, 70);
            this.grpBox2.Text = "Lập biên bản phạt (phòng thay đổi kết cấu)";
            this.grpBox2.TabStop = false;
            this.grpBox2.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 516);
            this.label9.Name = "label9";
            this.label9.Text = "Số BB:";
            this.label9.TabIndex = 21;
            // 
            // txtSoBB
            // 
            this.txtSoBB.Location = new System.Drawing.Point(75, 512);
            this.txtSoBB.Name = "txtSoBB";
            this.txtSoBB.Size = new System.Drawing.Size(90, 22);
            this.txtSoBB.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(185, 516);
            this.label10.Name = "label10";
            this.label10.Text = "Quy định phạt:";
            this.label10.TabIndex = 23;
            // 
            // cboQuyDinh
            // 
            this.cboQuyDinh.Location = new System.Drawing.Point(270, 512);
            this.cboQuyDinh.Name = "cboQuyDinh";
            this.cboQuyDinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuyDinh.FormattingEnabled = true;
            this.cboQuyDinh.Size = new System.Drawing.Size(380, 23);
            this.cboQuyDinh.TabIndex = 24;
            // 
            // btnLapBienBan
            // 
            this.btnLapBienBan.Location = new System.Drawing.Point(670, 510);
            this.btnLapBienBan.Name = "btnLapBienBan";
            this.btnLapBienBan.Size = new System.Drawing.Size(130, 28);
            this.btnLapBienBan.Text = "Lập biên bản phạt";
            this.btnLapBienBan.UseVisualStyleBackColor = true;
            this.btnLapBienBan.TabIndex = 25;
            this.btnLapBienBan.Click += new System.EventHandler(this.btnLapBienBan_Click);
            // 
            // btnNopPhat
            // 
            this.btnNopPhat.Location = new System.Drawing.Point(815, 510);
            this.btnNopPhat.Name = "btnNopPhat";
            this.btnNopPhat.Size = new System.Drawing.Size(120, 28);
            this.btnNopPhat.Text = "Ghi nhận nộp phạt";
            this.btnNopPhat.UseVisualStyleBackColor = true;
            this.btnNopPhat.TabIndex = 26;
            this.btnNopPhat.Click += new System.EventHandler(this.btnNopPhat_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(14, 568);
            this.label11.Name = "label11";
            this.label11.Text = "Biên bản phạt";
            this.label11.TabIndex = 27;
            // 
            // dgvBienBan
            // 
            this.dgvBienBan.Location = new System.Drawing.Point(14, 586);
            this.dgvBienBan.Name = "dgvBienBan";
            this.dgvBienBan.AllowUserToAddRows = false;
            this.dgvBienBan.AllowUserToDeleteRows = false;
            this.dgvBienBan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBienBan.BackgroundColor = System.Drawing.Color.White;
            this.dgvBienBan.MultiSelect = false;
            this.dgvBienBan.ReadOnly = true;
            this.dgvBienBan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBienBan.Size = new System.Drawing.Size(1150, 100);
            this.dgvBienBan.TabIndex = 28;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1064, 700);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 29;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmKiemTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDenHan);
            this.Controls.Add(this.btnThemVaoDot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaDot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtNgayKT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboNV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnLuuDot);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvDot);
            this.Controls.Add(this.dgvCTDot);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSoBB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboQuyDinh);
            this.Controls.Add(this.btnLapBienBan);
            this.Controls.Add(this.btnNopPhat);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dgvBienBan);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.grpBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmKiemTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kiểm tra phòng - Biên bản phạt";
            this.Load += new System.EventHandler(this.FrmKiemTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDenHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBienBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDenHan;
        private System.Windows.Forms.Button btnThemVaoDot;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaDot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtNgayKT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnLuuDot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvDot;
        private System.Windows.Forms.DataGridView dgvCTDot;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSoBB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboQuyDinh;
        private System.Windows.Forms.Button btnLapBienBan;
        private System.Windows.Forms.Button btnNopPhat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvBienBan;
        private System.Windows.Forms.Button btnDong;
    }
}
