using System.Configuration;

class Program
{
    static void Main()
    {
        try
        {
            // Reading configurations from app config
            AppConfig config = ConfigReader.ReadConfig();

            // Fetching data from GTFS API for 'routes.txt' file
            string routeData = DataFunctions.GetGTFSData(config.ApiUrl, config.DefaultRequestHeaders, "routes.txt");

            // Converting the fetched data into a list of lists
            List<List<string>> routeList = DataFunctions.ConvertToListOfLists(routeData);

            // Uploading data to the database using provided connection string, insert query, and select query
            int uploadCount = DatabaseFunctions.UploadDataToDatabase(config.ConnectionString, routeList, config.RoutesInsertString, config.RoutesSelectString);
            
            Console.WriteLine("Data uploaded successfully.");
            // Displaying the number of records uploaded to the database
            Console.WriteLine($"Number of records uploaded: {uploadCount}");
        }
        catch (Exception ex)
        {
            // Handling any exceptions that might occur during execution
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
