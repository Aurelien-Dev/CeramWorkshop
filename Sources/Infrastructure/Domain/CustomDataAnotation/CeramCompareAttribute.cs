using System.ComponentModel.DataAnnotations;

namespace Domain.CustomDataAnotation
{
    public class CeramCompareAttribute : CompareAttribute
    {
        public CeramCompareAttribute(string otherProperty) : base(otherProperty)
        {
            ErrorMessageResourceName = nameof(Resources.FormErrors.FieldCompare);
            ErrorMessageResourceType = typeof(Resources.FormErrors);
        }
    }
}
