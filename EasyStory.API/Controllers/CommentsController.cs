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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        [HttpGet]
        public async Task<IEnumerable<CommentResource>> GetAllAsync()
        {
            var Comments = await _CommentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Comment>,
                IEnumerable<CommentResource>>(Comments);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Create a Comment",
            Description = "Create a Comment",
            OperationId = "CreateComment",
            Tags = new[] { "Comments" }
        )]
        [SwaggerResponse(200, "Comment was created", typeof(CommentResource))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Comment = _mapper.Map<SaveCommentResource, Comment>(resource);

            var result = await _CommentService.SaveAsync(Comment);

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
        [HttpPut("id")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
        {
            var Comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _CommentService.UpdateAsync(id, Comment);

            if (!result.Success)
                return BadRequest(result.Message);
            var CommentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(CommentResource);
        }

    }
}