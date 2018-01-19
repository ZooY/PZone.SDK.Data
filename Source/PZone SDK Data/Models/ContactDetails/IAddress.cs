using Newtonsoft.Json;


namespace PZone.Data.Models.ContactDetails
{
    /// <summary>
    /// Адресная информация.
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Идентификатор адреса.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        string Id { get; set; }

        /// <summary>
        /// Почтовый индекс.
        /// </summary>
        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        string PostalCode { get; set; }

        /// <summary>
        /// Страна.
        /// </summary>
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        string Country { get; set; }

        /// <summary>
        /// Регион.
        /// </summary>
        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        string Region { get; set; }

        /// <summary>
        /// Район.
        /// </summary>
        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        string Area { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        string City { get; set; }

        /// <summary>
        /// Населенный пункт.
        /// </summary>
        [JsonProperty("settlement", NullValueHandling = NullValueHandling.Ignore)]
        string Settlement { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        string Street { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        [JsonProperty("house", NullValueHandling = NullValueHandling.Ignore)]
        string House { get; set; }

        /// <summary>
        /// Строение.
        /// </summary>
        [JsonProperty("block", NullValueHandling = NullValueHandling.Ignore)]
        string Block { get; set; }

        /// <summary>
        /// Квартира.
        /// </summary>
        [JsonProperty("flat", NullValueHandling = NullValueHandling.Ignore)]
        string Flat { get; set; }
    }
}