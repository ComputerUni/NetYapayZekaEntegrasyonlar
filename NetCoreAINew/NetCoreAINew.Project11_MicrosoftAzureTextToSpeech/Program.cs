using System.Net.Http.Headers;
using System.Text;

class Program
{
    private static readonly string subscriptionKey = "";
    private static readonly string region = "";
    private static readonly string tokenEndPoint = $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issuetoken";

    static async Task Main(string[] args)
    {
        string text = "The implementation of neural text-to-speech technology allows for seamless integration of sophisticated audio responses into our cloud-based infrastructure.";
        var token = await GetTokenAsync(subscriptionKey, tokenEndPoint);

        await SynthesizeSpeechAsync(token, region, text);
    }


    static async Task<string> GetTokenAsync(string key, string endPoint)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
            var response = await client.PostAsync(endPoint, null);
            return await response.Content.ReadAsStringAsync();
        }
    }

    static async Task SynthesizeSpeechAsync(string token, string region, string text)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("User-Agent", "AzureTTSClient");
            client.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "riff-16khz-16bit-mono-pcm");

            string ssml = $@"
<speak version='1.0'
       xmlns='http://www.w3.org/2001/10/synthesis'
       xml:lang='en-US'>
    <voice name='en-US-JennyNeural'>
        {text}
    </voice>
</speak>";

            var content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");
            var result = await client.PostAsync($"https://{region}.tts.speech.microsoft.com/cognitiveservices/v1", content);

            if (result.IsSuccessStatusCode)
            {
                var audioBytes = await result.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes("output.wav", audioBytes);
                Console.WriteLine("Ses dosyası oluşturuldu.");
            }

            else
            {
                Console.WriteLine("Hata: " + result.StatusCode);
                // Hata detayını daha net görmek için:
                string errorDetails = await result.Content.ReadAsStringAsync();
                Console.WriteLine("Detay: " + errorDetails);

            }
        }
    }
}