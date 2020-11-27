using System;
namespace EasyStory.API.Domain.Models
{
    public class PostHashtag
    {
        public long PostId { get; set; }
        public Post Post { get; set; }

        public long HashtagId { get; set; }
        public Hashtag Hashtag { get; set; }
    }
}
