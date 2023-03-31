using System.ComponentModel.DataAnnotations;

namespace Domain.Models.WorkshopDomaine
{
    /// <summary>
    /// Represents a parameter associated with a <see cref="Workshop"/>.
    /// </summary>
    public class WorkshopParameter
    {
        /// <summary>
        /// The unique identifier for the workshop parameter.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier of the <see cref="Workshop"/> associated with this parameter.
        /// </summary>
        [Required]
        public int IdWorkshop { get; set; }

        /// <summary>
        /// The key of the parameter.
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// The <see cref="Workshop"/> associated with this parameter.
        /// </summary>
        public Workshop Workshop { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkshopParameter"/> class.
        /// </summary>
        public WorkshopParameter()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkshopParameter"/> class with the specified parameters.
        /// </summary>
        /// <param name="idWorkshop">The unique identifier of the <see cref="Workshop"/> associated with this parameter.</param>
        /// <param name="key">The key of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        public WorkshopParameter(int idWorkshop, string key, string value)
        {
            IdWorkshop = idWorkshop;
            Key = key;
            Value = value;
        }
    }

}
