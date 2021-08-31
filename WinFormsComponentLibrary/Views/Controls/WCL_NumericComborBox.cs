using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WCL.Common;
using WCL.Win32;

namespace WCL.Views.Controls
{
    /// <summary>
    /// 項目の選択に加え、数値の編集も可能なコンボボックスコントロールを表します。
    /// </summary>
    public class WCL_NumericComborBox : WCL_ComboBoxBase, INunericTextControl
    {
        #region メンバ

        private int _maxLengthOfIntegerDigits = 9;
        private int _maxLengthOfDecimalDigits = 0;

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
                        message.AppendLine($"  Text: {value}");
                        DebugHelper.Assert(message.ToString());
                        return;
                    }
                    base.Text = this.Focused ? this.FormatTextOnFocus(value) : this.FormatTextOnIdle(value);
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(9)]
        public int MaxLengthOfIntegerDigits
        {
            get => this._maxLengthOfIntegerDigits;
            set
            {
                if (this._maxLengthOfIntegerDigits != value)
                {
                    this._maxLengthOfIntegerDigits = value;
                    this.UpdateMaxLength();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(0)]
        public int MaxLengthOfDecimalDigits
        {
            get => this._maxLengthOfDecimalDigits;
            set
            {
                if (this._maxLengthOfDecimalDigits != value)
                {
                    this._maxLengthOfDecimalDigits = value;
                    this.UpdateMaxLength();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(false)]
        public bool CommaSeparate { get; set; } = false;

        [Category("WCL")]
        [DefaultValue(false)]
        public bool AllowMinus { get; set; } = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDecimal => 0 < this.MaxLengthOfDecimalDigits;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UserInputText => this.GetUserInputText();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsValid => this.ValidateText(this.UserInputText);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete(Consts.PROPERTY_CAN_NOT_SET)]
        public new int MaxLength
        {
            get => base.MaxLength;
            set
            {
                var message = new StringBuilder();
                message.AppendLine(Consts.PROPERTY_CAN_NOT_SET);
                message.AppendLine($"  Type: {this.GetType().Name}");
                message.AppendLine($"  Name: {this.Name}");
                message.AppendLine($"  Prop: {nameof(this.MaxLength)}");
                message.AppendLine($"  Text: {value}");
                DebugHelper.Assert(message.ToString());
            }
        }

        #endregion

        #region メソッド

        public WCL_NumericComborBox()
        {
            this.RefreshBackColor();
            this.ImeMode = ImeMode.Disable;
        }

        protected virtual void UpdateMaxLength()
        {
            this.MaxLength = this.MaxLengthOfIntegerDigits + this.MaxLengthOfDecimalDigits + (this.AllowDecimal ? 1 : 0);
        }

        public override void ShowConstraintMessageForValue()
        {
            var message = new StringBuilder();
            message.AppendLine(Dependencies.Views.MessageBox.Texts[MessageDetailKind.Error_ValidateForText]);
            message.AppendLine($" - 整数部: {this.MaxLengthOfIntegerDigits} 桁まで");
            message.AppendLine($" - 小数部: {this.MaxLengthOfDecimalDigits} 桁まで");
            Utils.ShowMessage(this.FindForm(), MessageKind.Error, message.ToString());
        }

        #endregion

        #region イベント

        protected override void OnGotFocus(EventArgs e)
        {
            this.FormatTextOnFocus();
            base.OnGotFocus(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (ModifierKeys == Keys.Control ||
                ModifierKeys == Keys.Alt ||
                e.KeyChar == ControlChars.Back)
            {
                return;
            }

            if (this.ValidateKey(e.KeyChar))
            {
                var tmp = this.Text
                    .Remove(this.SelectionStart, this.SelectionLength)
                    .Insert(this.SelectionStart, e.KeyChar.ToString());
                if (this.ValidateText(tmp))
                {
                    return;
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// ウィンドウハンドルが生成されたときに行う処理を定義します。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            var pcbi = new COMBOBOXINFO();
            pcbi.Size = Marshal.SizeOf(pcbi);
            if (User32.GetComboBoxInfo(this.Handle, ref pcbi) == false ||
                pcbi.EditBoxHandle.Equals(IntPtr.Zero))
            {
                return;
            }

            var editBoxWindow = new EditBoxWindow(this);
            editBoxWindow.AssignHandle(pcbi.EditBoxHandle);
        }

        #endregion

        /// <summary>
        /// ComboBox が内包する EditBox のハンドルへのアクセスを提供します。
        /// </summary>
        private class EditBoxWindow : NativeWindow
        {
            private WCL_NumericComborBox _comboBox;

            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            /// <param name="comboBox">コンボボックスのインスタンス</param>
            public EditBoxWindow(WCL_NumericComborBox comboBox)
            {
                this._comboBox = comboBox;
                this._comboBox.HandleDestroyed += this.Parent_HandleDestroyed;
            }

            /// <summary>
            /// ハンドルが破棄されるときに行う処理を定義します。
            /// </summary>
            /// <param name="sender">イベントの発生源</param>
            /// <param name="e">イベントの情報</param>
            private void Parent_HandleDestroyed(object sender, EventArgs e)
            {
                this.ReleaseHandle();
            }

            /// <summary>
            /// ウィンドウメッセージを処理します。
            /// </summary>
            /// <param name="m">メッセージ</param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)WindowMessages.WM_PASTE:
                        var data = Clipboard.GetDataObject();
                        if (data?.GetDataPresent(DataFormats.Text) != true)
                        {
                            return;
                        }
                        var pasteText = this._comboBox.FormatTextOnFocus((string)data.GetData(DataFormats.Text));
                        var tmp = this._comboBox.Text
                            .Remove(this._comboBox.SelectionStart, this._comboBox.SelectionLength)
                            .Insert(this._comboBox.SelectionStart, pasteText);
                        if (this._comboBox.ValidateText(tmp) == false)
                        {
                            return;
                        }
                        this._comboBox.Text = tmp;
                        return;

                    default:
                        base.WndProc(ref m);
                        return;
                }
            }
        }
    }
}
