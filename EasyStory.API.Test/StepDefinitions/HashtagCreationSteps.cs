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
    public  class HashtagCreationSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private WebApplicationFactory<TestStartup> _factory;
        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }



        public HashtagCreationSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a user in the application")]
        public void GivenIAmAUserInTheApplication()
        {
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"https://localhost/")
            });
        }


        [When(@"I make a post hashtag request to '(.*)' with the following data '(.*)'")]
        public async Task WhenIMakeAPostHashtagRequestToWithTheFollowingData(string resourceEndPoint, string postDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            Response = await _client.PostAsync(postRelativeUri, content).ConfigureAwait(false);
        }

        [Then(@"the status response code is '(.*)'")]
        public void ThenTheStatusResponseCodeIs(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }


    }
}
