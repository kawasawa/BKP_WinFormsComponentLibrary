using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// ラベルコントロールを表します。
    /// </summary>
    public class WCL_Label : Label, IWCLControl
    {
        #region メンバ

        [Category("WCL")]
        [DefaultValue("")]
        public string For { get; set; } = string.Empty;

        #endregion

        #region メソッド

        #endregion

        #region イベント

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (string.IsNullOrEmpty(this.For) == false)
            {
                this.FindForm().Controls
                    .Find(this.For, true)
                    .Where(c => c is Control)
                    .FirstOrDefault()
                    ?.Select();
            }
        }

        #endregion
    }
}
