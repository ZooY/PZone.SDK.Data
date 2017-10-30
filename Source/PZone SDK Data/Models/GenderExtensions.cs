using System;
using System.Globalization;


namespace PZone.Data.Models
{
    /// <summary>
    /// ���������� ���������������� ��������� ������������ <see cref="Gender"/>.
    /// </summary>
    public static class GenderExtensions
    {
        /// <summary>
        /// �������������� � ����� �� ������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� � ������ ������� �������� ����������. ����� ��������.
        /// </returns>
        public static string ToLetter(this Gender gender)
        {
            return ToLetter(gender, CultureInfo.CurrentCulture)?.ToUpper();
        }


        /// <summary>
        /// �������������� � ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <param name="culture">��������.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ��������� �����. ����� ��������.
        /// </returns>
        public static string ToLetter(this Gender gender, CultureInfo culture)
        {
            switch (culture.TwoLetterISOLanguageName)
            {
                case "ru":
                    return GetLetter(gender, "�", "�");
                case "en":
                    return GetLetter(gender, "m", "f");
            }
            throw new NotImplementedException($"�� ����������� ��������� �������� {culture.Name}.");
        }


        /// <summary>
        /// �������������� � ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <param name="cultureName">�������� ��������.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ��������� �����. ����� ��������.
        /// </returns>
        public static string ToLetter(this Gender gender, string cultureName)
        {
            return ToLetter(gender, new CultureInfo(cultureName));
        }

        /// <summary>
        /// �������������� � ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ���������� �����. ����� ��������.
        /// </returns>
        public static string ToEnglishLetter(this Gender gender)
        {
            return ToLetter(gender, new CultureInfo("en-EN"))?.ToUpper();
        }


        /// <summary>
        /// �������������� � ��������� ����� �� ������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� � ������ ������� �������� ����������. ����� ���������.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender)
        {
            return ToLetter(gender)?.ToUpper();
        }

        /// <summary>
        /// �������������� � ��������� ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <param name="culture">��������.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ��������� �����. ����� ���������.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender, CultureInfo culture)
        {
            return ToLetter(gender, culture)?.ToUpper();
        }

        /// <summary>
        /// �������������� � ��������� ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <param name="cultureName">�������� ��������.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ��������� �����. ����� ���������.
        /// </returns>
        public static string ToUpperCaseLetter(this Gender gender, string cultureName)
        {
            return ToLetter(gender, cultureName)?.ToUpper();
        }

        /// <summary>
        /// �������������� � ��������� ����� �� ��������� �����.
        /// </summary>
        /// <param name="gender">���.</param>
        /// <returns>
        /// ����� ���������� ������ ����� �������� ���� �� ���������� �����. ����� ���������.
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