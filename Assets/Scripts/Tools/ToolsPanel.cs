using UnityEngine;
using UnityEditor;
using Core;
using Core.Interface.IModels;
using Core.Interface.IPresenters;

#if UNITY_EDITOR

namespace Tools
{
    public sealed class ToolsPanel : EditorWindow
    {
        private const string CAPTION = "GVB tools";
        private const string ADD_WEAPON_BUTTON_TEXT = "Add weapon";
        private const string ADD_PASSIVE_BUTTON_TEXT = "Add passive";
        private const string SELECT_WEAPON_CAPTION = "Weapons to add:";
        private const string SELECT_PASSIVE_CAPTION = "Passives to add:";

        private const string SPEED_MULTIPLER_CAPTION = "SpeedMultiplier";
        private const string MAX_HP_MULTIPLER_CAPTION = "MaxHealthMultiplier";
        private const string REGENERATION_MULTIPLER_CAPTION = "RegenerationMultipler";
        private const string WEAPON_DAMAGE_MULTIPLER_CAPTION = "WeaponDamageMultiplier";
        private const string WEAPON_AREA_MULTIPLER_CAPTION = "WeaponAreaMultiplier";
        private const string WEAPON_ATTACK_SPEED_CAPTION = "WeaponAttackSpeedMultiplier";
        private const string EXPIRIENCE_ATTRACTION_CAPTION = "ExperienceAttractionArea";
        private const string REVIVE_COUNT_CAPTION = "RevivesCount";

        private const string CHANDGE_VALUE_BUTTON = "Change value";

        private DevelopersToolsBehaviour _developersTools;
        private IDataProvider _dataProvider;
        private IModifiersModel _modifiersModel;

        private string[] _weaponOptions = new string[0];
        private string[] _passiveOptions = new string[0];

        private float _speedMultiplier = 1.0f;
        private float _maxHealthMultiplier = 1.0f;
        private float _regenerationMultiplier = 0f;
        private float _weaponDamageMultiplier = 1.0f;
        private float _weaponAreaMultiplier = 1.0f;
        private float _weaponAttackSpeedMultiplier = 1.0f;
        private float _experienceAttractionArea = 1.0f;
        private float _revivesCount = 0f;

        private int _selectedWeaponIndex = -1;
        private int _selectedPassiveIndex = -1;

        private bool _isGameSceneResidentConnected;


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.Label(CAPTION);
            GUILayout.Space(20);

            _selectedWeaponIndex = EditorGUILayout.Popup(SELECT_WEAPON_CAPTION, _selectedWeaponIndex, _weaponOptions);
            if (GUILayout.Button(ADD_WEAPON_BUTTON_TEXT))
            {
                AddWeaponToPlayer();
            }

            GUILayout.Space(20);

            _selectedPassiveIndex = EditorGUILayout.Popup(SELECT_PASSIVE_CAPTION, _selectedPassiveIndex, _passiveOptions);
            if (GUILayout.Button(ADD_PASSIVE_BUTTON_TEXT))
            {
                AddPasssiveToPlayer();
            }

            GUILayout.Space(20);

            _speedMultiplier = EditorGUILayout.FloatField(SPEED_MULTIPLER_CAPTION, _speedMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeSpeedMultipler();
            }

            GUILayout.Space(10);

            _maxHealthMultiplier = EditorGUILayout.FloatField(MAX_HP_MULTIPLER_CAPTION, _maxHealthMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeMaxHealthMultipler();
            }

            GUILayout.Space(10);

            _regenerationMultiplier = EditorGUILayout.FloatField(REGENERATION_MULTIPLER_CAPTION, _regenerationMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeRegenerationMultipler();
            }

            GUILayout.Space(10);

            _weaponDamageMultiplier = EditorGUILayout.FloatField(WEAPON_DAMAGE_MULTIPLER_CAPTION, _weaponDamageMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeWeaponDamageMultipler();
            }

            GUILayout.Space(10);

