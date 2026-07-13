using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://router.huggingface.co/hf-inference/models/deepset/roberta-base-squad2";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("-------------- CONTEXT AREA --------------");
        Console.WriteLine();
        Console.Write("Enter your context: ");
        var context = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("--------------QUESTION AREA--------------");
        Console.WriteLine();
        Console.Write("Enter your question: ");
        var question = Console.ReadLine();
        if (!string.IsNullOrEmpty(context) && !string.IsNullOrEmpty(question))
        {
            Console.WriteLine("AI Analiz Yapıyor...");
            Console.WriteLine();
            Console.WriteLine();
            string result = await RobertaBaseQA(context, question);
            Console.WriteLine();
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("Please enter both context and question.");
        }
    }

    static async Task<string> RobertaBaseQA(string context, string question)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var requestBody = new
            {
                inputs = new
                {
                    question = question,
                    context = context
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(responseString);
            if(doc.RootElement.TryGetProperty("answer", out var answer))
            {
                Console.WriteLine("❓ Soru  : " + question);
                Console.WriteLine();
                Console.WriteLine("🖨️ Metin : " + context);
                Console.WriteLine();
                Console.WriteLine("📱 Cevap : " + answer);
                Console.WriteLine();
                return answer.GetString() ?? "No answer found.";
            }
            else
            {
                Console.WriteLine("    ❗Cevap bulunamadı veya model henüz hazır değil");
                Console.WriteLine(responseString);
                return "No answer found";
            }



        }
    }
}
