using System.Linq;
using System.Text.RegularExpressions;


namespace PZone.Data.Validation
{
    /// <summary>
    /// Валидация российских документов.
    /// </summary>
    public static class RussianPersonalDataValidation
    {
        /// <summary>
        /// Валидация паспорта.
        /// </summary>
        /// <param name="serie">Серия паспорта.</param>
        /// <param name="number">Номер паспорта.</param>
        /// <returns>
        /// Метод возвращает результат валидации серии и номера паспорта.
        /// </returns>
        public static ValidationResult PassportNumber(string serie, string number)
        {
            // Проверка заполненности
            if (string.IsNullOrWhiteSpace(serie))
                return new ValidationResult(ValidationResultType.Empty, Localization.Error.PersonalDataValidationEmptySerie);
            if (string.IsNullOrWhiteSpace(number))
                return new ValidationResult(ValidationResultType.Empty, Localization.Error.PersonalDataValidationEmptyNumber);
            // Проверка длины
            if (serie.Length != 4)
                return new ValidationResult(ValidationResultType.IncorrectFormat, string.Format(Localization.Error.PersonalDataValidationLengthSerieX, 4));
            if (number.Length != 6)
                return new ValidationResult(ValidationResultType.IncorrectFormat, string.Format(Localization.Error.PersonalDataValidationLengthNumberX, 6));
            // Проверка на допустимые символы
            var notNumbersRegexp = new Regex(@"[^\d]");
            if (notNumbersRegexp.IsMatch(serie))
                return new ValidationResult(ValidationResultType.UnacceptableCharacters, Localization.Error.PersonalDataValidationCharactersSerieNumerics);
            if (notNumbersRegexp.IsMatch(number))
                return new ValidationResult(ValidationResultType.UnacceptableCharacters, Localization.Error.PersonalDataValidationCharactersNumberNumerics);

            return new ValidationResult(ValidationResultType.Success);
        }


        /// <summary>
        /// Валидация паспорта. 
        /// </summary>
        /// <param name="serieNumber">Серия и номер паспорта.</param>
        /// <returns>
        /// Метод возвращает результат валидации серии и номера паспорта.
        /// </returns>
        public static ValidationResult PassportNumber(string serieNumber)
        {
            // Проверка заполненности
            if (string.IsNullOrWhiteSpace(serieNumber))
                return new ValidationResult(ValidationResultType.Empty, Localization.Error.PersonalDataValidationEmptySerieNumber);

            var parts = serieNumber.Trim().Split(' ');
            if (parts.Length != 2)
                return new ValidationResult(ValidationResultType.IncorrectFormat, Localization.Error.PersonalDataValidationIncorrectFormatSerieNumberSpace);

            return PassportNumber(parts[0], parts[1]);
        }


        /// <summary>
        /// Валидация СНИЛС.
        /// </summary>
        /// <param name="snils">СНИЛС.</param>
        /// <returns>
        /// Метод возвращает результат валидации СНИЛС.
        /// </returns>
        public static ValidationResult Snils(string snils)
        {
            // Проверка заполненности
            if (string.IsNullOrWhiteSpace(snils))
                return new ValidationResult(ValidationResultType.Empty, Localization.Error.PersonalDataValidationEmptySnils);
            // Форматный контроль
            if (!new Regex(@"\d{3}-\d{3}-\d{3} \d{2}").IsMatch(snils))
                return new ValidationResult(ValidationResultType.IncorrectFormat, Localization.Error.PersonalDataValidationIncorrectFormatSnils);
            // Логический контроль
            var n = new Regex(@"\d").Matches(snils).Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();
            var check = n[9] * 10 + n[10];
            var sum = n[0] * 9 + n[1] * 8 + n[2] * 7 + n[3] * 6 + n[4] * 5 + n[5] * 4 + n[6] * 3 + n[7] * 2 + n[8] * 1;
            if ((sum == 100 || sum == 101) && check == 0)
                return new ValidationResult(ValidationResultType.Success);
            if (sum > 101)
                sum = sum % 101;
            if (sum == check || (sum == 100 && check == 0))
                return new ValidationResult(ValidationResultType.Success);

            return new ValidationResult(ValidationResultType.IncorrectValue, Localization.Error.PersonalDataValidationIncorrectValueSnils);
        }


