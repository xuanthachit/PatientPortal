﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using PatientPortal.Domain.Models.SPA;
using PatientPortal.Domain.LogManager;
using System.Text;

namespace PatientPortal.API.SPA.Controllers
{
    public class MedinetController : ApiController
    {
        public string RSSURL = ConfigurationManager.AppSettings["medinet:rssUrl"];

        [HttpGet]
        public HttpResponseMessage Get(string type)
        {
            //Check Request
            if (type == null || type.Length < 1) return Request.CreateResponse(HttpStatusCode.BadRequest, type);

            //Get RSS
            
            var url = RSSURL + type;
            try
            {
                WebClient wclient = new WebClient();
                wclient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wclient.Encoding = Encoding.UTF8;
                string RSSData = wclient.DownloadString(url);

                XDocument xml = XDocument.Parse(RSSData);
                var RSSFeedData = (from x in xml.Descendants("item")
                                   select new MedinetRSSFeed
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = ((string)x.Element("link")),
                                       Description = ((string)x.Element("description")),
                                       PublishDate = ((string)x.Element("pubDate"))
                                   });

                return Request.CreateResponse(HttpStatusCode.OK, RSSFeedData);
            }
            catch (Exception ex )
            {
                Logger.LogError(ex);
                throw;
            }
        }
    }
}