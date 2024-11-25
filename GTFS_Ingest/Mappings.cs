using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFS_Ingest;

public class Mappings
{
    public Dictionary<string, int> Routes()
    {
        // Dictionary to map Routes keys to their respective indices
        Dictionary<string, int> keyIndexMap = new Dictionary<string, int>
            {
                { "route_id", 5 },
                { "agency_id", 4 },
                { "route_short_name", 8 },
                { "route_long_name", 0 },
                { "route_desc", 6 },
                { "route_type", 1 },
                { "route_url", 7 },
                { "route_color", 3 },
                { "route_text_color", 2 }
            };

        return keyIndexMap;
    }
}
