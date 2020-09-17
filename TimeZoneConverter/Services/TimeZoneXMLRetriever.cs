using System.Collections.Generic;
using System.Configuration;
using TimeZoneConverter.Interfaces;
using System.Xml.Linq;
using TimeZoneConverter.Models;

namespace TimeZoneConverter.Services
{
    public class TimeZoneXMLRetriever : ITimeZoneListRetriever
    {
        public List<TimeZone> GetTimeZoneList()
        {
            string XMLLocation = ConfigurationManager.AppSettings.Get("XMLLocation");

            XElement windowsZones = XElement.Load(XMLLocation);
            IEnumerable<XElement> mapZones = windowsZones.Element("windowsZones").Element("mapTimezones").Elements("mapZone");

            List<TimeZone> timeZoneList = new List<TimeZone>();

            foreach (XElement mapZone in mapZones)
            {
                TimeZone timeZone = new TimeZone
                {
                    IANAOlson = mapZone.Attribute("type").Value,
                    Windows = mapZone.Attribute("other").Value,
                    Territory = mapZone.Attribute("territory").Value,
                };

                timeZoneList.Add(timeZone);
            }

            return timeZoneList;
        }
    }
}
