using UnityEngine;
using Configs;
using Core;
using Core.ResourceLoader;

namespace UI
{
    internal sealed class UiStageSelectLogick
    {
        private const int NONE_SELECTED = -1;
		private const string LOCATION_NOT_OPEN = "Локация еще не найдена";

        private Transform _content;
        private UiStageDescriptionPanel[] _panels;
        private LocationDescriptor[] _locationDescriptors;
        private LocationsPack _locationPack;
        private int _selectedPanel = NONE_SELECTED;
        private bool _isLocationListCreated;
		
        public UiStageSelectLogick(Transform content)
        {
            _content = content;
        }


        public void CreateLocationList()
        {
            if (!_isLocationListCreated)
            {
                _isLocationListCreated = true;
                _locationPack = ResourceLoadManager.GetConfig<LocationsPack>(string.Empty);

                if (_locationPack)
                {
                    UiStageDescriptionPanel prefab = ResourceLoadManager.
                        GetPrefabComponentOrGameObject<UiStageDescriptionPanel>(ConstantsProvider.STAGE_DESCRIPTION_PREFAB_NAME);
                    _locationDescriptors = _locationPack.LocationDescriptors;
                    _panels = new UiStageDescriptionPanel[_locationDescriptors.Length];

                    for (int i = 0; i < _locationDescriptors.Length; i++)
                    {
                        LocationDescriptor descriptor = _locationDescriptors[i];
                        UiStageDescriptionPanel panel = GameObject.Instantiate(prefab, _content);
                        _panels[i] = panel;
                        panel.IsSelected = false;
                        panel.Index = i;
                        panel.LocationName.text = descriptor.LocationName;
                        panel.LocationDescription.text = LOCATION_NOT_OPEN;
                        panel.LocationImage.sprite = descriptor.LocationImage;
                        if (descriptor.IsUnlocked == true)
                        {
                            panel.PointerClick += OnPanelClick;
                            panel.LocationDescription.text = descriptor.LocationDescription;
                        }
                    }
                }
            }
        }

        public string GetSelectedLocation()
        {
            string sceneName = null;

            if (_selectedPanel != NONE_SELECTED)
            {
                sceneName = _locationDescriptors[_selectedPanel].SceneName;
                AssignSelectedLocation(sceneName);
            }

            return sceneName;
        }

        private void OnPanelClick(int index)
        {
            if (index == _selectedPanel)
            {
                _panels[_selectedPanel].IsSelected = false;
                _selectedPanel = NONE_SELECTED;
            }
            else
            {
                if (_selectedPanel >= 0)
                {
                    _panels[_selectedPanel].IsSelected = false;
                }

                _selectedPanel = index;
                _panels[_selectedPanel].IsSelected = true;
            }
        }

        private void AssignSelectedLocation(string sceneName)
        {
            for (int i = 0; i < _locationDescriptors.Length; i++)
            {
                if (_locationDescriptors[i].SceneName == sceneName)
                {
                    _locationPack.CurrentLocation = _locationDescriptors[i];
                    _locationPack.HasBeenChanged = true;
                }
            }
        }

        public void Cleanup()
        {
            for(int i = 0; i < _panels.Length; i++)
            {
                if (_panels[i].LocationDescription.text != LOCATION_NOT_OPEN)
                {
                    _panels[i].PointerClick -= OnPanelClick;
                }
            }
        }
    }
}