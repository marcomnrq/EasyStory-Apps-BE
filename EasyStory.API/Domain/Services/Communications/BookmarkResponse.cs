using EasyStory.API.Domain.Models;

namespace EasyStory.API.Domain.Services.Communications
{
    public class BookmarkResponse : BaseResponse<Bookmark>
    {
        public BookmarkResponse(Bookmark resource) : base(resource)
        {
        }

        public BookmarkResponse(string message) : base(message)
        {
        }
    }
}