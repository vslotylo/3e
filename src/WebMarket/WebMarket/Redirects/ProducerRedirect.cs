using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebMarket.Filters;

namespace WebMarket.Redirects
{
    public class ProducerRedirect
    {
        private static string separator;

        public static string GetUrl(Uri url, string category, ProducersFilter producersFilter)
        {
            var collection = HttpUtility.ParseQueryString(url.Query);
            var sb = new StringBuilder();
            if (!url.LocalPath.EndsWith("/"))
            {
                sb.Append('/');
                sb.Append(category);
                sb.Append('/');
            }
            var names = new List<string>();
            separator = ";";
            if (producersFilter.ParsedProducers.Any(o => o.Contains(separator)))
            {
                foreach (var item in producersFilter.ParsedProducers)
                {
                    if (item.Contains(separator))
                    {
                        names.AddRange(item.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    else
                    {
                        names.Add(item);
                    }
                }

                names = names.Select(o => o.ToLower()).Distinct().ToList();
            }
            else
            {
                if ((!url.ToString().Contains("producers")))
                {
                    return string.Empty;
                }
            }

            string[] productNames = names.ToArray();
            var pr = collection.Get("producers");
            if (pr != null)
            {
                productNames = pr.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            }
            
            sb.Append(string.Join("-", productNames));
            var isFirst = true;
            foreach (var item in collection.Keys)
            {
                var key = item.ToString();
                var value = collection.Get(key);
                if (key == "producers")
                {
                    continue;
                }
                if (isFirst)
                {
                    sb.Append('?');
                    isFirst = false;
                }
                else
                {
                    sb.Append('&');
                }

                sb.Append(key);
                sb.Append('=');
                sb.Append(value);
            }

            return sb.ToString().ToLower();
        }
    }
}