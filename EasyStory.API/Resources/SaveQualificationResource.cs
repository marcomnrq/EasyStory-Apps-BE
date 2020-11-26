using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Resources
{
    public class SaveQualificationResource
    {
        [Required]
        public int Qualificate { get; set; }
    }
}
