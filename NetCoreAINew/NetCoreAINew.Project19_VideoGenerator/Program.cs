using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://api.replicate.com/v1/predictions";
    private static readonly string version = "";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║          📽️ ReplicateAI VideoGenerator       ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();


        Console.Write("Text to Video: ");
        string prompt = Console.ReadLine();
        await VideoGenerator(prompt);
    }

    private static async Task VideoGenerator(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);

            var requestBody = new
            {
                version,
                input = new
                {
                    prompt,
                    num_frames = 24,
                    fps = 8,
                    guidance_scale = 12.5,
                    num_inference_steps = 50,
                    width = 576,
                    height = 320
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine();
            Console.WriteLine("⏳ Video oluşturuluyor...");

            var response = await client.PostAsync(requestUrl, content);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("API Hatası: " +response.Content.ReadAsStringAsync());
                return;
            }

            var pred = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            string id = pred.RootElement.GetProperty("id").GetString();
            Console.WriteLine("🖼️ Video üretiliyor...");

            string status = "";
            string videoUrl = "";
            while(status != "succeeded")
            {
                await Task.Delay(5000);
                var check = await client.GetAsync($"{requestUrl}/{id}");
                var checkJson = JsonDocument.Parse(await check.Content.ReadAsStringAsync());
                status = checkJson.RootElement.GetProperty("status").GetString();
                Console.WriteLine($"⏳ Durum: {status}");
                if(status == "failed")
                {
                    Console.WriteLine("Video üretim işlemi başarısız oldu.");
                    return;
                }
                if(status == "succeeded")
                {
                    var output = checkJson.RootElement.GetProperty("output");
                    videoUrl = output.ValueKind == JsonValueKind.Array ? output[0].GetString() : output.GetString();
                }
            }

            Console.WriteLine($"Video Hazır: {videoUrl}");



            using var stream = await client.GetStreamAsync(videoUrl);
            await using var file = File.Create("generated_video.mp4");
            await stream.CopyToAsync(file);
            Console.WriteLine("Video İndirildi -> generated_video.mp4");


            Process.Start(new ProcessStartInfo
            {
                FileName = "generated_video.mp4",
                UseShellExecute = true
            });
                        


        }
    }
}