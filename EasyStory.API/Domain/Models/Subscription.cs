using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Models
{
    public class Subscription
    {
        public float Price { get; set; }

        public User Subscriber { get; set; }
        public long SubscriberId { get; set; }
        public User Subscribed { get; set; }
        public long SubscribedId { get; set; }
    }
}
