﻿using System.ComponentModel.DataAnnotations;

namespace EduConsultant.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
