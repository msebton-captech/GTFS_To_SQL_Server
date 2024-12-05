﻿using System.Configuration;

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
        // Routes
        appConfig.RoutesInsertString = ConfigurationManager.AppSettings["RoutesInsertString"];
        appConfig.RoutesSelectString = ConfigurationManager.AppSettings["RoutesSelectString"];
        appConfig.RoutesSelectAllString = ConfigurationManager.AppSettings["RoutesSelectAllString"];
        // Trips
        appConfig.TripsInsertString = ConfigurationManager.AppSettings["TripsInsertString"];
        appConfig.TripsSelectString = ConfigurationManager.AppSettings["TripsSelectString"];
        appConfig.TripsSelectAllString = ConfigurationManager.AppSettings["TripsSelectAllString"];
        // Stops
        appConfig.StopsInsertString = ConfigurationManager.AppSettings["StopsInsertString"];
        appConfig.StopsSelectString = ConfigurationManager.AppSettings["StopsSelectString"];
        appConfig.StopsSelectAllString = ConfigurationManager.AppSettings["StopsSelectAllString"];

        // Stop Time
        appConfig.StopTimesInsertString = ConfigurationManager.AppSettings["StopTimesInsertString"];
        appConfig.StopTimesSelectString = ConfigurationManager.AppSettings["StopTimesSelectString"];
        appConfig.StopTimesSelectAllString = ConfigurationManager.AppSettings["StopTimesSelectAllString"];

        // Calendar
        appConfig.CalendarInsertString = ConfigurationManager.AppSettings["CalendarInsertString"];
        appConfig.CalendarSelectString = ConfigurationManager.AppSettings["CalendarSelectString"];
        appConfig.CalendarSelectAllString = ConfigurationManager.AppSettings["CalendarSelectAllString"];

        // Calendar Dates
        appConfig.CalendarDatesInsertString = ConfigurationManager.AppSettings["CalendarDatesInsertString"];
        appConfig.CalendarDatesSelectString = ConfigurationManager.AppSettings["CalendarDatesSelectString"];
        appConfig.CalendarDatesSelectAllString = ConfigurationManager.AppSettings["CalendarDatesSelectAllString"];

        return appConfig;
    }
}

class AppConfig
{
    // Properties representing various utility configurations
    public string ApiUrl { get; set; } // URL of the API endpoint
    public string ConnectionString { get; set; }  // Connection string for the database
    public string DefaultRequestHeaders { get; set; } // Default request headers for HTTP requests
    // Routes
    public string RoutesInsertString { get; set; } // SQL query for insertion
    public string RoutesSelectString { get; set; } // SQL query for selection
    public string RoutesSelectAllString { get; set; } // SQL query for selection of all records
    // Trips
    public string TripsInsertString { get; set; } // SQL query for insertion
    public string TripsSelectString { get; set; } // SQL query for selection
    public string TripsSelectAllString { get; set; } // SQL query for selection of all records
    // Stops
    public string StopsInsertString { get; set; } // SQL query for insertion
    public string StopsSelectString { get; set; } // SQL query for selection
    public string StopsSelectAllString { get; set; } // SQL query for selection of all records
    //Stop Times
    public string StopTimesInsertString { get; set; } // SQL query for insertion
    public string StopTimesSelectString { get; set; } // SQL query for selection
    public string StopTimesSelectAllString { get; set; } // SQL query for selection of all records
    // Calendar
    public string CalendarInsertString { get; set; } // SQL query for insertion
    public string CalendarSelectString { get; set; } // SQL query for selection
    public string CalendarSelectAllString { get; set; } // SQL query for selection of all records
    // Calendar Dates
    public string CalendarDatesInsertString { get; set; } // SQL query for insertion
    public string CalendarDatesSelectString { get; set; } // SQL query for selection
    public string CalendarDatesSelectAllString { get; set; } // SQL query for selection of all records

}
