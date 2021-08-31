using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// テキストボックスコントロールの基底クラスを表します。
    /// </summary>
    public abstract class WCL_TextBoxBase : TextBox,
        IDesignControl,
        ITextControl,
        IPermitReadOnlyControl,
        IValidateValueControl,
        IValidateMandatoryControl,
        IDetectChangesControl
    {
        #region メンバ

        private bool _isMandatory = false;

        [Category("WCL")]
        [DefaultValue("")]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text?.Equals(value) != true)
                {
                    base.Text = value;
                    this.RefreshBackColor();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(false)]
        public new bool ReadOnly
        {
            get => base.ReadOnly;
            set
            {
                if (base.ReadOnly != value)
                {
                    base.ReadOnly = value;
                    this.RefreshBackColor();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(false)]
        public bool IsMandatory
        {
            get => this._isMandatory;
            set
            {
                if (this._isMandatory != value)
                {
                    this._isMandatory = value;
                    this.RefreshBackColor();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(true)]
        public bool SelectAllOnEnter { get; set; } = true;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEmpty => string.IsNullOrEmpty(this.Text);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsValid => true;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object PreviousValue { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PreviousValidationCancelled { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ImeMode ImeMode
        {
            get => base.ImeMode;
            protected set => base.ImeMode = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor
        {
            get => base.BackColor;
            protected set => base.BackColor = value;
        }

        [Category("WCL")]
        public event CancelEventHandler ValidatingOnValueChanged;

        #endregion

        #region メソッド

        public WCL_TextBoxBase()
        {
            this.RefreshBackColor();
            this.AutoSize = false;
        }

        public virtual object GetValue()
        {
            return this.Text;
        }

        public virtual void SetValue(object value)
        {
            this.Text = value is string str ? str : value.ToString();
        }

        public virtual void ClearValue()
        {
            this.Text = string.Empty;
        }

        public void UpdatePreviousValue()
        {
            this.PreviousValue = this.Text;
        }

        public override void Refresh()
        {
            base.Refresh();
            this.RefreshBackColor();
        }

        public virtual void RefreshBackColor()
        {
            // 以下の順で指定する
            // - 読み取り専用の状態
            // - フォーカスを取得した状態
            // - 必須項目でかつ未入力の状態
            // - アイドル状態

            if (this.ReadOnly)
            {
                this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.ReadOnly];
                return;
            }
            if (this.Focused)
            {
                this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.Focused];
                return;
            }
            if (this.IsMandatory && this.IsEmpty)
            {
                this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.Mandatory];
                return;
            }
            this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.Idle];
        }

        protected override void Select(bool directed, bool forward)
        {
            if (this.Focus() == false)
            {
                return;
            }

            base.Select(directed, forward);
            if (this.SelectAllOnEnter)
            {
                this.SelectAll();
            }
            else
            {
                this.SelectionStart = this.Text.Length;
                this.SelectionLength = 0;
            }
        }

        public virtual void ShowConstraintMessageForValue()
        {
            Utils.ShowMessage(this.FindForm(), MessageKind.Error, Dependencies.Views.MessageBox.Texts[MessageDetailKind.Error_ValidateForText]);
        }

        public virtual void ShowConstraintMessageForMandatory()
        {
            Utils.ShowMessage(this.FindForm(), MessageKind.Error, Dependencies.Views.MessageBox.Texts[MessageDetailKind.Error_MandatoryForText]);
        }

        #endregion

        #region イベント

        protected override void OnGotFocus(EventArgs e)
        {
            this.RefreshBackColor();
            base.OnGotFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (this.PreviousValidationCancelled == false)
            {
                this.UpdatePreviousValue();
            }

            this.Select();
            base.OnEnter(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            try
            {
                if (this.IsEmpty == false && this.IsValid == false)
                {
                    this.ShowConstraintMessageForValue();
                    e.Cancel = true;
                    return;
                }

                base.OnValidating(e);
                if (e.Cancel)
                {
                    return;
                }

                if ((this.PreviousValue != null || this.Text != null) &&
                    this.PreviousValue?.Equals(this.Text) == false)
                {
                    this.OnValidatingOnValueChanged(e);
                }
            }
            finally
            {
                this.PreviousValidationCancelled = e.Cancel;
            }
        }

        protected virtual void OnValidatingOnValueChanged(CancelEventArgs e)
        {
            this.ValidatingOnValueChanged?.Invoke(this, e);
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            this.RefreshBackColor();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.RefreshBackColor();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool canClear()
            {
                // 以下の場合は対象外とする
                // - 単独の ESC キー以外が押された
                // - 選択範囲が存在しない
                
                if ((keyData & Keys.KeyCode) != Keys.Escape ||
                    (keyData & Keys.Shift) == Keys.Shift ||
                    (keyData & Keys.Control) == Keys.Control ||
                    (keyData & Keys.Alt) == Keys.Alt)
                {
                    return false;
                }

                if (this.SelectionLength == 0)
                {
                    return false;
                }

                return true;
            }

            if (canClear())
            {
                this.SelectionLength = 0;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
