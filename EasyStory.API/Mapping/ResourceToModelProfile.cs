﻿using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePostResource,Post>();
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveHashtagResource,Hashtag>();
            CreateMap<SaveBookmarkResource, Bookmark>();
            CreateMap<SaveCommentResource, Comment>();
            CreateMap<SaveSubscriptionResource, Subscription>();
            CreateMap<SaveQualificationResource, Qualification>();
        }
    }
}
