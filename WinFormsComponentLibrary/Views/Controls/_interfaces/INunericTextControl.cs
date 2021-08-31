namespace WCL.Views.Controls
{
    /// <summary>
    /// 数値を編集できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface INunericTextControl : ITextControl
    {
        /// <summary>
        /// 整数部の最大入力桁数を取得または設定します。
        /// </summary>
        int MaxLengthOfIntegerDigits { get; set; }

        /// <summary>
        /// 小数部の最大入力桁数を取得または設定します。
        /// </summary>
        int MaxLengthOfDecimalDigits { get; set; }

        /// <summary>
        /// 小数の入力を許容するかどうかを示す値を取得します。
        /// </summary>
        bool AllowDecimal { get; }

        /// <summary>
        /// 負の値を許容するかどうかを示す値を取得または設定します
        /// </summary>
        bool AllowMinus { get; set; }

        /// <summary>
        /// カンマ区切りで表示するかどうかを示す値を取得または設定します。
        /// </summary>
        bool CommaSeparate { get; set; }

        /// <summary>
        /// ユーザが入力したテキストのみを取得します。
        /// </summary>
        string UserInputText { get; }
    }
}
