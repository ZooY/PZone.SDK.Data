using System;


namespace PZone.Data.Formatting
{
    /// <summary>
    /// Параметры обрезания строки.
    /// </summary>
    [Flags]
    public enum StringCropOptions
    {
        /// <summary>
        /// Удалить все теги HTML из результирующей строки.
        /// </summary>
        RemoveHtmlTags,

        /// <summary>
        /// Преобраховать перевод строки в пробел.
        /// </summary>
        NewLineToSpace,

        /// <summary>
        /// Объединение нескольких, подряд идущих, пробельных символа в один пробел.
        /// </summary>
        MergeSpaces
    }
}