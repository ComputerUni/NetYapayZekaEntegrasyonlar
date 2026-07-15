using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        string context = "Sen bir yapay zeka içerik planlayıcısısın. Kullanıcının fikrine göre içerik üretmesine yardım edeceksin. Fikri aldıktan sonra kullanıcıya doğru soruları sorarak onu yönlendirecek ve sonunda içerik planını çıkaracaksın";

        Console.Write("Bir fikrin mi var? (örnek: bir yazılım şirketi açmak istiyorum): ");
        string input = Console.ReadLine();

        string prompt = $"{context}\n\n Kullanıcının fikri: {input}\n Şimdi ona adım adım sorular sormaya başla.";

        for(int i = 0; i < 5; i++)
        {
            string question = await SendToAI(apiKey, requestUrl, prompt);
            Console.Write($"Agent: {question}");
            Console.WriteLine();

            Console.Write("Sen: ");
            string answer = Console.ReadLine();

            prompt += $"\n\nKullanıcının Cevabı: {answer}\n Yeni sorunu sor.";
        }

        string final = $"{prompt}\n\n Artık yeterli bilgiye sahipsin. Kullanıcı için detaylı bir içerik planı oluştur";
        string finalOutput = await SendToAI(apiKey, requestUrl, final);

        Console.WriteLine("\n İçerik Planı: \n" + finalOutput);
    } 

    static async Task<string> SendToAI(string apiKey, string requestUrl, string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
             {
                new
                {
                    role = "user",
                    content = prompt
                }
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