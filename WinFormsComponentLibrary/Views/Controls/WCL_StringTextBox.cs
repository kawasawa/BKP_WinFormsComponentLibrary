using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using WCL.Common;
using WCL.Win32;

namespace WCL.Views.Controls
{
    /// <summary>
    /// 文字列を編集できるテキストボックスコントロールを表します。
    /// </summary>
    public class WCL_StringTextBox : WCL_TextBoxBase, IStringTextControl
    {
        #region メンバ

        private StringTextEditRoleKind _role;

        [Category("WCL")]
        [DefaultValue("")]
        public override string Text
        {
            get => base.Text;
            set
            {
                value = value ?? string.Empty;
                if (base.Text?.Equals(value) != true)
                {
                    if (this.DesignMode)
                    {
                        return;
                    }
                    if (this.ValidateText(value) == false)
                    {
                        var message = new StringBuilder();
                        message.AppendLine("許容できない文字列が指定されています。");
                        message.AppendLine($"  Type: {this.GetType().Name}");
                        message.AppendLine($"  Name: {this.Name}");
                        message.AppendLine($"  Role: {this.Role.ToString()}");
                        message.AppendLine($"  Text: {value}");
                        DebugHelper.Assert(message.ToString());
                        return;
                    }
                    base.Text = value;
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(StringTextEditRoleKind.Normal)]
        public StringTextEditRoleKind Role
        {
            get => this._role;
            set
            {
                if (this._role != value)
                {
                    this._role = value;
                    this.ImeMode = this.GetImeMode();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsValid => this.ValidateText(this.Text);

        #endregion

        #region メソッド

        public WCL_StringTextBox()
        {
            this.RefreshBackColor();
            this.ImeMode = ImeMode.NoControl;
        }

        #endregion

        #region イベント

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (ModifierKeys == Keys.Control || 
                ModifierKeys == Keys.Alt ||
                e.KeyChar == ControlChars.Back)
            {
                return;
            }

            var tmp = this.Text
                .Remove(this.SelectionStart, this.SelectionLength)
                .Insert(this.SelectionStart, e.KeyChar.ToString());
            if (this.ValidateText(tmp))
            {
                return;
            }
            e.Handled = true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WindowMessages.WM_PASTE:
                    var data = Clipboard.GetDataObject();
                    if (data?.GetDataPresent(DataFormats.Text) != true ||
                        this.ValidateText((string)data.GetData(DataFormats.Text)) == false)
                    {
                        return;
                    }
                    base.WndProc(ref m);
                    return;
                default:
                    base.WndProc(ref m);
                    return;
            }
        }

        #endregion
    }
}
