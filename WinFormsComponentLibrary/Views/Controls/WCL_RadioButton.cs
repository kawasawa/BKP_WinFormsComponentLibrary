using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// ラジオボタンコントロールを表します。
    /// </summary>
    public class WCL_RadioButton : RadioButton,
        IDesignControl,
        IPermitReadOnlyControl
    {
        #region メンバ

        private bool _readOnly = false;

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
        [DefaultValue(Consts.DEFAULT_VALUE_RADIO_BUTTON_VALUE)]
        public int ValueAsInteger { get; set; } = Consts.DEFAULT_VALUE_RADIO_BUTTON_VALUE;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor
        {
            get => base.BackColor;
            protected set => base.BackColor = value;
        }

        #endregion

        #region メソッド

        public WCL_RadioButton()
        {
            this.RefreshBackColor();
            this.AutoSize = true;
        }

        public void RefreshBackColor()
        {
            if (this.ReadOnly)
            {
                this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.ReadOnly];
                return;
            }
            this.BackColor = Dependencies.Views.Controls.Colors[ControlStatusKind.Idle];
        }

        #endregion

        #region イベント

        protected override void OnGotFocus(EventArgs e)
        {
            this.RefreshBackColor();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.RefreshBackColor();
        }

        #endregion
    }
}
