using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAgreementManagement.Models
{
    /// <summary>
    /// Represents Agreement
    /// </summary>
    public class Agreement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey("ProductGroup")]
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ProductPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal NewPrice { get; set; }

        public bool Active { get; set; }
    }
}
