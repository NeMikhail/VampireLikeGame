using System;
using UnityEngine;
using MVP.Models.EnemyModels;
using MVP.Views.PlayerViews;
using Structs;

namespace Core.Interface.IFeatures
{
    internal interface IRespawnable
    {
        public event Action<IRespawnable> OnDie;
        public event Action<IRespawnable> OnRemove;
        public Transform RespawnableTransform { get; }
        public TypeOfExperience TypeOfExperience { get; }
        public void Init(PlayerView player, EnemyModel enemyModel);
        public void FixedExecute(float deltaTime);
        public void Remove();
    }
}