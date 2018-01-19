namespace PZone.Data.Models.ContactDetails
{
    /// <inheritdoc />
    public class Address : IAddress
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public string PostalCode { get; set; }

        /// <inheritdoc />
        public string Country { get; set; }

        /// <inheritdoc />
        public string Region { get; set; }

        /// <inheritdoc />
        public string Area { get; set; }

        /// <inheritdoc />
        public string City { get; set; }

        /// <inheritdoc />
        public string Settlement { get; set; }

        /// <inheritdoc />
        public string Street { get; set; }

        /// <inheritdoc />
        public string House { get; set; }

        /// <inheritdoc />
        public string Block { get; set; }

        /// <inheritdoc />
        public string Flat { get; set; }
    }
}