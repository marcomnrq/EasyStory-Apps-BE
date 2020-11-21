using System;
using System.Collections.Generic;

namespace EasyStory.API.Domain.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }

        public List<PostHashtag> PostHashtags { get; set; }
        public IList<Bookmark> Posts { get; set; } = new List<Bookmark>();

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
