using System;
using System.Text.RegularExpressions;


namespace PZone.Data.Formatting
{
    /// <summary>
    /// Форматирование строк.
    /// </summary>
    public static class StringFormatting
    {
        /// <summary>
        /// Обрезание строки до заданной длины.
        /// </summary>
        /// <param name="sourceString">Исходная строка.</param>
        /// <param name="maxLength">Максимальная длина результирующей строки.</param>
        /// <param name="options">Параметры обрезания.</param>
        /// <returns>Метод возвращает строку, обрезанную до указанного размера (с учетом целостности слов).</returns>
        public static string Crop(string sourceString, int maxLength, StringCropOptions options = StringCropOptions.RemoveHtmlTags | StringCropOptions.NewLineToSpace | StringCropOptions.MergeSpaces)
        {
            var strLength = sourceString.Length;
            if (strLength <= maxLength)
                return sourceString;

            var str = sourceString;

            if (options.HasFlag(StringCropOptions.RemoveHtmlTags))
            {
                str = Regex.Replace(str, @"<br\s*/*>", " ");
                str = Regex.Replace(str, @"<[^>]+>|&nbsp;", "");
            }

            if (options.HasFlag(StringCropOptions.NewLineToSpace))
                str = str.Replace(Environment.NewLine, " ");

            if (options.HasFlag(StringCropOptions.MergeSpaces))
                str = Regex.Replace(str, @"\s{2,}", " ");

            strLength = str.Length;
            if (strLength <= maxLength)
                return str;

            str = str.Substring(0, maxLength);
            var lastIndex = Regex.Match(str, @"\W+\w*$", RegexOptions.Multiline).Index;
            return str.Substring(0, lastIndex) + "...";
        }
    }
}