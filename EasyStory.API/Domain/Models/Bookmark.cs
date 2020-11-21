using System;
using System.Collections.Generic;

namespace EasyStory.API.Domain.Models
{
    public class Bookmark
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }
    }
}
