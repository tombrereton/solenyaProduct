namespace ProductService.Tests.Acceptance
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http.Results;

    using NUnit.Framework;

    using ProductService.Models;

    using TechTalk.SpecFlow;

    [Binding]
    public class ProductAPIFeatureSteps
    {
        [Given(@"I am in a browser")]
        public void GivenIAmInABrowser()
        {
            Console.WriteLine("Im in a browser");
        }

        [When(@"I enter the homepage url")]
        public void WhenIEnterTheHomepageUrl()
        {
            string url = "http://team-solenya-dev-client.azurewebsites.net";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            ScenarioContext.Current.Add("request", request);
        }

        [Then(@"I should get an OK response")]
        public void ThenIShouldGetAnOKResponse()
        {
            HttpWebRequest request = ScenarioContext.Current.Get<HttpWebRequest>("request");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}