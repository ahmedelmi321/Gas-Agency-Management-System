using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasAgencyAPI.Models
{
    [Table("GasCylinder")]
    public class GasCylinder
    {
        [Key]
        public int CylinderID { get; set; }

        [MaxLength(50)]
        public string? CylinderType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
