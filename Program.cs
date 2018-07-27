using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace T1
{
    class Program
    {
        public static readonly string stempl = "hello {{name}}, 欢迎光临{{location}},{{other}}!";
        static void Main(string[] args)
        {
            TestMatch();
            Console.WriteLine("Test2");
            TestMatchIgnoreCase();
            TestReplace();
            Console.ReadKey();
        }

        private static void TestMatch()
        {
            var spattern = @"\{\s*\{(\w+)\}\s*\}";
            var reg = new Regex(spattern);
            var mats = reg.Matches(stempl);
            foreach (Match mat in mats)
            {
                var value = mat.Groups[1].Value;
                Console.WriteLine("mat = {0}, group[1]={1}", mat, value);
            }
        }

        //ignore case
        private static void TestMatchIgnoreCase()
        {
            const string TEMPLATE_KEYWORD_HEAD = @"\{\s*\{\s*(";
            const string TEMPLATE_KEYWORD_TAIL = @")\s*\}\s*\}";

            string strLong = "hello { {name }   }, 欢迎光临{{location}},{{other}}!";
            var keyword = "NaME";
            var sFormat = TEMPLATE_KEYWORD_HEAD + keyword + TEMPLATE_KEYWORD_TAIL;
            var reg = new Regex(sFormat,RegexOptions.IgnoreCase|RegexOptions.Compiled);
            var mats = reg.Matches(strLong);
            foreach (Match mat in mats)
            {
                var value = mat.Groups[1].Value;
                Console.WriteLine("mat = {0}, group[1]={1}", mat, value);
            }
            if (mats.Count == 0)
            {
                Console.WriteLine("{0} not match", keyword);
            }
        }

        private static void TestReplace()
        {
            string strLong = "hello 【测试】，欢迎!";
            var sPat = @"【(.+)】";
            var reg = new Regex(sPat);
            var sNew = reg.Replace(strLong, "【*$1*】");
            Console.WriteLine("new: " + sNew);
        }
    }
}
