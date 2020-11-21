using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyStory.API.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string Token { get; set; }

        public IList<Post> Posts { get; set; } = new List<Post>();
        public IList<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

        public IList<Subscription> Subscribeds { get; set; } = new List<Subscription>();
        public IList<Subscription> Users { get; set; } = new List<Subscription>();

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
