using System.ComponentModel;

namespace WCL.Views.Controls
{
    /// <summary>
    /// 入力値の変更を検知できるコントロールが実装する処理を提供します。
    /// </summary>
    public interface IDetectChangesControl
    {
        /// <summary>
        /// 保持する前回値を取得します。
        /// </summary>
        object PreviousValue { get; }

        /// <summary>
        /// 検証を行うとき、入力値に変更がある場合に発生します。
        /// </summary>
        event CancelEventHandler ValidatingOnValueChanged;

        /// <summary>
        /// 正規の手段により前回値を更新します。
        /// </summary>
        void UpdatePreviousValue();
    }
}
