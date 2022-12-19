
namespace j11record
{
    partial class Form1
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
            this.button_start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_sendcmd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(636, 32);
            this.button_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(124, 32);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "连接";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(636, 101);
            this.Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(124, 40);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "断开";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 138);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(601, 76);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "点“连接”会连到rtt client,如果运行异常，可以检查程序管理器";
            // 
            // button_sendcmd
            // 
            this.button_sendcmd.Location = new System.Drawing.Point(636, 168);
            this.button_sendcmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sendcmd.Name = "button_sendcmd";
            this.button_sendcmd.Size = new System.Drawing.Size(124, 38);
            this.button_sendcmd.TabIndex = 4;
            this.button_sendcmd.Text = "Send Command";
            this.button_sendcmd.UseVisualStyleBackColor = true;
            this.button_sendcmd.Visible = false;
            this.button_sendcmd.Click += new System.EventHandler(this.button_sendcmd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 248);
            this.Controls.Add(this.button_sendcmd);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.button_start);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "J11 Dump";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_sendcmd;
    }
}

