using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConsultant.Models
{
    public class Office
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Address { get; set; }
        public string Timings { get; set; }
        [Phone]
        public string Phone { get; set; }

        public long ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public User Manager { get; set; }
    }
}