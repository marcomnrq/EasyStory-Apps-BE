using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services.Communications
{
    public class QualificationResponse : BaseResponse<Qualification>
    {
        public QualificationResponse(Qualification resource) : base(resource)
        {
        }

        public QualificationResponse(string message) : base(message)
        {
        }
    }
}
