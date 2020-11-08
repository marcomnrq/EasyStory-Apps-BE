using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/")]
    public class BookmarksController: ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public BookmarksController(IBookmarkService bookmarkService, IUserService userService, IPostService postService, IMapper mapper)
        {
            _userService = userService;
            _postService = postService;
            _bookmarkService = bookmarkService;
            _mapper = mapper;
        }

        [HttpGet("users/{userId}/bookmarks")]
        public async Task<IEnumerable<PostResource>> GetAllByUserIdAsync(long userId)
        {
           var bookmark = await _postService.ListByReaderIdAsync(userId);
           var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(bookmark);
           return resources;
        }

        [SwaggerResponse(200, "Bookmark was Assigned", typeof(BookmarkResource))]
        [HttpPost("users/{userId}/posts/{postId}/bookmarks")]
        public async Task<IActionResult> AssignUserPost(long userId, long postId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var result = await _bookmarkService.AssignUserPostAsync(userId, postId);
            if (!result.Success)
                return BadRequest(result.Message);

            var postResource = _mapper.Map<Bookmark, BookmarkResource>(result.Resource);
            return Ok(postResource);           

        }

        [HttpDelete("users/{userId}/posts/{postId}/bookmarks")]
        public async Task<IActionResult> UnassignReaderPost(long userId, long postId)
        {
            var result = await _bookmarkService.UnassignUserPostAsync(userId, postId);

            if (!result.Success)
                return BadRequest(result.Message);
            var postResource = _mapper.Map<Bookmark, BookmarkResource>(result.Resource);
            return Ok(postResource);
        }
    }
}