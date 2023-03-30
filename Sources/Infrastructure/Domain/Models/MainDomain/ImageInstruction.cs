using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public enum Location { Server, ImgBB }
    public class ImageInstruction
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public int IdProduct { get; set; }

        [CeramRequired]
        public string Url { get; set; }

        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Principalement utilisé pour définir le lien de suppression du fichier
        /// </summary>
        public Location FileLocation { get; set; }

        public Product? ProductAssociate { get; set; }

        public ImageInstruction() { }
    }
}