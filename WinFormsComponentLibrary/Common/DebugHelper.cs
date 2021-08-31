using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WCL.Common
{
    public static class DebugHelper
    {
        [Conditional("DEBUG")]
        public static void Assert(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Debug.Assert(false, message, GetMessage(filePath, lineNumber, memberName));
        }

        [Conditional("DEBUG")]
        public static void WriteLine(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            var msg = new StringBuilder();
            msg.AppendLine("# Message");
            msg.AppendLine($"  {message}");
            msg.Append(GetMessage(filePath, lineNumber, memberName));
            Debug.WriteLine(msg.ToString());
        }

        public static string GetMessage(string filePath = "", int lineNumber = 0, string memberName = "")
        {
            var message = new StringBuilder();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                message.AppendLine($" - FilePath  : {filePath}");
            }
            if (lineNumber <= 0)
            {
                message.AppendLine($" - LineNumber: {lineNumber}");
            }
            if (string.IsNullOrWhiteSpace(memberName))
            {
                message.AppendLine($" - MemberName: {memberName}");
            }
            if (0 < message.Length)
            {
                var body = message.ToString();
                message = new StringBuilder();
                message.AppendLine("# Caller");
                message.AppendLine(body);
            }
            return message.ToString().Trim(Environment.NewLine);
        }
    }
}
