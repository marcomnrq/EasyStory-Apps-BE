using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/")]
    public class PostHashtagsController : ControllerBase
    {
        private readonly IHashtagService _hashtagService;
        private readonly IPostHashtagService _postHashtagService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostHashtagsController(IHashtagService hashtagService, IPostHashtagService postHashtagService, IPostService postService, IMapper mapper)
        {
            _hashtagService = hashtagService;
            _postHashtagService = postHashtagService;
            _postService = postService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Hashtags By PostId",
            Description = "List of Hashtags By PostId",
            OperationId = "ListAllHashtagsByPostId"
        )]
        [SwaggerResponse(200, "List of Hashtags By PostId", typeof(IEnumerable<HashtagResource>))]
        [HttpGet("posts/{postId}/hashtags")]
        public async Task<IEnumerable<HashtagResource>> GetAllByPostIdAsync(long postId)
        {
            var hashtags = await _hashtagService.ListByPostIdAsync(postId);
            var resources = _mapper.Map<IEnumerable<Hashtag>, IEnumerable<HashtagResource>>(hashtags);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List all Posts By HashtagId",
            Description = "List of Posts by HashtagId",
            OperationId = "ListAllPostsByHashtagId"
        )]
        [SwaggerResponse(200, "List of Posts By HashtagId", typeof(IEnumerable<PostResource>))]
        [HttpGet("hashtags/{hashtagId}/posts")]
        public async Task<IEnumerable<PostResource>> GetAllByHashtagIdAsync(long hashtagId)
        {
            var posts = await _postService.ListByHashtagIdAsync(hashtagId);
            var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
            return resources;
            
        }


        [SwaggerOperation(
            Summary = "Assign Hashtag to Post",
            Description = "Assign Hashtag to Post",
            OperationId = "AssignHashtagToPost"
        )]
        [SwaggerResponse(200, "Assign Hashtag to Post", typeof(IEnumerable<PostResource>))]
        [HttpPost("posts/{postId}/hashtags/{hashtagId}")]
        public async Task<IActionResult> AssignPostHashtag(long postId, long hashtagId)
        {
            var result = await _postHashtagService.AssignPostHashtagAsync(postId,hashtagId);
            if (!result.Success)
                return BadRequest(result.Message);

            var hashtagResource = _mapper.Map<Hashtag, HashtagResource>(result.Resource.Hashtag);
            return Ok(hashtagResource);

        }

        [SwaggerOperation(
            Summary = "Unassign Hashtag to Post",
            Description = "Unassign Hashtag to Post",
            OperationId = "UnassignHashtagToPost"
        )]
        [SwaggerResponse(200, "Unassign Hashtag to Post", typeof(IEnumerable<HashtagResource>))]
        [HttpDelete("posts/{postId}/hashtags/{hashtagId}")]
        public async Task<IActionResult> UnassignPostHashtag(long postId, long hashtagId)
        {
            var result = await _postHashtagService.UnassignPostHashtagAsync(postId, hashtagId);

            if (!result.Success)
                return BadRequest(result.Message);
            var hashtagResource = _mapper.Map<Hashtag, HashtagResource>(result.Resource.Hashtag);
            return Ok(hashtagResource);
        }
    }
}
