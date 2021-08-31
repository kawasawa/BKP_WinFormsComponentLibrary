using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 暗号化／復号化を行うためのメソッドを提供します。
/// </summary>
namespace WCL.Common
{
    /// <summary>
    /// 暗号化に関する処理を提供します。
    /// </summary>
    public static class EncrptionService
    {
        /// <summary>
        /// ハッシュ値を作成するためのソルトキーを表します。
        /// </summary>
        private const string SALT_VALUE = "74Gy_QaNb_4btA_ew1H";

        /// <summary>
        /// 初期化ベクタを生成する際の演算回数を表します。
        /// </summary>
        private const int ITERATIONCOUNT = 1000;

        /// <summary>
        /// 指定された文字列を暗号化します。
        /// </summary>
        /// <param name="sourceString">暗号化する文字列</param>
        /// <param name="seed">暗号化に使用するシード値</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string sourceString, string seed)
        {
            // 共有キーと初期化ベクタを生成する
            var managed = new RijndaelManaged();
            GenerateKeyAndVector(seed, managed.KeySize, out byte[] key, managed.BlockSize, out byte[] vector);
            managed.Key = key;
            managed.IV = vector;

            // バイト型配列を暗号化し文字列に変換する
            var encryptor = managed.CreateEncryptor();
            var sourceBytes = Encoding.UTF8.GetBytes(sourceString);
            var resultBytes = encryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);
            encryptor.Dispose();
            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 暗号化された文字列を復号化します。
        /// </summary>
        /// <param name="sourceString">暗号化された文字列</param>
        /// <param name="seed">暗号化に使用されたシード値</param>
        /// <returns>復号化された文字列</returns>
        public static string Decrypt(string sourceString, string seed)
        {
            //　共有キーと初期化ベクタを生成する
            var managed = new RijndaelManaged();
            GenerateKeyAndVector(seed, managed.KeySize, out byte[] key, managed.BlockSize, out byte[] vector);
            managed.Key = key;
            managed.IV = vector;

            // バイト型配列を復号化し文字列に変換する
            var decryptor = managed.CreateDecryptor();
            var sourceBytes = Convert.FromBase64String(sourceString);
            var resultBytes = decryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);
            decryptor.Dispose();
            return Encoding.UTF8.GetString(resultBytes);
        }

        /// <summary>
        /// パスワードを基に指定したビットサイズの共有キーと初期化ベクタを生成します。
        /// </summary>
        /// <param name="password">基となるパスワード</param>
        /// <param name="keySize">共有キーのサイズ</param>
        /// <param name="key">[戻り値] 共有キー</param>
        /// <param name="vectorSize">初期化ベクタのサイズ（</param>
        /// <param name="vector">[戻り値] 初期化ベクタ</param>
        private static void GenerateKeyAndVector(string password, int keySize, out byte[] key, int vectorSize, out byte[] vector)
        {
            var salt = Encoding.UTF8.GetBytes(SALT_VALUE);
            var bytes = new Rfc2898DeriveBytes(password, salt) { IterationCount = ITERATIONCOUNT };
            key = bytes.GetBytes(keySize / 8);
            vector = bytes.GetBytes(vectorSize / 8);
        }
    }
}