        /// <summary>
        /// Валидация ИНН.
        /// </summary>
        /// <param name="inn">ИНН.</param>
        /// <returns>
        /// Метод возвращает результат валидации ИНН.
        /// </returns>
        public static ValidationResult Inn(string inn)
        {
            // Проверка заполненности
            if (string.IsNullOrWhiteSpace(inn))
                return new ValidationResult(ValidationResultType.Empty, Localization.Error.PersonalDataValidationEmptyInn);
            // Проверка длины
            var n = new Regex(@"\d").Matches(inn).Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();
            if (n.Length != 10 && n.Length != 12)
                return new ValidationResult(ValidationResultType.IncorrectFormat, Localization.Error.PersonalDataValidationIncorrectFormatInn);
            // Логический контроль

            //... для ИНН в 10 знаков
            if ((n.Length == 10) && (n[9] == ((2 * n[0] + 4 * n[1] + 10 * n[2] + 3 * n[3] + 5 * n[4] + 9 * n[5] + 4 * n[6] + 6 * n[7] + 8 * n[8]) % 11) % 10))
                return new ValidationResult(ValidationResultType.Success);
            //... для ИНН в 12 знаков
            if ((n.Length == 12) && ((n[10] == ((7 * n[0] + 2 * n[1] + 4 * n[2] + 10 * n[3] + 3 * n[4] + 5 * n[5] + 9 * n[6] + 4 * n[7] + 6 * n[8] + 8 * n[9]) % 11) % 10) && (n[11] == ((3 * n[0] + 7 * n[1] + 2 * n[2] + 4 * n[3] + 10 * n[4] + 3 * n[5] + 5 * n[6] + 9 * n[7] + 4 * n[8] + 6 * n[9] + 8 * n[10]) % 11) % 10)))
                return new ValidationResult(ValidationResultType.Success);

            return new ValidationResult(ValidationResultType.IncorrectValue, Localization.Error.PersonalDataValidationIncorrectValueInn);
        }


        /// <summary>
        /// Валидация фамилии.
        /// </summary>
        /// <param name="name">Фамилия.</param>
        /// <returns>
        /// Метод возвращает результат валидации фамилии.
        /// </returns>
        public static ValidationResult LastName(string name)
        {
            return ValidateName(name, Localization.Error.PersonalDataValidationEmptyLastName, Localization.Error.PersonalDataValidationCharactersLastName);
        }


        /// <summary>
        /// Валидация имени.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <returns>
        /// Метод возвращает результат валидации имени.
        /// </returns>
        public static ValidationResult FirstName(string name)
        {
            return ValidateName(name, Localization.Error.PersonalDataValidationEmptyFirstName, Localization.Error.PersonalDataValidationCharactersFirstName);
        }


        /// <summary>
        /// Валидация отчества.
        /// </summary>
        /// <param name="name">Отчество.</param>
        /// <returns>
        /// Метод возвращает результат валидации отчества.
        /// </returns>
        public static ValidationResult MiddleName(string name)
        {
            return ValidateName(name, Localization.Error.PersonalDataValidationEmptyMiddleName, Localization.Error.PersonalDataValidationCharactersMiddleName);
        }


        private static ValidationResult ValidateName(string name, string emptyError, string charactersError)
        {
            // Проверка заполненности
            if (string.IsNullOrWhiteSpace(name))
                return new ValidationResult(ValidationResultType.Empty, emptyError);
            // Проверка на допустимые символы
            if (new Regex(@"[\da-z]", RegexOptions.IgnoreCase).IsMatch(name))
                return new ValidationResult(ValidationResultType.UnacceptableCharacters, charactersError);

            return new ValidationResult(ValidationResultType.Success);
        }
    }
}