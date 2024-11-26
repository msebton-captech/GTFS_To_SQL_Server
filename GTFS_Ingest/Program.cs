class Program
{
    static void Main()
    {
        try
        {
            // Reading configurations from app config
            AppConfig config = ConfigReader.ReadConfig();

            // Fetching feed data from GTFS API
            DataFunctions.GetGTFSData(config.ApiUrl, config.DefaultRequestHeaders);

            // upload Routes data to the database
            DatabaseFunctions.UploadData(config);
        }
        catch (Exception ex)
        {
            // Handling any exceptions that might occur during execution
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
