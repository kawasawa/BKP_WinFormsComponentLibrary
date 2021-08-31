using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace WCL
{
    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 指定された文字列内の各単語の最初の文字を大文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToProperCase(this string text)
            => Strings.StrConv(text, VbStrConv.ProperCase);

        /// <summary>
        /// 指定された文字列内の半角文字を全角文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToWide(this string text)
            => Strings.StrConv(text, VbStrConv.Wide);

        /// <summary>
        /// 指定された文字列内の全角文字を半角文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToNarrow(this string text)
            => Strings.StrConv(text, VbStrConv.Narrow);

        /// <summary>
        /// 指定された文字列内のカタカナをひらがなに変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToHiragana(this string text)
            => Strings.StrConv(text, VbStrConv.Hiragana);

        /// <summary>
        /// 指定された文字列内のひらがなをカタカナに変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToKatakana(this string text)
            => Strings.StrConv(text, VbStrConv.Katakana);

        /// <summary>
        /// 指定した文字を指定した数だけ連結した文字列を取得します
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="number">連結する数</param>
        /// <returns>連結した文字列。</returns>
        public static string Repeat(this char text, int number)
            => text.ToString().Repeat(number);

        /// <summary>
        /// 指定した文字列を指定した数だけ連結した文字列を取得します
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="number">連結する数</param>
        /// <returns>連結した文字列。</returns>
        public static string Repeat(this string text, int number)
            => string.Concat(Enumerable.Repeat(text, number));

        /// <summary>
        /// この文字列の先頭が、指定した文字列と一致するかどうかを判断します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">指定した文字列</param>
        /// <returns>一致するかどうかを示す値</returns>
        public static bool StartsWith(this string text, string[] values)
            => 0 < values.Count(value => text.StartsWith(value));

        /// <summary>
        /// この文字列の末尾が、指定した文字列と一致するかどうかを判断します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">指定した文字列</param>
        /// <returns>一致するかどうかを示す値</returns>
        public static bool EndsWith(this string text, string[] values)
            => 0 < values.Count(value => text.EndsWith(value));

        /// <summary>
        /// この文字列の先頭と末尾から、指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="value">削除する文字列</param>
        /// <returns>文字列の先頭と末尾から、<paramref name="value"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string Trim(this string text, string value)
            => text.Trim(new[] { value });

        /// <summary>
        /// この文字列の先頭と末尾から、指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">削除する文字列の配列</param>
        /// <returns>文字列の先頭と末尾から、<paramref name="values"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string Trim(this string text, string[] values)
        {
            foreach (var value in values)
            {
                var startsWith = text.StartsWith(value);
                var endsWith = text.EndsWith(value);
                if (startsWith && endsWith)
                {
                    return text.Substring(0, text.Length - value.Length).Substring(value.Length);
                }
                else if (startsWith)
                {
                    return text.Substring(value.Length);
                }
                else if (endsWith)
                {
                    return text.Substring(0, text.Length - value.Length);
                }
            }
            return text;
        }

        /// <summary>
        /// この文字列の先頭から、指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="value">削除する文字列</param>
        /// <returns>文字列の先頭から、<paramref name="value"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimStart(this string text, string value)
            => text.TrimStart(new[] { value });

        /// <summary>
        /// この文字列の先頭から、配列で指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">削除する文字列の配列</param>
        /// <returns>文字列の先頭から、<paramref name="values"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimStart(this string text, string[] values)
        {
            foreach (var value in values)
            {
                if (text.StartsWith(value))
                {
                    return text.Substring(value.Length);
                }
            }
            return text;
        }

        /// <summary>
        /// この文字列の末尾から、指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="value">削除する文字列</param>
        /// <returns>文字列の末尾から、<paramref name="value"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimEnd(this string text, string value)
            => text.TrimEnd(new[] { value });

        /// <summary>
        /// この文字列の末尾から、配列で指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">削除する文字列の配列</param>
        /// <returns>文字列の末尾から、<paramref name="values"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimEnd(this string text, string[] values)
        {
            foreach (var value in values)
            {
                if (text.EndsWith(value))
                {
                    return text.Substring(0, text.Length - value.Length);
                }
            }
            return text;
        }

        /// <summary>
        /// 現在の <see cref="DateTime"/> オブジェクトの値を、それと等価な yyyyMMdd 形式の文字列に変換します。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> オブジェクト</param>
        /// <returns>等価な文字列</returns>
        public static string ToStringFormattedWithYYYYMMDD(this DateTime dateTime)
            => dateTime.ToString("yyyyMMdd");

        /// <summary>
        /// ストリームからバイトのブロックを読み取り、そのデータを特定のバッファーに書き込みます。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="array">このメソッドが返されるときに、現在のソースから読み取られたバイトに置き換えられます。</param>
        /// <returns>バッファーに読み取られた合計バイト数</returns>
        public static int Read(this FileStream stream, byte[] array)
            => stream.Read(array, 0, array.Length);

        /// <summary>
        /// バッファーのデータを使用して、ストリームにバイトのブロックを書き込みます。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="array">ストリームに書き込むデータを格納しているバッファー</param>
        public static void Write(this FileStream stream, byte[] array)
            => stream.Write(array, 0, array.Length);

        /// <summary>
        /// 指定されたインスタンスを指定されたストリームに書き込みます。
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <param name="target">インスタンス</param>
        /// <param name="writer">ストリームライター</param>
        public static void SerializeToXml<T>(this T target, TextWriter writer)
            => new XmlSerializer(typeof(T)).Serialize(writer, target);

        /// <summary>
        /// 指定されたストリームを読み込み、指定された型のインスタンスとして取得します。
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <param name="reader">ストリームリーダー</param>
        /// <returns>インスタンス</returns>
        public static T DeserializeFromXml<T>(this TextReader reader)
            => (T)new XmlSerializer(typeof(T)).Deserialize(reader);

        /// <summary>
        /// 指定されたコレクションの各要素に対して、指定された処理を実行します。
        /// </summary>
        /// <typeparam name="T"><paramref name="action"/> の引数の型</typeparam>
        /// <param name="array">処理を実行するコレクション</param>
        /// <param name="action"><paramref name="array"/> の要素に対して実行する <see cref="Action{T}"/></param>
        /// <returns><paramref name="action"/> を実行した <paramref name="array"/> の要素からなるコレクション</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable array, Action<T> action)
            => array.Cast<T>().ForEach(action);

        /// <summary>
        /// 指定されたコレクションの各要素に対して、指定された処理を実行します。
        /// </summary>
        /// <typeparam name="T"><paramref name="array"/> の要素の型</typeparam>
        /// <param name="array">処理を実行するコレクション</param>
        /// <param name="action"><paramref name="array"/> の要素に対して実行する <see cref="Action{T}"/></param>
        /// <returns><paramref name="action"/> を実行した <paramref name="array"/> の要素からなるコレクション</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var value in array)
            {
                action(value);
            }
            return array;
        }

        /// <summary>
        /// 指定されたコレクションの各要素に対して、指定された処理を実行します。
        /// </summary>
        /// <typeparam name="T"><paramref name="array"/> の要素の型</typeparam>
        /// <typeparam name="TResult">処理を実行した後のコレクションの要素の型</typeparam>
        /// <param name="array">処理を実行するコレクション</param>
        /// <param name="func"><paramref name="array"/> の要素に対して実行する <see cref="Func{T, TResult}"/></param>
        /// <returns><paramref name="func"/> を実行した <paramref name="array"/> の要素からなるコレクション</returns>
        public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> array, Func<T, TResult> func)
        {
            var list = new List<TResult>();
            foreach (var value in array)
            {
                var item = func(value);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// コレクションの各要素を連結します。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> の要素の型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <returns><paramref name="values"/> の要素を連結した文字列</returns>
        public static string Concat<T>(this IEnumerable<T> values)
            => string.Concat<T>(values);

        /// <summary>
        /// コレクションの各要素を連結します。
        /// 各要素の間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> の要素の型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <param name="separator">区切り記号</param>
        /// <returns><paramref name="values"/> の要素からなる、<paramref name="separator"/> で区切られた文字列</returns>
        public static string Join<T>(this IEnumerable<T> values, char separator)
            => values.Join(separator.ToString());

        /// <summary>
        /// コレクションの各要素を連結します。
        /// 各メンバーの間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> の要素の型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <param name="separator">区切り記号</param>
        /// <returns><paramref name="values"/> の要素からなる、<paramref name="separator"/> で区切られた文字列</returns>
        public static string Join<T>(this IEnumerable<T> values, string separator)
            => string.Join(separator, values);

        /// <summary>
        /// パブリックなプロパティの値を同名のプロパティに設定します。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> の型</typeparam>
        /// <typeparam name="TTarget"><paramref name="target"/> の型</typeparam>
        /// <param name="source">値を取得するインスタンス</param>
        /// <param name="target">値を設定するインスタンス</param>
        /// <param name="sourceProperty">転記するプロパティのメタデータ</param>
        public static void CopyProperty<TSource, TTarget>(TSource source, TTarget target, PropertyInfo sourceProperty)
        {
            var targetProperty = target.GetType().GetProperty(sourceProperty.Name);
            if (targetProperty?.CanWrite != true)
            {
                return;
            }
            targetProperty.SetValue(target, sourceProperty.GetValue(source));
        }

        /// <summary>
        /// パブリックなプロパティの値を同名のプロパティに設定します。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> の型</typeparam>
        /// <typeparam name="TTarget"><paramref name="target"/> の型</typeparam>
        /// <param name="source">値を取得するインスタンス</param>
        /// <param name="target">値を設定するインスタンス</param>
        /// <returns><paramref name="target"/> のインスタンスを返す</returns>
        public static TTarget CopyProperties<TSource, TTarget>(TSource source, TTarget target)
        {
            source.GetType().GetProperties().ForEach(p => CopyProperty(source, target, p));
            return target;
        }

        /// <summary>
        /// メモリ上のデータまで含めてオブジェクトを複製します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="target">複製するインスタンス</param>
        /// <returns>複製した新しいインスタンス</returns>
        public static T CloneDeep<T>(this T target)
        {
            T obj;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, target);
                stream.Position = 0;
                obj = (T)formatter.Deserialize(stream);
            }
            return obj;
        }

        /// <summary>
        /// 指定した <see cref="Color"/> 構造体と ARGB 値が等しいシステムカラーを取得します。
        /// 対応するシステムカラーが存在しない場合は <paramref name="color"/> をそのまま返します。
        /// </summary>
        /// <param name="color"><see cref="Color"/> 構造体</param>
        /// <returns>システムカラー</returns>
        public static Color GetKnownColor(this Color color)
        {
            if (color.IsKnownColor)
            {
                return color;
            }

            foreach (KnownColor c in Enum.GetValues(typeof(KnownColor)))
            {
                Color knownColor = Color.FromKnownColor(c);
                if (color.ToArgb() == knownColor.ToArgb())
                {
                    return knownColor;
                }
            }
            return color;
        }

        /// <summary>
        /// 指定されたイメージのインスタンスをグレースケールに変換します。
        /// </summary>
        /// <param name="self">イメージのインスタンス</param>
        /// <returns>グレースケールのイメージ</returns>
        public static Image ConvertToGrayScale(this Image self)
        {
            const float COLOR_MODEL_R = 0.299F;
            const float COLOR_MODEL_G = 0.587F;
            const float COLOR_MODEL_B = 0.114F;

            var matrix = new ColorMatrix(new[] {
                new[] { COLOR_MODEL_R, COLOR_MODEL_R, COLOR_MODEL_R, 0f, 0f },
                new[] { COLOR_MODEL_G, COLOR_MODEL_G, COLOR_MODEL_G, 0f, 0f },
                new[] { COLOR_MODEL_B, COLOR_MODEL_B, COLOR_MODEL_B, 0f, 0f },
                new[] { 0f, 0f, 0f, 1f, 0f },
                new[] { 0f, 0f, 0f, 0f, 1f },
            });
            var attribute = new ImageAttributes();
            attribute.SetColorMatrix(matrix);

            var result = new Bitmap(self.Width, self.Height);
            using (var g = Graphics.FromImage(result))
            {
                g.DrawImage(
                    self,
                    new Rectangle(0, 0, self.Width, self.Height),
                    0,
                    0,
                    self.Width,
                    self.Height,
                    GraphicsUnit.Pixel,
                    attribute);
            }
            return result;
        }

        /// <summary>
        /// 2つの <see cref="Rectangle"/> 構造体に交差部分が存在するかどうかを判定します。
        /// </summary>
        /// <param name="rect1">1つめの四角形</param>
        /// <param name="rect2">2つめの四角形</param>
        /// <returns>交差部分が存在するかどうかを示す値</returns>
        public static bool SomeContains(this Rectangle rect1, Rectangle rect2) => !Rectangle.Intersect(rect1, rect2).IsEmpty;

        ///// <summary>
        ///// フォームのサイズが AeroSanp により拡張されたかどうかを判定します。
        ///// </summary>
        ///// <param name="form">フォーム</param>
        ///// <returns>拡張されたかどうかを示す値</returns>
        //public static bool IsAeroSnapState(this Form form)
        //{
        //    WINDOWPLACEMENT lpwndpl = new WINDOWPLACEMENT();
        //    RECT lpRect = new RECT();
        //    User32.GetWindowPlacement(form.Handle, ref lpwndpl);
        //    User32.GetWindowRect(form.Handle, ref lpRect);
        //    if (lpwndpl.rcNormalPosition.Equals(lpRect) == false)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
           
    }
}
