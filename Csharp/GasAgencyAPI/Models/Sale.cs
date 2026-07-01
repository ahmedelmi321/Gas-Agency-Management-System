using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasAgencyAPI.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }

        public int CustomerID { get; set; }
        public int CylinderID { get; set; }
        public int QuantitySold { get; set; }
        public DateOnly SaleDate { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        [ForeignKey("CylinderID")]
        public GasCylinder? GasCylinder { get; set; }
    }
}
