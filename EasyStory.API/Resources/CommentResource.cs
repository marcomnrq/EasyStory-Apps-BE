using System;
namespace EasyStory.API.Resources
{
    public class CommentResource
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}
