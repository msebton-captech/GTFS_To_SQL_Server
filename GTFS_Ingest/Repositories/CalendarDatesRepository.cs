using Microsoft.Data.SqlClient;

namespace GTFS_Ingest.Repositories;

public class CalendarDatesRepository
{
    public void UploadCalendarDatesData(string insertString, string selectString, List<List<string>> newData, SqlConnection connection)
    {
        // Variable to keep track of the number of records uploaded
        int uploadCount = 0;

        // Dictionary to map keys to their respective indices
        var calendarDatesMappings = new Mappings().CalendarDates();

        // Iterating through the list of lists to upload data to the database
        foreach (List<string> values in newData)
        {
            // Checking if the data already exists in the database
            if (!DoesCalendarDatesDataExist(connection, values, selectString, calendarDatesMappings))
            {
                // If data does not exist, preparing to insert it into the database
                using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                {
                    // Adding parameters for insertion
                    insertCommand.Parameters.AddWithValue("@Value1", values[calendarDatesMappings["service_id"]]);
                    insertCommand.Parameters.AddWithValue("@Value2", values[calendarDatesMappings["date"]]);
                    insertCommand.Parameters.AddWithValue("@Value3", values[calendarDatesMappings["exception_type"]]);

                    // Executing the insertion command
                    insertCommand.ExecuteNonQuery();
                    // Incrementing the upload count
                    uploadCount++;
                }
            }
        }
        // Outputting the number of records uploaded
        Console.WriteLine($"Uploaded {uploadCount} records to the CalendarDates table.");
    }

    // Method to check if data already exists in the database
    public bool DoesCalendarDatesDataExist(SqlConnection connection, List<string> values, string selectString, Dictionary<string, int> calendarDatesMappings)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query
            selectCommand.Parameters.AddWithValue("@Value1", values[calendarDatesMappings["service_id"]]);
            selectCommand.Parameters.AddWithValue("@Value2", values[calendarDatesMappings["date"]]);
            selectCommand.Parameters.AddWithValue("@Value3", values[calendarDatesMappings["exception_type"]]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }

    public List<List<string>> RemoveCalendarDatesDuplicates(List<List<string>> list, Dictionary<string, int> calendarDatesMappings)
    {
        // HashSet to store unique IDs and dates
        HashSet<string> ids = new HashSet<string>();

        List<List<string>> newData = new List<List<string>>();

        // Iterating through each item in the list
        foreach (List<string> item in list)
        {
            // Creating a unique key for the combination of service_id and date
            string uniqueKey = item[calendarDatesMappings["service_id"]] + item[calendarDatesMappings["date"]];

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
}