using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    public partial class WCL_FunctionButtonPanel : UserControl
    {
        #region メンバ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Font Font
        {
            get => base.Font;
            protected set => base.Font = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            get => base.Size;
            protected set => base.Size = value;
        }

        #endregion

        #region メソッド

        public WCL_FunctionButtonPanel()
        {
            this.InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            var parent = this.FindForm();
            if (parent != null)
            {
                parent.Load += (sender, e) => this.AdjustSize(parent);
                parent.Resize += (sender, e) => this.AdjustSize(parent);
            }
        }

        public void AdjustSize(Control owner)
        {
            const int MARGIN_OF_PANELS = 10;
            const int COUNT_OF_MARGINS = 4;
            const int COUNT_OF_PANELS = 3;
            const int COUNT_OF_BUTTONS_PER_PANEL = 4;
            var buttonWidth = (owner.Width - MARGIN_OF_PANELS * COUNT_OF_MARGINS) / (COUNT_OF_PANELS * COUNT_OF_BUTTONS_PER_PANEL);

            this.SuspendLayout();
            try
            {
                this.Width = owner.Width;

                this.F1.Width = buttonWidth;
                this.F2.Width = buttonWidth;
                this.F3.Width = buttonWidth;
                this.F4.Width = buttonWidth;
                this.F5.Width = buttonWidth;
                this.F6.Width = buttonWidth;
                this.F7.Width = buttonWidth;
                this.F8.Width = buttonWidth;
                this.F9.Width = buttonWidth;
                this.F10.Width = buttonWidth;
                this.F11.Width = buttonWidth;
                this.F12.Width = buttonWidth;

                this.leftPanel.Width = buttonWidth * COUNT_OF_BUTTONS_PER_PANEL;
                this.centerPanel.Width = buttonWidth * COUNT_OF_BUTTONS_PER_PANEL;
                this.rightPanel.Width = buttonWidth * COUNT_OF_BUTTONS_PER_PANEL;

                this.F2.Left = this.F1.Right;
                this.F3.Left = this.F2.Right;
                this.F4.Left = this.F3.Right;
                this.F6.Left = this.F5.Right;
                this.F7.Left = this.F6.Right;
                this.F8.Left = this.F7.Right;
                this.F10.Left = this.F9.Right;
                this.F11.Left = this.F10.Right;
                this.F12.Left = this.F11.Right;

                this.centerPanel.Left = this.leftPanel.Right + MARGIN_OF_PANELS;
                this.rightPanel.Left = this.centerPanel.Right + MARGIN_OF_PANELS;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        #endregion

        #region イベント

        #endregion
    }
}
