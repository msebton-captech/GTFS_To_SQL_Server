using Microsoft.Data.SqlClient;
using System.Linq;

namespace GTFS_Ingest.Repositories;

public class StopTimesRepository
{
    public void UploadStopTimesData(string insertString, string selectString, List<List<string>> newData, SqlConnection connection)
    {
        // Variable to keep track of the number of records uploaded
        int uploadCount = 0;

        // Dictionary to map keys to their respective indices
        var stopTimesMappings = new Mappings().StopTimes();

        // Iterating through the list of lists to upload data to the database
        foreach (List<string> values in newData)
        {
            // Checking if the data already exists in the database
            if (!DoesStopDataExist(connection, values, selectString, stopTimesMappings))
            {
                // If data does not exist, preparing to insert it into the database
                using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                {
                    // Adding parameters for insertion
                    insertCommand.Parameters.AddWithValue("@Value1", values[stopTimesMappings["trip_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value2", GetFormattedTime(values[stopTimesMappings["arrival_time"]]));
                    insertCommand.Parameters.AddWithValue("@Value3", GetFormattedTime(values[stopTimesMappings["departure_time"]]));
                    insertCommand.Parameters.AddWithValue("@Value4", values[stopTimesMappings["stop_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value5", values[stopTimesMappings["stop_sequence"]]);
                    insertCommand.Parameters.AddWithValue("@Value6", values[stopTimesMappings["stop_headsign"]]);
                    insertCommand.Parameters.AddWithValue("@Value7", values[stopTimesMappings["pickup_type"]]);
                    insertCommand.Parameters.AddWithValue("@Value8", values[stopTimesMappings["drop_off_type"]]);
                    insertCommand.Parameters.AddWithValue("@Value9", values[stopTimesMappings["shape_dist_traveled"]]);
                    insertCommand.Parameters.AddWithValue("@Value10", values[stopTimesMappings["timepoint"]]);

                    // Executing the insertion command
                    insertCommand.ExecuteNonQuery();
                    // Incrementing the upload count
                    uploadCount++;
                }
            }
        }
        // Outputting the number of records uploaded
        Console.WriteLine($"Uploaded {uploadCount} records to the Stop Times table.");
    }

    // Method to check if data already exists in the database
    public bool DoesStopDataExist(SqlConnection connection, List<string> values, string selectString, Dictionary<string, int> stopTimesMappings)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query
            selectCommand.Parameters.AddWithValue("@Value1", values[stopTimesMappings["trip_id"]]);
            selectCommand.Parameters.AddWithValue("@Value2", GetFormattedTime(values[stopTimesMappings["departure_time"]]));
            selectCommand.Parameters.AddWithValue("@Value3", values[stopTimesMappings["stop_id"]]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }

    public List<List<string>> RemoveStopTimesDuplicates(List<List<string>> list, Dictionary<string, int> stopTimesMappings)
    {
        // HashSet to store unique IDs, names, and URLs
        HashSet<string> ids = new HashSet<string>();

        List<List<string>> newData = new List<List<string>>();

        // Iterating through each item in the list
        foreach (List<string> item in list)
        {
            // Creating a unique key for the combination of trip_id, departure_time and stop_id
            string uniqueKey = item[stopTimesMappings["trip_id"]] + item[stopTimesMappings["departure_time"]] + item[stopTimesMappings["stop_id"]];

            // Checking if the unique key is not already present
            if (!ids.Contains(uniqueKey))
            {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the unique key to the hash set
                ids.Add(uniqueKey);
            }
        }

        return newData;
    }

    private string GetFormattedTime(string time)
    {
        if (int.TryParse(time.Substring(0, 2), out int hours))
        {
            if (hours == 24)
            {
                return time.Replace("24:", "00:");
            }
            else if (hours > 24)
            {
                return time.Replace(hours.ToString(), (hours - 24).ToString());
            }
            else
            {
                return time;
            }
        }
        else
        {
            throw new FormatException("Invalid time format");
        }
    }
}
