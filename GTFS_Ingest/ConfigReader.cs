using Newtonsoft.Json;
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

        return appConfig;
    }
}

class AppConfig
{
    // Properties representing various utility configurations
    public string RoutesInsertString { get; set; } // SQL query for insertion
    public string RoutesSelectString { get; set; } // SQL query for selection
    public string RoutesSelectAllString { get; set; } // SQL query for selection of all records
    public string DefaultRequestHeaders { get; set; } // Default request headers for HTTP requests
    public string ApiUrl { get; set; } // URL of the API endpoint
    public string ConnectionString { get; set; }  // Connection string for the database
}
