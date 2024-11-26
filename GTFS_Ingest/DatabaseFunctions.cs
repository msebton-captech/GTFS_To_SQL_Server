using GTFS_Ingest;
using GTFS_Ingest.Repositories;
using System.Data.SqlClient;

class DatabaseFunctions
{
    // Method to upload data to the database
    public static void UploadDataToDatabase(AppConfig config, List<List<string>> newData)
    {
        string connectionString = config.ConnectionString;

        // Establishing a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Opening the database connection
            connection.Open();

            // Routes
            var routesRepository = new RoutesRepository();
            routesRepository.UploadRoutesData(config.RoutesInsertString, config.RoutesSelectString, newData, connection);
        }
    }

    public static void UploadData(AppConfig config)
    {
        // Getting data from the GTFS feed
        //Routes
        string? routeData = DataFunctions.GetRoutesData("routes.txt");

        // Converting the fetched data into a list of lists
        //Routes
        List<List<string>> routeList = DataFunctions.ConvertToListOfLists(routeData);

        // Uploading data to the database using provided connection string, insert query, and select query
        //Routes
        UploadDataToDatabase(config, routeList);
    }
}
