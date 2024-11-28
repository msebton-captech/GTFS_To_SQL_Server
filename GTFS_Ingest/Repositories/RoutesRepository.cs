using Microsoft.Data.SqlClient;

namespace GTFS_Ingest.Repositories;

public class RoutesRepository
{
    public void UploadRoutesData(string insertString, string selectString, List<List<string>> newData, SqlConnection connection)
    {
        // Variable to keep track of the number of records uploaded
        int uploadCount = 0;

        // Dictionary to map keys to their respective indices
        var routeMappings = new Mappings().Routes();

        // Iterating through the list of lists to upload data to the database
        foreach (List<string> values in newData)
        {
            // Checking if the data already exists in the database
            if (!DoesRouteDataExist(connection, values, selectString, routeMappings))
            {
                // If data does not exist, preparing to insert it into the database
                using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                {
                    // Adding parameters for insertion
                    insertCommand.Parameters.AddWithValue("@Value1", values[routeMappings["route_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value2", values[routeMappings["agency_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value3", values[routeMappings["route_short_name"]]);
                    insertCommand.Parameters.AddWithValue("@Value4", values[routeMappings["route_long_name"]]);
                    insertCommand.Parameters.AddWithValue("@Value5", values[routeMappings["route_desc"]]);
                    insertCommand.Parameters.AddWithValue("@Value6", values[routeMappings["route_type"]]);
                    insertCommand.Parameters.AddWithValue("@Value7", values[routeMappings["route_url"]]);
                    insertCommand.Parameters.AddWithValue("@Value8", values[routeMappings["route_color"]]);
                    insertCommand.Parameters.AddWithValue("@Value9", values[routeMappings["route_text_color"]]);

                    // Executing the insertion command
                    insertCommand.ExecuteNonQuery();
                    // Incrementing the upload count
                    uploadCount++;
                }
            }
        }
        // Outputting the number of records uploaded
        Console.WriteLine($"Uploaded {uploadCount} records to the Routes table.");
    }

    // Method to check if data already exists in the database
    public bool DoesRouteDataExist(SqlConnection connection, List<string> values, string selectString, Dictionary<string, int> routeMappings)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query
            selectCommand.Parameters.AddWithValue("@Value1", values[routeMappings["route_id"]]);
            selectCommand.Parameters.AddWithValue("@Value2", values[routeMappings["agency_id"]]);
            selectCommand.Parameters.AddWithValue("@Value3", values[routeMappings["route_long_name"]]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }

    public List<List<string>> RemoveRoutesDuplicates(List<List<string>> list, Dictionary<string, int> routeMappings)
    {
        // HashSet to store unique IDs, names, and URLs
        HashSet<int> ids = new HashSet<int>();
        HashSet<string> names = new HashSet<string>();
        HashSet<string> urls = new HashSet<string>();

        List<List<string>> newData = new List<List<string>>();

        // Iterating through each item in the list
        foreach (List<string> item in list)
        {
            // Checking if the item's ID, name, and URL are not already present
            if (!ids.Contains(int.Parse(item[routeMappings["route_id"]])) && !names.Contains(item[routeMappings["route_long_name"]]))
            {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the ID, name, and URL to their respective hash sets
                ids.Add(int.Parse(item[routeMappings["route_id"]]));
                names.Add(item[routeMappings["route_long_name"]]);
            }
        }
        return newData;
    }
}
