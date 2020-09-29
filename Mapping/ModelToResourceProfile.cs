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
            CreateMap<Comment, CommentResource>();

        }
    }
}
