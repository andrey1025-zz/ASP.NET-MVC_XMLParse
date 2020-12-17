using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMLParse.Models;
using System.Xml;

namespace XMLParse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            XmlDocument doc = new XmlDocument();
            List<ScenarioDetail> Scenarios = new List<ScenarioDetail>();
            List<Fullname> Fullnames = new List<Fullname>();
            List<Sampledate> Sampledates = new List<Sampledate>();
            doc.Load(Server.MapPath("~/assets/Data.xml"));
            XmlNodeList list = doc.SelectNodes("/Data/Scenario");
            foreach (XmlNode node in list)
            {
                Scenarios.Add(new ScenarioDetail
                {
                    ScenarioID = (node["ScenarioID"] != null) ? node["ScenarioID"].InnerText.Trim() : "",
                    Name = (node["Name"] != null) ? node["Name"].InnerText.Trim() : "",
                    Surname = (node["Surname"] != null) ? node["Surname"].InnerText.Trim() : "",
                    Forename = (node["Forename"] != null) ? node["Forename"].InnerText.Trim() : "",
                    Fullname = (node["Surname"].InnerText.Trim() + " " + node["Forename"].InnerText.Trim()),
                    UserID = (node["UserID"] != null) ? node["UserID"].InnerText.Trim() : "",
                    SampleDate = (node["SampleDate"] != null) ? node["SampleDate"].InnerText.Trim() : "",
                    CreationDate = (node["CreationDate"] != null) ? node["CreationDate"].InnerText.Trim() : "",
                    NumMonths = (node["NumMonths"] != null) ? node["NumMonths"].InnerText.Trim() : "",
                    MarketID = (node["MarketID"] != null) ? node["MarketID"].InnerText.Trim() : "",
                    NetworkLayerID = (node["NetworkLayerID"] != null) ? node["NetworkLayerID"].InnerText.Trim() : "",
                });
            }
            ViewBag.total = Scenarios.Count();
            var names = Scenarios.GroupBy(x => x.UserID).Select(
                                                            g => new
                                                            {
                                                                Key = g.Key,
                                                                UserID = g.First().UserID,
                                                                Username = g.First().Fullname,
                                                                count = g.Count()
                                                            });
            foreach (var name in names)
            {
                Fullnames.Add(new Fullname
                {
                    Key = name.Key,
                    UserID = name.UserID,
                    Username = name.Username,
                    Count = name.count
                });
            }            
            var dates = Scenarios.GroupBy(x => x.SampleDate).Select(
                                                            g => new
                                                            {
                                                                Key = g.Key,
                                                                UserID = g.First().UserID,
                                                                date = g.First().SampleDate,
                                                                count = g.Count()
                                                            });
            foreach (var date in dates)
            {
                Sampledates.Add(new Sampledate
                {
                    Key = date.Key,
                    UserID = date.UserID,
                    date = date.date,
                    Count = date.count
                });
            }
            ViewBag.fullnames = Fullnames;
            ViewBag.sampledates = Sampledates;
            return View();
        }

        public JsonResult Get(string groupBy, string groupByDirection, int? page, int? limit)
        {
            List<ScenarioDetail> records;
            XmlDocument doc = new XmlDocument();
            List<ScenarioDetail> Scenarios = new List<ScenarioDetail>();
            doc.Load(Server.MapPath("~/assets/Data.xml"));
            XmlNodeList list = doc.SelectNodes("/Data/Scenario");
            foreach (XmlNode node in list)
            {
                Scenarios.Add(new ScenarioDetail
                {
                    ScenarioID = (node["ScenarioID"] != null) ? node["ScenarioID"].InnerText.Trim() : "",
                    Name = (node["Name"] != null) ? node["Name"].InnerText.Trim() : "",
                    Surname = (node["Surname"] != null) ? node["Surname"].InnerText.Trim() : "",
                    Forename = (node["Forename"] != null) ? node["Forename"].InnerText.Trim() : "",
                    Fullname = (node["Surname"].InnerText.Trim() + " " + node["Forename"].InnerText.Trim()),
                    UserID = (node["UserID"] != null) ? node["UserID"].InnerText.Trim() : "",
                    SampleDate = (node["SampleDate"] != null) ? node["SampleDate"].InnerText.Trim() : "",
                    CreationDate = (node["CreationDate"] != null) ? node["CreationDate"].InnerText.Trim() : "",
                    NumMonths = (node["NumMonths"] != null) ? node["NumMonths"].InnerText.Trim() : "",
                    MarketID = (node["MarketID"] != null) ? node["MarketID"].InnerText.Trim() : "",
                    NetworkLayerID = (node["NetworkLayerID"] != null) ? node["NetworkLayerID"].InnerText.Trim() : "",
                });
            }
            var total = Scenarios.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = Scenarios.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = Scenarios.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

    }

}