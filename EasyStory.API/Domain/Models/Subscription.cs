using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Models
{
    public class Subscription
    {
        public float Price { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }
        public User Subscribed { get; set; }
        public long SubscribedId { get; set; }
    }
}
