using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
namespace EasyStory.API.Mapping
{
    public class ModelToResourceProfile<T,G> : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<T, G>();
        }
    }
}
