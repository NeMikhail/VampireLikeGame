using UnityEngine;
using Core.Interface.IScriptable;
using Infrastructure.Extentions;
using Enums;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(PlayerScriptableConfig), menuName = "Configs/Player/PlayerScriptableConfig",
        order = 1)]
    internal sealed class PlayerScriptableConfig : ScriptableObject, IPlayerConfig
    {
        private const CharacterName DEFAULT_CHARACTER_NAME = CharacterName.Jangling;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private CharacterName _currentCharacterName;
        [SerializeField] private SerializableDictionary<CharacterName, CharacterConfig> _characters;

        public GameObject PlayerPrefab => _playerPrefab;
        public CharacterName CurrentCharacterName
        {
            get => _currentCharacterName;
            set
            {
                _currentCharacterName = value;
                ChangePrefab();
            } 
        }
        public SerializableDictionary<CharacterName, CharacterConfig> Characters => _characters;


        public void LockCharacters()
        {
            for(int i = 0; i < _characters.Length; i++)
            {
                _characters.GetValueByIndex(i).IsUnlocked = false;
            }
            _characters[DEFAULT_CHARACTER_NAME].IsUnlocked = true;
        }
        
        private void ChangePrefab()
        {
            if (CurrentCharacterName == CharacterName.None)
            {
                return;
            }
            _playerPrefab = _characters.GetValue(_currentCharacterName).PlayerPrefab;
        }
    }
}