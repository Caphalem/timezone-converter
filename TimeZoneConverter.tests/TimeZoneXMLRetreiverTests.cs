using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TimeZoneConverter.Models;
using TimeZoneConverter.Services;

namespace TimeZoneConverter.tests
{
    [TestClass]
    public class TimeZoneXMLRetreiverTests
    {
        private TimeZoneXMLRetriever _timeZoneXMLRetreiver;
        private int _amountOfZonesInXML;

        [TestInitialize]
        public void Setup()
        {
            _timeZoneXMLRetreiver = new TimeZoneXMLRetriever();
            _amountOfZonesInXML = 504;
        }

        [TestMethod]
        public void ShouldRetrieveTimeZonesFromXML()
        {
            List<TimeZone> timeZoneList = _timeZoneXMLRetreiver.GetTimeZoneList();

            Assert.AreEqual(_amountOfZonesInXML, timeZoneList.Count);
        }
    }
}
