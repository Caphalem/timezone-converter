using System.Collections.Generic;
using TimeZoneConverter.Models;

namespace TimeZoneConverter.Interfaces
{
    public interface ITimeZoneListRetriever
    {
        List<TimeZone> GetTimeZoneList();
    }
}
