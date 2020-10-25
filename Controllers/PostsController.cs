using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/")]
    public class PostsController:ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        [SwaggerOperation(
            Summary = "List all Posts",
            Description = "List of Posts",
            OperationId = "ListAllPosts",
            Tags = new[] { "Posts" }
        )]
        [SwaggerResponse(200, "List of Posts", typeof(IEnumerable<PostResource>))]
        [HttpGet("posts")]
        public async Task<IEnumerable<PostResource>> GetPosts()
        {
            var posts = await _postService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
            return resources;
        }
        [SwaggerOperation(
            Summary = "List all Posts by User Id",
            Description = "List of Posts for a User",
            OperationId = "ListAllPostsByUser",
            Tags = new[] { "Posts" }
        )]
        [SwaggerResponse(200, "List of Posts for a User", typeof(IEnumerable<UserResource>))]
        [HttpGet("users/{userId}/posts")]
        public async Task<IEnumerable<PostResource>> GetAllByUserIdAsync(int userId)
        {
            var posts = await _postService.ListByUserIdAsync(userId);
            var resources = _mapper
                .Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
            return resources;
        }
        [SwaggerResponse(200, "Post was found", typeof(PostResource))]
        [HttpGet("posts/{id}")]
        public async Task<IActionResult> GetPostById(long id)
        {
            var post = await _postService.GetByIdAsync(id);
            var resource = _mapper.Map<Post, PostResource>(post.Resource);
            return Ok(resource);
        }
        [SwaggerResponse(200, "Post was created", typeof(PostResource))]
        [HttpPost("users/{userId}/posts")]
        public async Task<IActionResult> PostPostAsync([FromBody] SavePostResource postResource, long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var post = _mapper.Map<SavePostResource, Post>(postResource);
            var result = await _postService.SavePostAsync(post,userId);

            if (!result.Success)
                return BadRequest(result.Message);
            var postresource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(postresource);
            
        }
        [SwaggerResponse(200, "Post was updated", typeof(PostResource))]
        [HttpPut("posts/{id}")]
<<<<<<< Updated upstream:Controllers/PostsController.cs
        public async Task<IActionResult> PutPostAsync(long id, [FromBody] SavePostResource savePostResource, long userId)
=======
        public async Task<IActionResult> PutPostAsync(long id, [FromBody] SavePostResource savePostResource)
>>>>>>> Stashed changes:EasyStory.API/Controllers/PostsController.cs
        {
           
            var post = _mapper.Map<SavePostResource, Post>(savePostResource);
            var result = await _postService.UpdatePostAsync(id, post,userId);
            if (!result.Success)
                return BadRequest(result.Message);
            var postresource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(postresource);
        }
        [SwaggerResponse(200, "Post was removed", typeof(PostResource))]
        [HttpDelete("posts/{id}")]
        public async Task<IActionResult> DeletePostAsync(long id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var postresource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(postresource);
        }
    }
}
