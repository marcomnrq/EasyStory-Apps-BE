using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/")]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentService _CommentService;

        private readonly IMapper _mapper;

        public CommentsController(ICommentService CommentService, IMapper mapper)
        {
            _CommentService = CommentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Comments",
            Description = "List of Comments",
            OperationId = "ListAllComments",
            Tags = new[] { "Comments" }
            )]
        [SwaggerResponse(200, "List of Comments", typeof(IEnumerable<CommentResource>))]
        [ProducesResponseType(typeof(IEnumerable<CommentResource>), 200)]
        [HttpGet("comments")]
        public async Task<IEnumerable<CommentResource>> GetAllAsync()
        {
            var Comments = await _CommentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Comment>,
                IEnumerable<CommentResource>>(Comments);
            return resources;
        }

        [SwaggerOperation(
           Summary = "List all Comments by User Id",
           Description = "List of Comments for a User",
           OperationId = "ListAllCommentsByUser",
           Tags = new[] { "Comments" }
       )]
        [SwaggerResponse(200, "List of Comments for a User", typeof(IEnumerable<CommentResource>))]
        [HttpGet("users/{userId}/comments")]
        public async Task<IEnumerable<CommentResource>> GetAllByUserIdAsync(long userId)
        {
            var comments = await _CommentService.ListByUserIdAsync(userId);
            var resources = _mapper
                .Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
            return resources;
        }

        [SwaggerOperation(
           Summary = "List all Comments by Post Id",
           Description = "List of Comments for a Post",
           OperationId = "ListAllCommentsByPost",
           Tags = new[] { "Comments" }
       )]
        [SwaggerResponse(200, "List of Comments for a Post", typeof(IEnumerable<CommentResource>))]
        [HttpGet("posts/{postId}/comments")]
        public async Task<IEnumerable<CommentResource>> GetAllByPostIdAsync(long postId)
        {
            var comments = await _CommentService.ListByPostIdAsync(postId);
            var resources = _mapper
                .Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Comment by UserId And PostId",
            Description = "Get Comment by UserId And PostId",
            OperationId = "GetCommentByUserIdAndPostId"
        )]
        [SwaggerResponse(200, "List of Comment for a User and Post", typeof(IEnumerable<CommentResource>))]
        [HttpGet("users/{userId}/posts/{postId}/comments")]
        public async Task<IActionResult> GetCommentByUserIdAndPostId(long userId, long postId)
        {
            var comment = await _CommentService.GetByUserIdAndPostIdAsync(userId, postId);
            if (!comment.Success)
                return NotFound(comment.Message);
            var resource = _mapper.Map<Comment, CommentResource>(comment.Resource);
            return Ok(resource);
        }

        [SwaggerOperation(
            Summary = "Create a Comment",
            Description = "Create a Comment",
            OperationId = "CreateComment",
            Tags = new[] { "Comments" }
        )]
        [SwaggerResponse(200, "Comment was created", typeof(CommentResource))]
        [AllowAnonymous]
        [HttpPost("users/{userId}/posts/{postId}/comments")]
        public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource, long userId, long postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Comment = _mapper.Map<SaveCommentResource, Comment>(resource);

            var result = await _CommentService.SaveAsync(Comment,userId,postId);

            if (!result.Success)
                return BadRequest(result.Message);

            var CommentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

            return Ok(CommentResource);

        }

        [SwaggerOperation(
            Summary = "Update a Comment",
            Description = "Update a Comment",
            OperationId = "UpdateComment",
            Tags = new[] { "Comments" }
        )]
        [SwaggerResponse(200, "Comment was updated", typeof(CommentResource))]
        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> PutAsync(long commentId, [FromBody] SaveCommentResource resource)
        {
            var Comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _CommentService.UpdateAsync(commentId, Comment);

            if (!result.Success)
                return BadRequest(result.Message);
            var CommentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(CommentResource);
        }

        [SwaggerResponse(200, "Comment was removed", typeof(CommentResource))]
        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> DeletePostAsync(long commentId)
        {
            var result = await _CommentService.DeleteAsync(commentId);
            if (!result.Success)
                return BadRequest(result.Message);
            var commentresource = _mapper.Map<Comment,CommentResource>(result.Resource);
            return Ok(commentresource);
        }
    }
}