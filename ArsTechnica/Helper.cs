using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArsTechnica
{
    internal class Helper
    {

        public static string wordPressUsername = "Ghost";
        public static string wordPressPassword = "l49v 1hZO LiZ3 y5OH PaTj QVkn";

        public static string chatGPTToken = "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";


        public static void SaveData(object sources)
        {
            File.WriteAllText("Sources.json",Newtonsoft.Json.JsonConvert.SerializeObject( sources, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
