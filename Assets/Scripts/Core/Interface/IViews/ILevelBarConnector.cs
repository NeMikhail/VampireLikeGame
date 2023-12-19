using Structs;
using System;

namespace Core.Interface.IViews
{
    internal interface ILevelBarConnector
    {
        public event Action<int> OnLevelNumberChanged;
        public int CurrentLevelNumber { get; }
        public void AddValueExperience(TypeOfExperience value);
    }
}