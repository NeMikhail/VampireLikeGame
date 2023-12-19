using UnityEngine;
using Configs;
using Enums;
using Infrastructure.Extentions;

namespace Core.Interface.IScriptable
{
    internal interface IPlayerConfig
    {
        public GameObject PlayerPrefab { get; }
        public CharacterName CurrentCharacterName { get; set; }
        public SerializableDictionary<CharacterName, CharacterConfig> Characters { get; }
    }
}