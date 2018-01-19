namespace PZone.Data.Comparisons
{
    /// <summary>
    /// Настройки сравнения строк.
    /// </summary>
    public interface IStringComparisonSettings
    {
        /// <summary>
        /// Установливает или получает флаг чувствительности к регистру символов.
        /// </summary>
        /// <remarks>
        /// По умолчанию <c>false</c> - регистр символов не учитывается.
        /// </remarks>
        bool CaseSensitive { get; set; }

        /// <summary>
        /// Установливает или получает флаг чувствительности к диакритическим знакам.
        /// </summary>
        /// <remarks>
        /// <pre>Работает только для русского языка (буквы ё->е).</pre>
        /// <pre>По умолчанию <c>false</c> - регистр символов не учитывается.</pre>
        /// </remarks>
        bool AccentSensitive { get; set; }
    }
}