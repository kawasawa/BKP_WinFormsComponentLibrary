namespace WCL.Views.Controls
{
    /// <summary>
    /// 読み取り専用の状態が存在するコントロールが実装する処理を提供します。
    /// </summary>
    public interface IPermitReadOnlyControl
    {
        /// <summary>
        /// 読み取り専用の状態かどうかを示す値を取得または設定します。
        /// </summary>
        bool ReadOnly { get; set; }
    }
}
