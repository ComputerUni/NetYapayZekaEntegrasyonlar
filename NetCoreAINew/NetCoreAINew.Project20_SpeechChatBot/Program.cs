using NAudio.Wave;
using System.Net.Http.Headers;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string whisperUrl = "https://router.huggingface.co/hf-inference/models/openai/whisper-large-v3";
    private static readonly string openRouterUrl = "https://openrouter.ai/api/v1/chat/completions";
    private static readonly string hfToken = "";
    private static readonly string openRouterKey = "";

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("🎤 Sesli Chatbot Başladı. Konuşmak için Enter tuşuna basınız...");

        while (true)
        {
            Console.ReadLine();
            string audioFilePath = "recorded.wav";

            Console.WriteLine("🎙️ Konuşmaya başlayın");
            RecordAudio(audioFilePath);
            Console.WriteLine("🛑 Kayıt tamamlandı");

            string transcription = await TranscribeAudioAsync(audioFilePath);
            Console.WriteLine($"🙎‍ Sen: {transcription}");

            string reply = await AskAIAsync(transcription);
            Console.WriteLine($"🤖 Chatbot: {reply}");

            var synth = new SpeechSynthesizer();
            synth.Speak(reply);
        }
    }

    static void RecordAudio(string outputFilePath)
    {
        using var waveIn = new WaveInEvent();
        waveIn.WaveFormat = new WaveFormat(16000, 1);
        using var writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
        waveIn.DataAvailable += (s, a) => writer.Write(a.Buffer, 0, a.BytesRecorded);
        waveIn.StartRecording();
        Thread.Sleep(10000);
        waveIn.StopRecording();
    }

    static async Task<string> TranscribeAudioAsync(string audioFilePath)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", hfToken);

            using var fs = File.OpenRead(audioFilePath);
            var content = new StreamContent(fs);
            content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

            var response = await client.PostAsync(whisperUrl, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("text").ToString();

        }
    }

    static async Task<string> AskAIAsync(string userMessage)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openRouterKey);

            var payload = new
            {
                model = "openrouter/free",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = userMessage
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(openRouterUrl, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}