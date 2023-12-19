using Configs;
using Core.Interface.ISavers;

namespace Core.SaveClass
{
    internal static class Saver
    {
        private static ConfigLoader s_configLoader;


        public static void Init(ConfigLoader configLoader)
        {
            s_configLoader = configLoader;
        }

        public static void Save(ISave concreteSaver)
        {
            concreteSaver.TrySave(s_configLoader);
        }

        public static void Load(ISave concreteLoader)
        {
            concreteLoader.TryLoad(s_configLoader);
        }
        
        public static void SaveUpgrades(ISave concreteSaver)
        {
            concreteSaver.TrySaveUpgrades(s_configLoader);
        }

        public static void SaveSettings(ISave concreteSaver)
        {
            concreteSaver.TrySaveSettings();
        }
        
        public static void LoadSettings(ISave concreteSaver)
        {
            concreteSaver.TryLoadSettings();
        }

    }
}