using Core.Interface.AchievementSystem;
using Enums;

namespace Core.Interface.IData
{   
    internal interface IMetaprogressionSaver
    {
        void SaveCollectedCoins(int collectedCoins);
        void SaveUnlockedAchivment(IAchievement achievement); 
        void SaveUnlockedCharacter(CharacterName character);
    }
}
