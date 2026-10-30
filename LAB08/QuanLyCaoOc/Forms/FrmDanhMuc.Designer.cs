namespace QuanLyCaoOc.Forms
{
    partial class FrmDanhMuc
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
            this.txtNVMa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNVTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNVVaiTro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNVSDT = new System.Windows.Forms.TextBox();
            this.btnThemNV = new System.Windows.Forms.Button();
            this.dgvNV = new System.Windows.Forms.DataGridView();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDVMa = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDVDonVi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDVTen = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numDVGia = new System.Windows.Forms.NumericUpDown();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.btnCapNhatGiaDV = new System.Windows.Forms.Button();
            this.dgvDV = new System.Windows.Forms.DataGridView();
            this.grpBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQDMa = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtQDNoiDung = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numQDMuc = new System.Windows.Forms.NumericUpDown();
            this.btnThemQD = new System.Windows.Forms.Button();
            this.dgvQD = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQDMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBox1
            // 
            this.grpBox1.Location = new System.Drawing.Point(12, 18);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(350, 530);
            this.grpBox1.Text = "Nhân viên cao ốc";
            this.grpBox1.TabStop = false;
            this.grpBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Text = "Mã NV:";
            this.label1.TabIndex = 2;
            // 
            // txtNVMa
            // 
            this.txtNVMa.Location = new System.Drawing.Point(90, 36);
            this.txtNVMa.Name = "txtNVMa";
            this.txtNVMa.Size = new System.Drawing.Size(90, 22);
            this.txtNVMa.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 70);
            this.label2.Name = "label2";
            this.label2.Text = "Họ tên:";
            this.label2.TabIndex = 4;
            // 
            // txtNVTen
            // 
            this.txtNVTen.Location = new System.Drawing.Point(90, 66);
            this.txtNVTen.Name = "txtNVTen";
            this.txtNVTen.Size = new System.Drawing.Size(250, 22);
            this.txtNVTen.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 100);
            this.label3.Name = "label3";
            this.label3.Text = "Vai trò:";
            this.label3.TabIndex = 6;
            // 
            // cboNVVaiTro
            // 
            this.cboNVVaiTro.Location = new System.Drawing.Point(90, 96);
            this.cboNVVaiTro.Name = "cboNVVaiTro";
            this.cboNVVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNVVaiTro.FormattingEnabled = true;
            this.cboNVVaiTro.Size = new System.Drawing.Size(150, 23);
            this.cboNVVaiTro.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 130);
            this.label4.Name = "label4";
            this.label4.Text = "SĐT:";
            this.label4.TabIndex = 8;
            // 
            // txtNVSDT
            // 
            this.txtNVSDT.Location = new System.Drawing.Point(90, 126);
            this.txtNVSDT.Name = "txtNVSDT";
            this.txtNVSDT.Size = new System.Drawing.Size(150, 22);
            this.txtNVSDT.TabIndex = 9;
            // 
            // btnThemNV
            // 
            this.btnThemNV.Location = new System.Drawing.Point(250, 124);
            this.btnThemNV.Name = "btnThemNV";
            this.btnThemNV.Size = new System.Drawing.Size(90, 26);
            this.btnThemNV.Text = "Thêm NV";
            this.btnThemNV.UseVisualStyleBackColor = true;
            this.btnThemNV.TabIndex = 10;
            this.btnThemNV.Click += new System.EventHandler(this.btnThemNV_Click);
            // 
            // dgvNV
            // 
            this.dgvNV.Location = new System.Drawing.Point(24, 165);
            this.dgvNV.Name = "dgvNV";
            this.dgvNV.AllowUserToAddRows = false;
            this.dgvNV.AllowUserToDeleteRows = false;
            this.dgvNV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNV.BackgroundColor = System.Drawing.Color.White;
            this.dgvNV.MultiSelect = false;
            this.dgvNV.ReadOnly = true;
            this.dgvNV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNV.Size = new System.Drawing.Size(326, 370);
            this.dgvNV.TabIndex = 11;
            // 
            // grpBox2
            // 
            this.grpBox2.Location = new System.Drawing.Point(376, 18);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(370, 530);
            this.grpBox2.Text = "Dịch vụ";
            this.grpBox2.TabStop = false;
            this.grpBox2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(388, 40);
            this.label5.Name = "label5";
            this.label5.Text = "Mã DV:";
            this.label5.TabIndex = 13;
            // 
            // txtDVMa
            // 
            this.txtDVMa.Location = new System.Drawing.Point(470, 36);
            this.txtDVMa.Name = "txtDVMa";
            this.txtDVMa.Size = new System.Drawing.Size(80, 22);
            this.txtDVMa.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(565, 40);
            this.label6.Name = "label6";
            this.label6.Text = "Đơn vị:";
            this.label6.TabIndex = 15;
            // 
            // txtDVDonVi
            // 
            this.txtDVDonVi.Location = new System.Drawing.Point(615, 36);
            this.txtDVDonVi.Name = "txtDVDonVi";
            this.txtDVDonVi.Size = new System.Drawing.Size(110, 22);
            this.txtDVDonVi.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(388, 70);
            this.label7.Name = "label7";
            this.label7.Text = "Tên DV:";
            this.label7.TabIndex = 17;
            // 
            // txtDVTen
            // 
            this.txtDVTen.Location = new System.Drawing.Point(470, 66);
            this.txtDVTen.Name = "txtDVTen";
            this.txtDVTen.Size = new System.Drawing.Size(255, 22);
            this.txtDVTen.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(388, 100);
            this.label8.Name = "label8";
            this.label8.Text = "Đơn giá:";
            this.label8.TabIndex = 19;
            // 
            // numDVGia
            // 
            this.numDVGia.Location = new System.Drawing.Point(470, 96);
            this.numDVGia.Name = "numDVGia";
            this.numDVGia.Size = new System.Drawing.Size(130, 23);
            this.numDVGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDVGia.ThousandsSeparator = true;
            this.numDVGia.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            this.numDVGia.TabIndex = 20;
            // 
            // btnThemDV
            // 
            this.btnThemDV.Location = new System.Drawing.Point(388, 130);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(100, 26);
            this.btnThemDV.Text = "Thêm DV";
            this.btnThemDV.UseVisualStyleBackColor = true;
            this.btnThemDV.TabIndex = 21;
            this.btnThemDV.Click += new System.EventHandler(this.btnThemDV_Click);
            // 
            // btnCapNhatGiaDV
            // 
            this.btnCapNhatGiaDV.Location = new System.Drawing.Point(498, 130);
            this.btnCapNhatGiaDV.Name = "btnCapNhatGiaDV";
            this.btnCapNhatGiaDV.Size = new System.Drawing.Size(140, 26);
            this.btnCapNhatGiaDV.Text = "Cập nhật đơn giá";
            this.btnCapNhatGiaDV.UseVisualStyleBackColor = true;
            this.btnCapNhatGiaDV.TabIndex = 22;
            this.btnCapNhatGiaDV.Click += new System.EventHandler(this.btnCapNhatGiaDV_Click);
            // 
            // dgvDV
            // 
            this.dgvDV.Location = new System.Drawing.Point(388, 165);
            this.dgvDV.Name = "dgvDV";
            this.dgvDV.AllowUserToAddRows = false;
            this.dgvDV.AllowUserToDeleteRows = false;
            this.dgvDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDV.BackgroundColor = System.Drawing.Color.White;
            this.dgvDV.MultiSelect = false;
            this.dgvDV.ReadOnly = true;
            this.dgvDV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDV.Size = new System.Drawing.Size(346, 370);
            this.dgvDV.TabIndex = 23;
            this.dgvDV.SelectionChanged += new System.EventHandler(this.dgvDV_SelectionChanged);
            // 
            // grpBox3
            // 
            this.grpBox3.Location = new System.Drawing.Point(760, 18);
            this.grpBox3.Name = "grpBox3";
            this.grpBox3.Size = new System.Drawing.Size(350, 530);
            this.grpBox3.Text = "Quy định phạt";
            this.grpBox3.TabStop = false;
            this.grpBox3.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(772, 40);
            this.label9.Name = "label9";
            this.label9.Text = "Mã QĐ:";
            this.label9.TabIndex = 25;
            // 
            // txtQDMa
            // 
            this.txtQDMa.Location = new System.Drawing.Point(840, 36);
            this.txtQDMa.Name = "txtQDMa";
            this.txtQDMa.Size = new System.Drawing.Size(80, 22);
            this.txtQDMa.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(772, 70);
            this.label10.Name = "label10";
            this.label10.Text = "Nội dung:";
            this.label10.TabIndex = 27;
            // 
            // txtQDNoiDung
            // 
            this.txtQDNoiDung.Location = new System.Drawing.Point(840, 66);
            this.txtQDNoiDung.Name = "txtQDNoiDung";
            this.txtQDNoiDung.Size = new System.Drawing.Size(255, 22);
            this.txtQDNoiDung.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(772, 100);
            this.label11.Name = "label11";
            this.label11.Text = "Mức phạt:";
            this.label11.TabIndex = 29;
            // 
            // numQDMuc
            // 
            this.numQDMuc.Location = new System.Drawing.Point(840, 96);
            this.numQDMuc.Name = "numQDMuc";
            this.numQDMuc.Size = new System.Drawing.Size(130, 23);
            this.numQDMuc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQDMuc.ThousandsSeparator = true;
            this.numQDMuc.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            this.numQDMuc.TabIndex = 30;
            // 
            // btnThemQD
            // 
            this.btnThemQD.Location = new System.Drawing.Point(772, 130);
            this.btnThemQD.Name = "btnThemQD";
            this.btnThemQD.Size = new System.Drawing.Size(100, 26);
            this.btnThemQD.Text = "Thêm QĐ";
            this.btnThemQD.UseVisualStyleBackColor = true;
            this.btnThemQD.TabIndex = 31;
            this.btnThemQD.Click += new System.EventHandler(this.btnThemQD_Click);
            // 
            // dgvQD
            // 
            this.dgvQD.Location = new System.Drawing.Point(772, 165);
            this.dgvQD.Name = "dgvQD";
            this.dgvQD.AllowUserToAddRows = false;
            this.dgvQD.AllowUserToDeleteRows = false;
            this.dgvQD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQD.BackgroundColor = System.Drawing.Color.White;
            this.dgvQD.MultiSelect = false;
            this.dgvQD.ReadOnly = true;
            this.dgvQD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQD.Size = new System.Drawing.Size(326, 370);
            this.dgvQD.TabIndex = 32;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(1000, 560);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 33;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNVMa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNVTen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboNVVaiTro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNVSDT);
            this.Controls.Add(this.btnThemNV);
            this.Controls.Add(this.dgvNV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDVMa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDVDonVi);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDVTen);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numDVGia);
            this.Controls.Add(this.btnThemDV);
            this.Controls.Add(this.btnCapNhatGiaDV);
            this.Controls.Add(this.dgvDV);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtQDMa);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtQDNoiDung);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numQDMuc);
            this.Controls.Add(this.btnThemQD);
            this.Controls.Add(this.dgvQD);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.grpBox2);
            this.Controls.Add(this.grpBox3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh mục: nhân viên - dịch vụ - quy định phạt";
            this.Load += new System.EventHandler(this.FrmDanhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQDMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNVMa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNVTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboNVVaiTro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNVSDT;
        private System.Windows.Forms.Button btnThemNV;
        private System.Windows.Forms.DataGridView dgvNV;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDVMa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDVDonVi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDVTen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numDVGia;
        private System.Windows.Forms.Button btnThemDV;
        private System.Windows.Forms.Button btnCapNhatGiaDV;
        private System.Windows.Forms.DataGridView dgvDV;
        private System.Windows.Forms.GroupBox grpBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtQDMa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtQDNoiDung;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numQDMuc;
        private System.Windows.Forms.Button btnThemQD;
        private System.Windows.Forms.DataGridView dgvQD;
        private System.Windows.Forms.Button btnDong;
    }
}
