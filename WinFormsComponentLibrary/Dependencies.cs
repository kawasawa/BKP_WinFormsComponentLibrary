using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WCL.Views;
using WCL.Views.Controls;

namespace WCL
{
    /// <summary>
    /// アプリケーションに依存する要素を表します。
    /// </summary>
    public static class Dependencies
    {
        public static class Views
        {
            public static class Controls
            {
                public static readonly IDictionary<ControlStatusKind, Color> Colors = new Dictionary<ControlStatusKind, Color>()
                {
                    { ControlStatusKind.Idle, Color.FromArgb(255, 255, 255, 255) },
                    { ControlStatusKind.Focused, Color.FromArgb(255, 204, 236, 255) },
                    { ControlStatusKind.ReadOnly, Color.FromArgb(255, 255, 255, 151) },
                    { ControlStatusKind.Mandatory, Color.FromArgb(255, 255, 228, 225) },
                };
            }

            public static class FunctionButtons
            {
                private static IDictionary<FunctionButtonRoleKind, Bitmap> getImages()
                {
                    var result = new Dictionary<FunctionButtonRoleKind, Bitmap>();
                    foreach (var kind in (FunctionButtonRoleKind[])Enum.GetValues(typeof(FunctionButtonRoleKind)))
                    {
                        var info = typeof(Properties.Resource).GetProperty(kind.ToString());
                        if (info?.PropertyType != typeof(Bitmap))
                        {
                            continue;
                        }
                        result.Add(kind, (Bitmap)info.GetValue(null));
                    }
                    return result;
                }

                public static readonly IDictionary<FunctionButtonRoleKind, Bitmap> Images = getImages();

                public static readonly IDictionary<FunctionButtonRoleKind, string> Texts = new Dictionary<FunctionButtonRoleKind, string>()
                {
                    { FunctionButtonRoleKind.Save, "登録" },
                    { FunctionButtonRoleKind.SaveAll, "すべて登録" },
                    { FunctionButtonRoleKind.Store, "保存" },
                    { FunctionButtonRoleKind.Select, "選択" },
                    { FunctionButtonRoleKind.Search, "検索" },
                    { FunctionButtonRoleKind.Reference, "参照" },
                    { FunctionButtonRoleKind.Detail, "詳細" },
                    { FunctionButtonRoleKind.Filter, "抽出" },
                    { FunctionButtonRoleKind.History, "履歴" },
                    { FunctionButtonRoleKind.Delete, "削除" },
                    { FunctionButtonRoleKind.Eraser, "クリア" },
                    { FunctionButtonRoleKind.Close, "閉じる" },
                    { FunctionButtonRoleKind.Undo, "元に戻す" },
                    { FunctionButtonRoleKind.Redo, "やり直し" },
                    { FunctionButtonRoleKind.Refresh, "再読み込み" },
                    { FunctionButtonRoleKind.Synchronize, "同期" },
                    { FunctionButtonRoleKind.Connect, "接続" },
                    { FunctionButtonRoleKind.Excel, "Excel" },
                    { FunctionButtonRoleKind.Web, "ウェブ" },
                    { FunctionButtonRoleKind.Cloud, "クラウド" },
                    { FunctionButtonRoleKind.Database, "データ" },
                    { FunctionButtonRoleKind.Flag, "フラグ" },
                    { FunctionButtonRoleKind.Event, "イベント" },
                    { FunctionButtonRoleKind.Settings, "設定" },
                    { FunctionButtonRoleKind.Login, "ログイン" },
                    { FunctionButtonRoleKind.Logout, "ログアウト" },
                    { FunctionButtonRoleKind.User, "ユーザー" },
                    { FunctionButtonRoleKind.UserGuide, "ガイド" },
                    { FunctionButtonRoleKind.UserVoice, "コメント" },
                    { FunctionButtonRoleKind.UserPermission, "権限" },
                    { FunctionButtonRoleKind.Form, "画面表示" },
                    { FunctionButtonRoleKind.LinkForm, "連動" },
                    { FunctionButtonRoleKind.Print, "印刷" },
                    { FunctionButtonRoleKind.PrentPreview, "プレビュー" },
                    { FunctionButtonRoleKind.NewFile, "新規" },
                    { FunctionButtonRoleKind.AddFile, "追加" },
                    { FunctionButtonRoleKind.CopyFile, "複写" },
                    { FunctionButtonRoleKind.DeleteFile, "削除" },
                    { FunctionButtonRoleKind.NewFolder, "新規" },
                    { FunctionButtonRoleKind.AddFolder, "追加" },
                    { FunctionButtonRoleKind.DeleteFolder, "削除" },
                    { FunctionButtonRoleKind.AddRow, "行追加" },
                    { FunctionButtonRoleKind.InsertRow, "行挿入" },
                    { FunctionButtonRoleKind.DeleteRow, "行削除" },
                    { FunctionButtonRoleKind.Mail, "メール" },
                    { FunctionButtonRoleKind.SendMail, "メール送信" },
                    { FunctionButtonRoleKind.StatusOk, "許可" },
                    { FunctionButtonRoleKind.StatusNo, "拒否" },
                    { FunctionButtonRoleKind.StatusBlock, "ブロック" },
                    { FunctionButtonRoleKind.StatusHelp, "ヘルプ" },
                    { FunctionButtonRoleKind.StatusInformation, "情報" },
                    { FunctionButtonRoleKind.StatusAlert, "注意" },
                    { FunctionButtonRoleKind.StatusWarning, "警告" },
                    { FunctionButtonRoleKind.StatusError, "エラー" },
                    { FunctionButtonRoleKind.StatusRun, "実行" },
                    { FunctionButtonRoleKind.StatusPause, "中断" },
                    { FunctionButtonRoleKind.StatusStop, "停止" },
                };

                public static readonly IDictionary<FunctionButtonRoleKind, int> Authoritylevel = new Dictionary<FunctionButtonRoleKind, int>();
            }

            public static class MessageBox
            {
                public static readonly IDictionary<MessageKind, MessageBoxIcon> Images = new Dictionary<MessageKind, MessageBoxIcon>()
                {
                    { MessageKind.Information, MessageBoxIcon.Information },
                    { MessageKind.Question, MessageBoxIcon.Question },
                    { MessageKind.QuestionCancellable, MessageBoxIcon.Question },
                    { MessageKind.Warning, MessageBoxIcon.Warning },
                    { MessageKind.WarningCancellable, MessageBoxIcon.Warning },
                    { MessageKind.Error, MessageBoxIcon.Error },
                };

                public static readonly IDictionary<MessageKind, string> Titles = new Dictionary<MessageKind, string>()
                {
                    { MessageKind.Information, "情報" },
                    { MessageKind.Question, "確認" },
                    { MessageKind.QuestionCancellable, "確認" },
                    { MessageKind.Warning, "警告" },
                    { MessageKind.WarningCancellable, "警告" },
                    { MessageKind.Error, "エラー" },
                };

                public static readonly IDictionary<MessageDetailKind, string> Texts = new Dictionary<MessageDetailKind, string>()
                {
                    { MessageDetailKind.Error_NoAuthorization, $"処理を実行できませんでした。{Environment.NewLine}権限がありません。" },
                    { MessageDetailKind.Error_MandatoryForText, "必須項目を入力してください。" },
                    { MessageDetailKind.Error_MandatoryForSelect, "必須項目を選択してください。" },
                    { MessageDetailKind.Error_MandatoryForCheck, "必須項目を選択してください。" },
                    { MessageDetailKind.Error_ValidateForText, "正しい書式で入力してください。" },
                };
            }
        }
    }
}
