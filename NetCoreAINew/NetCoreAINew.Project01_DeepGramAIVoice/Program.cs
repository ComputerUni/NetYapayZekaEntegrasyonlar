using System.Net.Http.Headers;
using System.Text.Json;

//Bir nesne dışarıdaki bir kaynağı (dosya, bağlantı) tutuyorsa → using kullan.
class Program
{
    private static readonly string apiKey = "";
    private static readonly string filePath = "";
    static async Task Main(string[] args)
    {
        if(!File.Exists(filePath))
        {
            Console.WriteLine("Dosya Bulunamadı");
            return;
        }

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
        using var fileStream = File.OpenRead(filePath);

        var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3");

        var response = await client.PostAsync("https://api.deepgram.com/v1/listen", content);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"API Hatası ({response.StatusCode}): {json}");
            return;
        }

        try
        {
            var doc = JsonDocument.Parse(json);
            var transcript = doc.RootElement.GetProperty("results").GetProperty("channels")[0].GetProperty("alternatives")[0].GetProperty("transcript").GetString();
            Console.WriteLine();
            Console.WriteLine("Transcribe Metni: \n");
            Console.WriteLine(transcript);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
        
    }
}