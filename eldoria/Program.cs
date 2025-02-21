using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var url = "https://github.com/siddjoshi/copilot-hackathon-challenges/blob/main/scrolls.txt";
        var rawUrl = GetRawUrl(url);

        try
        {
            var content = await FetchContentFromUrl(rawUrl);
            ExtractSecrets(content);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    private static string GetRawUrl(string url)
    {
        return url.Replace("github.com", "raw.githubusercontent.com").Replace("/blob", string.Empty);
    }

    private static async Task<string> FetchContentFromUrl(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    private static void ExtractSecrets(string content)
    {
        var regex = new Regex(@"\*(.*?)\*", RegexOptions.Compiled | RegexOptions.Singleline);

        MatchCollection matches = regex.Matches(content);
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                Console.WriteLine(match.Groups[1].Value);
            }
        }
    }
}
