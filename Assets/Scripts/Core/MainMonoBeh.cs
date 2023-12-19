using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Configs;
using Audio;
using Audio.Service;
using Core.Interface;
using Core.Interface.IFactories;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Infrastructure.PoolSystems.Pool;
using Infrastructure.TimeRemaningService;

namespace Core
{
    public sealed class MainMonoBeh : MonoBehaviour
    {
        [SerializeField] private ConfigLoader _configLoader;
        private Presenters _presenters;
        private IDataProvider _dataProvider;
        private IDataFactory _dataFactory;
        private IViewProvider _viewProvider;

#if UNITY_EDITOR

        internal IDataProvider DataProvider
        {
            get => _dataProvider;
        }

#endif


        void Start()
        {
            ResourceLoadManager.Init(_configLoader);
            _presenters = new Presenters();
            TimeRemainingController timeRemainingController = new TimeRemainingController();
            _presenters.Add(timeRemainingController);
            _viewProvider = new ViewProvider();
            _dataProvider = new DataProvider();
            _dataFactory = new DataFactory(_dataProvider);
            _dataFactory.InitialiseData();
            var gameStateFactory = new GameStateFactory(_presenters, _dataProvider, _viewProvider, _dataFactory);
            new GameInitialisation(gameStateFactory, _viewProvider);
            _presenters.Initialization();
            GamePool.Init();
            InitMainMusic();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            _presenters.Execute(deltaTime);
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            _presenters.FixedExecute(fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _presenters.Cleanup();
            GamePool.Clear();
        }

        private void InitMainMusic()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            switch (currentScene)
            {
                case 1:
                    AudioService.Instance.SwapPlayMusic(AudioClipNames.Level1_Music, 2f, 1).Forget();
                    break;
                case 2:
                    AudioService.Instance.SwapPlayMusic(AudioClipNames.Level2_Music, 2f, 1).Forget();
                    break;
                case 3:
                    AudioService.Instance.SwapPlayMusic(AudioClipNames.Level3_Music, 2f, 1).Forget();
                    break;
                default:
                    Debug.Log("No audio resource");
                    break;
            }
        }
    }
}