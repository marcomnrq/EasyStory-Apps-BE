using Castle.Core.Resource;
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
    public class BookmarksResourceSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }



        public BookmarksResourceSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }


        [Given(@"I am a reader with bookmarks")]
        public void GivenIAmAReaderWithBookmarks()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }


        [When(@"I make a get request to '(.*)' with the user id fof '(.*)' and request '(.*)'")]
        public virtual async Task WhenIMakeAGetRequestToWithTheUserIdFofAndRequest(string endpointUser, long userId, string endpointBookmark)
        {
            var postRelativeUri = new Uri(endpointUser + userId + endpointBookmark, UriKind.Relative);
            Response = await _client.GetAsync(postRelativeUri).ConfigureAwait(false);
        }




        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }


    }
}
