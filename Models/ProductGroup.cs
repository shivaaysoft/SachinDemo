using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAgreementManagement.Models
{
    /// <summary>
    /// Represents product group
    /// </summary>
    public class ProductGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string GroupDescription { get; set; }
        public string GroupCode { get; set; }
        public bool Active { get; set; }
    }
}
