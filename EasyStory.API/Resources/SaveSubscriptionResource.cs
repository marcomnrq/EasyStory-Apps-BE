using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Resources
{
    public class SaveSubscriptionResource
    {
        [Required]
        public float Price { get; set; }
    }
}
