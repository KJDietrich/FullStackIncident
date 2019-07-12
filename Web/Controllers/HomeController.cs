using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// These keys would normally be heald in some encryption mechanism that is part of source control
        /// or as encrypted text in a config or json file somewhere.  Ideally, some type of certificate
        /// authentication would be used instead.
        /// </summary>
        public static string SortofSecretMapsKey => "AIzaSyB-lKYGVPrKtXc9jotVTKevxuxBN-kdNxE";
        public static string SortofSecretWeatherKey => "c029635e34cd9cdcbfcbf99e996c7984";
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ProcessFile(object sourceFile)
        {
            if (Request.Form.Files.Count == 0)
                return Json("Error: No File Uploaded.");

            var fileContents = Request.Form.Files.First();
            if (fileContents == null || fileContents.Length == 0)
                return Json("Error: Unable to read file.");

            Business.IncidentData myIncident = null; 
            using (var ms = new MemoryStream())
            {
                fileContents.CopyTo(ms);
                myIncident = new Business.IncidentData(byteArray:ms.ToArray());
            }
            return Json(myIncident);
        }
        public JsonResult WeatherAPI(string latitude, string longitude, long eventDateUnixTime)
        {
            string url = "https://api.darksky.net/forecast";

            url = $"{url}/{SortofSecretWeatherKey}/{latitude},{longitude},{eventDateUnixTime}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse responseObject = null;
            string responseJsonString = null;

            try
            {
                responseObject = (HttpWebResponse)request.GetResponse();
                Stream stream = responseObject.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                responseJsonString = streamReader.ReadToEnd();

                return Json(JsonConvert.DeserializeObject<Object>(responseJsonString));
            }
            catch (Exception ex) {
                return Json("Error calling weather API.");
            }
        }
        public JsonResult ParcelAPI(string latitude, string longitude, long eventDateUnixTime)
        {
            //Code duplicaiton here.. Could have a common method to impliment both 
            //Weather and Parcel APIs to get results back to JS, but I'm running low on time.

            string url = "http://gis.richmondgov.com/ArcGIS/rest/" + 
                "services/StatePlane4502/Ener/MapServer/0/query";

            string geometryType = "esriGeometryPoint";
            //string json = $"{{\"x\":{latitude},\"y\":{longitude},\"spatialReference\":{{\"wkid\":4326}}}}";
            string inSR = $"%7B%22x%22%3A{latitude}%2C%22y%22%3A{longitude}%2C%22spatialReference%22%3A%7B%22wkid%22%3A4326%7D%7D";
            string where = "1%3D1";

            url = $"{url}?geometryType={geometryType}&inSR={inSR}&where={where}&f=pjson";
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse responseObject = null;
            string responseJsonString = null;

            try
            {
                responseObject = (HttpWebResponse)request.GetResponse();
                Stream stream = responseObject.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                responseJsonString = streamReader.ReadToEnd();

                return Json(JsonConvert.DeserializeObject<Object>(responseJsonString));
            }
            catch (Exception ex)
            {
                return Json("Error calling parcel API.");
            }
        }
        //public async Task<JsonResult> ParcelAPIAsync(double latitude, double longitude)
        //{
        //    //string url = "http://gis.richmondgov.com/ArcGIS/rest/services/StatePlane4502/Ener/MapServer/0/query";///?f=json&pretty=true
        //    string url = "http://gis.richmondgov.com/ArcGIS/rest/services/StatePlane4502/Ener/MapServer/0/query?f=pjson";///?f=json&pretty=true
        //    dynamic param = GenParcelParam(latitude, longitude);

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        //    httpWebRequest.ContentType = "application/json; charset=utf-8";
        //    httpWebRequest.Method = "POST";

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        string json = JsonConvert.SerializeObject(param);

        //        streamWriter.Write(json);
        //        streamWriter.Flush();
        //    }
        //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //    var responseText = "";
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        responseText = streamReader.ReadToEnd();  
        //    }



        //    return Json(responseText);


        //    //var content = new FormUrlEncodedContent(param);
        //    //var response = await client.PostAsync(url, content);
        //    //var responseString = await response.Content.ReadAsStringAsync();

        //    //url = $"{url}?geometry={latitude},{longitude}&geometryType={geometryType}&f=pjson";
        //    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    //request.Method = "POST";
        //    //HttpWebResponse responseObject = null;
        //    //string responseJsonString = null;

        //    //try
        //    //{
        //    //    responseObject = (HttpWebResponse)request.GetResponse();
        //    //    Stream stream = responseObject.GetResponseStream();
        //    //    StreamReader streamReader = new StreamReader(stream);
        //    //    responseJsonString = streamReader.ReadToEnd();

        //    //    return Json(JsonConvert.DeserializeObject<Object>(responseJsonString));
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return Json("Error calling parcel API.");
        //    //}
        //    //return Json(myParameter);
        //}
        private dynamic GenParcelParam(double latitude, double longitude)
        {
            dynamic myParameter = new ExpandoObject();
            myParameter.spatialReference = new ExpandoObject();

            myParameter.x = latitude;
            myParameter.y = longitude;
            myParameter.spatialReference.wkid = 4326;
            return myParameter;
        }
    }
}