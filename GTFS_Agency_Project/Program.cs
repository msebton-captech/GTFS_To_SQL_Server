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
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            // Reading utility configurations from the JSON file
            UtilsConfig config = UtilsConfigReader.ReadUtilsConfig(utilsJsonPath);
            // Fetching data from GTFS API for 'agency.txt' file
            string agencyData = DataFunctions.GetGTFSData(config.ApiUrl, config.DefaultRequestHeaders, "agency.txt");

            // Converting the fetched data into a list of lists
            List<List<string>> agencyList = DataFunctions.ConvertToListOfLists(agencyData);

            // Uploading data to the database using provided connection string, insert query, and select query
            int uploadCount = DatabaseFunctions.UploadDataToDatabase(connectionString, agencyList, config.InsertString, config.SelectString);
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
