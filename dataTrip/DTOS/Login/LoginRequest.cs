﻿using System.ComponentModel.DataAnnotations;

namespace dataTrip.DTOS.Login
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
