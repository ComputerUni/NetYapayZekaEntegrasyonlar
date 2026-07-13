using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://router.huggingface.co/hf-inference/models/dslim/bert-base-NER";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Please input text here: ");
        var inputText = Console.ReadLine();

        if (!string.IsNullOrEmpty(inputText))
        {
            Console.WriteLine();
            Console.WriteLine("AI Tarafından Tarama Yapılıyor");
            var result = await NamedEntityRecognition(inputText);
            Console.WriteLine();
            Console.WriteLine("🔍 NER Çıktısı");
            Console.WriteLine();
            Console.WriteLine(result);
        }

    }

    private static async Task<string> NamedEntityRecognition(string text)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var requestBody = new
            {
                inputs = text,
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(responseString);
            var sb = new StringBuilder();

            foreach (var item in doc.RootElement.EnumerateArray())
            {
                string entity = item.GetProperty("entity_group").GetString();
                string word = item.GetProperty("word").GetString();
                if (word.StartsWith("##")) continue;
                double score = Math.Round(item.GetProperty("score").GetDouble() * 100, 2);
                string icon = entity switch
                {
                    "PER" => "👤",
                    "ORG" => "🏢",
                    "LOC" => "📍",
                    _ => "❓"
                };
                sb.AppendLine($" {icon} {entity,-6} | {word,-20} | %{score}");
            }

            return sb.ToString();
        }
    }
}
