using GTFS_Ingest;
using GTFS_Ingest.Repositories;
using System.IO.Compression;
using System.Text;

class DataFunctions
{
    public static ZipArchive? _zipArchive;

    // Method to remove duplicates from a list of lists based on specific criteria
    public static List<List<string>> RemoveDuplicates(List<List<string>> list, TransitData transitData)
    {
        List<List<string>> newData = new List<List<string>>();

        // List to store filtered data without duplicates       
        // Routes
        if (transitData == TransitData.Routes)
        {
            var routesRepository = new RoutesRepository();
            newData = routesRepository.RemoveRoutesDuplicates(list, new Mappings().Routes());

            // Returning the filtered data without duplicates
            return newData; 
        }
        // Trips
        else if (transitData == TransitData.Trips)
        {
            var tripsRepository = new TripsRepository();
            newData = tripsRepository.RemoveTripsDuplicates(list, new Mappings().Trips());
            
            // Returning the filtered data without duplicates
            return newData;
        }
        return newData;
    }

    public static string? GetTableData(string tableName)
    {
        if (_zipArchive != null)
        {
            // Finding the entry corresponding to the specified table name
            var entry = _zipArchive.GetEntry(tableName);
            // If the entry exists
            if (entry != null)
            {
                // Opening the entry stream
                using (var entryStream = entry.Open())
                using (var streamReader = new StreamReader(entryStream, Encoding.UTF8))
                {
                    // Reading and returning the content of the entry
                    return streamReader.ReadToEnd();
                }
            }
        }
        return null;
    }

    // Method to fetch GTFS data from a specified URL
    public static void GetGTFSData(string url, string defaultRequestHeaders)
    {
        // Using HttpClient to make HTTP requests
        using (var httpClient = new HttpClient())
        {
            // Adding default request headers
            httpClient.DefaultRequestHeaders.Add("User-Agent", defaultRequestHeaders);
            // Sending GET request to the specified URL
            var response = httpClient.GetAsync(url).Result;

            // Checking if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Extracting data from the response content
                var memoryStream = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result);
                var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read);
                // Setting zip archive if the data is successfully fetched
                _zipArchive = zipArchive;
            }
        }
    }

    // Method to convert a string input to a list of lists
    public static List<List<string>> ConvertToListOfLists(string input, TransitData transitData)
    {
        // List to store the converted data
        List<List<string>> result = new List<List<string>>();

        // Splitting the input string into rows
        string[] rows = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        // Iterating through each row
        for (int i = 1; i < rows.Length; i++)
            // Splitting each row into columns and adding it to the result list
            result.Add(new List<string>(rows[i].Split(',')));

        // Removing duplicates from the result and returning it
        return RemoveDuplicates(result, transitData);
    }

    public static void DisposeStream()
    {
        if (_zipArchive != null)
        {
            _zipArchive.Dispose();
            _zipArchive = null;
        }
    }
}
