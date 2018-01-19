using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZone.Data.Comparisons;


namespace PZone.Data.Tests.Comparisons
{
    [TestClass]
    public class StringComparison
    {
        [TestMethod]
        public void Cases()
        {
            var actual = StringComparisons.JaroWinklerDistance("Test", "test", new StringComparisonSettings { CaseSensitive = true });
            Assert.AreEqual(0.83, Math.Round(actual, 2));
            actual = StringComparisons.JaroWinklerDistance("Test", "test");
            Assert.AreEqual(1, Math.Round(actual, 2));

            actual = StringComparisons.JaroWinklerDistance("TEST", "TEST");
            Assert.AreEqual(1, actual);

            actual = StringComparisons.JaroWinklerDistance("Тест", "тест");
            Assert.AreEqual(1, Math.Round(actual,2));
            actual = Data.Comparisons.StringComparisons.JaroWinklerDistance("Тест", "тест", new StringComparisonSettings { CaseSensitive = true });
            Assert.AreEqual(0.83, Math.Round(actual, 2));

            actual = StringComparisons.JaroWinklerDistance("ТЕСТ", "ТЕСТ");
            Assert.AreEqual(1, actual);

            actual = StringComparisons.JaroWinklerDistance("ТЁСТ", "ТЕСТ");
            Assert.AreEqual(1, Math.Round(actual, 2));
            actual = StringComparisons.JaroWinklerDistance("Ёжик", "Ежик");
            Assert.AreEqual(1, Math.Round(actual, 2));
            actual = StringComparisons.JaroWinklerDistance("Ёжик", "Ежик", new StringComparisonSettings { AccentSensitive = true });
            Assert.AreEqual(0.83, Math.Round(actual, 2));
            actual = StringComparisons.JaroWinklerDistance("Ёжик", "Ежик", new StringComparisonSettings { CaseSensitive = true });
            Assert.AreEqual(1, Math.Round(actual, 2));

            actual = StringComparisons.JaroWinklerDistance("Ёжёк", "Ежек", new StringComparisonSettings { CaseSensitive = true });
            Assert.AreEqual(1, Math.Round(actual, 2));
        }
    }
}
