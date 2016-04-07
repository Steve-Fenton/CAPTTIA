namespace Fenton.Capttia
{
    public class ScriptIds
    {
        public ScriptIds(string moduleName)
        {
            InstanceId = ShortGuid.New();
            TokenId = moduleName;
            ScriptId = $"{moduleName}Script{InstanceId}";
            HoneyPotInputId = $"{moduleName}Value";
            HoneyPotContainerId = $"{moduleName}Input{InstanceId}";
        }

        public string InstanceId { get; private set; }

        public string TokenId { get; private set; }

        public string ScriptId { get; private set; }

        public string HoneyPotInputId { get; private set; }

        public string HoneyPotContainerId { get; private set; }
    }
}
