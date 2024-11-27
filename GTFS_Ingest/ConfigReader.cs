using System.Configuration;

class ConfigReader
{
    // Method to read utility configurations from a JSON file
    public static AppConfig ReadConfig()
    {
        var appConfig = new AppConfig();

        // Reading the config values from app config
        appConfig.ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        appConfig.ConnectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        appConfig.DefaultRequestHeaders = ConfigurationManager.AppSettings["DefaultRequestHeaders"];
        appConfig.RoutesInsertString = ConfigurationManager.AppSettings["RoutesInsertString"];
        appConfig.RoutesSelectString = ConfigurationManager.AppSettings["RoutesSelectString"];
        appConfig.RoutesSelectAllString = ConfigurationManager.AppSettings["RoutesSelectAllString"];
        appConfig.TripsInsertString = ConfigurationManager.AppSettings["TripsInsertString"];
        appConfig.TripsSelectString = ConfigurationManager.AppSettings["TripsSelectString"];
        appConfig.TripsSelectAllString = ConfigurationManager.AppSettings["TripsSelectAllString"];

        return appConfig;
    }
}

class AppConfig
{
    // Properties representing various utility configurations
    public string ApiUrl { get; set; } // URL of the API endpoint
    public string ConnectionString { get; set; }  // Connection string for the database
    public string DefaultRequestHeaders { get; set; } // Default request headers for HTTP requests
    public string RoutesInsertString { get; set; } // SQL query for insertion
    public string RoutesSelectString { get; set; } // SQL query for selection
    public string RoutesSelectAllString { get; set; } // SQL query for selection of all records
    public string TripsInsertString { get; set; } // SQL query for insertion
    public string TripsSelectString { get; set; } // SQL query for selection
    public string TripsSelectAllString { get; set; } // SQL query for selection of all records
}
