using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace WebMarket.Common
{
    public static class Settings
    {
        static Settings()
        {
            SmtpUserAccount = ConfigurationManager.AppSettings["smtpUserAccount"];
            SmtpUserPassword = ConfigurationManager.AppSettings["smtpUserPassword"];
            SmtpHost = ConfigurationManager.AppSettings["smtpHost"];
            To = SplitMailList(ConfigurationManager.AppSettings["smtpTo"]);
            From = ConfigurationManager.AppSettings["smtpFrom"];
            ReplyTo = SplitMailList(ConfigurationManager.AppSettings["smtpReplyTo"]);
            CC = SplitMailList(ConfigurationManager.AppSettings["smtpCC"]);
            BCC = SplitMailList(ConfigurationManager.AppSettings["smtpBCC"]);
        }

        public static string SmtpUserAccount { get; private set; }
        public static string SmtpUserPassword { get; private set; }
        public static string SmtpHost { get; private set; }
        public static IEnumerable<string> To { get; private set; }
        public static string From { get; private set; }
        public static IEnumerable<string> ReplyTo { get; private set; }
        public static IEnumerable<string> CC { get; private set; }
        public static IEnumerable<string> BCC { get; private set; }

        private static IEnumerable<string> SplitMailList(string mailList)
        {
            if (string.IsNullOrEmpty(mailList))
            {
                return new string[] { };
            }

            return mailList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Distinct();
        }
    }
}