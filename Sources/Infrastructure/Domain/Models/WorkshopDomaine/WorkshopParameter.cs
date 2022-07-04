using System.ComponentModel.DataAnnotations;

namespace Domain.Models.WorkshopDomaine
{
    public class WorkshopParameter
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdWorkshop { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public Workshop Worksĥop { get; set; } = default!;

        public WorkshopParameter()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        public WorkshopParameter(int idWorkshop, string key, string value)
        {
            IdWorkshop = idWorkshop;
            Key = key;
            Value = value;
        }
    }
}
