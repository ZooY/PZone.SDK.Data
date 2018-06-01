using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PZone.Data.Helpers;


namespace PZone.Data.Formatting
{
    /// <summary>
    /// Сумма прописью.
    /// </summary>
    public class AmountInWords
    {
        private readonly Dictionary<Currencies, CurrencyInfo> _currencies = new Dictionary<Currencies, CurrencyInfo>
        {
            {
                Currencies.RUB, new CurrencyInfo
                {
                    Wholes = new Dictionary<char, string> { { '0', "рублей" }, { '1', "рубль" }, { '2', "рубля" }, { '3', "рубля" }, { '4', "рубля" }, { '5', "рублей" }, { '6', "рублей" }, { '7', "рублей" }, { '8', "рублей" }, { '9', "рублей" } },
                    WholeGender = CurrencyGender.Masculine,
                    Fractionals = new Dictionary<char, string> { { '0', "копеек" }, { '1', "копейка" }, { '2', "копейки" }, { '3', "копейки" }, { '4', "копейки" }, { '5', "копеек" }, { '6', "копеек" }, { '7', "копеек" }, { '8', "копеек" }, { '9', "копеек" } },
                    FractionalGender = CurrencyGender.Feminine
                }
            }
        };
        private readonly Dictionary<char, string> _masculineUnits = new Dictionary<char, string> { { '0', "ноль" }, { '1', "один" }, { '2', "два" }, { '3', "три" }, { '4', "четыре" }, { '5', "пять" }, { '6', "шесть" }, { '7', "семь" }, { '8', "восемь" }, { '9', "девять" } };
        private readonly Dictionary<char, string> _feminineUnits = new Dictionary<char, string> { { '0', "ноль" }, { '1', "одна" }, { '2', "две" }, { '3', "три" }, { '4', "четыре" }, { '5', "пять" }, { '6', "шесть" }, { '7', "семь" }, { '8', "восемь" }, { '9', "девять" } };
        private readonly Dictionary<string, string> _unitsOverTen = new Dictionary<string, string> { { "10", "десять" }, { "11", "одинадцать" }, { "12", "двенадцать" }, { "13", "тринадцать" }, { "14", "четырнадцать" }, { "15", "пятнадцать" }, { "16", "шестнадцать" }, { "17", "семнадцать" }, { "18", "восемнадцать" }, { "19", "девятнадцать" } };
        private readonly Dictionary<char, string> _tens = new Dictionary<char, string> { { '2', "двадцать" }, { '3', "тридцать" }, { '4', "сорок" }, { '5', "пятьдесят" }, { '6', "шестьдесят" }, { '7', "семьдесят" }, { '8', "восемьдесят" }, { '9', "девяносто" } };
        private readonly Dictionary<char, string> _hundreds= new Dictionary<char, string> { { '1', "сто" }, { '2', "двести" }, { '3', "триста" }, { '4', "четыреста" }, { '5', "пятьсот" }, { '6', "шестьсот" }, { '7', "семьсот" }, { '8', "восемьсот" }, { '9', "девятьсот" } };
        private readonly Dictionary<char, string> _thousands = new Dictionary<char, string> { { '1', "тысяча" }, { '2', "тысячи" }, { '3', "тысячи" }, { '4', "тысячи" }, { '5', "тысяч" }, { '6', "тысяч" }, { '7', "тысяч" }, { '8', "тысяч" }, { '9', "тысяч" } };
        private readonly Dictionary<char, string> _millions = new Dictionary<char, string> { { '1', "милилон" }, { '2', "миллиона" }, { '3', "миллиона" }, { '4', "миллиона" }, { '5', "миллионов" }, { '6', "миллионов" }, { '7', "миллионов" }, { '8', "миллионов" }, { '9', "миллионов" } };
        private readonly Dictionary<char, string> _billions = new Dictionary<char, string> { { '1', "миллиард" }, { '2', "миллиарда" }, { '3', "миллиарда" }, { '4', "миллиарда" }, { '5', "миллиардов" }, { '6', "миллиардов" }, { '7', "миллиардов" }, { '8', "миллиардов" }, { '9', "миллиардов" } };
        private readonly Dictionary<char, string> _trillions = new Dictionary<char, string> { { '1', "триллион" }, { '2', "триллиона" }, { '3', "триллиона" }, { '4', "триллиона" }, { '5', "триллионов" }, { '6', "триллионов" }, { '7', "триллионов" }, { '8', "триллионов" }, { '9', "триллионов" } };


