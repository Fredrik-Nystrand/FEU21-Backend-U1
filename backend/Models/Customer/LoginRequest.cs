﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models.Customer
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
