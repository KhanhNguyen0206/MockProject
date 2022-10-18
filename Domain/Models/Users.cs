using FluentValidation;
using System;

namespace Domain.Models
{
    public class Users
    {

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public DateTime? InsAt { get; set; }
    }
    
}
