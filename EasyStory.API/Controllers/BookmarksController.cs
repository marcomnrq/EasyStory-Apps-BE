using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyStory.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/")]
    public class BookmarksController : ControllerBase
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

        [SwaggerOperation(
            Summary = "Get Bookmarks By UserId",
            Description = "Get Bookmarks By UserId",
            OperationId = "GetBookmarksById"
        )]
        [SwaggerResponse(200, "Bookmark was found", typeof(PostResource))]
        [AllowAnonymous]
        [HttpGet("users/{userId}/bookmarks")]
        public async Task<IEnumerable<PostResource>> GetAllByUserIdAsync(long userId)
        {

                var bookmark = await _postService.ListByReaderIdAsync(userId);
                var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(bookmark);
                return resources;
            

        }

        [SwaggerOperation(
            Summary = "Get Bookmark by UserId And PostId",
            Description = "Get Bookmark by UserId And PostId",
            OperationId = "GetBoomarkByUserIdAndPostId"
        )]
        [SwaggerResponse(200, "List of Bookmarks for a User and Post", typeof(IEnumerable<BookmarkResource>))]
        [AllowAnonymous]
        [HttpGet("users/{userId}/posts/{postId}/bookmarks")]
        public async Task<IActionResult> GetBookmarkByUserIdAndPostId(long userId, long postId)
        {
            var bookmark = await _bookmarkService.GetByUserIdAndPostIdAsync(userId, postId);
            if (!bookmark.Success)
                return NotFound(bookmark.Message);
            var resource = _mapper.Map<Bookmark, BookmarkResource>(bookmark.Resource);
            return Ok(resource);
        }

        [SwaggerOperation(
            Summary = "Assign Bookmark",
            Description = "Assign Bookmark",
            OperationId = "AssignBookmark"
        )]
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

        [SwaggerOperation(
            Summary = "Unassign Bookmark",
            Description = "Unassign Bookmark",
            OperationId = "UnassignBookmark"
        )]
        [SwaggerResponse(200, "Bookmark was Unassigned", typeof(BookmarkResource))]
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