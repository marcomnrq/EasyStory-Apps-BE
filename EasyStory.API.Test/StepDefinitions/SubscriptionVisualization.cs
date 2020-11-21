using EasyStory.API.Domain.Models;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace EasyStory.API.Test.StepDefinitions
{
    [Binding]
    public class SubscriptionVisualization : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private SubscriptionResource subscriptionResource = new SubscriptionResource();
        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public SubscriptionVisualization(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }


        [When(@"I make a get subscription request to '(.*)' with the user id of '(.*)' and request '(.*)' with the subscribed id of '(.*)'")]
        public async Task WhenIMakeAGetSubscriptionRequestToWithTheUserIdOfAndRequestWithTheSubscribedIdOf(string userEndPoint, long userId, string subscribedEndPoint, long subscribedId)
        {
            var qualificationRelativeUri = new Uri(userEndPoint + userId + subscribedEndPoint + subscribedId, UriKind.Relative);
            Response = await _client.GetAsync(qualificationRelativeUri).ConfigureAwait(false);
        }


        [Then(@"the response list should be a bad status code of '(.*)'")]
        public void ThenTheResponseListShouldBeABadStatusCodeOf(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"the response list should be a status code of '(.*)'")]
        public void ThenTheResponseListShouldBeAStatusCodeOf(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        


    }
}
