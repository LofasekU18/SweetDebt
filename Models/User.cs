﻿using System.ComponentModel.DataAnnotations;

namespace SweetDebt.Models
{

    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
    }
}

