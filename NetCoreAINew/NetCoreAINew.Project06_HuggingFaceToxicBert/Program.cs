using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://router.huggingface.co/hf-inference/models/unitary/toxic-bert";


    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║       AI Toxic Content Analyzer      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Enter your comment: ");
        var comment = Console.ReadLine();
        Console.WriteLine();

        if (!string.IsNullOrEmpty(comment))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Analiz yapılıyor");
            for (int i = 0; i < 3; i++)
            {
                await Task.Delay(500);
                Console.Write(".");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            string result = await ToxicBertAnalyzer(comment);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            Console.WriteLine("  ║            ANALİZ SONUCU             ║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"  {result}");
            Console.WriteLine();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠ Lütfen bir yorum girin.");
            Console.ResetColor();
        }
    }

    static async Task<string> ToxicBertAnalyzer(string comment)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                inputs = comment
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(responseString);
            var results = doc.RootElement[0];

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ┌─────────────────────────────────┐");
            Console.WriteLine("  │         Detay Skorları          │");
            Console.WriteLine("  ├─────────────────────────────────┤");
            foreach (var item in results.EnumerateArray())
            {
                var label = item.GetProperty("label").GetString();
                var score = item.GetProperty("score").GetDouble();

                Console.ForegroundColor = score > 0.5 ? ConsoleColor.Red : ConsoleColor.Green;
                var scoreStr = score.ToString("P2");
                Console.WriteLine($"  │  {label,-15} {scoreStr,8}       │");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  └─────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            var toxicScore = results.EnumerateArray()
                .First(x => x.GetProperty("label").GetString() == "toxic")
                .GetProperty("score")
                .GetDouble();

            string red = "\U0001F534"; 
            string yellow = "\U0001F7E1"; 
            string green = "\U0001F7E2"; 

            if (toxicScore > 0.7)
                return $"{red} Yüksek Risk ({toxicScore:P2})";
            else if (toxicScore > 0.4)
                return $"{yellow} Şüpheli İçerik ({toxicScore:P2})";
            else
                return $"{green} Temiz İçerik ({toxicScore:P2})";


        }
    }
}