using UnityEngine;
using Core.EnemyService;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLocation", menuName = "Configs/Location/NewLocation")]
    internal sealed class LocationDescriptor : ScriptableObject
    {
        [field: SerializeField] public string LocationName { get; private set; }
        [field: SerializeField] public Sprite LocationImage { get; private set; }
        [field: SerializeField] public string LocationDescription { get; private set; }
        [field: SerializeField] public string SceneName { get; private set; }
        [field: SerializeField] public float DurationTime { get; private set; }
        [field: SerializeField] public int DestructableObjectsAmount { get; private set; }
        [field: SerializeField] public RespownModel EnemyRespawnModel { get; private set; }
        [field: SerializeField] public LocationModifiersConfig LocationModifiersConfig { get; private set; }
        [field: SerializeField] public LocationGeneratorConfig LocationGeneratorConfig { get; private set; }
        [field: SerializeField] public bool IsUnlocked { get; set; }
    }
}