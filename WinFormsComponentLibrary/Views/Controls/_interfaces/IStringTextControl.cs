namespace WCL.Views.Controls
{
    /// <summary>
    /// 文字列を編集できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IStringTextControl : ITextControl
    {
        /// <summary>
        /// コントロールの役割を取得または設定します。
        /// </summary>
        StringTextEditRoleKind Role { get; set; }
    }
}
