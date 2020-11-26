using System;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;

namespace EasyStory.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Post, PostResource>();
            CreateMap<User, UserResource>();
            CreateMap<Hashtag,HashtagResource>();
            CreateMap<Bookmark, BookmarkResource>();
            CreateMap<Comment, CommentResource>();
            CreateMap<Subscription, SubscriptionResource>();
            CreateMap<Qualification, QualificationResource>();

        }
    }
}
