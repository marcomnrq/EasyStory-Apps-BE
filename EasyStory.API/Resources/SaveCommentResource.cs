using System;
using System.ComponentModel.DataAnnotations;

namespace EasyStory.API.Resources
{
    public class SaveCommentResource
    {
        [Required]
        public string Content { get; set; }
    }
}
