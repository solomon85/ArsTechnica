

using ArsTechnica;
using System;
using HtmlAgilityPack;

List<Source> sources = new List<Source>();
//sources.Add(new Source()
//{
//    Id = 1,
//    Category="هوش مصنوعی (AI)",
//    LastCrawl = DateTime.Now.AddDays(-30),
//    Url = "https://arstechnica.com/ai/"
//});
//File.WriteAllText("sources.txt", Newtonsoft.Json.JsonConvert.SerializeObject(sources, Newtonsoft.Json.Formatting.Indented));
sources = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Source>>(File.ReadAllText("sources.json"));

Console.WriteLine("Service Started - Version 1403.12.09");
foreach (var src in sources)
{
    var lastCrawlTime = Convert.ToDateTime(src.LastCrawl);

    HtmlWeb client = new HtmlWeb();
    HtmlDocument doc = client.Load(src.Url);

    var list = doc.DocumentNode.SelectNodes("//article");
    var ii = 0;
    foreach (var node in list)
    {
        ii++;
        if (ii < 3)
            continue;

        var timeTag = node.SelectSingleNode("//time");
        var timeString = timeTag.GetAttributeValue("title", "none");
        if (timeString != "none")
        {
            var postDate = Convert.ToDateTime(timeString);
            if (lastCrawlTime > postDate)
                continue;
        }
        var aTag = node.ChildNodes[1].ChildNodes[1];
        aTag = aTag.ChildNodes[3];
        aTag = aTag.ChildNodes[1];
        aTag = aTag.ChildNodes[1];
        aTag = aTag.ChildNodes[1];

        var enTitle = aTag.InnerText;
        var faTitle = aTag.InnerText;
        Console.WriteLine("EN Title : " + enTitle);

        ChatGPTResult chatGpt = ChatGPT.ChatGPTOnlyTranslate(enTitle, src.Category);
        if (chatGpt != null && chatGpt.error == null)
            faTitle = chatGpt.choices[0].message.content;
        else
            Console.WriteLine(chatGpt.error.message);


        var url = aTag.GetAttributeValue("href", "none");
        if (url != "none")
        {
            HtmlDocument post = client.Load(url);
            string imgUrl = "";
            if (post.DocumentNode.SelectNodes("//*[contains(concat('', normalize-space(@class), ''), 'intro-image')]") != null)
            {
                var imgTag = post.DocumentNode.SelectNodes("//*[contains(concat('', normalize-space(@class), ''), 'intro-image')]")
                    .FirstOrDefault(node => node.GetAttributeValue("class", "").Split(' ').Contains("intro-image"));
                if (imgTag == null)
                    continue;

                imgUrl = imgTag.GetAttributeValue("src", "none");

                var article = post.DocumentNode.SelectNodes("//article")[0];
                var contents = article.SelectNodes("//*[contains(concat('', normalize-space(@class), ''), 'post-content')]");
                String _content = "";

                foreach (var conDiv in contents)
                {
                    foreach (var item in conDiv.ChildNodes)
                    {
                        if (item.Name == "p" || item.Name == "h2" || item.Name == "h3" || item.Name == "h4")
                        {
                            chatGpt = ChatGPT.ChatGPTOnlyTranslate(item.InnerText, src.Category);
                            if (chatGpt != null && chatGpt.error == null)
                                _content += "<" + item.Name + ">" + chatGpt.choices[0].message.content + "</" + item.Name + ">\n";
                            else
                                Console.WriteLine(chatGpt.error.message);
                        }
                        else if (item.Name == "figure")
                        {
                            var _imgTg = item.ChildNodes[1].ChildNodes[1];
                            var _imgUrl = _imgTg.GetAttributeValue("src", "none");
                            var figureCaption = item.SelectNodes("//*[contains(concat('', normalize-space(@class), ''), 'caption-content')]")[3];
                            var imgCaption = figureCaption.GetDirectInnerText().Trim();
                            string EnImgCaption = imgCaption;

                            WordPress.DownloadImage(_imgUrl, imgCaption.Replace("\"", "").Replace(".", "").Replace("—", "").Replace("'", ""));
                            var imgId = WordPress.UploadImage("ArsTechnica Images/" + imgCaption.Replace("\"", "").Replace(".", "").Replace("—", "").Replace("'", "") + ".png");
                            chatGpt = ChatGPT.ChatGPTOnlyTranslate(imgCaption, src.Category);
                            if (chatGpt != null && chatGpt.error == null)
                                imgCaption = chatGpt.choices[0].message.content;
                            else
                                Console.WriteLine(chatGpt.error.message);


                            _content += "[caption id=\"attachment_" + imgId + "\" align=\"aligncenter\" width=\"757\"]" +
                                "<img class=\" wp-image-" + imgId + "\" src=\"https://rumisoft.ir/wp-content/uploads/" + DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" +
                                EnImgCaption
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace(".", "")
                                    .Replace("\"", "")
                                    .Replace(" ", "-") +
                                    ".png\" " +
                                "alt=\"" + imgCaption + "\" width=\"757\" height=\"434\" />" +
                                imgCaption + "[/caption]\n";

                        }
                    }
                }

                WordPress.CreatePost(faTitle, enTitle, _content, imgUrl, src.CategoryId);
            }
        }
    }
    // src.LastCrawl = DateTime.Now;
}
Helper.SaveData(sources);
Console.WriteLine("Service End " + DateTime.Now);
