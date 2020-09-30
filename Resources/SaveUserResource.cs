using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasyStory.API.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        public int AccountBalance { get; set; }
        [Required]
        public int SubscriptionPrice { get; set; }

    }
}
