using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace WCL.Views
{
    /// <summary>
    /// 汎用的な処理を提供します。
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="owner">メッセージボックスを所有する要素</param>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="message">メッセージのテキスト</param>
        /// <returns>ユーザの選択結果を示す値</returns>
        public static bool? ShowMessage(IWin32Window owner, MessageKind kind, string message)
        {
            return ShowMessage(owner, kind, message, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="owner">メッセージボックスを所有する要素</param>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="message">メッセージのテキスト</param>
        /// <param name="defaultButton">フォーカスを取得する既定のボタン</param>
        /// <returns>ユーザの選択結果を示す値</returns>
        public static bool? ShowMessage(IWin32Window owner, MessageKind kind, string message, MessageBoxDefaultButton defaultButton)
        {
            return ConvertToTernaryValue(
                MessageBox.Show(
                    owner,
                    message,
                    Dependencies.Views.MessageBox.Titles[kind],
                    GetMessageBoxButtons(kind),
                    Dependencies.Views.MessageBox.Images[kind],
                    defaultButton));
        }
        
        /// <summary>
        /// メッセージボックスのボタンを取得します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <returns>メッセージボックスのボタン</returns>
        public static MessageBoxButtons GetMessageBoxButtons(MessageKind kind)
        {
            switch (kind)
            {
                case MessageKind.Information:
                case MessageKind.Warning:
                case MessageKind.Error:
                    return MessageBoxButtons.OK;
                case MessageKind.Question:
                    return MessageBoxButtons.YesNo;
                case MessageKind.QuestionCancellable:
                    return MessageBoxButtons.YesNoCancel;
                case MessageKind.WarningCancellable:
                    return MessageBoxButtons.OKCancel;
                default:
                    throw new InvalidEnumArgumentException(nameof(kind));
            }
        }

        /// <summary>
        /// ダイアログの戻り値をそれと等価な三値に変換します。
        /// </summary>
        /// <param name="kind">ダイアログの戻り値</param>
        /// <returns>ダイアログの戻り値をそれと等価な三値</returns>
        public static bool? ConvertToTernaryValue(DialogResult kind)
        {
            switch (kind)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                case DialogResult.Retry:
                    return true;
                case DialogResult.No:
                case DialogResult.Abort:
                    return false;
                default:
                    return null;
            }
        }
    }
}
