using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// 複数の <see cref="WCL_RadioButton"/> クラスをまとめるためのパネルコントロールを表します。
    /// </summary>
    public class WCL_RadioButtonGroup : Panel,
        IEditControl,
        IPermitEmptyControl,
        IPermitReadOnlyControl
    {
        #region メンバ

        private bool _readOnly = false;

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
                    this.FindForm().GetControls()
                        .Where(c => c is IPermitReadOnlyControl)
                        .Cast<IPermitReadOnlyControl>()
                        .ForEach(r => r.ReadOnly = true);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEmpty => this.FindForm().GetControls()
            .Where(c => c is WCL_RadioButton radio && radio.Checked)
            .Count() <= 0;

        #endregion

        #region メソッド

        public virtual object GetValue()
        {
            return (this.FindForm().GetControls()
                .Where(c => c is WCL_RadioButton radio && radio.Checked)
                .FirstOrDefault() as WCL_RadioButton)?.ValueAsInteger;
        }

        public virtual object GetValue(int index)
        {
            return this.FindForm().GetControls()
                .Where(c => c is WCL_RadioButton)
                .Cast<WCL_RadioButton>()
                .ToList()[index].ValueAsInteger;
        }

        public virtual void SetValue(object value)
        {
            var tmp = false;
            var intValue = Convert.ToInt32(value);
            var controls = this.FindForm().GetControls()
                .Where(c => c is WCL_RadioButton)
                .Cast<WCL_RadioButton>()
                .ToList();
            for (var i = 0; i < controls.Count; i++)
            {
                if (tmp == false && controls[i].ValueAsInteger == intValue)
                {
                    controls[i].Checked = true;
                    tmp = true;
                }
                else
                {
                    controls[i].Checked = false;
                }
            }
        }

        public virtual void ClearValue()
        {
            this.ClearValue(true);
        }

        public virtual void ClearValue(bool selectFirstButton)
        {
            var controls = this.FindForm().GetControls()
                .Where(c => c is WCL_RadioButton)
                .Cast<WCL_RadioButton>()
                .ToList();
            for (var i = 0; i < controls.Count; i++)
            {
                if (i == 0 && selectFirstButton)
                {
                    controls[i].Checked = true;
                }
                else
                {
                    controls[i].Checked = false;
                }
            }
        }

        public virtual void ClearValue(int defaultValue)
        {
            var controls = this.FindForm().GetControls()
                .Where(c => c is WCL_RadioButton)
                .Cast<WCL_RadioButton>()
                .ToList();
            for (var i = 0; i < controls.Count; i++)
            {
                if (controls[i].ValueAsInteger == defaultValue)
                {
                    controls[i].Checked = true;
                }
                else
                {
                    controls[i].Checked = false;
                }
            }
        }

        #endregion
    }
}
