using System.ComponentModel.DataAnnotations;

namespace Domain.CustomDataAnotation
{
    /// <summary>
    /// An extension of the standard <see cref="CompareAttribute"/> that provides
    /// a default error message for comparing two values.
    /// </summary>
    public class CeramCompareAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CeramCompareAttribute"/> class with
        /// the name of the property to compare against.
        /// </summary>
        /// <param name="otherProperty">The name of the property to compare against.</param>
        public CeramCompareAttribute(string otherProperty) : base(otherProperty)
        {
            ErrorMessageResourceName = nameof(Resources.FormErrors.FieldCompare);
            ErrorMessageResourceType = typeof(Resources.FormErrors);
        }
    }
}