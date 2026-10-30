namespace QuanLyCaoOc.Forms
{
    partial class FrmThongKe
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
            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.btnTK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvThang = new System.Windows.Forms.DataGridView();
            this.dgvTinhTrang = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvCongNo = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongNo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Text = "Từ ngày:";
            this.label1.TabIndex = 1;
            // 
            // dtTu
            // 
            this.dtTu.Location = new System.Drawing.Point(70, 10);
            this.dtTu.Name = "dtTu";
            this.dtTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTu.Size = new System.Drawing.Size(130, 23);
            this.dtTu.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 14);
            this.label2.Name = "label2";
            this.label2.Text = "Đến ngày:";
            this.label2.TabIndex = 3;
            // 
            // dtDen
            // 
            this.dtDen.Location = new System.Drawing.Point(285, 10);
            this.dtDen.Name = "dtDen";
            this.dtDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDen.Size = new System.Drawing.Size(130, 23);
            this.dtDen.TabIndex = 4;
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(440, 8);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(110, 28);
            this.btnTK.Text = "Thống kê";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.TabIndex = 5;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(14, 48);
            this.label3.Name = "label3";
            this.label3.Text = "Tổng hợp";
            this.label3.TabIndex = 6;
            // 
            // dgvTongHop
            // 
            this.dgvTongHop.Location = new System.Drawing.Point(14, 66);
            this.dgvTongHop.Name = "dgvTongHop";
            this.dgvTongHop.AllowUserToAddRows = false;
            this.dgvTongHop.AllowUserToDeleteRows = false;
            this.dgvTongHop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTongHop.BackgroundColor = System.Drawing.Color.White;
            this.dgvTongHop.MultiSelect = false;
            this.dgvTongHop.ReadOnly = true;
            this.dgvTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTongHop.Size = new System.Drawing.Size(1070, 60);
            this.dgvTongHop.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(14, 138);
            this.label4.Name = "label4";
            this.label4.Text = "Doanh thu theo tháng";
            this.label4.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(560, 138);
            this.label5.Name = "label5";
            this.label5.Text = "Tình trạng phòng theo tầng";
            this.label5.TabIndex = 9;
            // 
            // dgvThang
            // 
            this.dgvThang.Location = new System.Drawing.Point(14, 156);
            this.dgvThang.Name = "dgvThang";
            this.dgvThang.AllowUserToAddRows = false;
            this.dgvThang.AllowUserToDeleteRows = false;
            this.dgvThang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThang.BackgroundColor = System.Drawing.Color.White;
            this.dgvThang.MultiSelect = false;
            this.dgvThang.ReadOnly = true;
            this.dgvThang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThang.Size = new System.Drawing.Size(530, 170);
            this.dgvThang.TabIndex = 10;
            // 
            // dgvTinhTrang
            // 
            this.dgvTinhTrang.Location = new System.Drawing.Point(560, 156);
            this.dgvTinhTrang.Name = "dgvTinhTrang";
            this.dgvTinhTrang.AllowUserToAddRows = false;
            this.dgvTinhTrang.AllowUserToDeleteRows = false;
            this.dgvTinhTrang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTinhTrang.BackgroundColor = System.Drawing.Color.White;
            this.dgvTinhTrang.MultiSelect = false;
            this.dgvTinhTrang.ReadOnly = true;
            this.dgvTinhTrang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTinhTrang.Size = new System.Drawing.Size(524, 170);
            this.dgvTinhTrang.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(14, 340);
            this.label6.Name = "label6";
            this.label6.Text = "Công nợ tiền thuê (kỳ đã đến hạn, chưa thu đủ)";
            this.label6.TabIndex = 12;
            // 
            // dgvCongNo
            // 
            this.dgvCongNo.Location = new System.Drawing.Point(14, 358);
            this.dgvCongNo.Name = "dgvCongNo";
            this.dgvCongNo.AllowUserToAddRows = false;
            this.dgvCongNo.AllowUserToDeleteRows = false;
            this.dgvCongNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCongNo.BackgroundColor = System.Drawing.Color.White;
            this.dgvCongNo.MultiSelect = false;
            this.dgvCongNo.ReadOnly = true;
            this.dgvCongNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCongNo.Size = new System.Drawing.Size(1070, 260);
            this.dgvCongNo.TabIndex = 13;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(984, 650);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 14;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtTu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtDen);
            this.Controls.Add(this.btnTK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvTongHop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvThang);
            this.Controls.Add(this.dgvTinhTrang);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvCongNo);
            this.Controls.Add(this.btnDong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê cho thuê văn phòng";
            this.Load += new System.EventHandler(this.FrmThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDen;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvTongHop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvThang;
        private System.Windows.Forms.DataGridView dgvTinhTrang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvCongNo;
        private System.Windows.Forms.Button btnDong;
    }
}
