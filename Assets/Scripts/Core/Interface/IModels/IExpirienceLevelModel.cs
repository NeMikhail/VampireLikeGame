using Core.Interface.IViews;
using System;

namespace Core.Interface.IModels
{
    internal interface IExpirienceLevelModel : ILevelBarConnector, IResetable, IModel
    {
        public event Action<int> OnLevelValueChange;
        public event Action<int, int> OnFirstLevelUpdateExperience;

        public void Initialize();
    }
}