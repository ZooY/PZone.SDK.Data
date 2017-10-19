namespace PZone.Data.Validation
{
    /// <summary>
    /// Результат валидации.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Результат валидации.
        /// </summary>
        public ValidationResultType Result { get; }
        /// <summary>
        /// Причина резальтата.
        /// </summary>
        public string Reason { get; }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="result">Результат валидации.</param>
        public ValidationResult(ValidationResultType result)
        {
            Result = result;
        }


        /// <summary> 
        /// Конструтор класса.
        /// </summary>
        /// <param name="result">Результат валидации.</param>
        /// <param name="reason">Причина резальтата.</param>
        public ValidationResult(ValidationResultType result, string reason) : this(result)
        {
            Reason = reason;
        }
    }
}
