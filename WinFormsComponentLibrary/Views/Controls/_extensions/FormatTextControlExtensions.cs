using System;
using System.Text.RegularExpressions;

namespace WCL.Views.Controls
{
    /// <summary>
    /// <see cref="IFormatTextControl"/> インターフェースへの拡張メソッドを提供します。
    /// </summary>
    public static class FormatTextControlExtensions
    {
        public static bool ValidateText(this IFormatTextControl self, string targetText)
        {
            // 入力状態を確認する
            switch (self.Role)
            {
                case FormatTextEditRoleKind.Zip:
                case FormatTextEditRoleKind.Date:
                case FormatTextEditRoleKind.DateTime:
                    if (self.MaskCompleted == false)
                    {
                        return false;
                    }
                    break;
            }

            // 正規表現で検証する
            string pattern = string.Empty;
            switch (self.Role)
            {
                case FormatTextEditRoleKind.Zip:
                    pattern = @"^[0-9]{3}-[0-9]{4}$";
                    break;
                case FormatTextEditRoleKind.Tel:
                    pattern = @"^\([0-9 ]{5}\)[0-9 ]{4}-[0-9 ]{1,4}$";
                    break;
            }
            if (string.IsNullOrEmpty(pattern) == false &&
                Regex.IsMatch(targetText, pattern) == false)
            {
                return false;
            }

            // 型で検証する
            switch (self.Role)
            {
                case FormatTextEditRoleKind.Date:
                case FormatTextEditRoleKind.DateTime:
                    if (DateTime.TryParse(targetText, out DateTime _) == false)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        public static string AdjustText(this IFormatTextControl self, string targetText)
        {
            string result = targetText;
            switch (self.Role)
            {
                case FormatTextEditRoleKind.Date:
                case FormatTextEditRoleKind.DateTime:
                    if (DateTime.TryParse(targetText, out var tmp))
                    {
                        result = tmp.ToString("yyyy/MM/dd HH:mm");
                    }
                    break;
            }
            return result;
        }

        public static void ApplyRole(this IFormatTextControl self)
        {
            switch (self.Role)
            {
                case FormatTextEditRoleKind.Zip:
                    self.Text = "   -";
                    self.Mask = "000-0000";
                    return;
                case FormatTextEditRoleKind.Tel:
                    self.Text = "(     )    -";
                    self.Mask = "(00000)0000-0000";
                    return;
                case FormatTextEditRoleKind.Date:
                    self.Text = "    /  /";
                    self.Mask = "0000/00/00";
                    return;
                case FormatTextEditRoleKind.DateTime:
                    self.Text = "    /  /     :";
                    self.Mask = "0000/00/00 90:00";
                    return;
                default:
                    self.Text = String.Empty;
                    self.Mask = String.Empty;
                    return;
            }
        }
    }
}
