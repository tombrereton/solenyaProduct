namespace ProductService.Tests.Acceptance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
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

        [When(@"I hit the productAPI endpoint")]
        public void WhenIHitTheProductAPIEndpoint()
        {
            string url = "http://team-solenya-product-dev.azurewebsites.net";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            ScenarioContext.Current.Add("request", request);
        }

        [Then(@"I should get a response containing JSON data for the plp")]
        public void ThenIShouldGetAResponseContainingJSONDataForThePlp()
        {
            HttpWebRequest request = ScenarioContext.Current.Get<HttpWebRequest>("request");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseContent;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseContent = reader.ReadToEnd();
            }

            string warehouseJumper = "Warehouse Side Split Roll Neck Jumper";

            bool isWarehouseJumperInResponse = Regex.IsMatch(responseContent, warehouseJumper);
            Assert.True(isWarehouseJumperInResponse);
        }
    }
}