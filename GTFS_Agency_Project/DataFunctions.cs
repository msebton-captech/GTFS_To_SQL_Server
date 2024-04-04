using System.IO.Compression;
using System.Text;

class DataFunctions
{
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
            if (!ids.Contains(int.Parse(item[0])) && !names.Contains(item[1]) && !urls.Contains(item[2]))
            {
                // Adding the item to the filtered data
                newData.Add(item);
                // Adding the ID, name, and URL to their respective hash sets
                ids.Add(int.Parse(item[0]));
                names.Add(item[1]);
                urls.Add(item[2]);
            }
        }
        // Returning the filtered data without duplicates
        return newData;
    }

    // Method to fetch GTFS data from a specified URL
    public static string GetGTFSData(string url, string defaultRequestHeaders, string tableName)
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
                using (var memoryStream = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result))
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                {
                    // Finding the entry corresponding to the specified table name
                    var agencyEntry = zipArchive.GetEntry(tableName);
                    // If the entry exists
                    if (agencyEntry != null)
                    {
                        // Opening the entry stream
                        using (var entryStream = agencyEntry.Open())
                        using (var streamReader = new StreamReader(entryStream, Encoding.UTF8))
                        {
                            // Reading and returning the content of the entry
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                // Returning error message if the request fails
                return $"Failed to fetch data from the URL: {(int)response.StatusCode}";
            }
        }

        // Returning null if data retrieval fails
        return null;
    }

    // Method to convert a string input to a list of lists
    public static List<List<string>> ConvertToListOfLists(string input)
    {
        // List to store the converted data
        List<List<string>> result = new List<List<string>>();
        // Splitting the input string into rows
        string[] rows = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        // Iterating through each row
        for (int i = 1; i < rows.Length; i++)
            // Splitting each row into columns and adding it to the result list
            result.Add(new List<string>(rows[i].Split(',')));
        // Removing duplicates from the result and returning it
        return RemoveDuplicates(result);
    }
}
