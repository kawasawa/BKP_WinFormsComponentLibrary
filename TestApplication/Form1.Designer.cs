namespace TestApplication
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.wcL_TextEditBox1 = new WCL.Views.Controls.WCL_StringTextBox();
            this.wcL_FormatTextBox1 = new WCL.Views.Controls.WCL_FormatTextBox();
            this.wcL_Label1 = new WCL.Views.Controls.WCL_Label();
            this.SuspendLayout();
            // 
            // wcL_TextEditBox1
            // 
            this.wcL_TextEditBox1.Location = new System.Drawing.Point(23, 13);
            this.wcL_TextEditBox1.Name = "wcL_TextEditBox1";
            this.wcL_TextEditBox1.Size = new System.Drawing.Size(216, 158);
            this.wcL_TextEditBox1.TabIndex = 0;
            // 
            // wcL_FormatTextBox1
            // 
            this.wcL_FormatTextBox1.Location = new System.Drawing.Point(23, 188);
            this.wcL_FormatTextBox1.Name = "wcL_FormatTextBox1";
            this.wcL_FormatTextBox1.Role = WCL.Views.Controls.FormatTextEditRoleKind.DateTime;
            this.wcL_FormatTextBox1.Size = new System.Drawing.Size(100, 22);
            this.wcL_FormatTextBox1.TabIndex = 1;
            // 
            // wcL_Label1
            // 
            this.wcL_Label1.AutoSize = true;
            this.wcL_Label1.For = "wcL_TextEditBox1";
            this.wcL_Label1.Location = new System.Drawing.Point(146, 188);
            this.wcL_Label1.Name = "wcL_Label1";
            this.wcL_Label1.Size = new System.Drawing.Size(79, 15);
            this.wcL_Label1.TabIndex = 2;
            this.wcL_Label1.Text = "wcL_Label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.wcL_Label1);
            this.Controls.Add(this.wcL_FormatTextBox1);
            this.Controls.Add(this.wcL_TextEditBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WCL.Views.Controls.WCL_StringTextBox wcL_TextEditBox1;
        private WCL.Views.Controls.WCL_FormatTextBox wcL_FormatTextBox1;
        private WCL.Views.Controls.WCL_Label wcL_Label1;
    }
}

