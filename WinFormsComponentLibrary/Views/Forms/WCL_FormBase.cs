using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WCL.Win32;

namespace WCL.Views.Forms
{
    /// <summary>
    /// フレームワークの既定のフォームを表します。
    /// </summary>
    public partial class WCL_FormBase : Form
    {
        /// <summary>
        /// Escape キーを押下することで画面を閉じるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("WCL")]
        [DefaultValue(true)]
        public bool CloseByEscapeKey { get; set; } = true;

        /// <summary>
        /// Enter キーを押下することで入力フォーカスを別のコントロールに遷移させるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("WCL")]
        [DefaultValue(true)]
        public bool MoveFocusByEntarKey { get; set; } = true;

        [Category("WCL")]
        [DefaultValue(typeof(Size), "1000, 750")]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        [Category("WCL")]
        [DefaultValue(FormStartPosition.CenterParent)]
        public new FormStartPosition StartPosition
        {
            get => base.StartPosition;
            protected set => base.StartPosition = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool AutoSize
        {
            get => base.AutoSize;
            protected set => base.AutoSize = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoValidate AutoValidate
        {
            get => base.AutoValidate;
            protected set => base.AutoValidate = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoScaleMode AutoScaleMode
        {
            get => base.AutoScaleMode;
            protected set => base.AutoScaleMode = value;
        }

        /// <summary>
        /// 内包するコントロールが入力フォーカスを取得したときに発生します。
        /// </summary>
        [Category("WCL")]
        public event EventHandler ControlEnter;

        /// <summary>
        /// 内包するコントロールが検証を行うときに発生します。
        /// </summary>
        [Category("WCL")]
        public event CancelEventHandler ControlValidating;

        /// <summary>
        /// 内包するコントロールが検証を行った後に発生します。
        /// </summary>
        [Category("WCL")]
        public event EventHandler ControlValidated;

        /// <summary>
        /// 内包するコントロールが入力フォーカスを失ったときに発生します。
        /// </summary>
        [Category("WCL")]
        public event EventHandler ControlLeave;

        /// <summary>
        /// 他のハンドルからウィンドウメッセージを受信したときに発生します。
        /// </summary>
        [Category("WCL")]
        public event Win32EventHandler WindowMessageReceived;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public WCL_FormBase()
        {
            this.DoubleBuffered = true;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = SystemFonts.MenuFont;
        }

        /// <summary>
        /// <see cref="Form.ProcessCmdKey"/> を呼び出します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        public bool RaiseProcessCmdKey(ref Message msg, Keys keyData)
        {
            return this.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// <see cref="ControlEnter"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnControlEnter(EventArgs e)
        {
            this.ControlEnter?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="ControlValidating"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnControlValidating(CancelEventArgs e)
        {
            this.ControlValidating?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="ControlValidated"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnControlValidated(EventArgs e)
        {
            this.ControlValidated?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="ControlLeave"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnControlLeave(EventArgs e)
        {
            this.ControlLeave?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="Control.ControlAdded"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Enter += this.Control_Enter;
            e.Control.Validating += this.Control_Validating;
            e.Control.Validated += this.Control_Validated;
            e.Control.Leave += this.Control_Leave;
            base.OnControlAdded(e);
        }

        /// <summary>
        /// <see cref="Control.ControlRemoved"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            e.Control.Enter -= this.Control_Enter;
            e.Control.Validating -= this.Control_Validating;
            e.Control.Validated -= this.Control_Validated;
            e.Control.Leave -= this.Control_Leave;
            base.OnControlRemoved(e);
        }

        private void Control_Enter(object sender, EventArgs e) =>  this.OnControlEnter(e);
        private void Control_Validating(object sender, CancelEventArgs e) => this.OnControlValidating(e);
        private void Control_Validated(object sender, EventArgs e) => this.OnControlValidated(e);
        private void Control_Leave(object sender, EventArgs e) => this.OnControlLeave(e);

        /// <summary>
        /// <see cref="WindowMessageReceived"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnWindowMessageReceived(Win32EventArgs e)
        {
            this.WindowMessageReceived?.Invoke(this, e);
        }

        /// <summary>
        /// ダイアログキーを処理します。
        /// </summary>
        /// <param name="keyData">キーデータ</param>
        /// <returns>処理されたかどうかを示す値</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool canClose()
            {
                if (this.CloseByEscapeKey == false)
                {
                    return false;
                }
                if ((keyData & Keys.KeyCode) != Keys.Escape ||
                    (keyData & Keys.Shift) == Keys.Shift ||
                    (keyData & Keys.Control) == Keys.Control ||
                    (keyData & Keys.Alt) == Keys.Alt)
                {
                    return false;
                }
                return true;
            }

            bool canMoveFoces()
            {
                if (this.MoveFocusByEntarKey == false)
                {
                    return false;
                }
                if ((keyData & Keys.KeyCode) != Keys.Return ||
                    (keyData & Keys.Alt) == Keys.Alt)
                {
                    return false;
                }
                if (this.ActiveControl is Button)
                {
                    return false;
                }
                if (this.ActiveControl is TextBoxBase textBox)
                {
                    if (textBox.Multiline && (keyData & Keys.Control) == Keys.Control)
                    {
                        return false;
                    }
                }
                return true;
            }

            if (canClose())
            {
                this.Close();
                return true;
            }
            else if (canMoveFoces())
            {
                return this.ProcessTabKey((keyData & Keys.Shift) != Keys.Shift);
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ウィンドウメッセージを処理します。
        /// </summary>
        /// <param name="m">メッセージ</param>
        protected override void WndProc(ref Message m)
        {
            switch ((WindowMessages)m.Msg)
            {
                case WindowMessages.WM_CLOSE:
                    this.AutoValidate = AutoValidate.Disable;
                    base.WndProc(ref m);
                    this.AutoValidate = AutoValidate.EnablePreventFocusChange;
                    return;
                case WindowMessages.WM_COPYDATA:
                    this.OnWindowMessageReceived(new Win32EventArgs(m));
                    base.WndProc(ref m);
                    return;
                default:
                    base.WndProc(ref m);
                    return;
            }
        }
    }
}
