namespace WCL.Views.Forms
{
    partial class WCL_BasicForm
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
            this.TitleLabel = new WCL.Views.Controls.WCL_Label();
            this.MessageLabel = new WCL.Views.Controls.WCL_Label();
            this.FunctionButtonPanel = new WCL.Views.Controls.WCL_FunctionButtonPanel();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.TitleLabel.Size = new System.Drawing.Size(982, 40);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessageLabel
            // 
            this.MessageLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MessageLabel.Location = new System.Drawing.Point(0, 683);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.MessageLabel.Size = new System.Drawing.Size(982, 20);
            this.MessageLabel.TabIndex = 1;
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FunctionButtonPanel
            // 
            this.FunctionButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.FunctionButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FunctionButtonPanel.Location = new System.Drawing.Point(0, 40);
            this.FunctionButtonPanel.Name = "FunctionButtonPanel";
            this.FunctionButtonPanel.TabIndex = 2;
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(0, 80);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(982, 603);
            this.ContentPanel.TabIndex = 3;
            // 
            // WCL_BasicForm
            // 
            this.ClientSize = new System.Drawing.Size(982, 703);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.FunctionButtonPanel);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.TitleLabel);
            this.Name = "WCL_BasicForm";
            this.Text = "Title";
            this.ResumeLayout(false);

        }

        #endregion

        protected Controls.WCL_FunctionButtonPanel FunctionButtonPanel;
        protected System.Windows.Forms.Panel ContentPanel;
        protected Controls.WCL_Label TitleLabel;
        protected Controls.WCL_Label MessageLabel;
    }
}