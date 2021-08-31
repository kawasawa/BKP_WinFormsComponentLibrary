namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力の必須指定およびそれを検知できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IValidateMandatoryControl : IPermitEmptyControl
    {
        /// <summary>
        /// 入力が必須であるかどうかを示す値を取得または設定します。
        /// </summary>
        bool IsMandatory { get; set; }

        /// <summary>
        /// 制約に違反していることを通知するメッセージを表示します。
        /// </summary>
        void ShowConstraintMessageForMandatory();
    }
}
