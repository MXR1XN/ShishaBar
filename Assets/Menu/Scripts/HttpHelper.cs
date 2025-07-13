using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HttpHelper
{
    public static async Task<string> Post(string url, List<KeyValuePair<string, string>> formData)
    {
        using (HttpClient client = new HttpClient())
        using (FormUrlEncodedContent content = new FormUrlEncodedContent(formData))
        {
            try
            {
                // Send POST
                var response = await client.PostAsync(url, content);

                // Check status code
                response.EnsureSuccessStatusCode();

                // Read the response 
                var responseString = await response.Content.ReadAsStringAsync();

                // Return response
                return responseString;
            }
            catch (Exception ex)
            {
                return "{\"status\":\"error\",\"message\":\"Server connection error. Error explanation: " + ex.Message + "\"}";
            }
        }
    }

    // GET Method
    public static async Task<string> Get(string url, List<KeyValuePair<string, string>> formData)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                if (formData != null && formData.Count > 0)
                {
                    var queryString = await new FormUrlEncodedContent(formData).ReadAsStringAsync();
                    url = $"{url}?{queryString}";
                }

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                return $"HttpRequestException: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
    public static string ExtractValue(string json, string key)
    {
        string searchKey = $"\"{key}\":";
        int startIndex = json.IndexOf(searchKey) + searchKey.Length;

        if (startIndex == -1)
            throw new Exception($"Key '{key}' not found in JSON");

        char firstChar = json[startIndex];
        if (firstChar == '"')
        {
            startIndex++;
            int endIndex = json.IndexOf('"', startIndex);
            return json.Substring(startIndex, endIndex - startIndex);
        }
        else
        {
            int endIndex = json.IndexOfAny(new char[] { ',', '}' }, startIndex);
            return json.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}

public class Response
{
    public string status;
    public string message;
}

public class ScoreData
{
    public int scour;
    public string createdAt;
    public string userName;
    public int rank;
}

public class DLCData
{
    public string Name;
    public string Price;
}

public class ResponseData
{
    public string status;
    public List<ScoreData> data;
}

public class ResponseDataDLC
{
    public string status;
    public List<DLCData> data;
}