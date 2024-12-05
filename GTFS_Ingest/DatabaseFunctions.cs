using GTFS_Ingest;
using GTFS_Ingest.Repositories;
using Microsoft.Data.SqlClient;
class DatabaseFunctions
{
    // Method to upload data to the database
    public static void UploadDataToDatabase(AppConfig config, List<List<string>> newData, TransitData transitData)
    {
        string connectionString = config.ConnectionString;

        // Establishing a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Opening the database connection
            connection.Open();

            // Calendar
            if (transitData == TransitData.Calendar)
            {
                var calendarRepository = new CalendarRepository();
                calendarRepository.UploadCalendarData(config.CalendarInsertString, config.CalendarSelectString, newData, connection);
            }

            // Calendar Dates
            if (transitData == TransitData.CalendarDates)
            {
                var calendarDatesRepository = new CalendarDatesRepository();
                calendarDatesRepository.UploadCalendarDatesData(config.CalendarDatesInsertString, config.CalendarDatesSelectString, newData, connection);
            }

            // Routes
            if (transitData == TransitData.Routes)
            {
                var routesRepository = new RoutesRepository();
                routesRepository.UploadRoutesData(config.RoutesInsertString, config.RoutesSelectString, newData, connection); 
            }

            // Trips
            if (transitData == TransitData.Trips)
            {
                var tripsRepository = new TripsRepository();
                tripsRepository.UploadTripsData(config.TripsInsertString, config.TripsSelectString, newData, connection); 
            }

            // Stops
            if (transitData == TransitData.Stops)
            {
                var stopsRepository = new StopsRepository();
                stopsRepository.UploadStopsData(config.StopsInsertString, config.StopsSelectString, newData, connection);
            }

            // Stop Times
            if (transitData == TransitData.StopTimes)
            {
                var stopTimesRepository = new StopTimesRepository();
                stopTimesRepository.UploadStopTimesData(config.StopTimesInsertString, config.StopTimesSelectString, newData, connection);
            }
        }
    }

    public static void UploadData(AppConfig config)
    {
        /* Calendar*/
        // Getting data from the GTFS feed
        string? calendarData = DataFunctions.GetTableData("calendar.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> calendarList = DataFunctions.ConvertToListOfLists(calendarData, TransitData.Calendar);

        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, calendarList, TransitData.Calendar);

        /* Calendar Dates */
        // Getting data from the GTFS feed
        string? calendarDatesData = DataFunctions.GetTableData("calendar_dates.txt");
        // Converting the fetched data into a list of lists
        List<List<string>> calendarDatesList = DataFunctions.ConvertToListOfLists(calendarDatesData, TransitData.CalendarDates);
        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, calendarDatesList, TransitData.CalendarDates);

        /* Routes */
        // Getting data from the GTFS feed
        string? routeData = DataFunctions.GetTableData("routes.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> routeList = DataFunctions.ConvertToListOfLists(routeData, TransitData.Routes);

        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, routeList, TransitData.Routes);

        /* Trips */
        // Getting data from the GTFS feed
        string? tripData = DataFunctions.GetTableData("trips.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> tripList = DataFunctions.ConvertToListOfLists(tripData, TransitData.Trips);

        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, tripList, TransitData.Trips);

        /* Stops */
        // Getting data from the GTFS feed
        string? stopData = DataFunctions.GetTableData("stops.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> stopList = DataFunctions.ConvertToListOfLists(stopData, TransitData.Stops);

        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, stopList, TransitData.Stops);

        /* Stop Times */
        // Getting data from the GTFS feed
        string? stopTimesData = DataFunctions.GetTableData("stop_times.txt");

        // Converting the fetched data into a list of lists
        List<List<string>> stopTimesList = DataFunctions.ConvertToListOfLists(stopTimesData, TransitData.StopTimes);

        // Uploading data to the database using provided connection string, insert query, and select query
        UploadDataToDatabase(config, stopTimesList, TransitData.StopTimes);


        // dispose data stream
        DataFunctions.DisposeStream();
    }
}
