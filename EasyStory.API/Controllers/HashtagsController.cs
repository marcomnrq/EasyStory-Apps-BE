using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Controllers
{

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HashtagsController: ControllerBase
    {
        private readonly IHashtagService _hashtagService;
        private readonly IMapper _mapper;

        public HashtagsController(IHashtagService hashtagService, IMapper mapper)
        {
            _hashtagService = hashtagService;
            _mapper = mapper;
        }
        [SwaggerOperation(
            Summary = "List all Hashtags",
            Description = "List of Hashtags",
            OperationId = "ListAllHashtags",
            Tags  = new[] { "Hashtags" }
        )]
        [SwaggerResponse(200, "List of Hashtags", typeof(IEnumerable<HashtagResource>))]
        [HttpGet]
        public async Task<IEnumerable<HashtagResource>> GetHashtags()
        {
            
            var hashtags = await _hashtagService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Hashtag>, IEnumerable<HashtagResource>>(hashtags);
            return resources;
        }
        [SwaggerResponse(200, "hashTag was found", typeof(HashtagResource))]
        [HttpGet("{hashtagId}")]
        public async Task<IActionResult> GetHashtagById(long hashtagId)
        {
            var hashtag = await _hashtagService.GetByIdAsync(hashtagId);
            var resource = _mapper.Map<Hashtag, HashtagResource>(hashtag.Resource);
            return Ok(resource);
        }
        [SwaggerResponse(200, "hashTag was created", typeof(HashtagResource))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostHashtagAsync([FromBody]SaveHashtagResource hashtagResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var hashtag = _mapper.Map<SaveHashtagResource, Hashtag>(hashtagResource);
            var result = await _hashtagService.SaveHashtagAsync(hashtag);

            if (!result.Success)
                return BadRequest(result.Message);
            var hashtagresource = _mapper.Map<Hashtag, HashtagResource>(result.Resource);
            return Ok(hashtagresource);

        }
        [SwaggerResponse(200, "Hashtag was updated", typeof(HashtagResource))]
        [HttpPut("{hashtagId}")]
        public async Task<IActionResult> PutHashtagAsync(long id, [FromBody]SaveHashtagResource saveHashtagResource)
        {
            var hashtag = _mapper.Map<SaveHashtagResource, Hashtag>(saveHashtagResource);
            var result = await _hashtagService.UpdateHashtagAsync(id, hashtag);
            if (!result.Success)
                return BadRequest(result.Message);
            var hashtagresource = _mapper.Map<Hashtag, HashtagResource>(result.Resource);
            return Ok(hashtagresource);
        }
        [SwaggerResponse(200, "Hashtag was removed", typeof(HashtagResource))]
        [HttpDelete("{hashtagId}")]
        public async Task<IActionResult> DeleteHashtagAsync(long id)
        {
            var result = await _hashtagService.DeleteHashtagAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var hashtagresource = _mapper.Map<Hashtag, HashtagResource>(result.Resource);
            return Ok(hashtagresource);
        }
    }
}
