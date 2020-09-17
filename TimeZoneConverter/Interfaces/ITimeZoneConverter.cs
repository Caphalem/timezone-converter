using TimeZoneConverter.Models;

namespace TimeZoneConverter.Interfaces
{
    public interface ITimeZoneConverter
    {
        string IANAToWindows(string IANAZoneName);
        string WindowsToIANA(string windowsZoneName, string territory = null);
        TimeZone FindTimeZone(string timeZoneName, string territory = null);
    }
}
