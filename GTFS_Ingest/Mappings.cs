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
}
