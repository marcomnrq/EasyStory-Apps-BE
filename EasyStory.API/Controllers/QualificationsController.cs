using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Extensions;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api")]
    public class QualificationsController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;
        private readonly IMapper _mapper;

        public QualificationsController(IQualificationService qualificationService, IMapper mapper)
        {
            _qualificationService = qualificationService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List of Qualifications",
            Description = "List of Qualifications",
            OperationId = "ListQualifications",
            Tags = new[] { "Qualifications" }
        )]
        [SwaggerResponse(200, "List of Qualifications", typeof(IEnumerable<QualificationResource>))]
        [HttpGet("qualifications")]
        public async Task<IEnumerable<QualificationResource>> GetQualifications()
        {

            var qualifications = await _qualificationService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Qualification>, IEnumerable<QualificationResource>>(qualifications);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Qualification By PostId",
            Description = "Get Qualification By PostId",
            OperationId = "GetQualificationByPostId"
        )]
        [SwaggerResponse(200, "List of Qualifications for a Post", typeof(IEnumerable<QualificationResource>))]
        [HttpGet("posts/{postId}/qualifications")]
        public async Task<IEnumerable<QualificationResource>> GetQualificationsbyPostId(long postId)
        {

            var qualifications = await _qualificationService.ListByPostIdAsync(postId);
            var resources = _mapper.Map<IEnumerable<Qualification>, IEnumerable<QualificationResource>>(qualifications);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Qualification By UserId",
            Description = "Get Qualification By UserId",
            OperationId = "GetQualificationByUserId"
        )]
        [SwaggerResponse(200, "List of Qualifications for a User", typeof(IEnumerable<QualificationResource>))]
        [HttpGet("users/{userId}/qualifications")]
        public async Task<IEnumerable<QualificationResource>> GetQualificationsbyUserId(long userId)
        {

            var qualifications = await _qualificationService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Qualification>, IEnumerable<QualificationResource>>(qualifications);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Qualification by UserId And PostId",
            Description = "Get Qualification by UserId And PostId",
            OperationId = "GetQualificationByUserIdAndPostId"
        )]
        [SwaggerResponse(200, "List of Qualifications for a User and Post", typeof(IEnumerable<QualificationResource>))]
        [AllowAnonymous]
        [HttpGet("users/{userId}/posts/{postId}/qualifications")]
        public async Task<IActionResult> GetQualificationByUserIdAndPostId(long userId, long postId)
        {
            var qualification = await _qualificationService.GetByPostIdandUserIdAsync(userId, postId);
            if (!qualification.Success)
                return NotFound(qualification.Message);
            var resource = _mapper.Map<Qualification, QualificationResource>(qualification.Resource);
            return Ok(resource);
        }

        [SwaggerOperation(
            Summary = "Assign Qualification to Post",
            Description = "Assign Qualification to Post",
            OperationId = "AssignQualificationToPost"
        )]
        [SwaggerResponse(200, "Qualification was Assigned", typeof(IEnumerable<QualificationResource>))]
        [HttpPost("users/{userId}/posts/{postId}/qualifications")]
        public async Task<IActionResult> PostQualificationAsync(long userId, long postId, [FromBody] SaveQualificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var qualification = _mapper.Map<SaveQualificationResource, Qualification>(resource);

            var result = await _qualificationService.SaveQualificationAsync(userId, postId, qualification);

            if (!result.Success)
                return BadRequest(result.Message);

            var qualificationResource = _mapper.Map<Qualification, QualificationResource>(result.Resource);

            return Ok(qualificationResource);

        }

        [SwaggerOperation(
            Summary = "Unassign Qualification",
            Description = "Unassign Qualification",
            OperationId = "UnassignQualification"
        )]
        [SwaggerResponse(200, "Qualification was Unassigned", typeof(IEnumerable<CommentResource>))]
        [HttpDelete("users/{userId}/posts/{postId}/qualifications")]
        public async Task<IActionResult> DeleteQualificationAsync(long userId, long postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());



            var result = await _qualificationService.UnnasignQualificationAsync(userId, postId);

            if (!result.Success)
                return BadRequest(result.Message);

            var qualificationResource = _mapper.Map<Qualification, QualificationResource>(result.Resource);

            return Ok(qualificationResource);

        }

        [SwaggerOperation(
            Summary = "Update a Qualification",
            Description = "Update a Qualification",
            OperationId = "UpdateQualification"
        )]
        [SwaggerResponse(200, "Qualification was updated", typeof(IEnumerable<CommentResource>))]
        [HttpPut("users/{userId}/posts/{postId}/qualifications")]
        public async Task<IActionResult> UpdateQualificationAsync(long userId, long postId, [FromBody] SaveQualificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var qualification = _mapper.Map<SaveQualificationResource, Qualification>(resource);

            var result = await _qualificationService.UpdateQualificationAsync(userId, postId, qualification);

            if (!result.Success)
                return BadRequest(result.Message);

            var qualificationResource = _mapper.Map<Qualification, QualificationResource>(result.Resource);

            return Ok(qualificationResource);

        }
    }
}
