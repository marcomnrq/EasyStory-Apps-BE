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
    [Route("api/")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionService subscriptionService, IUserService userService ,IMapper mapper)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Subscriptions",
            Description = "List of Subscriptions",
            OperationId = "ListAllSubscriptions",
            Tags = new[] { "Subscriptions" }
        )]
        [SwaggerResponse(200, "List of Subscriptions", typeof(IEnumerable<SubscriptionResource>))]
        [HttpGet("subscriptions")]
        public async Task<IEnumerable<SubscriptionResource>> GetSubscriptions()
        {
            var subscriptions = await _subscriptionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionResource>>(subscriptions);
            return resources;
        }
        
        [SwaggerOperation(
            Summary = "List all Subscriptions of a Reader",
            Description = "List of Subscriptions of a Reader",
            OperationId = "ListAllSubscriptionsOfAReader"
        )]
        [SwaggerResponse(200, "List of Subscriptions for a Reader", typeof(SubscriptionResource))]
        [HttpGet("users/{userId}/subscriptions")]
        public async Task<IEnumerable<UserResource>> GetAllBySubscriberIdAsync(long userId)
        {
 
            var subscribed = await _userService.ListBySubscriberIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(subscribed);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List all users of a Writer Subscription",
            Description = "List of users of a Writer Subscription",
            OperationId = "ListAllUsersOfWriterSubscription"
        )]
        [SwaggerResponse(200, "List of Users for a Writer Subscription", typeof(UserResource))]
        [HttpGet("users/{userId}/subscribers")]
        public async Task<IEnumerable<UserResource>> GetAllBySubscribedIdAsync(long userId)
        {

            var users = await _userService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get Subscription by UserId And SubscribedId",
            Description = "Get Subscription by UserId And SubscribedId",
            OperationId = "GetSubscriptionByUserIdAndSubscribedId"
        )]
        [SwaggerResponse(200, "List of Subscriptions for a User and Subscribed", typeof(IEnumerable<SubscriptionResource>))]
        [AllowAnonymous]
        [HttpGet("users/{userId}/subscriptions/{subscribedId}")]
        public async Task<IActionResult> GetSubscriptionByUserIdAndSubscribedId(long userId, long subscribedId)
        {
            var subscription = await _subscriptionService.GetBySubscriberIdAndSubscribedIdAsync(userId, subscribedId);
            if (!subscription.Success)
                return NotFound(subscription.Message);
            var resource = _mapper.Map<Subscription, SubscriptionResource>(subscription.Resource);
            return Ok(resource);
        }

        [SwaggerOperation(
            Summary = "Assign a subscription",
            Description = "Assign a subscription",
            OperationId = "AssignSubscription"
        )]
        [SwaggerResponse(200, "Subscription was Assigned", typeof(SubscriptionResource))]
        [HttpPost("users/{userId}/subscriptions/{subscribedId}")]
        public async Task<IActionResult> AssignSubscriberSubscribed([FromBody] SaveSubscriptionResource subscriptionResource, long userId, long subscribedId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var subscription = _mapper.Map<SaveSubscriptionResource, Subscription>(subscriptionResource);
            var result = await _subscriptionService.AssignSubscriberSubscribedAsync(subscription,userId, subscribedId);
            if (!result.Success)
                return BadRequest(result.Message);

            var subscribedResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscribedResource);

        }

        [SwaggerOperation(
            Summary = "Unassign Subscription",
            Description = "Unassign Subscription",
            OperationId = "UnassignSubscription"
        )]
        [SwaggerResponse(200, "Subscription was unassigned", typeof(SubscriptionResource))]
        [HttpDelete("users/{userId}/subscriptions/{subscribedId}")]
        public async Task<IActionResult> UnassignSubscriberSubscribed(long userId, long subscribedId)
        {
            var result = await _subscriptionService.UnassignSubscriberSubscribedAsync(userId, subscribedId);

            if (!result.Success)
                return BadRequest(result.Message);
            var subscribedResource = _mapper.Map<Subscription, SubscriptionResource>(result.Resource);
            return Ok(subscribedResource);
        }
    }
}
