
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Common.Entities
{
    public class UserEntity : Entity
    {
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public bool IsAdmin { get; set; }
    }
}
