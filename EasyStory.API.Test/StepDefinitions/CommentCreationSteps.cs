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
    public class CommentCreationSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public CommentCreationSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a reader or writer")]
        public void GivenIAmAReaderOrWriter()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }

        [When(@"I  make a post comment request to '(.*)' with the user id of '(.*)' and request '(.*)' with post id of '(.*)' and request '(.*)' with the data: '(.*)'")]
        public async Task WhenIMakeAPostCommentRequestToWithTheUserIdOfAndRequestWithPostIdOfAndRequestWithTheData(string endpointUser, long userId, string endpointPost, long postId, string endpointComment, string postDataJson)
        {
            var postRelativeUri = new Uri(endpointUser + userId + endpointPost + postId + endpointComment, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            Response = await _client.PostAsync(postRelativeUri, content).ConfigureAwait(false);
        }


        [Then(@"the response should be this status code of '(.*)'")]
        public void ThenTheResponseShouldBeThisStatusCodeOf(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

    }
}
