using Enums;
using System.Collections.Generic;
using Core.Interface.AchievementSystem;

namespace Core.Interface.IData
{
    internal interface IMetaprogressionLoader
    {
        int LoadCollectedCoins();
        Dictionary<IAchievement, bool> LoadAchivments();
        Dictionary<CharacterName, bool> LoadCharacters();
    }
}
