using System;
using System.Globalization;


namespace PZone.Data.Models
{
    /// <summary>
    /// Расширение функциональности элементов перечисления <see cref="Gender"/>.
    /// </summary>
    public static class GenderExtensions
    {
        /// <summary>
        /// Преобразование в букву на текущем языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола с учетом текущей культуры приложения. Буква строчная.
        /// </returns>
        public static string ToLetter(this Gender gender)
        {
            return ToLetter(gender, CultureInfo.CurrentCulture)?.ToUpper();
        }


        /// <summary>
        /// Преобразование в букву на указанном языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="culture">Культура.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на указанном языке. Буква строчная.
        /// </returns>
        public static string ToLetter(this Gender gender, CultureInfo culture)
        {
            switch (culture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return GetLetter(gender, "м", "ж");
                case "en":
                    return GetLetter(gender, "m", "f");
            }
            throw new NotImplementedException($"Не реализована обработка культуры {culture.Name}.");
        }


        /// <summary>
        /// Преобразование в букву на указанном языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="cultureName">Название культуры.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на указанном языке. Буква строчная.
        /// </returns>
        public static string ToLetter(this Gender gender, string cultureName)
        {
            return ToLetter(gender, new CultureInfo(cultureName));
        }

        /// <summary>
        /// Преобразование в букву на аглийском языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на английском языке. Буква строчная.
        /// </returns>
        public static string ToEnglishLetter(this Gender gender)
        {
            return ToLetter(gender, new CultureInfo("en-EN"))?.ToUpper();
        }


        /// <summary>
        /// Преобразование в заглавную букву на текущем языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола с учетом текущей культуры приложения. Буква заглавная.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender)
        {
            return ToLetter(gender)?.ToUpper();
        }

        /// <summary>
        /// Преобразование в заглавную букву на указанном языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="culture">Культура.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на указанном языке. Буква заглавная.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender, CultureInfo culture)
        {
            return ToLetter(gender, culture)?.ToUpper();
        }

        /// <summary>
        /// Преобразование в заглавную букву на указанном языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="cultureName">Название культуры.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на указанном языке. Буква заглавная.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender, string cultureName)
        {
            return ToLetter(gender, cultureName)?.ToUpper();
        }

        /// <summary>
        /// Преобразование в заглавную букву на аглийском языке.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <returns>
        /// Метод возвращает первую букву названия пола на английском языке. Буква заглавная.
        /// </returns>
        public static string ToUpperCaseEnglishLetter(this Gender gender)
        {
            return ToEnglishLetter(gender)?.ToUpper();
        }


        private static string GetLetter(Gender gender, string maleLetter, string femaleLetter)
        {
            return gender == Gender.Male ? maleLetter : gender == Gender.Female ? femaleLetter : null;
        }
    }
}