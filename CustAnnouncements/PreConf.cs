using System.Collections.Generic;
using System;

namespace CustAnnouncements
{
    public class PreConf
    {
        public static Dictionary<string, string> GetDictonaryValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (!value.Contains(","))
            {
                var splitted = value.Split(':');
                if (splitted.Length != 2)
                    return null;
                dict.Add(value.Split(':')[0], value.Split(':')[1]);
                return dict;
            }
            string[] tl = value.Split(',');
            foreach (string t in tl)
            {
                string[] vl = t.Split(':');
                dict.Add(vl[0], vl[1]);
            }
            return dict;
        }
    }
}
