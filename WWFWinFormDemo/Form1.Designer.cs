namespace WWFWinFormDemo
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.tb_bookMarkName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_money = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动工作流";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_bookMarkName
            // 
            this.tb_bookMarkName.Location = new System.Drawing.Point(130, 24);
            this.tb_bookMarkName.Name = "tb_bookMarkName";
            this.tb_bookMarkName.Size = new System.Drawing.Size(100, 21);
            this.tb_bookMarkName.TabIndex = 1;
            this.tb_bookMarkName.Text = "tb_bookMarkName";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "继续工作流";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tb_money
            // 
            this.tb_money.Location = new System.Drawing.Point(130, 67);
            this.tb_money.Name = "tb_money";
            this.tb_money.Size = new System.Drawing.Size(100, 21);
            this.tb_money.TabIndex = 3;
            this.tb_money.Text = "textBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tb_money);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_bookMarkName);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_bookMarkName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_money;
    }
}