        /// <summary>
        /// Шаблон значения целой части числа.
        /// </summary>
        public string WholeValueTemplate { get; set; } = "{0}";
        /// <summary>
        /// Шаблон валюты челой части числа.
        /// </summary>
        public string WholeCurrencyTemplate { get; set; } = "{0}";

        /// <summary>
        /// Разделитель значения и валюты целой части числа.
        /// </summary>
        public string WholeDelimiter { get; set; } = " ";

        /// <summary>
        /// Шаблон значения дробной части числа.
        /// </summary>
        public string FractionalValueTemplate { get; set; } = "{0}";
        /// <summary>
        /// Шаблон валюты дробной части числа.
        /// </summary>
        public string FractionalCurrencyTemplate { get; set; } = "{0}";

        /// <summary>
        /// Разделитель значения и валюты дробной части числа.
        /// </summary>
        public string FractionalDelimiter { get; set; } = " ";


        /// <summary>
        /// Разделитель целой и дробной частей числа.
        /// </summary>
        public string WholeFractionalDelimiter { get; set; } = " ";


        /// <summary>
        /// Выполнение преобразования для числа, представленного типом <see cref="decimal"/>.
        /// </summary>
        /// <param name="amount">Исходное число.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="options">Параметры преобразования.</param>
        /// <returns>
        /// Метод возвращает указанное число, написанное прописью.
        /// </returns>
        public string Execute(decimal amount, Currencies currency, AmountInWordsOptions options = AmountInWordsOptions.NumericFractional)
        {
            return Execute(amount.ToString(CultureInfo.InvariantCulture), currency);
        }


        /// <summary>
        /// Выполнение преобразования для числа, представленного типом <see cref="float"/>.
        /// </summary>
        /// <param name="amount">Исходное число.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="options">Параметры преобразования.</param>
        /// <returns>
        /// Метод возвращает указанное число, написанное прописью.
        /// </returns>
        public string Execute(float amount, Currencies currency, AmountInWordsOptions options = AmountInWordsOptions.NumericFractional)
        {
            return Execute(amount.ToString(CultureInfo.InvariantCulture), currency);
        }


        /// <summary>
        /// Выполнение преобразования для числа, представленного типом <see cref="string"/>.
        /// </summary>
        /// <param name="amount">Исходное число в виде строки.</param>
        /// <param name="currency">Валюта.</param>
        /// <param name="options">Параметры преобразования.</param>
        /// <remarks>
        /// Строка должна представлять собой корректное число, в котором целая и дробная части разделены символами "." (точка) или "," (запятая).
        /// </remarks>
        /// <returns>
        /// Метод возвращает указанное число, написанное прописью.
        /// </returns>
        public string Execute(string amount, Currencies currency, AmountInWordsOptions options = AmountInWordsOptions.NumericFractional)
        {
            string
                whole, // целая часть числа
                fractional; // дробная часть числа
            NumberHelper.Split(amount, out whole, out fractional, NumberSplitOptions.RemoveWasteZeros);

            var currencyInfo = _currencies[currency];
            string wholeValue = null;
            string wholeCurrency = null;
            string fractionalValue = null;
            string fractionalCurrency = null;

            // триллионы миллиарды  миллионы  тысячи     сотни
            //  /     \   /     \   /     \   /     \   /     \
            //  5  4  3 ' 2  1  0 ' 9  8  7 ' 6  5  4 ' 3  2  1  .  0  0
            // 15 14 13  12 11 10   9  8  7   6  5  4   3  2  1  —  index 

            if (!options.HasFlag(AmountInWordsOptions.FractionalOnly))
            {
                if (options.HasFlag(AmountInWordsOptions.NumericWhole))
                {
                    wholeValue = whole;
                }
                else
                {
                    var builder = new List<string>();
                    builder.AddRange(ProcessingRank(whole, 15, _masculineUnits, _trillions)); // триллионы
                    builder.AddRange(ProcessingRank(whole, 12, _masculineUnits, _billions)); // миллиарды
                    builder.AddRange(ProcessingRank(whole, 9, _masculineUnits, _millions)); // миллионы
                    builder.AddRange(ProcessingRank(whole, 6, _feminineUnits, _thousands)); // тысячи
                    builder.AddRange(ProcessingRank(whole, 3, currencyInfo.WholeGender == CurrencyGender.Masculine ? _masculineUnits : _feminineUnits, null)); // сотни
                    wholeValue = string.Join(" ", builder);
                }
                wholeCurrency = GetLabel(whole, currencyInfo.Wholes);
            }

            if (!options.HasFlag(AmountInWordsOptions.WholeOnly))
            {
                if (options.HasFlag(AmountInWordsOptions.NumericFractional))
                {
                    fractionalValue = fractional;
                }
                else
                {
                    var builder = new List<string>();
                    builder.AddRange(ProcessingRank(fractional, 6, _feminineUnits, _thousands)); // тысячи
                    builder.AddRange(ProcessingRank(fractional, 3, currencyInfo.FractionalGender == CurrencyGender.Masculine ? _masculineUnits : _feminineUnits, null)); // сотни
                    fractionalValue = string.Join(" ", builder);
                }
                fractionalCurrency = GetLabel(fractional, currencyInfo.Fractionals);
            }

            var result = new List<string>();
            if (wholeValue != null)
                result.Add(string.Format(WholeValueTemplate, wholeValue) + WholeDelimiter + string.Format(WholeCurrencyTemplate, wholeCurrency));
            if (fractionalValue != null)
                result.Add(string.Format(FractionalValueTemplate, fractionalValue) + FractionalDelimiter + string.Format(FractionalCurrencyTemplate, fractionalCurrency));
            return string.Join(WholeFractionalDelimiter, result);
        }


