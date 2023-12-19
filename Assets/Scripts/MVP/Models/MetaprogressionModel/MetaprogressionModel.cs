using System.Collections.Generic;
using Core.Interface.AchievementSystem;
using Core.Interface.IData;
using Core.Interface.IModels;
using Enums;

namespace MVP.Models.MetaprogressionModel
{
    internal class MetaprogressionModel : IMetaprogressionModel
    {
        private readonly IMetaprogressionLoader _loader;
        private readonly IMetaprogressionSaver _saver;
        
        private int _collectedCoins;
        private Dictionary<IAchievement, bool> _achievements;
        private Dictionary<CharacterName, bool> _characters;

        public int CollectedCoins => _collectedCoins;
        public IReadOnlyDictionary<IAchievement, bool> Achievements => _achievements;
        public IReadOnlyDictionary<CharacterName, bool> Characters => _characters;

    
        public MetaprogressionModel(IMetaprogressionLoader loader, IMetaprogressionSaver saver)
        {
            _loader = loader;
            _saver = saver;

            _collectedCoins = _loader.LoadCollectedCoins();
            _achievements = _loader.LoadAchivments();
            _characters = _loader.LoadCharacters();
        }


        public void AddCollctedCoins(int collectedCoins)
        {
            _collectedCoins += collectedCoins;
            _saver.SaveCollectedCoins(_collectedCoins);
            
        }

        public void UnlockAchivment(IAchievement achievement)
        {
            _achievements[achievement] = true;
            _saver.SaveUnlockedAchivment(achievement);
        }

        public void UnlockCharacter(CharacterName character)
        {
            _characters[character] = true;
            _saver.SaveUnlockedCharacter(character);
        }
        
    }
}
