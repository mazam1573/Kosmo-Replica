using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConsultant.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public long? Zipcode { get; set; }

        public string? Status { get; set; }
        [InverseProperty("Manager")]
        public ICollection<Office>? Offices { get; set; }
    }
}
