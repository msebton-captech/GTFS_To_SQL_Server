using Microsoft.Data.SqlClient;

namespace GTFS_Ingest.Repositories;

public class StopsRepository
{
    public void UploadStopsData(string insertString, string selectString, List<List<string>> newData, SqlConnection connection)
    {
        // Variable to keep track of the number of records uploaded
        int uploadCount = 0;

        // Dictionary to map keys to their respective indices
        var stopMappings = new Mappings().Stops();

        // Iterating through the list of lists to upload data to the database
        foreach (List<string> values in newData)
        {
            // Checking if the data already exists in the database
            if (!DoesStopDataExist(connection, values, selectString, stopMappings))
            {
                // If data does not exist, preparing to insert it into the database
                using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                {
                    // Adding parameters for insertion
                    insertCommand.Parameters.AddWithValue("@Value1", values[stopMappings["stop_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value2", values[stopMappings["stop_code"]]);
                    insertCommand.Parameters.AddWithValue("@Value3", values[stopMappings["stop_name"]]);
                    insertCommand.Parameters.AddWithValue("@Value4", values[stopMappings["stop_desc"]]);
                    insertCommand.Parameters.AddWithValue("@Value5", values[stopMappings["stop_lat"]]);
                    insertCommand.Parameters.AddWithValue("@Value6", values[stopMappings["stop_lon"]]);
                    insertCommand.Parameters.AddWithValue("@Value7", values[stopMappings["zone_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value8", values[stopMappings["stop_url"]]);
                    insertCommand.Parameters.AddWithValue("@Value9", values[stopMappings["location_type"]]);
                    insertCommand.Parameters.AddWithValue("@Value10", values[stopMappings["parent_station"]]);
                    insertCommand.Parameters.AddWithValue("@Value11", values[stopMappings["stop_timezone"]]);
                    insertCommand.Parameters.AddWithValue("@Value12", values[stopMappings["wheelchair_boarding"]]);

                    // Executing the insertion command
                    insertCommand.ExecuteNonQuery();
                    // Incrementing the upload count
                    uploadCount++;
                }
            }
        }
        // Outputting the number of records uploaded
        Console.WriteLine($"Uploaded {uploadCount} records to the Stops table.");
    }

    // Method to check if data already exists in the database
    public bool DoesStopDataExist(SqlConnection connection, List<string> values, string selectString, Dictionary<string, int> stopMappings)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query
            selectCommand.Parameters.AddWithValue("@Value1", values[stopMappings["stop_id"]]);
            selectCommand.Parameters.AddWithValue("@Value2", values[stopMappings["stop_lat"]]);
            selectCommand.Parameters.AddWithValue("@Value3", values[stopMappings["stop_lon"]]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }

    public List<List<string>> RemoveStopsDuplicates(List<List<string>> list, Dictionary<string, int> stopMappings)
    {
        // HashSet to store unique IDs, names, and URLs
        HashSet<string> ids = new HashSet<string>();

        List<List<string>> newData = new List<List<string>>();

        // Iterating through each item in the list
        foreach (List<string> item in list)
        {
            // Checking if the item's ID, name, and URL are not already present
            if (!ids.Contains(item[stopMappings["stop_id"]]))
            {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the ID, name, and URL to their respective hash sets
                ids.Add(item[stopMappings["stop_id"]]);
                
            }
        }
        return newData;
    }
}
