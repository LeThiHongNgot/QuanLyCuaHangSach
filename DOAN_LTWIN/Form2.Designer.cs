namespace DOAN_LTWIN
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoTK = new System.Windows.Forms.RadioButton();
            this.rdoS = new System.Windows.Forms.RadioButton();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.btnthem = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.txtxn = new System.Windows.Forms.TextBox();
            this.btnDN = new System.Windows.Forms.Button();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.txtMK = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblTK = new System.Windows.Forms.Label();
            this.lblxn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(717, 730);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(547, 39);
            this.button1.TabIndex = 15;
            this.button1.Text = "Đăng nhập";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(477, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 87);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(406, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 40);
            this.label3.TabIndex = 17;
            this.label3.Text = "ĐĂNG NHẬP";
            // 
            // rdoTK
            // 
            this.rdoTK.AutoSize = true;
            this.rdoTK.Location = new System.Drawing.Point(909, 12);
            this.rdoTK.Name = "rdoTK";
            this.rdoTK.Size = new System.Drawing.Size(141, 20);
            this.rdoTK.TabIndex = 28;
            this.rdoTK.TabStop = true;
            this.rdoTK.Text = "Quản Lý Tài Khoản";
            this.rdoTK.UseVisualStyleBackColor = true;
            // 
            // rdoS
            // 
            this.rdoS.AutoSize = true;
            this.rdoS.Location = new System.Drawing.Point(12, 12);
            this.rdoS.Name = "rdoS";
            this.rdoS.Size = new System.Drawing.Size(174, 20);
            this.rdoS.TabIndex = 29;
            this.rdoS.TabStop = true;
            this.rdoS.Text = "Quản Lý Cửa Hàng Sách";
            this.rdoS.UseVisualStyleBackColor = true;
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.btnQuayLai.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(930, 559);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(131, 43);
            this.btnQuayLai.TabIndex = 24;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Visible = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click_1);
            // 
            // btnthem
            // 
            this.btnthem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.btnthem.Enabled = false;
            this.btnthem.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnthem.ForeColor = System.Drawing.Color.White;
            this.btnthem.Location = new System.Drawing.Point(249, 531);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(235, 43);
            this.btnthem.TabIndex = 25;
            this.btnthem.Text = "Thêm";
            this.btnthem.UseVisualStyleBackColor = false;
            this.btnthem.Visible = false;
            this.btnthem.Click += new System.EventHandler(this.btnthem_Click_1);
            // 
            // btnxoa
            // 
            this.btnxoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.btnxoa.Enabled = false;
            this.btnxoa.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnxoa.ForeColor = System.Drawing.Color.White;
            this.btnxoa.Location = new System.Drawing.Point(556, 531);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(240, 43);
            this.btnxoa.TabIndex = 26;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = false;
            this.btnxoa.Visible = false;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click_1);
            // 
            // txtxn
            // 
            this.txtxn.BackColor = System.Drawing.Color.Azure;
            this.txtxn.Location = new System.Drawing.Point(249, 421);
            this.txtxn.Multiline = true;
            this.txtxn.Name = "txtxn";
            this.txtxn.PasswordChar = '*';
            this.txtxn.Size = new System.Drawing.Size(547, 43);
            this.txtxn.TabIndex = 22;
            this.txtxn.Visible = false;
            // 
            // btnDN
            // 
            this.btnDN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.btnDN.FlatAppearance.BorderSize = 0;
            this.btnDN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDN.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDN.ForeColor = System.Drawing.Color.White;
            this.btnDN.Location = new System.Drawing.Point(249, 480);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(547, 45);
            this.btnDN.TabIndex = 34;
            this.btnDN.Text = "Đăng nhập";
            this.btnDN.UseVisualStyleBackColor = false;
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click);
            // 
            // txtTK
            // 
            this.txtTK.BackColor = System.Drawing.Color.Azure;
            this.txtTK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTK.Location = new System.Drawing.Point(249, 256);
            this.txtTK.Multiline = true;
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(547, 39);
            this.txtTK.TabIndex = 32;
            // 
            // txtMK
            // 
            this.txtMK.BackColor = System.Drawing.Color.Azure;
            this.txtMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMK.Location = new System.Drawing.Point(249, 338);
            this.txtMK.Multiline = true;
            this.txtMK.Name = "txtMK";
            this.txtMK.PasswordChar = '*';
            this.txtMK.Size = new System.Drawing.Size(547, 40);
            this.txtMK.TabIndex = 33;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblPass.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblPass.Location = new System.Drawing.Point(231, 312);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(94, 23);
            this.lblPass.TabIndex = 30;
            this.lblPass.Text = "Mật Khẩu";
            // 
            // lblTK
            // 
            this.lblTK.AutoSize = true;
            this.lblTK.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTK.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblTK.Location = new System.Drawing.Point(231, 224);
            this.lblTK.Name = "lblTK";
            this.lblTK.Size = new System.Drawing.Size(140, 23);
            this.lblTK.TabIndex = 31;
            this.lblTK.Text = "Tên đăng nhập";
            // 
            // lblxn
            // 
            this.lblxn.AutoSize = true;
            this.lblxn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblxn.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblxn.Location = new System.Drawing.Point(231, 395);
            this.lblxn.Name = "lblxn";
            this.lblxn.Size = new System.Drawing.Size(177, 23);
            this.lblxn.TabIndex = 30;
            this.lblxn.Text = "Xác nhận mật khẩu";
            this.lblxn.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1073, 614);
            this.Controls.Add(this.btnDN);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.lblxn);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblTK);
            this.Controls.Add(this.rdoTK);
            this.Controls.Add(this.rdoS);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnthem);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.txtxn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Form2";
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdoTK;
        private System.Windows.Forms.RadioButton rdoS;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button btnthem;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.TextBox txtxn;
        private System.Windows.Forms.Button btnDN;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblTK;
        private System.Windows.Forms.Label lblxn;
    }
}