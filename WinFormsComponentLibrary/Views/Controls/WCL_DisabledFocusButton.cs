using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// フォーカスを受け取らないボタンコントロールを表します。
    /// </summary>
    public class WCL_DisabledFocusButton : WCL_Button
    {
        public WCL_DisabledFocusButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
