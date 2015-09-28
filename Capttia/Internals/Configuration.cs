using System;
using System.Configuration;

namespace Fenton.Capttia
{
    public class CapttiaSection : ConfigurationSection
    {
        [ConfigurationProperty("ModuleName", DefaultValue = "capttia")]
        public string ModuleName
        {
            get
            {
                var value = this["ModuleName"] as string;
                return value;
            }
        }

        [ConfigurationProperty("PassPhrase", DefaultValue = "DEFAULT-PASSPHRASE")]
        public string PassPhrase
        {
            get
            {
                var value = this["PassPhrase"] as string;
                return value;
            }
        }

        [ConfigurationProperty("PassPhraseB", DefaultValue = "ALTERNATE-PASSPHRASE")]
        public string PassPhraseB
        {
            get
            {
                var value = this["PassPhraseB"] as string;
                return value;
            }
        }

        [ConfigurationProperty("Salt", DefaultValue = "bD84Ae8g7f15cF9B")]
        public string Salt
        {
            get
            {
                var value = this["Salt"] as string;
                if (value.Length != 16)
                {
                    throw new ArgumentException("Salt must be 16 characters");
                }
                return value;
            }
        }

        [ConfigurationProperty("ErrorMessage", DefaultValue = "Please ensure cookies are enabled in your browser.")]
        public string ErrorMessage
        {
            get
            {
                var value = this["ErrorMessage"] as string;
                return value;
            }
        }

        [ConfigurationProperty("CookieName", DefaultValue = "ctt-cookie")]
        public string CookieName
        {
            get
            {
                var value = this["CookieName"] as string;
                return value;
            }
        }
    }
}
