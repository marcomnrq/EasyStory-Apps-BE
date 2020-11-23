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
    public class PostsResearchSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }



        public PostsResearchSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a reader")]
        public void GivenIAmAReader()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }

        [When(@"I make a get the post request to '(.*)' with the post id of '(.*)'")]
        public async Task WhenIMakeAGetThePostRequestToWithThePostIdOf(string endpoint, long postId)
        {
            var postRelativeUri = new Uri(endpoint + postId, UriKind.Relative);
            Response = await _client.GetAsync(postRelativeUri).ConfigureAwait(false);
        }



        [Then(@"the result should be a status code of '(.*)'")]
        public void ThenTheResultShouldBeAStatusCodeOf(int statusCode)
        {
          var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }





    }
}
