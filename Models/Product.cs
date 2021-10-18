using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAgreementManagement.Models
{
    /// <summary>
    /// Represents product
    /// </summary>
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductGroup")]
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public string ProductDescription { get; set; }
        public string ProductNumber { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
