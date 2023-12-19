using UnityEngine;
using Audio.Service;
using Configs;
using Core.ResourceLoader;
using Core.SaveClass;
using Core.SaveClass.SaveModules;

namespace Core
{
    public sealed class NoGameMonoBeh : MonoBehaviour
    {
        [SerializeField] private ConfigLoader _configLoader;
        private ViewProvider _viewProvider;
        private MainMenuScreenLogick _mainMenuScreenLogick;
        private Presenters _presenters;


        private void Awake()
        {
            ResourceLoadManager.Init(_configLoader);
            var audioManager = FindObjectOfType<AudioService>();
            if (audioManager == null)
            {
                Instantiate(ResourceLoadManager.GetPrefabComponentOrGameObject<AudioService>("AudioService"));
            }
            Saver.Init(_configLoader);
            Saver.Load(new PlayerPrefsParserModule());
            _presenters = new Presenters();
        }

        private void Start()
        {
            _viewProvider = new ViewProvider();
            new MenuInitialisation(_viewProvider, _presenters);
            _presenters.Initialization();
            _mainMenuScreenLogick = new MainMenuScreenLogick(_viewProvider);
        }

        private void OnDisable()
        {
            _presenters.Cleanup();
            _mainMenuScreenLogick.Cleanup();
        }
    }
}