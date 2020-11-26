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
    public class QualificationAssignationSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }



        public QualificationAssignationSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a Client")]
        public void GivenIAmAClient()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }

        [When(@"I  make a get request to '(.*)' with the user id of '(.*)' and request '(.*)' with post id of '(.*)' and request '(.*)'")]
        public async Task WhenIMakeAGetRequestToWithTheUserIdOfAndRequestWithPostIdOfAndRequest(string endpointUser, int userId, string endpointPost, int postId, string endPointQualification)
        {
            var qualificationRelativeUri = new Uri(endpointUser + userId + endpointPost + postId + endPointQualification, UriKind.Relative);
            Response = await _client.GetAsync(qualificationRelativeUri).ConfigureAwait(false);
        }

        [Then(@"the result of status code is '(.*)'")]
        public void ThenTheResultOfStatusCodeIs(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

    }
}
