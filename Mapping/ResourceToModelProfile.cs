using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Resources;

namespace EasyStory.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveBookmarkResource, Bookmark>();
        }
    }
}
