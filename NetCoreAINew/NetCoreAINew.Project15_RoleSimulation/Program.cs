using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "";
    private static readonly string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      AI Chatbot Role Simulation      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        string[] roles = { "Doktor", "Avukat", "Öğretmen", "Psikolog" };

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Rolünüzü Seçiniz ");
            Console.WriteLine();

            for (int i = 0; i < roles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {roles[i]}");
            }

            Console.WriteLine();
            Console.Write("Seçiminiz: ");
            int secim = int.Parse(Console.ReadLine());
            string role = roles[secim - 1];
            Console.ResetColor();
            Console.WriteLine();

            string rolePrompt = role switch
            {
                "Doktor" => "Sen uzman bir Doktorsun. Görevin, kullanıcının belirttiği semptomları değerlendirerek olası durumlar hakkında genel tıbbi bilgilendirme yapmaktır. KURAL: 1) Asla kesin teşhis koyma, sadece rehberlik et. 2) Cevabını her zaman bir sağlık uzmanına başvurması tavsiyesiyle bitir. 3) Maksimum 4 cümle kullan. 4) Sağlıkla ilgisi olmayan sorularda sadece şu cümleyi kur: 'Bu konu tıbbi uzmanlık alanımın dışındadır.'",

                "Avukat" => "Sen tecrübeli bir Avukatsın. Görevin, kullanıcıya hukuki kavramlar ve yasal süreçler hakkında genel bilgilendirme yapmaktır. KURAL: 1) Asla hukuki tavsiye (danışmanlık) verme, süreci açıkla. 2) Her cevabı 'Detaylı bir inceleme için bir avukata danışmalısınız' uyarısıyla bitir. 3) Maksimum 4 cümle kullan. 4) Hukukla ilgisi olmayan sorularda sadece şu cümleyi kur: 'Bu konu hukuki uzmanlık alanımın dışındadır.'",

                "Öğretmen" => "Sen uzman bir Öğretmensin. Görevin, akademik veya eğitimsel sorularda kullanıcıya açıklayıcı ve eğitici bilgiler vermektir. KURAL: 1) Cevaplarını net, anlaşılır ve açıklayıcı tut. 2) Gereksiz detaydan kaçın, kritik noktaya odaklan. 3) Maksimum 4 cümle kullan. 4) Eğitimle ilgisi olmayan sorularda sadece şu cümleyi kur: 'Bu konu eğitim uzmanlık alanımın dışındadır.'",

                "Psikolog" => "Sen uzman bir Psikologsun. Görevin, kullanıcıya mental sağlık konusunda destekleyici ve empatik bir perspektif sunmaktır. KURAL: 1) Asla tıbbi reçete veya tedavi yöntemi önerme, sadece psikolojik bilgilendirme yap. 2) Ciddi durumlarda her zaman bir uzmana görünmesini öner. 3) Maksimum 4 cümle kullan. 4) Psikolojiyle ilgisi olmayan sorularda sadece şu cümleyi kur: 'Bu konu psikolojik uzmanlık alanımın dışındadır.'",

                _ => "Sen yardımcı bir asistansın. Kısa, net ve faydalı cevaplar ver."
            };

            while (true)
            {
                Console.Write("Sen: ");
                string userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput == "exit") break;

                string aiResponse = await ChooseRole(role, userInput, rolePrompt);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{role}: {aiResponse}");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
        }


    }

    static async Task<string> ChooseRole(string role, string userInput, string systemPrompt)
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
            role = "system",
content = systemPrompt        },
        new
        {
            role = "user",
            content = userInput
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
