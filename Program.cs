using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter URL to scrape: ");
        string url = Console.ReadLine();

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            Console.WriteLine("Invalid URL.");
            return;
        }

        var httpClient = new HttpClient();
        try
        {
            string html = await httpClient.GetStringAsync(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            Console.WriteLine("\nAll links found:");
            foreach (var link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string href = link.GetAttributeValue("href", string.Empty);
                string text = link.InnerText.Trim();
                Console.WriteLine($"{text} → {href}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
