namespace Domain.Models
{
    public enum Location { Server, ImgBB }
    public class ImageInstruction
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty;
        public string MediumUrl { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Principalement utilisé pour définir le lien de suppression du fichier
        /// </summary>
        public Location FileLocation { get; set; }

        public Product? ProductAssociate { get; set; }

        public ImageInstruction(string url, string thumbUrl, string mediumUrl)
        {
            Url = url;
            ThumbUrl = thumbUrl;
            MediumUrl = mediumUrl;
        }
    }
}