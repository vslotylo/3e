using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebMarket.Core
{
    public class MetadataProvider
    {
        private static readonly MetadataProvider Instance;
        private readonly Dictionary<string, Metadata> dict;
        static MetadataProvider()
        {
            Instance = new MetadataProvider();
        }

        private MetadataProvider()
        {
            dict = new Dictionary<string, Metadata>();
            string metadataString;
            using (var sr = File.OpenText(HttpContext.Current.Server.MapPath("~/Content/metadata/metadata.csv")))
            {
                metadataString = sr.ReadToEnd();
            }

            var metaDataItem = metadataString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var info in metaDataItem.Select(item => item.Split(new []{"\","}, StringSplitOptions.RemoveEmptyEntries)))
            {
                //todo improve logic
                var categoryId = ParseValue(info[0]).ToLower();
                var categoryDisplayName = this.ParseValue(info[1]);
                var listTitle = this.ParseValue(info[2]);
                var detailTitle = this.ParseValue(info[3]);
                this.dict[categoryId] = new Metadata(categoryDisplayName, listTitle, detailTitle);
            }
        }

        public static MetadataProvider Current
        {
            get
            {
                return Instance;
            }
        }

        public Metadata GetMetadataInfo(string category)
        {
            return dict[category.ToLower()];
        }

        private string ParseValue(string val)
        {
            return val.Replace("\"", string.Empty);
        }
    }
}