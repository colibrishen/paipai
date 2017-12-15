namespace PaiPai
{
    partial class PaiPai
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ButSetPosition = new System.Windows.Forms.Button();
            this.AddPriceTimer = new System.Windows.Forms.Timer(this.components);
            this.AddPriceTextTimer = new System.Windows.Forms.Timer(this.components);
            this.ButAddPrice = new System.Windows.Forms.Button();
            this.ButStart = new System.Windows.Forms.Button();
            this.WebNowTime = new System.Windows.Forms.Label();
            this.ButStop = new System.Windows.Forms.Button();
            this.TxtOptInfor = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LocalNowTime = new System.Windows.Forms.Label();
            this.TxtTimeCalibration = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtWebTimeCal = new System.Windows.Forms.TextBox();
            this.TxtLocalTimeCal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RadButLocal = new System.Windows.Forms.RadioButton();
            this.RadButWeb = new System.Windows.Forms.RadioButton();
            this.LabRunStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButSetPosition
            // 
            this.ButSetPosition.Location = new System.Drawing.Point(19, 87);
            this.ButSetPosition.Name = "ButSetPosition";
            this.ButSetPosition.Size = new System.Drawing.Size(100, 25);
            this.ButSetPosition.TabIndex = 0;
            this.ButSetPosition.Text = "SetPosition";
            this.ButSetPosition.UseVisualStyleBackColor = true;
            this.ButSetPosition.Click += new System.EventHandler(this.ButSetPosition_Click);
            // 
            // AddPriceTimer
            // 
            this.AddPriceTimer.Tick += new System.EventHandler(this.AddPriceTimer_Tick);
            // 
            // AddPriceTextTimer
            // 
            this.AddPriceTextTimer.Tick += new System.EventHandler(this.AddPriceTextTimer_Tick);
            // 
            // ButAddPrice
            // 
            this.ButAddPrice.Location = new System.Drawing.Point(173, 87);
            this.ButAddPrice.Name = "ButAddPrice";
            this.ButAddPrice.Size = new System.Drawing.Size(100, 25);
            this.ButAddPrice.TabIndex = 5;
            this.ButAddPrice.Text = "Add Price";
            this.ButAddPrice.UseVisualStyleBackColor = true;
            this.ButAddPrice.Click += new System.EventHandler(this.ButAddPrice_Click);
            // 
            // ButStart
            // 
            this.ButStart.Location = new System.Drawing.Point(327, 87);
            this.ButStart.Name = "ButStart";
            this.ButStart.Size = new System.Drawing.Size(100, 25);
            this.ButStart.TabIndex = 6;
            this.ButStart.Text = "Start";
            this.ButStart.UseVisualStyleBackColor = true;
            this.ButStart.Click += new System.EventHandler(this.ButStart_Click);
            // 
            // WebNowTime
            // 
            this.WebNowTime.AutoSize = true;
            this.WebNowTime.Location = new System.Drawing.Point(109, 23);
            this.WebNowTime.Name = "WebNowTime";
            this.WebNowTime.Size = new System.Drawing.Size(65, 12);
            this.WebNowTime.TabIndex = 8;
            this.WebNowTime.Text = "WebNowTime";
            // 
            // ButStop
            // 
            this.ButStop.Location = new System.Drawing.Point(481, 87);
            this.ButStop.Name = "ButStop";
            this.ButStop.Size = new System.Drawing.Size(100, 25);
            this.ButStop.TabIndex = 9;
            this.ButStop.Text = "Stop";
            this.ButStop.UseVisualStyleBackColor = true;
            this.ButStop.Click += new System.EventHandler(this.ButStop_Click);
            // 
            // TxtOptInfor
            // 
            this.TxtOptInfor.Location = new System.Drawing.Point(6, 20);
            this.TxtOptInfor.Multiline = true;
            this.TxtOptInfor.Name = "TxtOptInfor";
            this.TxtOptInfor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtOptInfor.Size = new System.Drawing.Size(562, 228);
            this.TxtOptInfor.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtOptInfor);
            this.groupBox1.Location = new System.Drawing.Point(13, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 254);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // LocalNowTime
            // 
            this.LocalNowTime.AutoSize = true;
            this.LocalNowTime.Location = new System.Drawing.Point(109, 49);
            this.LocalNowTime.Name = "LocalNowTime";
            this.LocalNowTime.Size = new System.Drawing.Size(77, 12);
            this.LocalNowTime.TabIndex = 13;
            this.LocalNowTime.Text = "LoaclNowTime";
            // 
            // TxtTimeCalibration
            // 
            this.TxtTimeCalibration.AutoSize = true;
            this.TxtTimeCalibration.Location = new System.Drawing.Point(257, 23);
            this.TxtTimeCalibration.Name = "TxtTimeCalibration";
            this.TxtTimeCalibration.Size = new System.Drawing.Size(89, 12);
            this.TxtTimeCalibration.TabIndex = 14;
            this.TxtTimeCalibration.Text = "网络时间校准：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "本地时间校准：";
            // 
            // TxtWebTimeCal
            // 
            this.TxtWebTimeCal.Location = new System.Drawing.Point(351, 19);
            this.TxtWebTimeCal.Name = "TxtWebTimeCal";
            this.TxtWebTimeCal.Size = new System.Drawing.Size(80, 21);
            this.TxtWebTimeCal.TabIndex = 16;
            this.TxtWebTimeCal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OlayNum_KeyPress);
            // 
            // TxtLocalTimeCal
            // 
            this.TxtLocalTimeCal.Location = new System.Drawing.Point(352, 45);
            this.TxtLocalTimeCal.Name = "TxtLocalTimeCal";
            this.TxtLocalTimeCal.Size = new System.Drawing.Size(80, 21);
            this.TxtLocalTimeCal.TabIndex = 17;
            this.TxtLocalTimeCal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OlayNum_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "毫秒";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(438, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "毫秒";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RadButLocal);
            this.groupBox2.Controls.Add(this.RadButWeb);
            this.groupBox2.Controls.Add(this.TxtTimeCalibration);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.WebNowTime);
            this.groupBox2.Controls.Add(this.TxtLocalTimeCal);
            this.groupBox2.Controls.Add(this.TxtWebTimeCal);
            this.groupBox2.Controls.Add(this.LocalNowTime);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 73);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择时间";
            // 
            // RadButLocal
            // 
            this.RadButLocal.AutoSize = true;
            this.RadButLocal.Location = new System.Drawing.Point(6, 47);
            this.RadButLocal.Name = "RadButLocal";
            this.RadButLocal.Size = new System.Drawing.Size(89, 16);
            this.RadButLocal.TabIndex = 22;
            this.RadButLocal.TabStop = true;
            this.RadButLocal.Text = "RadButLocal";
            this.RadButLocal.UseVisualStyleBackColor = true;
            // 
            // RadButWeb
            // 
            this.RadButWeb.AutoSize = true;
            this.RadButWeb.Location = new System.Drawing.Point(6, 21);
            this.RadButWeb.Name = "RadButWeb";
            this.RadButWeb.Size = new System.Drawing.Size(77, 16);
            this.RadButWeb.TabIndex = 21;
            this.RadButWeb.TabStop = true;
            this.RadButWeb.Text = "RadButWeb";
            this.RadButWeb.UseVisualStyleBackColor = true;
            // 
            // LabRunStatus
            // 
            this.LabRunStatus.AutoSize = true;
            this.LabRunStatus.Location = new System.Drawing.Point(17, 132);
            this.LabRunStatus.Name = "LabRunStatus";
            this.LabRunStatus.Size = new System.Drawing.Size(77, 12);
            this.LabRunStatus.TabIndex = 21;
            this.LabRunStatus.Text = "LabRunStatus";
            // 
            // PaiPai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 452);
            this.Controls.Add(this.LabRunStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButStop);
            this.Controls.Add(this.ButStart);
            this.Controls.Add(this.ButAddPrice);
            this.Controls.Add(this.ButSetPosition);
            this.Name = "PaiPai";
            this.Text = "拍牌软件";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Close_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButSetPosition;
        private System.Windows.Forms.Timer AddPriceTimer;
        private System.Windows.Forms.Timer AddPriceTextTimer;
        private System.Windows.Forms.Button ButAddPrice;
        private System.Windows.Forms.Button ButStart;
        private System.Windows.Forms.Label WebNowTime;
        private System.Windows.Forms.Button ButStop;
        private System.Windows.Forms.TextBox TxtOptInfor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LocalNowTime;
        private System.Windows.Forms.Label TxtTimeCalibration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtWebTimeCal;
        private System.Windows.Forms.TextBox TxtLocalTimeCal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RadButLocal;
        private System.Windows.Forms.RadioButton RadButWeb;
        private System.Windows.Forms.Label LabRunStatus;
    }
}

