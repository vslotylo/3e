using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using WebMarket.DAL.Common;

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
            using (var sr = File.OpenText(HostingEnvironment.MapPath(("~/Content/metadata/metadata.csv"))))
            {
                metadataString = sr.ReadToEnd();
            }

            var metaDataItem = metadataString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var context = new WebMarketDbContext();
            var categories = context.Categories.ToList();
            foreach (var info in metaDataItem.Select(item => item.Split(new []{"\","}, StringSplitOptions.RemoveEmptyEntries)))
            {
                //todo improve logic
                var categoryName = ParseValue(info[0]).ToLower();
                var listTitle = this.ParseValue(info[1]);
                var detailTitle = this.ParseValue(info[2]);
                var categoryDisplayName = categories.SingleOrDefault(obj=>obj.Name == categoryName).DisplayName;
                this.dict[categoryName] = new Metadata(categoryDisplayName, listTitle, detailTitle);
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