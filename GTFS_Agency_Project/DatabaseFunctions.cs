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
                        insertCommand.Parameters.AddWithValue("@Value1", values[0]);
                        insertCommand.Parameters.AddWithValue("@Value2", values[1]);
                        insertCommand.Parameters.AddWithValue("@Value3", values[2]);
                        insertCommand.Parameters.AddWithValue("@Value4", values[3]);
                        insertCommand.Parameters.AddWithValue("@Value5", values[4]);
                        insertCommand.Parameters.AddWithValue("@Value6", values[5]);
                        insertCommand.Parameters.AddWithValue("@Value7", values[6]);
                        insertCommand.Parameters.AddWithValue("@Value8", " ");

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
            selectCommand.Parameters.AddWithValue("@Value1", values[0]);
            selectCommand.Parameters.AddWithValue("@Value2", values[1]);
            selectCommand.Parameters.AddWithValue("@Value3", values[2]);

            // Executing the SELECT query and retrieving the count of matching records
            int count = (int)selectCommand.ExecuteScalar();
            // Returning true if count is greater than 0, indicating data exists; otherwise, false
            return count > 0;
        }
    }
}
