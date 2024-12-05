using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFS_Ingest;

public class Mappings
{
    // Dictionary to map Routes keys to their respective indices
    public Dictionary<string, int> Routes()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "route_id", 5 },
                { "agency_id", 4 },
                { "route_short_name", 8 },
                { "route_long_name", 0 },
                { "route_desc", 7 },
                { "route_type", 1 },
                { "route_url", 6 },
                { "route_color", 3 },
                { "route_text_color", 2 }
            };

        return keyIndexMap;
    }

    // Dictionary to map Trips keys to their respective indices
    public Dictionary<string, int> Trips()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "route_id", 2 },
                { "service_id", 7 },
                { "trip_id", 8 },
                { "trip_headsign", 5 },
                { "direction_id", 4 },
                { "block_id", 0 },
                { "shape_id", 6 },
                { "wheelchair_accessible", 3 },
                { "bikes_allowed", 1 }
            };

        return keyIndexMap;
    }

    // Dictionary to map Stops keys to their respective indices
    public Dictionary<string, int> Stops()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "stop_id", 10 },
                { "stop_code", 2 },
                { "stop_name", 8 },
                { "stop_desc", 7 },
                { "stop_lat", 0 },
                { "stop_lon", 3 },
                { "zone_id", 11 },
                { "stop_url", 5 },
                { "location_type", 9 },
                { "parent_station", 6 },
                { "stop_timezone", 4 },
                { "wheelchair_boarding", 1 }
            };

        return keyIndexMap;
    }

    // Dictionary to map Stop Times keys to their respective indices
    public Dictionary<string, int> StopTimes()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "trip_id", 0 },
                { "arrival_time", 1 },
                { "departure_time", 2 },
                { "stop_id", 3 },
                { "stop_sequence", 4 },
                { "stop_headsign", 5 },
                { "pickup_type", 6 },
                { "drop_off_type", 7 },
                { "shape_dist_traveled", 8 },
                { "timepoint", 9 }
            };

        return keyIndexMap;
    }

    // Dictionary to map Calendar keys to their respective indices
    public Dictionary<string, int> Calendar()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "service_id", 0 },
                { "monday", 3 },
                { "tuesday", 4 },
                { "wednesday", 5 },
                { "thursday", 6 },
                { "friday", 7 },
                { "saturday", 8 },
                { "sunday", 9 },
                { "start_date", 1 },
                { "end_date", 2 }
            };

        return keyIndexMap;
    }

    // Dictionary to map Calendar Dates keys to their respective indices
    public Dictionary<string, int> CalendarDates()
    {
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "service_id", 0 },
                { "date", 1 },
                { "exception_type", 2 }
            };
        return keyIndexMap;
    }
}
