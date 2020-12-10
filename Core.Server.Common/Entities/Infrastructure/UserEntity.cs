using Core.Server.Shared.Resources.User;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class UserEntity : Entity
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public UserStatus Status { get; set; }
    }
}
