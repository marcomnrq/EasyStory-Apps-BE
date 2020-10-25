using System;
namespace EasyStory.API.Domain.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
