namespace Fenton.Capttia
{
    public class ScriptIds
    {
        public ScriptIds(string moduleName)
        {
            TokenId = moduleName;
            ScriptId = $"{moduleName}Script";
            HoneyPotInputId = $"{moduleName}Value";
            HoneyPotContainerId = $"{moduleName}Input";
        }

        public string TokenId { get; private set; }

        public string ScriptId { get; private set; }

        public string HoneyPotInputId { get; private set; }

        public string HoneyPotContainerId { get; private set; }
    }
}
