using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string model = "";
    private static readonly string endpoint = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";
    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Sormak istediğiniz soruyu yazınız: ");
        var prompt = Console.ReadLine();
        Console.ResetColor();

        if(!string.IsNullOrEmpty(prompt))
        {
            var result = await QuestionAnswer(prompt);
            Console.WriteLine("🤖 AI Sonucu");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ResetColor();
        }
    }

    static async Task<string> QuestionAnswer(string question)
    {
        using (var client = new HttpClient())
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts=new[]
                        {
                            new
                            {
                                text = question
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                var doc = JsonDocument.Parse(responseString);
                string answer = doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
                return $"Gemini Cevap: {answer}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Yanıt çözümlemesi başarısız oldu.");
                Console.WriteLine("Gelen Yanıt: " + responseString);
                return $"Bir hata oluştu: {ex.Message}"; 
            }

        }
    }
}