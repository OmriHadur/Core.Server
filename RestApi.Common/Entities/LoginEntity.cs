using System;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Common.Entities
{
    public class LoginEntity : Entity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
    }
}
