using System;


namespace PZone.Data.Helpers
{
    /// <summary>
    /// Параметры разделения числа.
    /// </summary>
    [Flags]
    public enum NumberSplitOptions
    {
        /// <summary>
        /// Удаление лишних нулей (ведущие у целой части и конечные у дробной).
        /// </summary>
        RemoveWasteZeros
    }
}