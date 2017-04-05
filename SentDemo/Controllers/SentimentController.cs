using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using SentDemo.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace SentDemo.Controllers
{
    public class SentimentController : Controller
    {

        // GET: Sentiment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Demo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Demo(SentimentInput document)
        {
            string uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
            string apiKey = "3bdeeed74dbc4fdea35f12782d873d73";

            if (string.IsNullOrWhiteSpace(document.id))
            {
                document.id = "1";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(document);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            return View();
        }

        // GET: Sentiment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sentiment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sentiment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sentiment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sentiment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sentiment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sentiment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
