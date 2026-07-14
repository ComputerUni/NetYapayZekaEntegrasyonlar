using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        string prompt = "Bana 'Yazılım Geliştirici' pozisyonu için hazırlanan, profesyonel ama samimi tonda bir iş başvuru e-postası yazar mısın? Adım Kadir Yıldırım, 6 aydır .NET geliştiricisiyim. Ekip çalışmasına yatkınım ve uzaktan çalışmaya uygunum.";

        Console.OutputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine("║        ✉  AI İş Başvuru E-Posta Asistanı     ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("📤 İstek gönderiliyor");
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(500);
            Console.Write(".");
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();

        string result = await JobEmail(prompt);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("┌──────────────────────────────────────────────┐");
        Console.WriteLine("│                  📧 E-Posta                  │");
        Console.WriteLine("└──────────────────────────────────────────────┘");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(result);
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("──────────────────────────────────────────────");
        Console.WriteLine("✅ İşlem tamamlandı.");
        Console.ResetColor();

    }



    static async Task<string> JobEmail(string text)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "openrouter/free",
                messages = new[]
{
  new { role = "system", content = @"
Sen deneyimli bir kariyer danışmanısın. Görevin, kullanıcının verdiği bilgilere dayanarak profesyonel iş başvuru e-postaları yazmak.

KURALLAR:
- Yalnızca kullanıcının istediği e-postayı yaz, fazladan açıklama ekleme
- Markdown kullanma, yıldız (*), kare (#) gibi semboller kullanma
- Türkçe dilbilgisi kurallarına tam uy
- E-postayı şu yapıda yaz:

Konu: [pozisyon adı] Başvurusu

Sayın İlgili Kişi,

[Giriş paragrafı - kim olduğun ve neden başvurduğun]

[Deneyim paragrafı - teknik beceriler ve güçlü yönler]

[Kapanış paragrafı - iletişim ve teşekkür]

Saygılarımla,
[Ad Soyad]

[Buradaki ifadeleri rastgele kendin oluştur.]
LinkedIn : [linkedin profil linki]
GitHub   : [github profil linki]
Telefon  : [telefon numarası]
" },
    new { role = "user", content = text }
}
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                return $"{answer}";
                
            }
            else
            {
                Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                return $"{responseString}";
            }
        }
    }

}



