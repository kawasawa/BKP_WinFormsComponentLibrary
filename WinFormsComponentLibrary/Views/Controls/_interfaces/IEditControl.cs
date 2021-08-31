namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力値を編集できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IEditControl : IWCLControl
    {
        /// <summary>
        /// コントロールの入力値を取得します。
        /// </summary>
        /// <returns>入力値</returns>
        object GetValue();

        /// <summary>
        /// コントロールの入力値を設定します。
        /// </summary>
        /// <param name="value">入力値</param>
        void SetValue(object value);

        /// <summary>
        /// コントロールの入力値をクリアします。
        /// </summary>
        void ClearValue();
    }
}
