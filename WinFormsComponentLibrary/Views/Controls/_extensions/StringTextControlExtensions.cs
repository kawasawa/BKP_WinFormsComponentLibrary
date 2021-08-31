using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WCL.Views.Controls
{
    /// <summary>
    /// <see cref="IStringTextControl"/> インターフェースへの拡張メソッドを提供します。
    /// </summary>
    public static class StringTextControlExtensions
    {
        public static bool ValidateText(this IStringTextControl self, string targetText)
        {
            if (string.IsNullOrEmpty(targetText))
            {
                return true;
            }

            string pattern = string.Empty;
            switch (self.Role)
            {
                case StringTextEditRoleKind.Numeric:
                    pattern = @"^[0-9]*$";
                    break;
                case StringTextEditRoleKind.AlphaNumeric:
                    pattern = @"^[a-zA-Z0-9]*$";
                    break;
                case StringTextEditRoleKind.Hiragana:
                    pattern = @"^[\p{IsHiragana}]*$";
                    break;
                case StringTextEditRoleKind.Katakana:
                    pattern = @"^[｡-ﾟ- \p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F　]*$";
                    break;
                case StringTextEditRoleKind.KatakanaHalf:
                    pattern = @"^[｡-ﾟ- ]*$";
                    break;
                case StringTextEditRoleKind.KatakanaHalfAlphaNumeric:
                    pattern = @"^[a-zA-Z0-9｡-ﾟ- ]*$";
                    break;
                case StringTextEditRoleKind.Half:
                    pattern = @"^[ -~｡-ﾟ]*$";
                    break;
            }
            if (string.IsNullOrEmpty(pattern) == false &&
                Regex.IsMatch(targetText, pattern) == false)
            {
                return false;
            }
            return true;
        }

        public static ImeMode GetImeMode(this IStringTextControl self)
        {
            switch (self.Role)
            {
                case StringTextEditRoleKind.Normal:
                    return ImeMode.NoControl;
                case StringTextEditRoleKind.Hiragana:
                    return ImeMode.Hiragana;
                case StringTextEditRoleKind.Katakana:
                    return ImeMode.Katakana;
                case StringTextEditRoleKind.KatakanaHalf:
                case StringTextEditRoleKind.KatakanaHalfAlphaNumeric:
                    return ImeMode.KatakanaHalf;
                default:
                    return ImeMode.Disable;
            }
        }
    }
}
