using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using MVP.Models.PassiveItemModels;
using MVP.Models.WeaponModels;

namespace Core.Interface.IModels
{
    public interface IPassiveItemModel : IModel
    {
        public bool IsActive { get; }

        public PassiveItemsName Name { get; }
        public Sprite Icon { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public List<PassiveBlock> Modifiers { get; }
        public int Level { get;}
        public int MaxLevel { get; }
        public float Probability { get; }
        public bool LevelUpInfo(out ItemInfo info);
        public void LevelUp();

        public event Action OnLevelUp;
    }
}