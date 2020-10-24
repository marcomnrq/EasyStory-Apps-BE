using System;
using System.ComponentModel.DataAnnotations;

namespace EasyStory.API.Resources
{
    public class SaveCommentResource
    {
        [Required]
        [MaxLength(150)]
        public string Content { get; set; }
    }
}
