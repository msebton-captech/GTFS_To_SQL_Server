using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;

class DataFunctions
{
    public static ZipArchive? _zipArchive { get; set; }

    // Method to remove duplicates from a list of lists based on specific criteria
    public static List<List<string>> RemoveDuplicates(List<List<string>> list)
    {
        // HashSet to store unique IDs, names, and URLs
        HashSet<int> ids = new HashSet<int>();
        HashSet<string> names = new HashSet<string>();
        HashSet<string> urls = new HashSet<string>();
        // List to store filtered data without duplicates
        List<List<string>> newData = new List<List<string>>();

        // Iterating through each item in the list
        foreach (List<string> item in list)
        {
            // Checking if the item's ID, name, and URL are not already present

            // israel
            /* if (!ids.Contains(int.Parse(item[0])) && !names.Contains(item[1]) && !urls.Contains(item[2]))
              {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the ID, name, and URL to their respective hash sets
                ids.Add(int.Parse(item[0]));
                names.Add(item[1]);
                urls.Add(item[2]);
              }
            */

            // metro
            if (!ids.Contains(int.Parse(item[5])) && !names.Contains(item[0]))
            {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the ID, name, and URL to their respective hash sets
                ids.Add(int.Parse(item[5]));
                names.Add(item[0]);
            }
        }
        // Returning the filtered data without duplicates
        return newData;
    }

    public static string? GetRoutesData(string tableName)
    {
        if (_zipArchive != null)
        {
            // Finding the entry corresponding to the specified table name
            var routeEntry = _zipArchive.GetEntry(tableName);
            // If the entry exists
            if (routeEntry != null)
            {
                // Opening the entry stream
                using (var entryStream = routeEntry.Open())
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
    public static List<List<string>> ConvertToListOfLists(string input)
    {
        // List to store the converted data
        List<List<string>> result = new List<List<string>>();
        // Splitting the input string into rows
        //string[] rows = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); // for israel
        string[] rows = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);   // for metro
        // Iterating through each row
        for (int i = 1; i < rows.Length; i++)
            // Splitting each row into columns and adding it to the result list
            result.Add(new List<string>(rows[i].Split(',')));
        // Removing duplicates from the result and returning it
        return RemoveDuplicates(result);
    }
}
