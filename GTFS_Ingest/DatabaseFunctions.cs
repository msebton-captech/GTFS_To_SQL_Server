using System.Data.SqlClient;

class DatabaseFunctions
{
    // Method to upload data to the database
    public static int UploadDataToDatabase(string connectionString, List<List<string>> newData, string insertString, string selectString)
    {
        // Variable to keep track of the number of records uploaded
        int uploadCount = 0;

        // Establishing a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Opening the database connection
            connection.Open();

            // Dictionary to map keys to their respective indices
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

            // Iterating through each set of data in newData
            foreach (List<string> values in newData)
            {
                // Checking if the data already exists in the database
                if (!IsDataExists(connection, values, selectString, keyIndexMap))
                {
                    // If data does not exist, preparing to insert it into the database
                    using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                    {
                        // Adding parameters for insertion
                        insertCommand.Parameters.AddWithValue("@Value1", values[keyIndexMap["route_id"]]);
                        insertCommand.Parameters.AddWithValue("@Value2", values[keyIndexMap["agency_id"]]);
                        insertCommand.Parameters.AddWithValue("@Value3", values[keyIndexMap["route_short_name"]]);
                        insertCommand.Parameters.AddWithValue("@Value4", values[keyIndexMap["route_long_name"]]);
                        insertCommand.Parameters.AddWithValue("@Value5", values[keyIndexMap["route_desc"]]);
                        insertCommand.Parameters.AddWithValue("@Value6", values[keyIndexMap["route_type"]]);
                        insertCommand.Parameters.AddWithValue("@Value7", values[keyIndexMap["route_url"]]);
                        insertCommand.Parameters.AddWithValue("@Value8", values[keyIndexMap["route_color"]]);
                        insertCommand.Parameters.AddWithValue("@Value9", values[keyIndexMap["route_text_color"]]);

                        // Executing the insertion command
                        insertCommand.ExecuteNonQuery();
                        // Incrementing the upload count
                        uploadCount++;
                    }
                }
            }
        }

        // Returning the total number of records uploaded
        return uploadCount;
    }

    // Method to check if data already exists in the database
    public static bool IsDataExists(SqlConnection connection, List<string> values, string selectString, Dictionary<string, int> keyIndexMap)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query
            selectCommand.Parameters.AddWithValue("@Value1", values[keyIndexMap["route_id"]]);
            selectCommand.Parameters.AddWithValue("@Value2", values[keyIndexMap["agency_id"]]);
            selectCommand.Parameters.AddWithValue("@Value3", values[keyIndexMap["route_long_name"]]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }

    public static void UploadRoutesData(AppConfig config)
    {
        // Getting data from the GTFS feed for 'routes.txt' file
        string? routeData = DataFunctions.GetRoutesData("routes.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> routeList = DataFunctions.ConvertToListOfLists(routeData);

        // Uploading data to the database using provided connection string, insert query, and select query
        int uploadCount = UploadDataToDatabase(config.ConnectionString, routeList, config.RoutesInsertString, config.RoutesSelectString);

        Console.WriteLine("Routes data uploaded successfully.");
        // Displaying the number of records uploaded to the database
        Console.WriteLine($"Number of records uploaded: {uploadCount}");
        Console.WriteLine();
    }
}
