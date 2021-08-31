namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力値を検証できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IValidateValueControl
    {
        /// <summary>
        /// 入力値が妥当であるかどうかを示す値を取得します。
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 制約に違反していることを通知するメッセージを表示します。
        /// </summary>
        void ShowConstraintMessageForValue();
    }
}
