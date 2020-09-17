using System;
using TimeZoneConverter.Interfaces;
using TimeZoneConverter.Services;

namespace TimeZoneConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ITimeZoneConverter timeZoneConverter = new BasicTimeZoneConverter(new TimeZoneXMLRetriever());

                Console.WriteLine("Enter either an IANA/Olson or Windows timezone name (e.g. Mountain Standard Time). Optionally, you can also enter a territory seperated by a \",\" (e.g. Mountain Standard Time, CA)");

                Models.TimeZone foundTimeZone;

                while (true)
                {
                    string[] inputs = Console.ReadLine().Split(',');
                    string timeZoneName = inputs[0];
                    string territory = inputs.Length > 1 ? inputs[1] : null;

                    foundTimeZone = timeZoneConverter.FindTimeZone(timeZoneName, territory);

                    if (foundTimeZone != null)
                    {
                        Console.WriteLine($"IANA/Olson: {foundTimeZone.IANAOlson}");
                        Console.WriteLine($"Windows: {foundTimeZone.Windows}, Territory: {foundTimeZone.Territory}");
                    }
                    else
                    {
                        Console.WriteLine("Timezone not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            
        }
    }
}
