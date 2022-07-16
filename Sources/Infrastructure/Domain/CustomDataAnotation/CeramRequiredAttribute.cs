using System.ComponentModel.DataAnnotations;

namespace Domain.CustomDataAnotation
{
    public class CeramRequiredAttribute : RequiredAttribute
    {
        public CeramRequiredAttribute() : base()
        {
            ErrorMessageResourceName = nameof(Resources.FormErrors.FieldRequired);
            ErrorMessageResourceType = typeof(Resources.FormErrors);
        }
    }
}