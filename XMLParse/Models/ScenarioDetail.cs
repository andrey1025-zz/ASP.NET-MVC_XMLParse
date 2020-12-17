using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XMLParse.Models
{
    public class ScenarioDetail
    {
        public string ScenarioID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public string Fullname { get; set; }
        public string UserID { get; set; }
        public string SampleDate { get; set; }
        public string CreationDate { get; set; }
        public string NumMonths { get; set; }
        public string MarketID { get; set; }
        public string NetworkLayerID { get; set; }
    }
}