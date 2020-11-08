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
    public  class CommentCreationSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {

        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public CommentCreationSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a reader/writer")]
        public void GivenIAmAReaderWriter()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }

        [When(@"I make a get comment request to '(.*)'")]
        public virtual async  Task WhenIMakeAGetCommentRequestTo(string endpoint)
        {
            var getRelativeUri = new Uri(endpoint, UriKind.Relative);
            Response = await _client.GetAsync(getRelativeUri).ConfigureAwait(false);
        }



        [Then(@"the response should be a status code of '(.*)'")]
        public void ThenTheResponseShouldBeAStatusCodeOf(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

    }
}
