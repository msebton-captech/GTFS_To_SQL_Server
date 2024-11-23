using System.Configuration;

class Program
{
    static void Main()
    {
        try
        {
            // Path to the JSON file containing utility configurations
            string utilsJsonPath = "utils.json";
            // Retrieving the connection string from the application configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["GoMetroConnectionString"].ConnectionString;
            // Reading utility configurations from the JSON file
            UtilsConfig config = UtilsConfigReader.ReadUtilsConfig(utilsJsonPath);
            // Fetching data from GTFS API for 'agency.txt' file
            string routeData = DataFunctions.GetGTFSData(config.ApiUrl, config.DefaultRequestHeaders, "routes.txt");

            // Converting the fetched data into a list of lists
            List<List<string>> routeList = DataFunctions.ConvertToListOfLists(routeData);

            // Uploading data to the database using provided connection string, insert query, and select query
            int uploadCount = DatabaseFunctions.UploadDataToDatabase(connectionString, routeList, config.InsertString, config.SelectString);
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
