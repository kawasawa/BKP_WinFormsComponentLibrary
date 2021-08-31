namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力書式が厳密に指定される文字列を編集できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IFormatTextControl : ITextControl
    {
        /// <summary>
        /// コントロールの役割を取得または設定します。
        /// </summary>
        FormatTextEditRoleKind Role { get; set; }

        /// <summary>
        /// 入力マスクを取得または設定します。
        /// </summary>
        string Mask{ get; set; }

        /// <summary>
        /// 入力マスクが満たされているかどうかを示す値を取得します。
        /// </summary>
        bool MaskCompleted { get; }
    }
}
