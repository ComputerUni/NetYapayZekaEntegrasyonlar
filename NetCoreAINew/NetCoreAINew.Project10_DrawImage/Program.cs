using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string token = "";
    private static readonly string requestUrl = "https://router.huggingface.co/hf-inference/models/stabilityai/stable-diffusion-3-medium-diffusers";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Text to Image: ");
        string prompt = Console.ReadLine();
        string result = await textToImage(prompt);
        Console.WriteLine(result);
    }

    private static async Task<string> textToImage(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                inputs = prompt
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("⏳ Resim oluşturuluyor...");

            var response = await client.PostAsync(requestUrl, content);
           

            if (response.IsSuccessStatusCode)
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