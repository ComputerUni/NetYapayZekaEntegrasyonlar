using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://api.deepai.org/api/text2img";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║            🤖 DeepAI Text To Image           ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Text to Image: ");
        string prompt = Console.ReadLine();
        string result = await ImageGenerator(prompt);
        Console.WriteLine(result);
    }

    private static async Task<string> ImageGenerator(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("api-key", apiKey);
            var content = new MultipartFormDataContent();
            Console.WriteLine("⏳ Resim oluşturuluyor...");

            var response = await client.PostAsync(requestUrl, content);

            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsByteArrayAsync();
                string fullPath = Path.GetFullPath("output.png");
                File.WriteAllBytes(fullPath, responseString);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });

                return $"✅ Resim kaydedildi: {fullPath}";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"❌ Hata: {response.StatusCode}\n{error}";
            }
        }
    }
}