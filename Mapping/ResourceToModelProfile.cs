using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Mapping
{
    public class ResourceToModelProfile<T,G>:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<T, G>();
        }
    }
}
