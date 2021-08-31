namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力値として空の状態が存在するコントロールが実装する処理を提供します。
    /// </summary>
    public interface IPermitEmptyControl
    {
        /// <summary>
        /// 入力値が空であるかどうかを示す値を取得します。
        /// </summary>
        bool IsEmpty { get; }
    }
}
