using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SentDemo.Models;
using System.IO;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;

namespace SentDemo.Service.Controllers
{
    [RoutePrefix("api/sentiment")]
    public class AnalyticsController : ApiController
    {
        private const string _uri = "https://westus.api.cognitive.microsoft.com/";
        private const string _media = "application/json";
        private const string _apiKey = "INSERT_API_KEY";
        private const string _lang = "en";
        private const string _sentimentAnalysis = "text/analytics/v2.0/sentiment";
        private const string _keyPhrases = "text/analytics/v2.0/keyPhrases";


        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("API works!");
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(List<SentimentInput> input)
        {
            //Exit immediately if input is empty
            if (input == null || input.Count < 1)
            {
                return BadRequest("Invalid input");
            }
            else
            {
                //Automatically change all the language configurations.
                input = input.Select(x => { x.language = _lang; return x; }).ToList();
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_media));

                    SentimentInputDoc inputDoc = new SentimentInputDoc();
                    inputDoc.documents = new List<SentimentInput>();
                    inputDoc.documents = input;

                    string serializedInput = new JavaScriptSerializer().Serialize(inputDoc);
                    byte[] byteData = Encoding.UTF8.GetBytes(serializedInput);

                    using (var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue(_media);

                        var response = Task.Run(() =>
                        {
                            return client.PostAsync(_keyPhrases, content);
                        }).Result;

                        SentimentOutputDoc outputDoc = new SentimentOutputDoc();
                        outputDoc.documents = new List<SentimentOutput>();

                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string res = response.Content.ReadAsStringAsync().Result;
                                outputDoc = new JavaScriptSerializer().Deserialize<SentimentOutputDoc>(res);
                                return Ok(outputDoc);
                            }
                            else
                            {
                                return Content(HttpStatusCode.InternalServerError, response.ReasonPhrase);
                            }
                        }

                        return Content(HttpStatusCode.InternalServerError, "No response from API");
                    }                    
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }        
    }
}
