using System;
using System.Collections.Generic;
using System.Globalization;


namespace PZone.Data
{
    /// <summary>
    /// Информация о валюте.
    /// </summary>
    public class CurrencyInfo
    {
        /// <summary>
        /// Язык всех текстов с информацией о валюте. По умолчанию используется язык текущего потока.
        /// </summary>
        public CultureInfo Culture { get; }


        /// <summary>
        /// Трехбуквенный код валюты в соответствии со стандартом ISO 4217.
        /// </summary>
        public string IsoCode { get; }



        /// <summary>
        /// Числовой код валюты в соответствии со стандартом ISO 4217.
        /// </summary>
        public int IsoNumericCode { get; private set; }


        /// <summary>
        /// Короткое наименование валюты? применяемое в обиходе.
        /// </summary> 
        public string Name { get; set; }


        /// <summary>
        /// Полное наименование валюты.
        /// </summary> 
        /// <remarks>
        /// Для русского языка полное наименование соответствует стандарту ОКВ (Общероссийский классификатор валют).
        /// </remarks>
        public string FullName { get; set; }


        /// <summary>
        /// Символьное обозначение валюты.
        /// </summary> 
        public string Symbol { get; set; }


        /// <summary>
        /// Курс валюты, по отношению к базовой.
        /// </summary> 
        public decimal ExchangeRate { get; set; }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="isoCode">Трехбуквенный код валюты в соответствии со стандартом ISO 4217.</param>
        /// <remarks>
        /// Все тексты возвращабтся на языке, установленном для текущего потока по умолчанию.
        /// </remarks>
        public CurrencyInfo(string isoCode) : this(isoCode, CultureInfo.CurrentUICulture)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="isoCode">Трехбуквенный код валюты в соответствии со стандартом ISO 4217.</param>
        /// <param name="cultureName">Язык всех текстов с информацией о валюте.</param>
        public CurrencyInfo(string isoCode, string cultureName) : this(isoCode, new CultureInfo(cultureName))
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="currency">Значение валюты из перечисления <see cref="Currencies"/>.</param>
        /// <remarks>
        /// Все тексты возвращабтся на языке, установленном для текущего потока по умолчанию.
        /// </remarks>
        public CurrencyInfo(Currencies currency) : this(currency.ToString(), CultureInfo.CurrentUICulture)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="currency">Значение валюты из перечисления <see cref="Currencies"/>.</param>
        /// <param name="culture">Язык всех текстов с информацией о валюте.</param>
        public CurrencyInfo(Currencies currency, CultureInfo culture) : this(currency.ToString(), culture)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="currency">Значение валюты из перечисления <see cref="Currencies"/>.</param>
        /// <param name="cultureName">Язык всех текстов с информацией о валюте.</param>
        public CurrencyInfo(Currencies currency, string cultureName) : this(currency.ToString(), new CultureInfo(cultureName))
        {
        }
        

        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="isoCode">Трехбуквенный код валюты в соответствии со стандартом ISO 4217.</param>
        /// <param name="culture">Язык всех текстов с информацией о валюте.</param>
        public CurrencyInfo(string isoCode, CultureInfo culture)
        {
            IsoCode = isoCode.Trim().ToUpper();
            Culture = culture;

            var info = new Dictionary<string, dynamic>
            {
                {
                    "RUB", new
                    {
                        IsoNumericCode = 643,
                        Translate = new Dictionary<string, dynamic>
                        {
                            { "EN", new { Name = "Ruble", FullName = "Russian Ruble", Symbol = "₽" } },
                            { "RU", new { Name = "Рубль", FullName = "Российский рубль", Symbol = "₽" } },
                            { "DE", new { Name = "Rubel", FullName = "Russischer Rubel", Symbol = "₽" } }
                        }
                    }
                },
                {
                    "USD", new
                    {
                        IsoNumericCode = 840,
                        Translate = new Dictionary<string, dynamic>
                        {
                            { "EN", new { Name = "Dollar", FullName = "US Dollar", Symbol = "$" } },
                            { "RU", new { Name = "Доллар", FullName = "Доллар США", Symbol = "$" } },
                            { "DE", new { Name = "Dollar", FullName = "US Dollar", Symbol = "$" } }
                        }
                    }
                },
                {
                    "EUR", new
                    {
                        IsoNumericCode = 978,
                        Translate = new Dictionary<string, dynamic>
                        {
                            { "EN", new { Name = "Euro", FullName = "Euro", Symbol = "€" } },
                            { "RU", new { Name = "Евро", FullName = "Евро", Symbol = "€" } },
                            { "DE", new { Name = "Euro", FullName = "Euro", Symbol = "€" } }
                        }
                    }
                }
            };

            if (!info.ContainsKey(IsoCode))
                throw new Exception($"Unknown currency with ISO-code \"{isoCode}\".");

            var currencyInfo = info[IsoCode];
            var cultureName = Culture.Name.ToUpper();
            var translate = (Dictionary<string, dynamic>)currencyInfo.Translate;
            if (!translate.ContainsKey(cultureName))
            {
                var culturePart = cultureName.Split('-')[0];
                if (translate.ContainsKey(culturePart))
                    cultureName = culturePart;
                else
                {
                    var currentCulture = CultureInfo.CurrentUICulture.Name.ToUpper();
                    if (translate.ContainsKey(currentCulture))
                        cultureName = currentCulture;
                    else
                    {
                        culturePart = currentCulture.Split('-')[0];
                        if (translate.ContainsKey(culturePart))
                            cultureName = culturePart;
                        else
                        {
                            currentCulture = CultureInfo.CurrentCulture.Name.ToUpper();
                            if (translate.ContainsKey(currentCulture))
                                cultureName = currentCulture;
                            else
                            {
                                culturePart = currentCulture.Split('-')[0];
                                cultureName = translate.ContainsKey(culturePart) ? culturePart : "EN";
                            }
                        }
                    }
                }
            }

            var currency = translate[cultureName];
            Name = currency.Name;
            FullName = currency.FullName;
            Symbol = currency.Symbol;
            IsoNumericCode = currencyInfo.IsoNumericCode;
        }
    }
}