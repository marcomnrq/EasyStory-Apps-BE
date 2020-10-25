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
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [SwaggerOperation(
            Summary = "List all Users",
            Description = "List of Users",
            OperationId = "ListAllUsers",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<UserResource>))]
        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetUsers()
        {

            var users = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }
        [SwaggerResponse(200, "User was found", typeof(UserResource))]
        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            var resource = _mapper.Map<User, UserResource>(user.Resource);
            return Ok(resource);
        }
        [SwaggerResponse(200, "User was created", typeof(UserResource))]
        [HttpPost]
        public async Task<IActionResult> PostUserAsync([FromBody] SaveUserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var user = _mapper.Map<SaveUserResource, User>(userResource);
            var result = await _userService.SaveUserAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);
            var userresource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userresource);

        }
        [SwaggerResponse(200, "User was updated", typeof(UserResource))]
        [HttpPut("id")]
        public async Task<IActionResult> PutUserAsync(long id, [FromBody] SaveUserResource saveUserResource)
        {
            var user = _mapper.Map<SaveUserResource, User>(saveUserResource);
            var result = await _userService.UpdateUserAsync(id, user);
            if (!result.Success)
                return BadRequest(result.Message);
            var userresource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userresource);
        }
        [SwaggerResponse(200, "User was removed", typeof(UserResource))]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUserAsync(long id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var userresource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userresource);
        }
    }
}
