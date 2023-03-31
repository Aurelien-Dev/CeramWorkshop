using Domain.Models.MainDomain;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.WorkshopDomaine
{
    /// <summary>
    /// Represents a workshop in the system.
    /// </summary>
    public class Workshop
    {
        /// <summary>
        /// The unique identifier of the workshop.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The email address of the workshop.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The username of the workshop.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// The hashed password of the workshop.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// The salt used in password hashing.
        /// </summary>
        [Required]
        public byte[] Salt { get; set; }

        /// <summary>
        /// The name of the workshop.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The culture of the workshop (e.g. "fr" for French).
        /// </summary>
        [Required]
        public string Culture { get; set; }

        /// <summary>
        /// The logo of the workshop, if any.
        /// </summary>
        public string? Logo { get; set; }

        /// <summary>
        /// The products offered by the workshop.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// The parameters for the workshop.
        /// </summary>
        public ICollection<WorkshopParameter> WorkshopParameters { get; set; } = new List<WorkshopParameter>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Workshop"/> class.
        /// </summary>
        public Workshop() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workshop"/> class with the specified parameters and default french culture.
        /// </summary>
        /// <param name="name">The name of the workshop.</param>
        /// <param name="logo">The logo of the workshop, if any.</param>
        /// <param name="email">The email address of the workshop.</param>
        /// <param name="userName">The username of the workshop.</param>
        /// <param name="password">The password of the workshop.</param>
        /// <param name="salt">The salt used in password hashing.</param>
        public Workshop(string name, string? logo, string email, string userName, string password, byte[] salt)
        {
            Name = name;
            Logo = logo;
            Email = email;
            UserName = userName;
            PasswordHash = password;
            Salt = salt;
            Culture = "fr";
        }
    }

}