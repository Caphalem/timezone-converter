using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter.Interfaces;
using TimeZoneConverter.Models;

namespace TimeZoneConverter.Services
{
    public class BasicTimeZoneConverter : ITimeZoneConverter
    {
        private List<TimeZone> _timeZoneList;
        private string _notFoundMessage = "Timezone not found!";

        public BasicTimeZoneConverter(ITimeZoneListRetriever timeZoneListRetriever)
        {
            _timeZoneList = timeZoneListRetriever.GetTimeZoneList();
        }

        public TimeZone FindTimeZone(string timeZoneName, string territory = null)
        {
            TimeZone foundTimeZone;
            timeZoneName = timeZoneName.Trim().ToLower();

            if (!string.IsNullOrEmpty(territory) && !string.IsNullOrWhiteSpace(territory))
            {
                territory = territory.Trim().ToLower();
                foundTimeZone = _timeZoneList
                    .Where(x => x.IANAOlson.Trim().ToLower().Contains(timeZoneName) && x.Territory.Trim().ToLower() == territory ||
                                x.Windows.Trim().ToLower() == timeZoneName && x.Territory.Trim().ToLower() == territory)
                    .FirstOrDefault();
            }
            else
            {
                foundTimeZone = _timeZoneList
                    .Where(x => x.IANAOlson.Trim().ToLower().Contains(timeZoneName) ||
                                x.Windows.Trim().ToLower() == timeZoneName)
                    .FirstOrDefault();
            }

            return foundTimeZone;
        }

        public string IANAToWindows(string IANAZoneName)
        {
            TimeZone foundTimeZone = FindTimeZone(IANAZoneName);

            if (foundTimeZone != null)
            {
                return foundTimeZone.Windows;
            }
            else
            {
                return _notFoundMessage;
            }
        }

        public string WindowsToIANA(string windowsZoneName, string territory = null)
        {
            TimeZone foundTimeZone = FindTimeZone(windowsZoneName, territory);

            if (foundTimeZone != null)
            {
                return foundTimeZone.IANAOlson;
            }
            else
            {
                return _notFoundMessage;
            }
        }
    }
}
