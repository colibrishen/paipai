namespace PaiPai
{
    partial class Password
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
            this.TxtLogin = new System.Windows.Forms.TextBox();
            this.ButSign = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtLogin
            // 
            this.TxtLogin.Location = new System.Drawing.Point(13, 13);
            this.TxtLogin.Name = "TxtLogin";
            this.TxtLogin.PasswordChar = '*';
            this.TxtLogin.Size = new System.Drawing.Size(199, 21);
            this.TxtLogin.TabIndex = 0;
            this.TxtLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButSign_KeyDown);
            // 
            // ButSign
            // 
            this.ButSign.Location = new System.Drawing.Point(218, 12);
            this.ButSign.Name = "ButSign";
            this.ButSign.Size = new System.Drawing.Size(75, 23);
            this.ButSign.TabIndex = 1;
            this.ButSign.Text = "Sign In";
            this.ButSign.UseVisualStyleBackColor = true;
            this.ButSign.Click += new System.EventHandler(this.ButSign_Click);
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 47);
            this.Controls.Add(this.ButSign);
            this.Controls.Add(this.TxtLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Password";
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtLogin;
        private System.Windows.Forms.Button ButSign;
    }
}