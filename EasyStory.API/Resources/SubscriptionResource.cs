using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Resources
{
    public class SubscriptionResource
    {
        public float Price { get; set; }
        public long UserId { get; set; }
        public long SubscribedId { get; set; }
    }
}
