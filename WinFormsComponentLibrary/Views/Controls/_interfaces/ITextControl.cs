namespace WCL.Views.Controls
{
    /// <summary>
    /// テキストを編集できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface ITextControl : IEditControl
    {
        /// <summary>
        /// コントロールの入力値を取得または設定します。
        /// </summary>
        string Text { get; set; }
    }
}
