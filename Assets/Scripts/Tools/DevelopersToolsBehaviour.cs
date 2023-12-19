using System;
using UnityEngine;
using Core.Interface.IPresenters;
using Core.Interface.IFactories;

#if UNITY_EDITOR
namespace Tools
{
    internal class DevelopersToolsBehaviour : MonoBehaviour
    {

        public event Action OnDisableEvent;
        
        private IDataProvider _dataProvider;
        private IDataFactory _dataFactory;

        public IDataProvider DataProvider
        {
            get => _dataProvider;
            set => _dataProvider = value;
        }

        public IDataFactory DataFactory
        {
            get => _dataFactory;
            set => _dataFactory = value;
        }

        private void OnDisable()
        {
            OnDisableEvent?.Invoke();
        }

    }
}
#endif