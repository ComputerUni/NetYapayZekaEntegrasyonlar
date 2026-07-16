# 🤖 NetCore AI - Yapay Zeka Entegrasyon Projeleri
---

## 📋 İçindekiler

- [Proje Genel Bakışı](#proje-genel-bakışı)
- [Proje Listesi](#-proje-listesi)
- [Teknoloji Yığını](#-teknoloji-yığını)
- [Başlangıç](#başlangıç)
- [Proje Detayları](#-proje-detayları)

---

## 🎯 Proje Genel Bakışı

**NetCore AI**, .NET 9 ile geliştirilmiş **20 ayrı yapay zeka ve makine öğrenmesi projesi** koleksiyonudur. Projeler, sesli tanıma, duygu analizi, görüntü işleme, metin analizi, sohbet botları ve daha birçok AI işlem örneğini içerir.

### Proje Amacı
Bu koleksiyon, geliştiricilerin çeşitli AI/ML API'lerini ve hizmetlerini C# ve .NET ekosistemi içinde nasıl kullanacağını öğrenmelerine yardımcı olmak amacıyla hazırlanmıştır.

- ✅ Başlangıç düzeyinde öğrenme için ideal
- ✅ Gerçek API entegrasyonu örnekleri
- ✅ Modüler ve bağımsız projeler
- ✅ Türkçe dökümantasyon ve kodlar

---

## 📦 Proje Listesi

### 🎙️ **Ses & Konuşma İşleme Projeleri**

| # | Proje Adı | Açıklama | Teknoloji |
|---|-----------|---------|-----------|
| **01** | DeepGram AI Voice | MP3 dosyalarını metne dönüştürme (Speech-to-Text) | DeepGram API |
| **11** | Microsoft Azure Text-to-Speech | Metni sesli konuşmaya dönüştürme | Azure Cognitive Services |
| **20** | Speech ChatBot | Sesli giriş, AI yanıt ve TTS çıkış | Whisper + OpenRouter + NAudio |

---

### 📝 **Metin Analizi & NLP Projeleri**

| # | Proje Adı | Açıklama | Teknoloji |
|---|-----------|---------|-----------|
| **02** | HuggingFace Sentiment Analysis | Metin duygu analizi (Pozitif/Negatif/Nötr) | HuggingFace Inference |
| **03** | HuggingFace Summarize Text | Uzun metinleri kısa özete çevirme | HuggingFace Summarization |
| **04** | HuggingFace Named Entity Recognition | Metinde kişi, yer, kuruluş adları tanıma | HuggingFace NER Model |
| **05** | HuggingFace RoBERTa Base QA | Soruya cevap bulma sistemi | RoBERTa QA Model |
| **06** | HuggingFace ToxicBERT | Metinde toksik/uygunsuz içerik tespiti | ToxicBERT Model |
| **08** | PDF Summarize | PDF dosyalarını oku ve özetleme | ITextSharp + HuggingFace |
| **14** | AI Q&A | Sorular sormak ve AI cevap alma | Foundation Model |

---

### 🖼️ **Görüntü İşleme Projeleri**

| # | Proje Adı | Açıklama | Teknoloji |
|---|-----------|---------|-----------|
| **10** | Draw Image | Basit şekiller ve görseller oluşturma | System.Drawing |
| **12** | Microsoft Azure Computer Vision | Görüntü analizi, etiketleme ve tanıma | Azure Vision API |
| **13** | Azure Computer Vision (Objects & Details) | Detaylı nesne algılama ve bölge analizi | Azure Vision API v4 |
| **18** | DeepAI Text-to-Image | Metin açıklamasından görüntü oluşturma | DeepAI API |
| **19** | Video Generator | Video dosyaları oluşturma ve işleme | FFmpeg/OpenCV |

---

### 💬 **Sohbet & Konuşma Projeleri**

| # | Proje Adı | Açıklama | Teknoloji |
|---|-----------|---------|-----------|
| **07** | Anthropic Claude Chat | Metin tabanlı AI sohbeti (Claude modeli) | OpenRouter + Claude API |
| **15** | Role Simulation | AI'ya belirli rol vermek ve oyun oynamak | Prompt Engineering |
| **16** | Auto Agent Prompt | Otomatik prompt oluşturma ve optimizasyon | LLM-based Prompting |
| **17** | Code Editor | AI destekli kod editörü | Code Analysis + AI |

---

### 📧 **Özel Uygulamalar**

| # | Proje Adı | Açıklama | Teknoloji |
|---|-----------|---------|-----------|
| **09** | Job Email | İş ilanlarına otomatik e-posta gönderme | SMTP + Template Engine |

---

## 🛠️ Teknoloji Yığını

### Backend Framework
- **Platform:** .NET 9
- **Dil:** C# 13

### Harici API'ler & Servisler
```
🤖 AI/ML Modelleri:
  ├─ HuggingFace API (Inference)
  ├─ DeepGram (Ses Tanıma)
  ├─ OpenRouter (LLM Erişim)
  ├─ Anthropic Claude
  ├─ Microsoft Azure Cognitive Services
  │  ├─ Computer Vision
  │  ├─ Text-to-Speech
  │  └─ Speech-to-Text
  └─ DeepAI (Görüntü Oluşturma)
```

### Kütüphaneler & Paketler
| Paket | Amaç |
|-------|------|
| `System.Net.Http` | HTTP istekleri |
| `System.Text.Json` | JSON işlemleri |
| `System.Speech` | Sistem sesli çıkış |
| `NAudio` | Ses kaydı ve işleme |
| `iTextSharp` | PDF okuma ve işleme |
| `System.Drawing` | Görüntü oluşturma |

---

## 🚀 Başlangıç

### Sistem Gereksinimleri
- **.NET 9 SDK** veya üzeri
- **Visual Studio 2026** (Community/Professional)
- İçeriğe bağlı olarak **API Anahtarları:**
  - HuggingFace Token
  - DeepGram API Key
  - Azure Subscription
  - OpenRouter API Key
  - DeepAI API Key

### Kurulum Adımları

1. **Repository'yi klonlayın:**
```bash
git clone https://github.com/ComputerUni/NetYapayZekaEntegrasyonlar.git
cd NetCoreAINew
```

2. **Solution'ı açın:**
```bash
# Visual Studio'da açma
cd NetCoreAINew
start NetCoreAINew.slnx
```

3. **Dependencies Yükleyin:**
```bash
dotnet restore
```

4. **API Anahtarlarını Yapılandırın:**
Her proje içindeki `Program.cs` dosyasında:
```csharp
private static readonly string apiKey = "YOUR_API_KEY_HERE";
```

5. **Projeyi Çalıştırın:**
```bash
# Belirli bir projeyi çalıştırma
cd NetCoreAINew/NetCoreAINew.Project01_DeepGramAIVoice
dotnet run

# Veya Visual Studio'da Ctrl+F5
```

---

## 📚 Proje Detayları

### 📌 Project 01: DeepGram AI Voice
**Amaç:** Ses dosyalarını metni dönüştürme
- **Giriş:** MP3 ses dosyası
- **Çıkış:** Transkripsiyon metni
- **API:** DeepGram v1/listen
- **Diller:** İngilizce, Türkçe
- **Örnek Dosyalar:** `testeng.mp3`, `testtr.mp3`

**Kullanım:**
```csharp
// Ses dosya yolunu ve API anahtarını belirleyin
// Program otomatik olarak DeepGram API'ye gönder
// Yanıt olarak transkripsiyon metnini alın
```

---

### 📌 Project 02: HuggingFace Sentiment Analysis
**Amaç:** Verilen metin hakkında duygu analizi
- **Model:** `cardiffnlp/twitter-roberta-base-sentiment-latest`
- **Çıkış:** Pozitif 😍 / Negatif 😠 / Nötr 😐
- **API:** HuggingFace Inference API
- **Mimari:** REST API Tabanlı

**Kullanım:**
```csharp
Console.Write("Enter your text here: ");
var text = Console.ReadLine();
// Program API'ye metin gönderir
// Duygusal analiz sonucu görüntülenir
```

---

### 📌 Project 03: HuggingFace Summarize Text
**Amaç:** Uzun metinleri kısa ve öz hâle getirme
- **Model:** facebook/bart-large-cnn (CNN/DailyMail)
- **Dava Kullanımı:** Haber özetleme, dokümantasyon kısaltması
- **Output:** Özet metin

---

### 📌 Project 04: HuggingFace NER (Named Entity Recognition)
**Amaç:** Metindeki adları tanımlama
- **Tarafından Tanınanlar:** 
  - Kişi adları
  - Yer adları
  - Kuruluş adları
  - Diğer varlıklar
- **Model:** dslim/bert-base-multilingual-cased-ner
- **Dil Desteği:** Multilingual

---

### 📌 Project 05: HuggingFace RoBERTa Base QA
**Amaç:** Verilen metinde soruya cevap bulma
- **Mekanizma:** Context-based Question Answering
- **Model:** deepset/roberta-base-squad2
- **Örnek Akış:** 
  1. Kaynak metni girin
  2. Sorunuzu sorun
  3. AI, metinde sorunun cevabını bulur

---

### 📌 Project 06: HuggingFace ToxicBERT
**Amaç:** Metinde toksik/uygunsuz içerik tespiti
- **Kullanım Alanı:** İçerik moderasyonu, güvenlik filtreleme
- **Model:** unitary/toxic-bert
- **Kategoriler:** Toksiklik, tehdit, küfür, vb.

---

### 📌 Project 07: Anthropic Claude Chat
**Amaç:** Claude AI modeli ile interaktif sohbet
- **Platform:** OpenRouter (API gateway)
- **Model:** Claude 3 / Claude 2
- **Başlangıç:** `exit` yazana kadar sohbet
- **Özellikler:** Renkli konsol output, prompt yönetimi

**Ekran Görünüşü:**
```
╔═════════════════════════════════════════╗
║            AI Chatbot                   ║
║       Çıkmak için 'exit' yazın          ║
╚═════════════════════════════════════════╝

Sen: Merhaba!
Yanıt Bekleniyor...
Claude: Merhaba! Size nasıl yardımcı olabilirim?
```

---

### 📌 Project 08: PDF Summarize
**Amaç:** PDF dosyasını oku ve özetleme
- **PDF Okuma:** iTextSharp
- **Özetleme:** HuggingFace API
- **Kullanım:** Dökümanlar, raporlar, bildiriler
- **Çıkış:** Özet metin

---

### 📌 Project 09: Job Email
**Amaç:** İş ilanlarına otomatik e-posta gönderme
- **SMTP Ayarları:** SendGrid veya Gmail
- **Şablon:** İş başvurusu email template
- **Otomasyon:** Batch processing

---

### 📌 Project 10: Draw Image
**Amaç:** Programatik olarak görseller oluşturma
- **Kütüphane:** System.Drawing
- **Çıkış:** PNG/JPG dosyaları
- **Özellikler:** 
  - Şekil çizme (dikdörtgen, daire, etc.)
  - Metin ekleme
  - Renk yönetimi

---

### 📌 Project 11: Microsoft Azure Text-to-Speech
**Amaç:** Metni doğal konuşmaya dönüştürme
- **Servis:** Azure Cognitive Services
- **Dil Desteği:** Çoklu dil
- **Çıkış:** MP3/WAV ses dosyası
- **Kullanım:** Erişilebilirlik, sesli rehberler

---

### 📌 Project 12: Azure Computer Vision
**Amaç:** Görüntüleri analiz etme ve etiketleme
- **Özellikler:**
  - Nesneleri tanıma
  - Renk analizi
  - Metin okuma (OCR)
  - İçerik etiketleri
- **API Versiyonu:** v4.0

---

### 📌 Project 13: Azure Computer Vision - Objects & Details
**Amaç:** Detaylı görüntü analizi ve nesne tespiti
- **Avancé Özellikler:**
  - Bounding boxes (sınırlama kutuları)
  - Nesne koordinatları
  - Güven seviyeleri
  - Yoğun analiz
- **Kullanım:** Ürün kataloğu, güvenlik izleme

---

### 📌 Project 14: AI Q&A
**Amaç:** Soruya cevap alma sistemi
- **Altyapı:** Foundation Model
- **Modeli:** Belirli LLM teknolojisi
- **Etkileşim:** Soruya yanıt döngüsü

---

### 📌 Project 15: Role Simulation
**Amaç:** AI'ya belirli karakter/rol vererek benzersiz etkileşim
- **Özellikler:**
  - Sistem promotu ile karakter tanımlama
  - Rolü saklama (oturum içinde)
  - Tutuarlı konuşma
- **Örnek Roller:** Tarih öğretmeni, dedektif, uzman doktor

---

### 📌 Project 16: Auto Agent Prompt
**Amaç:** Sistem promutlarını otomatik oluşturma ve optimize etme
- **Mekanizma:** Prompt engineering otomasyonu
- **Başlama:** Temel açıklama → Geliştirilmiş prompt
- **Kullanım:** Prompt fine-tuning, konsistens

---

### 📌 Project 17: Code Editor
**Amaç:** AI destekli kod düzenleme aracı
- **Özellikler:**
  - Kod analizi
  - Otomatik tamamlama
  - Hata önerisi
  - Refactoring tavsiye
- **Dil:** C# (veya diğer)

---

### 📌 Project 18: DeepAI Text-to-Image
**Amaç:** Metin açıklamasından görüntü üretme (Generative AI)
- **API:** DeepAI text2img endpoint
- **Kullanım:** İllüstrasyon oluşturma, concept art
- **Örnekler:**
  - Prompt: "Kırmızı bir robot ayakkabı"
  - Çıkış: Oluşturulan görüntü

---

### 📌 Project 19: Video Generator
**Amaç:** Video dosyaları oluşturma ve işleme
- **Araçlar:** FFmpeg, OpenCV (C# binding)
- **Özellikler:**
  - Görüntüleri video'ya dönüştürme
  - Video düzenleme
  - Efekt ekleme

---

### 📌 Project 20: Speech ChatBot
**Amaç:** Tam entegre sesli chatbot uygulaması
- **Pipeline:**
  1. 🎤 Kullanıcı ses kaydı (NAudio)
  2. 📝 Whisper ile transcription
  3. 🤖 AI tarafından cevap oluşturma
  4. 🔊 Yanıt sesli çıkış (TTS)
- **Döngü:** Devamlı konuşma oturumu

**Akış Diyagramı:**
```
┌─────────────────┐
│ Kullanıcı Sesi  │
│   (Kayıt)       │
└────────┬────────┘
		 │
		 ▼
┌─────────────────┐     ┌──────────────┐
│   Whisper API   │────▶│  Transkript  │
│ (Speech-to-Text)│     │    (Metin)   │
└─────────────────┘     └──────┬───────┘
								│
								▼
						┌───────────────┐
						│  Claude AI    │
						│   (Cevap)     │
						└───────┬───────┘
								│
								▼
						┌───────────────┐
						│     TTS       │
						│  (Ses Çıkış)  │
						└───────────────┘
```

---


## 📖 API Belgeleri Linkileri

| Hizmet | Belgeler |
|--------|----------|
| 🤗 HuggingFace | https://huggingface.co/docs/hub/inference-api |
| 🎙️ DeepGram | https://developers.deepgram.com/docs |
| 🔷 OpenRouter | https://openrouter.ai/docs |
| ☁️ Azure Cognitive | https://learn.microsoft.com/en-us/azure/cognitive-services |
| 🎨 DeepAI | https://deepai.org/api |

---

## 💡 Kullanım Örnekleri

### Proje 02 - Duygu Analizi
```csharp
// Çalıştırma
dotnet run

// Girdi:
// "Bu proje harika! Çok beğendim."

// Çıktı:
// ✨ Duygu Analizi ✨
// 📝 Girdi Metni: "Bu proje harika! Çok beğendim."
// 😍 Sonuç: POSITIVE - Güven: 0.98
```


### Proje 20 - Sesli Chatbot
```
🎤 Sesli Chatbot Başladı
🎙️ Konuşmaya başlayın...
🔍 Dinliyor...
📝 Sen: Hava nasıl?
🤖 Chatbot: Bugün havanın çok güzel olduğunu duydum...
🔊 [Ses çıkışı gösterildi]
```

---

## 🌐 Dil & Yerelleştirme

- **Kodlar:** Türkçe yorum ve çıktılar
- **Konsol:** UTF-8 Türkçe karakter desteği
- **API Çağrıları:** İngilizce (standart)
- **UI/UX:** Emoji desteği ✨

---

