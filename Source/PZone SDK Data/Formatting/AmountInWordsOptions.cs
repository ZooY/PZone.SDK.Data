using System;


namespace PZone.Data.Formatting
{
    /// <summary>
    /// Параметры получения числа прописью.
    /// </summary>
    [Flags]
    public enum AmountInWordsOptions
    {
        /// <summary>
        /// Не установлено ни каких параметров.
        /// </summary>
        None = 0,

        /// <summary>
        /// Обработать и вернуть только целую часть числа.
        /// </summary>
        WholeOnly = 1,

        /// <summary>
        /// Обработать и вернуть только дробную часть числа.
        /// </summary>
        FractionalOnly = 2,

        /// <summary>
        /// Дробная часть в виде числа.
        /// </summary>
        NumericFractional = 4,

        /// <summary>
        /// Дробная часть в виде числа.
        /// </summary>
        NumericWhole = 8
    }
}