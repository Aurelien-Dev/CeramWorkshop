using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public class ProductFiring
    {
        [CeramRequired]
        public int Id { get; set; }

        public int IdFiring { get; set; }

        public int IdProduct { get; set; }

        /// <summary>
        /// Kw/h utilisé pour la cuisson, par défaut on prend le kw/h de la cuisson linké, 
        /// mais il peut être modifié pour cet élément précis
        /// </summary>
        public double TotalKwH { get; set; }

        /// <summary>
        /// Cout de la cuisson, par défaut on prend le coup de la cuisson linké, 
        /// mais il peut être modifié pour cet élément précis
        /// </summary>
        public double CostKwH { get; set; }

        /// <summary>
        /// Nombres de produits possible de mettre dans une cuisson
        /// </summary>
        public int NumberProducts { get; set; }

        public Firing Firing { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public ProductFiring(int idFiring, int idProduct, double totalKwH, double costKwH, int numberProducts)
        {
            IdFiring = idFiring;
            IdProduct = idProduct;
            TotalKwH = totalKwH;
            CostKwH = costKwH;
            NumberProducts = numberProducts;
        }

        public ProductFiring(int idFiring, int idProduct, double totalKwH, double costKwH)
        {
            IdFiring = idFiring;
            IdProduct = idProduct;
            TotalKwH = totalKwH;
            CostKwH = costKwH;
            NumberProducts = 1;
        }
    }
}