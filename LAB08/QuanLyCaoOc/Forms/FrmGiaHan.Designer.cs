namespace QuanLyCaoOc.Forms
{
    partial class FrmGiaHan
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
            this.dtNgayLap = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.lblToiDa = new System.Windows.Forms.Label();
            this.btnKiemTraNo = new System.Windows.Forms.Button();
            this.lblNoThue = new System.Windows.Forms.Label();
            this.lblNoDV = new System.Windows.Forms.Label();
            this.lblKetLuan = new System.Windows.Forms.Label();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoPhieu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numSoThangGH = new System.Windows.Forms.NumericUpDown();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvPhieu = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSoThangGH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Text = "Hợp đồng:";
            this.label1.TabIndex = 1;
            // 
            // cboHopDong
            // 
            this.cboHopDong.Location = new System.Drawing.Point(80, 10);
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
            this.label2.Location = new System.Drawing.Point(230, 14);
            this.label2.Name = "label2";
            this.label2.Text = "NV lập:";
            this.label2.TabIndex = 3;
            // 
            // cboNV
            // 
            this.cboNV.Location = new System.Drawing.Point(280, 10);
            this.cboNV.Name = "cboNV";
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Size = new System.Drawing.Size(170, 23);
            this.cboNV.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 14);
            this.label3.Name = "label3";
            this.label3.Text = "Ngày lập:";
            this.label3.TabIndex = 5;
            // 
            // dtNgayLap
            // 
            this.dtNgayLap.Location = new System.Drawing.Point(530, 10);
            this.dtNgayLap.Name = "dtNgayLap";
            this.dtNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgayLap.Size = new System.Drawing.Size(130, 23);
            this.dtNgayLap.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(14, 46);
            this.label4.Name = "label4";
            this.label4.Text = "Các phòng của hợp đồng";
            this.label4.TabIndex = 7;
            // 
            // dgvPhong
            // 
            this.dgvPhong.Location = new System.Drawing.Point(14, 64);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhong.MultiSelect = false;
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(1030, 100);
            this.dgvPhong.TabIndex = 8;
            this.dgvPhong.SelectionChanged += new System.EventHandler(this.dgvPhong_SelectionChanged);
            // 
            // grpBox1
            // 
            this.grpBox1.Location = new System.Drawing.Point(14, 190);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(500, 160);
            this.grpBox1.Text = "Điều kiện gia hạn";
            this.grpBox1.TabStop = false;
            this.grpBox1.TabIndex = 9;
            // 
            // lblToiDa
            // 
            this.lblToiDa.AutoSize = true;
            this.lblToiDa.Location = new System.Drawing.Point(28, 214);
            this.lblToiDa.Name = "lblToiDa";
            this.lblToiDa.Text = "Gia hạn tối đa: ≤ 1/2 thời gian thuê";
            this.lblToiDa.TabIndex = 10;
            // 
            // btnKiemTraNo
            // 
            this.btnKiemTraNo.Location = new System.Drawing.Point(28, 240);
            this.btnKiemTraNo.Name = "btnKiemTraNo";
            this.btnKiemTraNo.Size = new System.Drawing.Size(160, 28);
            this.btnKiemTraNo.Text = "Kiểm tra công nợ";
            this.btnKiemTraNo.UseVisualStyleBackColor = true;
            this.btnKiemTraNo.TabIndex = 11;
            this.btnKiemTraNo.Click += new System.EventHandler(this.btnKiemTraNo_Click);
            // 
            // lblNoThue
            // 
            this.lblNoThue.AutoSize = true;
            this.lblNoThue.Location = new System.Drawing.Point(28, 280);
            this.lblNoThue.Name = "lblNoThue";
            this.lblNoThue.Text = "Nợ tiền thuê đến hạn:";
            this.lblNoThue.TabIndex = 12;
            // 
            // lblNoDV
            // 
            this.lblNoDV.AutoSize = true;
            this.lblNoDV.Location = new System.Drawing.Point(28, 302);
            this.lblNoDV.Name = "lblNoDV";
            this.lblNoDV.Text = "Nợ dịch vụ:";
            this.lblNoDV.TabIndex = 13;
            // 
            // lblKetLuan
            // 
            this.lblKetLuan.AutoSize = true;
            this.lblKetLuan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKetLuan.Location = new System.Drawing.Point(28, 324);
            this.lblKetLuan.Name = "lblKetLuan";
            this.lblKetLuan.Text = "Kết luận";
            this.lblKetLuan.TabIndex = 14;
            // 
            // grpBox2
            // 
            this.grpBox2.Location = new System.Drawing.Point(540, 190);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(504, 160);
            this.grpBox2.Text = "Phiếu gia hạn";
            this.grpBox2.TabStop = false;
            this.grpBox2.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 216);
            this.label5.Name = "label5";
            this.label5.Text = "Số phiếu:";
            this.label5.TabIndex = 16;
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.Location = new System.Drawing.Point(620, 212);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(110, 22);
            this.txtSoPhieu.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 250);
            this.label6.Name = "label6";
            this.label6.Text = "Số tháng gia hạn:";
            this.label6.TabIndex = 18;
            // 
            // numSoThangGH
            // 
            this.numSoThangGH.Location = new System.Drawing.Point(660, 246);
            this.numSoThangGH.Name = "numSoThangGH";
            this.numSoThangGH.Size = new System.Drawing.Size(60, 23);
            this.numSoThangGH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSoThangGH.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numSoThangGH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoThangGH.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoThangGH.TabIndex = 19;
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.Location = new System.Drawing.Point(554, 290);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(140, 30);
            this.btnLapPhieu.Text = "Lập phiếu gia hạn";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.TabIndex = 20;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(14, 366);
            this.label7.Name = "label7";
            this.label7.Text = "Lịch sử phiếu gia hạn";
            this.label7.TabIndex = 21;
            // 
            // dgvPhieu
            // 
            this.dgvPhieu.Location = new System.Drawing.Point(14, 384);
            this.dgvPhieu.Name = "dgvPhieu";
            this.dgvPhieu.AllowUserToAddRows = false;
            this.dgvPhieu.AllowUserToDeleteRows = false;
            this.dgvPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieu.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhieu.MultiSelect = false;
            this.dgvPhieu.ReadOnly = true;
            this.dgvPhieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieu.Size = new System.Drawing.Size(1030, 150);
            this.dgvPhieu.TabIndex = 22;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(944, 548);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 23;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmGiaHan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboHopDong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboNV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtNgayLap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.lblToiDa);
            this.Controls.Add(this.btnKiemTraNo);
            this.Controls.Add(this.lblNoThue);
            this.Controls.Add(this.lblNoDV);
            this.Controls.Add(this.lblKetLuan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSoPhieu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numSoThangGH);
            this.Controls.Add(this.btnLapPhieu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvPhieu);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.grpBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmGiaHan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập phiếu gia hạn thuê văn phòng";
            this.Load += new System.EventHandler(this.FrmGiaHan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoThangGH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHopDong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtNgayLap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label lblToiDa;
        private System.Windows.Forms.Button btnKiemTraNo;
        private System.Windows.Forms.Label lblNoThue;
        private System.Windows.Forms.Label lblNoDV;
        private System.Windows.Forms.Label lblKetLuan;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoPhieu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numSoThangGH;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvPhieu;
        private System.Windows.Forms.Button btnDong;
    }
}
