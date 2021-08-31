using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// チェックボックスコントロールを表します。
    /// </summary>
    public class WCL_CheckBox : CheckBox,
        IDesignControl,
        IEditControl,
        IPermitReadOnlyControl,
        IValidateMandatoryControl,
        IDetectChangesControl
    {
        #region メンバ

        private bool _readOnly = false;
        private bool _isMandatory = false;

        [Category("WCL")]
        [DefaultValue("")]
        public new virtual bool Checked
        {
            get => base.Checked;
            set
            {
                if (base.Checked != value)
                {
                    base.Checked = value;
                    this.RefreshBackColor();
                }
            }
        }

        [Category("WCL")]
        [DefaultValue(false)]
        public new bool ReadOnly
        {
            get => this._readOnly;
            set
            {
                if (this._readOnly != value)
                {
                    this._readOnly = value;
                    this.Enabled = !value;
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
        [DefaultValue(Consts.DEFAULT_VALUE_CHECK_BOX_VALUE)]
        public int ValueAsInteger { get; set; } = Consts.DEFAULT_VALUE_CHECK_BOX_VALUE;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEmpty => string.IsNullOrEmpty(this.Text);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object PreviousValue { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PreviousValidationCancelled { get; private set; }

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

        public WCL_CheckBox()
        {
            this.RefreshBackColor();
            this.AutoSize = true;
        }

        public void SetValue(object value)
        {
            this.Checked = Convert.ToBoolean(value);
        }

        public object GetValue()
        {
            return this.Checked;
        }

        public void ClearValue()
        {
            this.Checked = false;
            this.RefreshBackColor();
        }

        public void UpdatePreviousValue()
        {
            this.PreviousValue = this.Checked;
        }

        public override void Refresh()
        {
            base.Refresh();
            this.RefreshBackColor();
        }

        public virtual void RefreshBackColor()
        {
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

        public virtual void ShowConstraintMessageForMandatory()
        {
            Utils.ShowMessage(this.FindForm(), MessageKind.Error, Dependencies.Views.MessageBox.Texts[MessageDetailKind.Error_MandatoryForCheck]);
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
                if (this.IsEmpty == false)
                {
                    e.Cancel = true;
                    return;
                }

                base.OnValidating(e);
                if (e.Cancel)
                {
                    return;
                }

                if (this.PreviousValue?.Equals(this.Checked) == false)
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

        #endregion
    }
}
