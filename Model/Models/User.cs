﻿using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}