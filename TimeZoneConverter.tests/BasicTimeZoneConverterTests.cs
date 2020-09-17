using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TimeZoneConverter.Interfaces;
using TimeZoneConverter.Models;
using TimeZoneConverter.Services;
using Moq;

namespace TimeZoneConverter.tests
{
    [TestClass]
    public class BasicTimeZoneConverterTests
    {
        private Mock<ITimeZoneListRetriever> _timeZoneListRetriever;
        private BasicTimeZoneConverter _basicTimeZoneConverter;
        private List<TimeZone> _timeZoneList;
        private string _notFoundMessage = "Timezone not found!";

        [TestInitialize]
        public void Setup()
        {
            _timeZoneList = new List<TimeZone>
            {
                new TimeZone
                {
                    IANAOlson = "Fake/IANA",
                    Windows = "Fake Windows Zone",
                    Territory = "ABC"
                },
                new TimeZone
                {
                    IANAOlson = "Fake/IANA Elsewhere",
                    Windows = "Fake Windows Zone",
                    Territory = "001"
                }
            };

            _timeZoneListRetriever = new Mock<ITimeZoneListRetriever>();
            _timeZoneListRetriever.Setup(x => x.GetTimeZoneList()).Returns(_timeZoneList);

            _basicTimeZoneConverter = new BasicTimeZoneConverter(_timeZoneListRetriever.Object);
        }

        [TestMethod]
        public void ShouldConvertFromIANAToWindowsTimeZone()
        {
            string windowsTimeZone = _basicTimeZoneConverter.IANAToWindows("Fake/IANA");

            Assert.AreEqual("Fake Windows Zone", windowsTimeZone);
        }

        [TestMethod]
        public void ShouldConvertFromNestedIANAToWindowsTimeZone()
        {
            string windowsTimeZone = _basicTimeZoneConverter.IANAToWindows("Elsewhere");

            Assert.AreEqual("Fake Windows Zone", windowsTimeZone);
        }

        [TestMethod]
        public void ShouldFailConvertFromIANAToWindowsTimeZone()
        {
            string windowsTimeZone = _basicTimeZoneConverter.IANAToWindows("Non/Existent");

            Assert.AreEqual(_notFoundMessage, windowsTimeZone);
        }

        [TestMethod]
        public void ShouldConvertFromWindowsToIANATimeZone()
        {
            string IANATimeZone = _basicTimeZoneConverter.WindowsToIANA("Fake Windows Zone");

            Assert.AreEqual("Fake/IANA", IANATimeZone);
        }

        [TestMethod]
        public void ShouldConvertFromWindowsWithTerritoryToIANATimeZone()
        {
            string IANATimeZone = _basicTimeZoneConverter.WindowsToIANA("Fake Windows Zone", "001");

            Assert.AreEqual("Fake/IANA Elsewhere", IANATimeZone);
        }

        [TestMethod]
        public void ShouldFailToConvertFromWindowsToIANATimeZone()
        {
            string IANATimeZone = _basicTimeZoneConverter.WindowsToIANA("Non existent");

            Assert.AreEqual(_notFoundMessage, IANATimeZone);
        }

        [TestMethod]
        public void ShouldFailToConvertFromWindowsWithTerritoryToIANATimeZone()
        {
            string IANATimeZone = _basicTimeZoneConverter.WindowsToIANA("Fake Windows Zone", "000");

            Assert.AreEqual(_notFoundMessage, IANATimeZone);
        }

        [TestMethod]
        public void ShouldFindTimeZoneBasedOnIANATimeZoneName()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Fake/IANA");

            Assert.AreEqual("Fake/IANA", timeZone.IANAOlson);
            Assert.AreEqual("Fake Windows Zone", timeZone.Windows);
            Assert.AreEqual("ABC", timeZone.Territory);
        }

        [TestMethod]
        public void ShouldFindTimeZoneBasedOnNestedIANATimeZoneName()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Elsewhere");

            Assert.AreEqual("Fake/IANA Elsewhere", timeZone.IANAOlson);
            Assert.AreEqual("Fake Windows Zone", timeZone.Windows);
            Assert.AreEqual("001", timeZone.Territory);
        }

        [TestMethod]
        public void ShouldFailToFindTimeZoneBasedOnIANATimeZoneName()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Non/Existent");

            Assert.AreEqual(null, timeZone);
        }

        [TestMethod]
        public void ShouldFindTimeZoneBasedOnWindowsTimeZoneName()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Fake Windows Zone");

            Assert.AreEqual("Fake/IANA", timeZone.IANAOlson);
            Assert.AreEqual("Fake Windows Zone", timeZone.Windows);
            Assert.AreEqual("ABC", timeZone.Territory);
        }

        [TestMethod]
        public void ShouldFindTimeZoneBasedOnWindowsTimeZoneNameWithTerritory()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Fake Windows Zone", "001");

            Assert.AreEqual("Fake/IANA Elsewhere", timeZone.IANAOlson);
            Assert.AreEqual("Fake Windows Zone", timeZone.Windows);
            Assert.AreEqual("001", timeZone.Territory);
        }

        [TestMethod]
        public void ShouldFailToFindTimeZoneBasedOnWindowsTimeZoneName()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Non existent");

            Assert.AreEqual(null, timeZone);
        }

        [TestMethod]
        public void ShouldFailToFindTimeZoneBasedOnWindowsTimeZoneNameWithTerritory()
        {
            TimeZone timeZone = _basicTimeZoneConverter.FindTimeZone("Fake Windows Zone", "000");

            Assert.AreEqual(null, timeZone);
        }
    }
}
