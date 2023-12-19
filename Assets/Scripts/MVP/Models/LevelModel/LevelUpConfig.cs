using UnityEngine;


namespace Configs.LevelUpConfig
{
    [CreateAssetMenu(fileName = nameof(LevelUpConfig), menuName = "Configs/LevelUpConfig")]
    public class LevelUpConfig : ScriptableObject
    {
        public int StartXPRequirement;
        [Space]
        public int InitialXPIncrease;
        public int MediumXPIncrease;
        public int FinalXPIncrease;
        [Space]
        public int FirstBorderLevel;
        public int SecondBorderLevel;
        [Space]
        public int FirstBorderRequirement;
        public int SecondBorderRequirement;
    }
}