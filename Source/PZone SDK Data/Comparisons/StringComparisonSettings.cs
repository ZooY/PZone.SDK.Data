namespace PZone.Data.Comparisons
{
    /// <inheritdoc />
    public class StringComparisonSettings : IStringComparisonSettings
    {
        /// <inheritdoc />
        public bool CaseSensitive { get; set; }

        /// <inheritdoc />
        public bool AccentSensitive { get; set; }
    }
}