        private static string GetLabel(string value, Dictionary<char, string> labels)
        {
            var wholeLen = value.Length;
            var preLast = wholeLen == 1 ? '0' : value.Substring(wholeLen - 2).First();
            return preLast == '1' ? labels['9'] : labels[value.Last()];
        }


        /// <summary>
        /// Обработка одного разряда целой части числа.
        /// </summary>
        /// <param name="value">Целая часть числа.</param>
        /// <param name="rankBorder">Левая граница разряда.</param>
        /// <param name="units">Метки единичных значений.</param>
        /// <param name="rankLabels">Метки разряда.</param>
        private IEnumerable<string> ProcessingRank(string value, int rankBorder, Dictionary<char, string> units, IDictionary<char, string> rankLabels)
        {
            var len = value.Length;
            var rightBorder = rankBorder - 2;

            if (len < rightBorder)
                return new List<string>();

            var rankLen = len >= rankBorder ? 3 : len - rightBorder + 1;
            var rank = value.Substring(len - (rightBorder + rankLen - 1), rankLen);
            var result = ParceRank(rank, units);
            if (rankLabels == null)
                return result;

            var preLast = rankLen == 1 ? '0' : rank.Substring(rankLen - 2).First();
            result.Add(preLast == '1' ? rankLabels['9'] : rankLabels[rank.Last()]);
            return result;
        }


        private List<string> ParceRank(string rank, Dictionary<char, string> units)
        {
            var result = new List<string>();
            var len = rank.Length;
            if (len == 3)
            {
                var key3 = rank[0];
                if (key3 != '0')
                    result.Add(_hundreds[key3]);
            }
            if (len >= 2)
            {
                var key2 = rank[len - 2];
                var key1 = rank[len - 1];
                switch (key2)
                {
                    case '0':
                        if (key1 != '0')
                            result.Add(units[key1]);
                        break;
                    case '1':
                        result.Add(_unitsOverTen[key2.ToString() + key1]);
                        break;
                    default:
                        result.Add(_tens[key2]);
                        if (key1 != '0')
                            result.Add(units[key1]);
                        break;
                }
            }
            if (len == 1)
                result.Add(units[rank[0]]);
            return result;
        }


        private class CurrencyInfo
        {

            public Dictionary<char, string> Fractionals { get; set; }
            public Dictionary<char, string> Wholes { get; set; }
            public CurrencyGender WholeGender { get; set; }
            public CurrencyGender FractionalGender { get; set; }
        }


        private enum CurrencyGender
        {
            /// <summary>
            /// Женский.
            /// </summary>
            Feminine,

            /// <summary>
            /// Мужской.
            /// </summary>
            Masculine
        }
    }
}