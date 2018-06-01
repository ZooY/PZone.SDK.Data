using System.Text.RegularExpressions;


namespace PZone.Data.Helpers
{
    /// <summary>
    /// Инструменты для работы с числами.
    /// </summary>
    public static class NumberHelper
    {
        /// <summary>
        /// Разделение числа на целую и дробную часть.
        /// </summary>
        /// <param name="number">Исходной число.</param>
        /// <param name="whole">Дробная часть числа.</param>
        /// <param name="fractional">Целая часть числа.</param>
        /// <param name="options">Паарметры разделения.</param>
        /// <remarks>
        /// В возвращаемых значенийх целой и дробной части убираются лишние нули (ведущие у целой части и конечные у дробной).
        /// </remarks>
        public static void Split(string number, out string whole, out string fractional, NumberSplitOptions? options = null)
        {
            var amountParts = number.Split('.', ',');
            whole= string.IsNullOrWhiteSpace(amountParts[0]) ? "0" : amountParts[0];
            fractional = amountParts.Length > 1 ? amountParts[1] : "0";

            if (options.HasValue && options.Value.HasFlag(NumberSplitOptions.RemoveWasteZeros))
            {
                var zerosReduction = new Regex(@"^0+$");
                whole = zerosReduction.Replace(whole, "0");
                if (whole.Length > 1)
                    whole = new Regex(@"^0+").Replace(whole, "");
                fractional = zerosReduction.Replace(fractional, "0");
                if (fractional.Length > 1)
                    fractional = new Regex(@"0+$").Replace(fractional, "");
            }
        }
    }
}