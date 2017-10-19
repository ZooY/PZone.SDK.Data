using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZone.Data.Validation;


namespace PZone.Data.Tests.Validation
{
    [TestClass]
    public class RussianPersonalDataValidationTest
    {
        [TestMethod]
        public void PassportNumberNull()
        {
            var actual = RussianPersonalDataValidation.PassportNumber(null);
            Assert.AreEqual(ValidationResultType.Empty, actual.Result);
            Assert.AreEqual("Серия и номер пусты.", actual.Reason);

            var culture = "en-EN";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            actual = RussianPersonalDataValidation.PassportNumber(null);
            Assert.AreEqual(ValidationResultType.Empty, actual.Result);
            Assert.AreEqual("The serie and number are empty.", actual.Reason);
        }


        [TestMethod]
        public void SnilsIncorrectValue()
        {
            var actual = RussianPersonalDataValidation.Snils("123-456-789 01");
            Assert.AreEqual(ValidationResultType.IncorrectValue, actual.Result);
            Assert.AreEqual("Некорректный СНИЛС.", actual.Reason);
        }
    }
}