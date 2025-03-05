using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordPressPCL.Client;
using WordPressPCL;
using WordPressPCL.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ArsTechnica
{
    internal class WordPress
    {

        public static void CreatePost(String faTitle, String enTitle, String description, string baseImageUrl, int baseCategoryId)
        {

            Post newPost = new Post();
            DownloadImage(baseImageUrl, enTitle.Replace("\"", "").Replace(".", "").Replace("—", "").Replace("'", ""));
            int imageId = UploadImage("ArsTechnica Images/" + enTitle.Replace("\"", "").Replace(".", "").Replace("—", "").Replace("'", "") + ".png");
            if (imageId > 0)
            {
                newPost.FeaturedMedia = imageId;
                newPost.Title = new Title()
                {
                    Raw = faTitle
                };
                newPost.Content = new Content()
                {
                    Raw = description
                };
                newPost.Categories = new List<int>();
                newPost.Categories.Add(baseCategoryId);

                var wclient = new WordPressClient("https://rumisoft.ir/wp-json/");
                wclient.Auth.UseBasicAuth(Helper.wordPressUsername, Helper.wordPressPassword);

               var res =  wclient.Posts.CreateAsync(newPost).Result;
            }
        }

        public static int UploadImage(string filePath)
        {
            string USER = "Ghost";
            string APPLICATION_PASSWORD = "l49v 1hZO LiZ3 y5OH PaTj QVkn";

            using (var httpClient = new HttpClient())
            {
                var byteArray = System.Text.Encoding.ASCII.GetBytes($"{USER}:{APPLICATION_PASSWORD}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using (var form = new MultipartFormDataContent())
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                        form.Add(fileContent, "file", Path.GetFileName(filePath));

                        var response = httpClient.PostAsync("https://rumisoft.ir/wp-json/wp/v2/media", form).Result;
                        var responseText = response.Content.ReadAsStringAsync().Result;
                        int id = 0;
                        var firstIndex = responseText.IndexOf("\"id\":");
                        if (firstIndex > -1)
                        {
                            var secondIndex = responseText.IndexOf("\"", firstIndex + 5);
                            id = Convert.ToInt32(responseText.Substring(firstIndex + 5, secondIndex - firstIndex - 6));
                        }
                        return id;
                    }
                }
            }
        }
        public static void DownloadImage(string url, string imageName)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), "ArsTechnica Images/" + imageName.Replace("|", " ") + ".png");
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
