using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var url = GetRawUrl("https://github.com/siddjoshi/copilot-hackathon-challenges/blob/main/scrolls.txt");

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                
                string[] lines = responseBody.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                Regex regex = new Regex(@"\*(.*)\*");

                foreach (string line in lines)
                {                    
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        Console.WriteLine(match.Groups[1].Value);
                    }
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }

    private static string GetRawUrl(string url)
    {
        var rawUrl = url.Replace("github.com", "raw.githubusercontent.com");
        rawUrl = rawUrl.Replace("/blob", string.Empty);
        return rawUrl;
    }
}
