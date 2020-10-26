using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Resources
{
    public class SavePostResource
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(60)]
        public string Description { get; set; }
        [Required]
        [MaxLength(80)]
        public string Content { get; set; }
        
    }
}
