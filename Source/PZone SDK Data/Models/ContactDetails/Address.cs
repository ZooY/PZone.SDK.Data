namespace PZone.Data.Models.ContactDetails
{
    /// <summary>
    /// Адресная информация.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Почтовый индекс.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Страна.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Регион.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Район.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Населенный пункт.
        /// </summary>
        public string Settlement { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Строение.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Квартира.
        /// </summary>
        public string Flat { get; set; }
    }
}
