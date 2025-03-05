using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArsTechnica
{
    internal class ChatGPT
    {
        public static ChatGPTResult ChatGPTOnlyTranslate(string text, string category)
        {
            text = text.Replace("\"", "\\\"");
            text = text.Replace("\n", "\\\n");
            Console.Write("chatGPT Called");
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "متن روبرو که بخشی از یک مقاله در حوزه " +category+ " است رو به زبان فارسی ترجمه کن : " + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            Console.WriteLine(" - chatGPT Responed");
            return result;
        }
        public static ChatGPTResult ChatGPTOnlyTranslateForTitle(string text, string category)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "متن روبرو که عنوان یک مقاله در حوزه " + category + " است رو به زبان فارسی ترجمه کن : " + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            Console.WriteLine(" - chatGPT Responed");
            return result;
        }

        public static ChatGPTResult ChatGPTOnlyTranslateForReview(string text)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "با توجه به متن روبرو که یک نظر در مورد عطر است  یک متن به عنوان نظر به فارسی عامیانه تولید کن و تاریخ ها و سال ها رو به شمسی تبدیل کن و در نتیجه فقط نظر نهایی باشد: " + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }

        public static ChatGPTResult ChatGPTTranslate(string text, string brand)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "متن روبرو رو به زبان فارسی عامیانه و با اضافه کردن توضیحات تکمیلی در حد 3000 کلمه برای یک سایت فروشگاهی ترجمه کن نام برند محصول رو " + brand + " ترجمه کن و نتیجه را در تگهای html  قرار بده و تگ های head و body و title  در نتیجه حذف کن : " + text);
            //template = template.Replace("MyText", "متن روبرو رو به زبان فارسی عامیانه و با اضافه کردن توضیحات تکمیلی در حد بیش از 3 صفحه برای یک سایت فروشگاهی ترجمه کن و اسم افراد رو ترجمه نکن: " + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }
        public static ChatGPTResult ChatGPTTranslateForTitle(string text, string brand)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "متن روبرو رو به زبان فارسی عامیانه برای یک سایت فروشگاهی ترجمه کن نام برند محصول رو " + brand + " ترجمه کن : " + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }
        public static ChatGPTResult ChatGPTAskQuestion(string text)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }
        public static ChatGPTResult ChatGPTGetKeyword(string text)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "کلمه کانونی (meta Keyword) برای مطلب روبرو که هر کلمه در یک li جداگانه قرار داده شده باشد" + text);
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }
        public static ChatGPTResult ChatGPTGetMetaDescription(string keyword, string productName)
        {

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

            request.Headers.Add("Authorization", "Bearer " + Helper.chatGPTToken);


            var template = File.ReadAllText("ChatGPTTemplate.json");
            template = template.Replace("MyText", "meta description برای محصول " + productName + " با تمکرز بر کلمه کلیدی " + keyword + " و نتیجه متا را در یک تگ Strong قرار بده");
            request.Content = new StringContent(template);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = client.Send(request);
            //  response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            var stringResult = reader.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatGPTResult>(stringResult);
            return result;
        }

    }

    public class ChatGPTResult
    {
        public string id { get; set; }
        public string model { get; set; }
        public List<ChatGPTResultChoise> choices { get; set; }
        public ChatGPTResultError error { get; set; }

    }
    public class ChatGPTResultError
    {
        public string message { get; set; }
        public string type { get; set; }
        public string param { get; set; }
        public string code { get; set; }
    }
    public class ChatGPTResultChoise
    {
        public ChatGPTResultChoiseMessage message { get; set; }

    }
    public class ChatGPTResultChoiseMessage
    {

        public string content { get; set; }
    }
}
