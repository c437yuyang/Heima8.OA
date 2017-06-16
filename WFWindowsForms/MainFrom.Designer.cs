namespace WFWindowsForms
{
    partial class MainFrom
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_continue = new System.Windows.Forms.Button();
            this.tb_bookMark = new System.Windows.Forms.TextBox();
            this.tb_money = new System.Windows.Forms.TextBox();
            this.btn_startStateMachine = new System.Windows.Forms.Button();
            this.tb_persistence_guid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(29, 44);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_continue
            // 
            this.btn_continue.Location = new System.Drawing.Point(29, 82);
            this.btn_continue.Name = "btn_continue";
            this.btn_continue.Size = new System.Drawing.Size(75, 23);
            this.btn_continue.TabIndex = 0;
            this.btn_continue.Text = "继续";
            this.btn_continue.UseVisualStyleBackColor = true;
            this.btn_continue.Click += new System.EventHandler(this.btn_continue_Click);
            // 
            // tb_bookMark
            // 
            this.tb_bookMark.Location = new System.Drawing.Point(169, 45);
            this.tb_bookMark.Name = "tb_bookMark";
            this.tb_bookMark.Size = new System.Drawing.Size(100, 21);
            this.tb_bookMark.TabIndex = 1;
            // 
            // tb_money
            // 
            this.tb_money.Location = new System.Drawing.Point(169, 82);
            this.tb_money.Name = "tb_money";
            this.tb_money.Size = new System.Drawing.Size(100, 21);
            this.tb_money.TabIndex = 1;
            // 
            // btn_startStateMachine
            // 
            this.btn_startStateMachine.Location = new System.Drawing.Point(29, 127);
            this.btn_startStateMachine.Name = "btn_startStateMachine";
            this.btn_startStateMachine.Size = new System.Drawing.Size(75, 23);
            this.btn_startStateMachine.TabIndex = 2;
            this.btn_startStateMachine.Text = "启动状态机工作流";
            this.btn_startStateMachine.UseVisualStyleBackColor = true;
            this.btn_startStateMachine.Click += new System.EventHandler(this.btn_startStateMachine_Click);
            // 
            // tb_persistence_guid
            // 
            this.tb_persistence_guid.Location = new System.Drawing.Point(169, 127);
            this.tb_persistence_guid.Name = "tb_persistence_guid";
            this.tb_persistence_guid.Size = new System.Drawing.Size(100, 21);
            this.tb_persistence_guid.TabIndex = 3;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 284);
            this.Controls.Add(this.tb_persistence_guid);
            this.Controls.Add(this.btn_startStateMachine);
            this.Controls.Add(this.tb_money);
            this.Controls.Add(this.tb_bookMark);
            this.Controls.Add(this.btn_continue);
            this.Controls.Add(this.btn_start);
            this.Name = "MainFrom";
            this.Text = "MainFrom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_continue;
        private System.Windows.Forms.TextBox tb_bookMark;
        private System.Windows.Forms.TextBox tb_money;
        private System.Windows.Forms.Button btn_startStateMachine;
        private System.Windows.Forms.TextBox tb_persistence_guid;
    }
}