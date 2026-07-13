using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var apiKey = "";
Console.OutputEncoding = Encoding.UTF8;
Console.Write("Enter your text here: ");
var inputText = Console.ReadLine();

var requestData = new
{
    inputs = inputText
};


var json = JsonSerializer.Serialize(requestData);
var content = new StringContent(json, Encoding.UTF8, "application/json");

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
var response = await client.PostAsync("https://router.huggingface.co/hf-inference/models/facebook/bart-large-cnn", content);
var responseString = await response.Content.ReadAsStringAsync();

var doc = JsonDocument.Parse(responseString);
var summary = doc.RootElement[0].GetProperty("summary_text").GetString();

Console.WriteLine();
Console.WriteLine("🗒️ Text Summarize 🗒️");
Console.WriteLine();
Console.WriteLine($"{summary}");


var wordCount = inputText
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(w => w.Trim('.', ',', '!', '?', ';', ':').ToLower())
    .Where(w => w.Length > 3)
    .GroupBy(w => w)
    .OrderByDescending(g => g.Count())
    .Take(5);

Console.WriteLine("\n📊 En Çok Geçen 5 Kelime:");
foreach(var word in wordCount)
{
    Console.WriteLine($"  {word.Key} → {word.Count()} kez");
}