using System;
using UnityEngine;
using Audio;
using Audio.Service;
using Core.Interface;
using Core.Interface.ICollectable;
using Enums;

namespace MVP.Views.CollectableViews
{
    internal sealed class CollectableView : MonoBehaviour, IPoolObject<CollectableView>, ICollectable
    {
        [SerializeField] private GameObject _collectablePrefab;
        [SerializeField] private CollectableType _collectableType;

        public event Action<CollectableView> OnSpawnObject;
        public event Action<CollectableView> OnDeSpawnObject;

        public bool IsActive { get; set; }
        public GameObject CollectablePrefab
        {
            get { return _collectablePrefab; }
            set { _collectablePrefab = value; }
        }
        public CollectableType CollectableType
        {
            get => _collectableType;
            set => _collectableType = value;
        }
        public Transform Transform => this.transform;


        public void OnEnable()
        {
            OnSpawnObject?.Invoke(this);
        }

        public void OnDisable()
        {
            switch (_collectableType)
            {
                case CollectableType.GoldCoin:
                    PlayOneShot(AudioClipNames.Coins_Collected);
                    break;
                case CollectableType.BagOfCoins:
                    PlayOneShot(AudioClipNames.CoinsBag_Collected);
                    break;
                case CollectableType.BigBagOfCoins:
                    PlayOneShot(AudioClipNames.CoinsBagBig_Collected);
                    break;
                case CollectableType.CommonChest:
                    PlayOneShot(AudioClipNames.CommonChest_Collected);
                    break;
                case CollectableType.RareChest:
                    PlayOneShot(AudioClipNames.RareChest);
                    break;
                case CollectableType.EpicChest:
                    PlayOneShot(AudioClipNames.EpicChest_Collected);
                    break;
                case CollectableType.Healing:
                    PlayOneShot(AudioClipNames.Player_Healing);
                    break;
                default:
                    Debug.Log("No sound");
                    break;
            }
            OnDeSpawnObject?.Invoke(this);
        }
        
        private void PlayOneShot(AudioClipNames parameter)
        {
            AudioService.Instance.PlayAudioOneShot(parameter);
        }
    }
}