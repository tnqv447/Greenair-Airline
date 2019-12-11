using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace ApplicationCore
{
    public static class StringExec
    {
        public static IList<string> reverseStringFormat(string template, string str)
        {
            //Handels regex special characters.
            template = Regex.Replace(template, @"[\\\^\$\.\|\?\*\+\(\)]", m => "\\"
             + m.Value);
            Console.WriteLine(template);
            string pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";
            Console.WriteLine(pattern);

            Regex r = new Regex(pattern);
            Match m = r.Match(str);
            List<string> ret = new List<string>();

            for (int i = 1; i < m.Groups.Count; i++)
            {
                ret.Add(m.Groups[i].Value);
            }

            return ret;
        }
        public static IList<string> RegexSplit(string str, string splitOptions)
        {
            return Regex.Split(str, splitOptions);
        }
    }
}