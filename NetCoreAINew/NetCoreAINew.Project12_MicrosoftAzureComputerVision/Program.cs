using System.Net.Http.Headers;
using System.Text.Json;

class Program
{
    private static readonly string subscriptionKey = "";
    private static readonly string endPoint = "";
    private static readonly string apiUrl = $"{endPoint}/vision/v3.2/analyze";
    private static readonly string requestParameters = "visualFeatures=Categories,Description,Tags,Color&language=en";
    private static readonly string uri = apiUrl + "?" + requestParameters;

    static async Task Main(string[] args)
    {
        string imagePath = "";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file is not found!" + imagePath);
            return;
        }

        byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);

        using (var client = new HttpClient())
        using (ByteArrayContent content = new ByteArrayContent(imageBytes))
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await client.PostAsync(uri, content);
            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Azure Yanıtı: ");
                JsonDocument json = JsonDocument.Parse(result);
                var desc = json.RootElement.GetProperty("description").GetProperty("captions")[0];
                string text = desc.GetProperty("text").GetString();

                double confidence = desc.GetProperty("confidence").GetDouble();

                Console.WriteLine($"Açıklama: {text} (Güven: %{confidence * 100:0.00})");

            }
            else
            {
                Console.WriteLine("Bir hata oluştu!");
                Console.WriteLine($"Status {response.StatusCode}");
                Console.WriteLine("Yanıt: " + result);
            }
        }
    }
}