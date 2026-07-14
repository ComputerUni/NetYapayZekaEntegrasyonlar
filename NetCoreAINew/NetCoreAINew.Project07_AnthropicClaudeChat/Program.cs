using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║            AI Chatbot                ║");
        Console.WriteLine("║       Çıkmak için 'exit' yazın       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        while(true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Sen: ");
            Console.ResetColor();
            var prompt = Console.ReadLine();
            if (string.IsNullOrEmpty(prompt)) continue;
            if (prompt?.ToLower() == "exit")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Görüşmek Üzere!");
                Console.ResetColor();
                break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.Write("Yanıt Bekleniyor");
            for(int i = 0; i < 3; i++)
            {
                await Task.Delay(500);
                Console.Write(".");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            string answer = await Chating(prompt);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("AI: ");
            Console.ResetColor();
            Console.WriteLine(answer);
            Console.WriteLine();
        }
    }

    private static async Task<string> Chating(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "openrouter/free",
                max_tokens = 2048,
                temperature = 0.7,
                messages = new[]
    {
       new { role = "system", content = "Sen yardımcı bir asistansın. Kullanıcıların sorularını Türkçe olarak yanıtla. Kesinlikle markdown formatlaması kullanma. Yıldız (*), diyez (#), tire (-), alt çizgi (_) gibi hiçbir özel karakter kullanma. Tablo oluşturma. Madde işareti yerine 1. 2. 3. şeklinde numaralı liste kullan. Düz ve okunaklı metin yaz." },
        new { role = "user", content = prompt }
    }
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                return result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            }
            else
            {
                Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                return $"Hata: {responseString}";

            }
        }
    }
}