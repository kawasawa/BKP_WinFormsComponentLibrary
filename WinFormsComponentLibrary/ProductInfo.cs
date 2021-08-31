using System;
using System.Reflection;
using static System.Attribute;

namespace WCL
{
    /// <summary>
    /// プロダクト情報を表します。
    /// </summary>
    public static class ProductInfo
    {
        private static readonly Lazy<Assembly> _assembly
            = new Lazy<Assembly>(() => Assembly.GetEntryAssembly());
        private static readonly Lazy<string> _title
            = new Lazy<string>(() => ((AssemblyTitleAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyTitleAttribute))).Title);
        private static readonly Lazy<string> _description
            = new Lazy<string>(() => ((AssemblyDescriptionAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyDescriptionAttribute))).Description);
        private static readonly Lazy<string> _company
            = new Lazy<string>(() => ((AssemblyCompanyAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyCompanyAttribute))).Company);
        private static readonly Lazy<string> _product
            = new Lazy<string>(() => ((AssemblyProductAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyProductAttribute))).Product);
        private static readonly Lazy<string> _copyritht
            = new Lazy<string>(() => ((AssemblyCopyrightAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyCopyrightAttribute))).Copyright);
        private static readonly Lazy<string> _trademark
            = new Lazy<string>(() => ((AssemblyTrademarkAttribute)GetCustomAttribute(_assembly.Value, typeof(AssemblyTrademarkAttribute))).Trademark);

        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        public static string Title => _title.Value;

        /// <summary>
        /// 説明を取得します。
        /// </summary>
        public static string Description => _description.Value;

        /// <summary>
        /// 社名を取得します。
        /// </summary>
        public static string Company => _company.Value;

        /// <summary>
        /// 製品名を取得します。
        /// </summary>
        public static string Product => _product.Value;

        /// <summary>
        /// 著作権情報を取得します。
        /// </summary>
        public static string Copyright => _copyritht.Value;

        /// <summary>
        /// 商標情報を取得します。
        /// </summary>
        public static string Trademark => _trademark.Value;

        /// <summary>
        /// 完全パスを取得します。
        /// </summary>
        public static string Location => _assembly.Value.Location;

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public static Version Version => _assembly.Value.GetName().Version;

        /// <summary>
        /// バージョン情報を示す文字列を取得します。
        /// </summary>
        public static string VersionString
        {
            get
            {
                var str = Version.ToString(3);
                if (0 < Version.Revision)
                {
                    str += $" rev.{Version.Revision}";
                }
#if DEBUG
                str += " [Debug Build]";
#endif
                return str;
            }
        }
    }
}