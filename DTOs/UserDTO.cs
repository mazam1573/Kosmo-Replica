using System.ComponentModel.DataAnnotations;

namespace EduConsultant.DTOs
{
    public class CreateUserDto
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
    }
    public class ReadLongUserDto
    {
        public long Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public long? Zipcode { get; set; }
        public string? Status { get; set; }
        public string? Token { get; set; }
    }

    public class ReadShortUserDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
    }
}
