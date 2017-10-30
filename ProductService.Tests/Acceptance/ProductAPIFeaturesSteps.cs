namespace ProductService.Tests.Acceptance
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http.Results;

    using NUnit.Framework;

    using ProductService.Models;

    using TechTalk.SpecFlow;

    [Binding]
    public class ProductAPIFeaturesSteps
    {
        [Given(@"I am in a browser")]
        public void GivenIAmInABrowser()
        {
        }

        [When(@"I enter the homepage url")]
        public void WhenIEnterTheHomepageUrl()
        {
            string url = "team-solenya-dev-client.azurewebsites.net";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            ScenarioContext.Current.Add("request", request);
        }

        [Then(@"I should get an OK response")]
        public void ThenIShouldGetAnOKResponse()
        {
            HttpWebRequest request = ScenarioContext.Current.Get<HttpWebRequest>("request");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Assert.IsInstanceOf<OkNegotiatedContentResult<List<PlpItem>>>(response);
        }
    }
}