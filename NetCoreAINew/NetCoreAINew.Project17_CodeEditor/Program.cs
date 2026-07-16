using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║        🤖 AI Kod Asistanına Hoş Geldiniz     ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Aşağıdaki işlemlerden birini seç.\n");

        string[] choices = { "Kodu Açıkla", "Hata Bul & Düzelt", "Optimize Et", "Unit Test Yaz" };

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Yapmak istediğiniz işlemi seçiniz (çıkış yapmak için 'exit' yazınız): ");
            Console.WriteLine();

            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i]}");
            }

            Console.WriteLine();
            Console.Write("Seçiminiz: ");

            string input = Console.ReadLine();

            if (input.ToLower() == "exit") Environment.Exit(0);

            if(!int.TryParse(input, out int choose) || choose < 1 || choose > choices.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı seçim yaptınız lütfen tekrar deneyin!\n");
                Console.ResetColor();
                continue;
            } 

            string choice = choices[choose - 1];
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Kodunuzu yapıştırın (bitince ENTER'a bas):\n");
            Console.ResetColor();

            var satirlar = new StringBuilder();
            string satir = Console.ReadLine();

            while (!string.IsNullOrEmpty(satir))
            {
                satirlar.AppendLine(satir);
                satir = Console.ReadLine();
            }

            string code = satirlar.ToString().Trim();

            string choosePrompt = choose switch
            {
                1 => $"Aşağıdaki kodu Türkçe olarak açıkla:\n\n{code}",
                2 => $"Aşağıdaki kodun hatalarını bul ve düzeltilmiş halini yaz:\n\n{code}",
                3 => $"Aşağıdaki kodu optimize et ve neden optimize ettiğini açıkla:\n\n{code}",
                4 => $"Aşağıdaki kod için unit test yaz:\n\n{code}",
                _ => null
            };

            string system = "Sen uzman bir yazılım geliştirme asistanısın. Net ve Türkçe cevaplar ver.";
            string answer = await CodeEditor(choice, choosePrompt, system);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAI Yanıtı:\n");
            Console.WriteLine(answer);
            Console.ResetColor();
        }
    }

    private static async Task<string> CodeEditor(string choice, string code, string system)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "nvidia/nemotron-3-ultra-550b-a55b:free",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = system
                    },

                    new
                    {
                        role = "user",
                        content = code
                    }

                },

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