            _weaponAreaMultiplier = EditorGUILayout.FloatField(WEAPON_AREA_MULTIPLER_CAPTION, _weaponAreaMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeWeaponAreaMultipler();
            }

            GUILayout.Space(10);

            _weaponAttackSpeedMultiplier = EditorGUILayout.FloatField(WEAPON_ATTACK_SPEED_CAPTION,
                _weaponAttackSpeedMultiplier);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeWeaponAttackSpeedMultipler();
            }

            GUILayout.Space(10);

            _experienceAttractionArea = EditorGUILayout.FloatField(EXPIRIENCE_ATTRACTION_CAPTION,
                _experienceAttractionArea);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeExperienceAttractionArea();
            }
            
            GUILayout.Space(10);

            _revivesCount = EditorGUILayout.FloatField(REVIVE_COUNT_CAPTION,
                _revivesCount);
            if (GUILayout.Button(CHANDGE_VALUE_BUTTON))
            {
                ChangeRevivesCount();
            }
        }

        private void OnFocus()
        {
            IDataProvider dataProvider = GetDataProvider();

            if (dataProvider != null)
            {
                GenerateWeaponOptionsList(dataProvider);
                GeneratePassiveOptionsList(dataProvider);
                LoadModifersValues(dataProvider);
            }
        }

        private void OnDisable()
        {
            GameSceneUnload();
        }

        #endregion

        [MenuItem("Tools/GvbTools/Show tools panel")]
        public static void ShowCustomTools()
        {
            GetWindow(typeof(ToolsPanel));
        }

        private void AddWeaponToPlayer()
        {
            IDataProvider dataProvider = GetDataProvider();

            if (dataProvider != null)
            {
                if (_selectedWeaponIndex >= 0)
                {
                    IPlayerModel playerModel = dataProvider.PlayerModel;
                    if (playerModel != null)
                    {
                        IWeaponModel weapon = dataProvider.GetWeaponModel(_selectedWeaponIndex);
                        playerModel.AddWeapon(weapon);
                    }
                }
            }
        }

        private void AddPasssiveToPlayer()
        {
            IDataProvider dataProvider = GetDataProvider();

            if (dataProvider != null)
            {
                if (_selectedPassiveIndex >= 0)
                {
                    IPlayerModel playerModel = dataProvider.PlayerModel;
                    if (playerModel != null)
                    {
                        IPassiveItemModel passive = dataProvider.GetPassiveItemModel(_selectedPassiveIndex);
                        playerModel.AddPassiveItem(passive);
                    }
                }
            }
        }

        private IDataProvider GetDataProvider()
        {
            IDataProvider provider = null;

            if (_isGameSceneResidentConnected)
            {
                provider = _dataProvider;
            }
            else
            {
                if (EditorApplication.isPlaying)
                {
                    var developersTools = FindObjectOfType<DevelopersToolsBehaviour>();

                    if (developersTools)
                    {
                        _developersTools = developersTools;
                        _developersTools.OnDisableEvent += GameSceneUnload;
                        _isGameSceneResidentConnected = true;

                        var mainMonoBeh = FindObjectOfType<MainMonoBeh>();

                        if (mainMonoBeh)
                        {
                            provider = mainMonoBeh.DataProvider;
                            _dataProvider = provider;
                        }
                    }
                }
            }

            return provider;
        }

        private void GenerateWeaponOptionsList(IDataProvider dataProvider)
        {
            int weaponsQuantity = dataProvider.GetCurrentWeponsListCount();
            _weaponOptions = new string[weaponsQuantity];

            for (int i = 0; i < weaponsQuantity; i++)
            {
                IWeaponModel model = dataProvider.GetWeaponModel(i);
                _weaponOptions[i] = model.Name.ToString();
            }
        }

        private void GeneratePassiveOptionsList(IDataProvider dataProvider)
        {
            int passiveQuantity = dataProvider.GetPassiveItemsListCount();
            _passiveOptions = new string[passiveQuantity];

            for (int i = 0; i < passiveQuantity; i++)
            {
                IPassiveItemModel model = dataProvider.GetPassiveItemModel(i);
                _passiveOptions[i] = model.Name.ToString();
            }
        }

        private void LoadModifersValues(IDataProvider dataProvider)
        {
            IModifiersModel model;

            if (_modifiersModel == null)
            {
                model = dataProvider.ModifiersModel;
                _modifiersModel = model;
                SubscribeToModifers();
            }
            else
            {
                model = _modifiersModel;
            }

            if (model != null)
            {
                _speedMultiplier = model.SpeedMultiplier;
                _maxHealthMultiplier = model.MaxHealthMultiplier;
                _regenerationMultiplier = model.Regeneration;
                _weaponDamageMultiplier = model.WeaponDamageMultiplier;
                _weaponAreaMultiplier = model.WeaponAreaMultiplier;
                _weaponAttackSpeedMultiplier = model.ProjectileSpeedMultiplier;
                _experienceAttractionArea = model.ExperienceAttractionArea;
                _revivesCount = model.RevivesCount;
            }
        }

        private void SubscribeToModifers()
        {
            _modifiersModel.SpeedMultiplierChanged += ModifersChanged;
            _modifiersModel.MaxHealthMultiplierChanged += ModifersChanged;
            _modifiersModel.RegenerationChanged += ModifersChanged;
            _modifiersModel.DamageMultiplierChanged += ModifersChanged;
            _modifiersModel.AreaMultiplierChanged += ModifersChanged;
            _modifiersModel.ProjectileSpeedMultiplierChanged += ModifersChanged;
            _modifiersModel.ExperienceAttractionAreaChanged += ModifersChanged;
            _modifiersModel.RevivesCountChanged += ModifersChanged;
        }

        private void ModifersChanged(float modifier)
        {
            if (_dataProvider != null)
            {
                LoadModifersValues(_dataProvider);
                Repaint();
            }
        }

        private void ChangeSpeedMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.SpeedMultiplier = _speedMultiplier;
                }
            }
        }

        private void ChangeMaxHealthMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.MaxHealthMultiplier = _maxHealthMultiplier;
                }
            }
        }

        private void ChangeRegenerationMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.Regeneration = _regenerationMultiplier;
                }
            }
        }

        private void ChangeWeaponDamageMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.WeaponDamageMultiplier = _weaponDamageMultiplier;
                }
            }
        }

        private void ChangeWeaponAreaMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.WeaponAreaMultiplier = _weaponAreaMultiplier;
                }
            }
        }

        private void ChangeWeaponAttackSpeedMultipler()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.ProjectileSpeedMultiplier = _weaponAttackSpeedMultiplier;
                }
            }
        }

        private void ChangeExperienceAttractionArea()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.ExperienceAttractionArea = _experienceAttractionArea;
                }
            }
        }
        private void ChangeRevivesCount()
        {
            IDataProvider datas = GetDataProvider();

            if (datas != null)
            {
                IModifiersModel model = datas.ModifiersModel;

                if (model != null)
                {
                    model.RevivesCount = _revivesCount;
                }
            }
        }

        private void GameSceneUnload()
        {
            if (_isGameSceneResidentConnected)
            {
                if (_modifiersModel != null)
                {
                    UnSubscribeToModifers();
                    _modifiersModel = null;
                }

                _developersTools.OnDisableEvent -= GameSceneUnload;
                _developersTools = null;
                _isGameSceneResidentConnected = false;
                _dataProvider = null;
            }
        }

        private void UnSubscribeToModifers()
        {
            _modifiersModel.SpeedMultiplierChanged -= ModifersChanged;
            _modifiersModel.MaxHealthMultiplierChanged -= ModifersChanged;
            _modifiersModel.RegenerationChanged -= ModifersChanged;
            _modifiersModel.DamageMultiplierChanged -= ModifersChanged;
            _modifiersModel.AreaMultiplierChanged -= ModifersChanged;
            _modifiersModel.ProjectileSpeedMultiplierChanged -= ModifersChanged;
            _modifiersModel.ExperienceAttractionAreaChanged -= ModifersChanged;
        }
    }
}

#endif