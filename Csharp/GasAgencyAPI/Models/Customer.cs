using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GasAgencyAPI.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [MaxLength(40)]
        public string? FullName { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(30)]
        public string? Address { get; set; }

        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
