using System;
using System.Collections.Generic;
namespace EasyStory.API.Domain.Models
{
    public class Hashtag
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<PostHashtag> PostHashtags { get; set; }

    }
}
