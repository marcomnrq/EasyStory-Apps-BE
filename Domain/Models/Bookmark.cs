using System;
using System.Collections.Generic;

namespace EasyStory.API.Domain.Models
{
    public class Bookmark
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public List<Post> Posts { get; set; }
    }
}
