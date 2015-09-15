using System.Configuration;

namespace Fenton.Capttia
{
    public class CapttiaSection : ConfigurationSection
    {
        [ConfigurationProperty("PassPhrase", DefaultValue = "DEFAULT-PASSPHRASE")]
        public string PassPhrase
        {
            get
            {
                var value = this["PassPhrase"] as string;
                return value;
            }
        }

        [ConfigurationProperty("ModuleName", DefaultValue = "capttia")]
        public string ModuleName
        {
            get
            {
                var value = this["ModuleName"] as string;
                return value;
            }
        }
    }
}
