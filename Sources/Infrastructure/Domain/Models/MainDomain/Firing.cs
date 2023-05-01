using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    /// <summary>
    /// Represents a firing process in the system.
    /// </summary>
    public class Firing
    {
        /// <summary>
        /// Gets or sets the ID of the firing process.
        /// </summary>
        [CeramRequired]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the firing process.
        /// </summary>
        [CeramRequired]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the duration of the firing process in hours.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Gets or sets the total amount of energy used by the firing process in kilowatt hours.
        /// </summary>
        public double TotalKwH { get; set; }

        /// <summary>
        /// Gets or sets the cost per kilowatt hour of the firing process.
        /// </summary>
        public double CostKwH { get; set; }

        /// <summary>
        /// Gets or sets the product firings associated with the firing process.
        /// </summary>
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();

        public Firing Clone()
        {
            //Both Cloned and Existing Object Point to the Same Memory Location of the Address Object
            return (Firing)this.MemberwiseClone();
        }
    }
}