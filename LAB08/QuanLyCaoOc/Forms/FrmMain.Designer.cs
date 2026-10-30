namespace QuanLyCaoOc.Forms
{
    partial class FrmMain
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnTangPhong = new System.Windows.Forms.Button();
            this.btnHopDong = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.btnNhanVienCT = new System.Windows.Forms.Button();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(250, 18);
            this.label1.Name = "label1";
            this.label1.Text = "QUẢN LÝ CHO THUÊ VĂN PHÒNG CAO ỐC";
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(335, 52);
            this.label2.Name = "label2";
            this.label2.Text = "Nguyễn Sĩ Quỳnh - 1250080157";
            this.label2.TabIndex = 2;
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Location = new System.Drawing.Point(40, 110);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDanhMuc.Size = new System.Drawing.Size(190, 90);
            this.btnDanhMuc.Text = "Danh mục";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.TabIndex = 3;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            // 
            // btnTangPhong
            // 
            this.btnTangPhong.Location = new System.Drawing.Point(245, 110);
            this.btnTangPhong.Name = "btnTangPhong";
            this.btnTangPhong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTangPhong.Size = new System.Drawing.Size(190, 90);
            this.btnTangPhong.Text = "Tầng - Phòng";
            this.btnTangPhong.UseVisualStyleBackColor = true;
            this.btnTangPhong.TabIndex = 4;
            this.btnTangPhong.Click += new System.EventHandler(this.btnTangPhong_Click);
            // 
            // btnHopDong
            // 
            this.btnHopDong.Location = new System.Drawing.Point(450, 110);
            this.btnHopDong.Name = "btnHopDong";
            this.btnHopDong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHopDong.Size = new System.Drawing.Size(190, 90);
            this.btnHopDong.Text = "Hợp đồng thuê";
            this.btnHopDong.UseVisualStyleBackColor = true;
            this.btnHopDong.TabIndex = 5;
            this.btnHopDong.Click += new System.EventHandler(this.btnHopDong_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(655, 110);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThanhToan.Size = new System.Drawing.Size(190, 90);
            this.btnThanhToan.Text = "Thu tiền - Dịch vụ";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.TabIndex = 6;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.Location = new System.Drawing.Point(40, 230);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnGiaHan.Size = new System.Drawing.Size(190, 90);
            this.btnGiaHan.Text = "Gia hạn hợp đồng";
            this.btnGiaHan.UseVisualStyleBackColor = true;
            this.btnGiaHan.TabIndex = 7;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // btnNhanVienCT
            // 
            this.btnNhanVienCT.Location = new System.Drawing.Point(245, 230);
            this.btnNhanVienCT.Name = "btnNhanVienCT";
            this.btnNhanVienCT.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnNhanVienCT.Size = new System.Drawing.Size(190, 90);
            this.btnNhanVienCT.Text = "Nhân viên công ty - Thẻ xe";
            this.btnNhanVienCT.UseVisualStyleBackColor = true;
            this.btnNhanVienCT.TabIndex = 8;
            this.btnNhanVienCT.Click += new System.EventHandler(this.btnNhanVienCT_Click);
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.Location = new System.Drawing.Point(450, 230);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnKiemTra.Size = new System.Drawing.Size(190, 90);
            this.btnKiemTra.Text = "Kiểm tra phòng - Phạt";
            this.btnKiemTra.UseVisualStyleBackColor = true;
            this.btnKiemTra.TabIndex = 9;
            this.btnKiemTra.Click += new System.EventHandler(this.btnKiemTra_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(655, 230);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThongKe.Size = new System.Drawing.Size(190, 90);
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.TabIndex = 10;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(710, 365);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Size = new System.Drawing.Size(150, 36);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 380);
            this.label3.Name = "label3";
            this.label3.Text = "Hợp đồng hết hạn được tự động cập nhật khi mở chương trình.";
            this.label3.TabIndex = 12;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 470);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnTangPhong);
            this.Controls.Add(this.btnHopDong);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnGiaHan);
            this.Controls.Add(this.btnNhanVienCT);
            this.Controls.Add(this.btnKiemTra);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý cho thuê văn phòng cao ốc";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnTangPhong;
        private System.Windows.Forms.Button btnHopDong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Button btnNhanVienCT;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label label3;
    }
}
