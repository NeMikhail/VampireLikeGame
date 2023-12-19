using Configs;

namespace Core.Interface.ISavers
{
    internal interface ISave
    {
        public void TrySave(ConfigLoader configLoader);
        public void TryLoad(ConfigLoader configLoader);
        public void TrySaveUpgrades(ConfigLoader configLoader);
        public void TrySaveSettings();
        public void TryLoadSettings();
    }
}