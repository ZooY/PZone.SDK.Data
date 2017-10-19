namespace PZone.Data.Validation
{
    /// <summary>
    /// Результат проверки данных.
    /// </summary>
    public enum ValidationResultType
    {
        /// <summary>
        /// Проверка пройдена успешно.
        /// </summary>
        Success,

        /// <summary>
        /// Данные пусты.
        /// </summary>
        Empty,

        /// <summary>
        /// Некорректный формат.
        /// </summary>
        IncorrectFormat,

        /// <summary>
        /// Некорректное значение.
        /// </summary>
        IncorrectValue,

        /// <summary>
        /// Недопустимые символы.
        /// </summary>
        UnacceptableCharacters
    }
}