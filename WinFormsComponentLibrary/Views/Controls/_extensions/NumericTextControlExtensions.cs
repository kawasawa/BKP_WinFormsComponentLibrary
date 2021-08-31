using System.Text.RegularExpressions;

namespace WCL.Views.Controls
{
    /// <summary>
    /// <see cref="INunericTextControl"/> インターフェースへの拡張メソッドを提供します。
    /// </summary>
    public static class NumericTextControlExtensions
    {
        private const char SPECIAL_CHAR_COMMA = ',';
        private const char SPECIAL_CHAR_POINT = '.';
        private const char SPECIAL_CHAR_MINUS = '-';

        public static bool ValidateText(this INunericTextControl self, string targetText)
        {
            if (string.IsNullOrEmpty(targetText))
            {
                return true;
            }

            string pattern;
            if (self.AllowDecimal)
            {
                if (self.AllowMinus)
                    pattern = @"^(" + SPECIAL_CHAR_MINUS + @"?([1-9]\d{0," + (self.MaxLengthOfIntegerDigits - 1).ToString() + @"}|0)(\" + SPECIAL_CHAR_POINT + @"\d{0," + self.MaxLengthOfDecimalDigits.ToString() + @"})?|-)$";
                else
                    pattern = @"^(([1-9]\d{0," + (self.MaxLengthOfIntegerDigits - 1).ToString() + @"}|0)(\" + SPECIAL_CHAR_POINT + @"\d{0," + self.MaxLengthOfDecimalDigits.ToString() + @"})?)$";
            }
            else
            {
                if (self.AllowMinus)
                    pattern = @"^(" + SPECIAL_CHAR_MINUS + @"?[1-9]\d{0," + (self.MaxLengthOfIntegerDigits - 1).ToString() + @"}|0|-)$";
                else
                    pattern = @"^([1-9]\d{0," + (self.MaxLengthOfIntegerDigits - 1).ToString() + @"}|0)$";
            }
            return Regex.IsMatch(targetText, pattern);
        }

        public static bool ValidateKey(this INunericTextControl self, char targetKey)
        {
            if (('0' <= targetKey && targetKey <= '9') ||
                (self.AllowDecimal && targetKey == SPECIAL_CHAR_POINT) ||
                (self.AllowMinus && targetKey == SPECIAL_CHAR_MINUS))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetUserInputText(this INunericTextControl self)
        {
            if (self.CommaSeparate)
                return self.Text.Replace(SPECIAL_CHAR_COMMA.ToString(), string.Empty);
            else
                return self.Text;
        }

        public static void FormatTextOnIdle(this INunericTextControl self)
        {
            self.Text = FormatTextOnIdle(self, self.Text);
        }

        public static string FormatTextOnIdle(this INunericTextControl self, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var result = text;
            if (self.CommaSeparate)
            {
                var length = text.IndexOf(SPECIAL_CHAR_POINT);
                if (length < 0)
                {
                    length = text.Length;
                }
                result = text.Remove(0, length).Insert(0, string.Format("{0:#,0}", long.Parse(text.Substring(0, length))));
            }
            return result;
        }

        public static void FormatTextOnFocus(this INunericTextControl self)
        {
            self.Text = FormatTextOnFocus(self, self.Text);
        }

        public static string FormatTextOnFocus(this INunericTextControl self, string text)
        {
            return text.Replace(SPECIAL_CHAR_COMMA.ToString(), string.Empty);
        }

        public static string FormatTextOnValidated(this INunericTextControl self, string text)
        {
            var tmp = text;
            if (tmp.StartsWith(SPECIAL_CHAR_MINUS.ToString()))
            {
                switch (tmp.Length)
                {
                    case 1:
                        tmp = string.Empty;
                        break;
                    case 2:
                        if (tmp.Equals($"{SPECIAL_CHAR_MINUS}0"))
                        {
                            tmp = "0";
                        }
                        break;
                }
            }
            tmp = tmp.TrimEnd(SPECIAL_CHAR_POINT.ToString());
            return FormatTextOnIdle(self, tmp);
        }
    }
}
