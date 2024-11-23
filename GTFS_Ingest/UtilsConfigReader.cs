using Newtonsoft.Json;

class UtilsConfigReader
{
    // Method to read utility configurations from a JSON file
    public static UtilsConfig ReadUtilsConfig(string filePath)
    {
        // Reading the JSON content from the specified file path
        string json = File.ReadAllText(filePath);
        // Deserializing the JSON content into UtilsConfig object and returning it
        return JsonConvert.DeserializeObject<UtilsConfig>(json);
    }
}

class UtilsConfig
{
    // Properties representing various utility configurations
    public string InsertString { get; set; } // SQL query for insertion
    public string SelectString { get; set; } // SQL query for selection
    public string DefaultRequestHeaders { get; set; } // Default request headers for HTTP requests
    public string ApiUrl { get; set; } // URL of the API endpoint
}
