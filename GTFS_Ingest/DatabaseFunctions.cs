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

            // Iterating through each set of data in newData
            foreach (List<string> values in newData)
            {
                // Checking if the data already exists in the database
                if (!IsDataExists(connection, values, selectString))
                {
                    // If data does not exist, preparing to insert it into the database
                    using (SqlCommand insertCommand = new SqlCommand(insertString, connection))
                    {
                        // Adding parameters for insertion
                        
                        // israel
                        //insertCommand.Parameters.AddWithValue("@Value1", values[0]);
                        //insertCommand.Parameters.AddWithValue("@Value2", values[1]);
                        //insertCommand.Parameters.AddWithValue("@Value3", values[2]);
                        //insertCommand.Parameters.AddWithValue("@Value4", values[3]);
                        //insertCommand.Parameters.AddWithValue("@Value5", values[4]);
                        //insertCommand.Parameters.AddWithValue("@Value6", values[5]);
                        //insertCommand.Parameters.AddWithValue("@Value7", values[6]);
                        //insertCommand.Parameters.AddWithValue("@Value8", "FFFFFF");
                        //insertCommand.Parameters.AddWithValue("@Value9", 0);

                        // metro
                        insertCommand.Parameters.AddWithValue("@Value1", values[5]);
                        insertCommand.Parameters.AddWithValue("@Value2", values[4]);
                        insertCommand.Parameters.AddWithValue("@Value3", values[8]);
                        insertCommand.Parameters.AddWithValue("@Value4", values[0]);
                        insertCommand.Parameters.AddWithValue("@Value5", values[6]);
                        insertCommand.Parameters.AddWithValue("@Value6", values[1]);
                        insertCommand.Parameters.AddWithValue("@Value7", values[7]);
                        insertCommand.Parameters.AddWithValue("@Value8", values[3]);
                        insertCommand.Parameters.AddWithValue("@Value9", values[2]);

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
    public static bool IsDataExists(SqlConnection connection, List<string> values, string selectString)
    {
        // Executing a SELECT query to check for existing data
        using (SqlCommand selectCommand = new SqlCommand(selectString, connection))
        {
            // Adding parameters to the SELECT query

            // israel
            //selectCommand.Parameters.AddWithValue("@Value1", values[0]);
            //selectCommand.Parameters.AddWithValue("@Value2", values[1]);
            //selectCommand.Parameters.AddWithValue("@Value3", values[2]);

            // metro
            selectCommand.Parameters.AddWithValue("@Value1", values[5]);
            selectCommand.Parameters.AddWithValue("@Value2", values[4]);
            selectCommand.Parameters.AddWithValue("@Value3", values[0]);

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
