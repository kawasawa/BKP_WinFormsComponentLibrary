namespace WCL.Views.Controls
{
    /// <summary>
    /// 描画に関連するコントロールの処理を提供します。
    /// </summary>
    public interface IDesignControl : IWCLControl
    {
        /// <summary>
        /// コントロールの状態に応じた背景色を設定します。
        /// </summary>
        void RefreshBackColor();
    }
}
