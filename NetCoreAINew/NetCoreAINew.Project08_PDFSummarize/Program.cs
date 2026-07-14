using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║            PDF Analyzer              ║");
        Console.WriteLine("║       Çıkmak için 'exit' yazın       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();


        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("PDF Dosya Yolunu Giriniz: ");
        Console.ResetColor();
        string pdfPath = Console.ReadLine();
        if(!File.Exists(pdfPath))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PDF Dosyası Bulunamadı");
            return;
        }

        if (pdfPath?.ToLower() == "exit")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Çıkış Yapılıyor!");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();

        var cts = new CancellationTokenSource();

        var loadingTask = Task.Run(async () =>
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            while (!cts.Token.IsCancellationRequested)
            {
                Console.Write("\rPDF Analizi AI Tarafından Yapılıyor   ");
                await Task.Delay(400);
                Console.Write("\rPDF Analizi AI Tarafından Yapılıyor.  ");
                await Task.Delay(400);
                Console.Write("\rPDF Analizi AI Tarafından Yapılıyor.. ");
                await Task.Delay(400);
                Console.Write("\rPDF Analizi AI Tarafından Yapılıyor...");
                await Task.Delay(400);
            }
            Console.ResetColor();
        }, cts.Token);

        await Task.Delay(100);

        string pdfText = ExtractTextFromPDF(pdfPath);

        cts.Cancel();
        await loadingTask;
        Console.Write("\r" + new string(' ', 50) + "\r");
        Console.WriteLine();
        Console.WriteLine();

        await PDFAnalyzer(pdfText, "PDF İçeriği");
    }

    private static string ExtractTextFromPDF(string filePath)
    {
        StringBuilder text = new StringBuilder();
        using (PdfDocument pdf = PdfDocument.Open(filePath))
        {
            foreach (var page in pdf.GetPages())
            {
                text.AppendLine(page.Text);
            }
        }
        return text.ToString();
    }

    private static async Task PDFAnalyzer(string text, string sourceType)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
                 {
                    new {role="system", content="Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver. Markdown gibi yazma * işareti kullanmadan başlıkları yaz, başlıkları ve maddeleri vurgulayabilirsin."},
                    new {role="user", content=$"Analyze and summarize the following {sourceType}: \n\n {text}"}
                }
            };


            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(requestUrl, content);
            string responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseJson);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            🤖 AI Analizi             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{answer}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                Console.WriteLine(responseJson);
            }




        }
    }
}

//C:\Users\BURHAN\Desktop\kitap.pdf
//C:\Users\BURHAN\Desktop\1.pdf