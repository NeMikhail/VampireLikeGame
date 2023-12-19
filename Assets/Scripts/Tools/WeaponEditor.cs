using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Configs;
using Enums;
using MVP.Models.PassiveItemModels;

#if UNITY_EDITOR

namespace Tools
{
    [CustomEditor(typeof(WeaponScriptableObject))]
    public sealed class WeaponEditor : Editor
    {
        private WeaponScriptableObject _weaponAttributes;
        private List<bool> _showPosition = new();
        private GUIStyle _styleHeaderYellow = new GUIStyle();
        private GUIStyle _styleHeaderGreen = new GUIStyle();
        private GUIStyle _styleHeaderBoldWhite = new GUIStyle();
        private GUIStyle _styleTextArea = new GUIStyle();


        private void OnEnable()
        {
            _weaponAttributes = (WeaponScriptableObject)target;
        }

        public override void OnInspectorGUI()
        {
            GetStyles();
            serializedObject.Update();

            _weaponAttributes.Type = (WeaponScriptableObjectType)EditorGUILayout.EnumPopup("Type",
                _weaponAttributes.Type);
            EditorGUILayout.Space();

            if (_weaponAttributes.Type == WeaponScriptableObjectType.None)
            {
                return;
            }

            if (_weaponAttributes.Type == WeaponScriptableObjectType.ListWeapons)
            {
                EditorGUILayout.LabelField($"Editor of weapons ({_weaponAttributes.Items.Count})", _styleHeaderGreen);
            }

            if (_weaponAttributes.Type == WeaponScriptableObjectType.LevelUpConfig)
            {
                EditorGUILayout.LabelField($"Editor of LevelUp weapons ({_weaponAttributes.Items.Count})",
                    _styleHeaderGreen);
            }

            if (_weaponAttributes.Type == WeaponScriptableObjectType.SinergyWeapon)
            {
                EditorGUILayout.LabelField("Editor of Sinergy Weapon", _styleHeaderGreen);
            }

            _weaponAttributes.Name = EditorGUILayout.TextField("Name", _weaponAttributes.Name);
            _weaponAttributes.Description = EditorGUILayout.TextField("Description", _weaponAttributes.Description);

            EditorGUILayout.Space();
            Show(serializedObject.FindProperty("_items"));
            EditorUtility.SetDirty(_weaponAttributes);
            serializedObject.ApplyModifiedProperties();
        }

        public void Show(SerializedProperty list)
        {
            AddWeapon();
            EditorGUILayout.Space();
            for (int i = 0; i < list.arraySize; i++)
            {
                _showPosition.Add(false);
                _showPosition[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_showPosition[i], NameWeapon(i));

                if (_showPosition[i])
                {
                    EditorGUILayout.BeginVertical("box");
                    _weaponAttributes.Items[i].DisplayName = EditorGUILayout.TextField("DisplayName",
                        _weaponAttributes.Items[i].DisplayName);
                    _weaponAttributes.Items[i].Name = (WeaponsName)EditorGUILayout.EnumPopup("Name",
                        _weaponAttributes.Items[i].Name);
                    _weaponAttributes.Items[i].Icon = (Sprite)EditorGUILayout.ObjectField("Icon",
                        _weaponAttributes.Items[i].Icon, typeof(Sprite), true);

                    if (_weaponAttributes.Type == WeaponScriptableObjectType.ListWeapons)
                        _weaponAttributes.Items[i].LevelUpCfg = (WeaponScriptableObject)EditorGUILayout.
                            ObjectField("LevelUp Config", _weaponAttributes.Items[i].LevelUpCfg,
                            typeof(WeaponScriptableObject), true);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.LabelField("Description");
                    _weaponAttributes.Items[i].Description = EditorGUILayout.TextArea(_weaponAttributes.Items[i].
                        Description, _styleTextArea, GUILayout.Height(60));

                    EditorGUILayout.Space();
                    if (_weaponAttributes.Type == WeaponScriptableObjectType.ListWeapons)
                    {
                        _weaponAttributes.Items[i].IsSynergy = EditorGUILayout.BeginToggleGroup("Sinergy Enabled",
                            _weaponAttributes.Items[i].IsSynergy);

                        EditorGUI.indentLevel += 1;
                        EditorGUILayout.BeginVertical("box");

                        if (_weaponAttributes.Items[i].IsSynergy)
                        {
                            _weaponAttributes.Items[i].WeaponSynergy =(WeaponScriptableObject)EditorGUILayout.
                                ObjectField("Config Weapon Sinergy", _weaponAttributes.Items[i].WeaponSynergy,
                                typeof(WeaponScriptableObject), true);

                            ShowPassiveItemInfo(list.GetArrayElementAtIndex(i).FindPropertyRelative("_passiveForSynergy"),
                                _weaponAttributes.Items[i].PassiveForSynergy);

                            EditorGUILayout.BeginHorizontal();
                            if (GUILayout.Button("Add Passive"))
                            {
                                _weaponAttributes.Items[i].PassiveForSynergy.Add(new PassiveItemInfo());
                            }

                            if (GUILayout.Button("Delete Passive"))
                            {
                                if (_weaponAttributes.Items[i].PassiveForSynergy.Count > 0)
                                {
                                    _weaponAttributes.Items[i].PassiveForSynergy.RemoveAt(_weaponAttributes.Items[i].
                                        PassiveForSynergy.Count - 1);
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUI.indentLevel -= 1;
                        EditorGUILayout.EndToggleGroup();
                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Weapon Attribute", _styleHeaderYellow);
                    EditorGUI.indentLevel += 1;
                    EditorGUILayout.BeginVertical("box");
                    _weaponAttributes.Items[i].WeaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type",
                        _weaponAttributes.Items[i].WeaponType);
                    ShowWeaponAttribute(_weaponAttributes.Items[i]);
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel -= 1;

                    EditorGUILayout.LabelField("Projectile Attribute", _styleHeaderYellow);
                    EditorGUI.indentLevel += 1;
                    EditorGUILayout.BeginVertical("box");
                    ShowProjectileAttribute(_weaponAttributes.Items[i]);
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel -= 2;
                    DeleteWeapon(i);
                }
                else
                {
                    _showPosition[i] = false;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }

        private void ShowPassiveItemInfo(SerializedProperty list, List<PassiveItemInfo> listPasssive)
        {
            EditorGUILayout.LabelField($"Passive for Sinergy ({listPasssive.Count}):");

            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUI.indentLevel += 1;
                GUIContent gUIContent = new GUIContent($"{listPasssive[i].Name.ToString()} - LVL{listPasssive[i].Level}");
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), gUIContent);
                EditorGUI.indentLevel -= 1;
            }
        }

        private void ShowWeaponAttribute(WeaponScriptableObject.WeaponAttributes list)
        {
            if (list.WeaponType == WeaponType.None)
                return;

            list.Damage = EditorGUILayout.FloatField("Damage", list.Damage);
            list.Area = EditorGUILayout.FloatField("Area", list.Area);
            list.ChanceCritical = EditorGUILayout.Slider("Chance Critical", list.ChanceCritical, 0, 1);
            list.MultiplierCritical = EditorGUILayout.IntSlider("Multiplier Critical", list.MultiplierCritical, 0, 3);

            if (list.WeaponType == WeaponType.AreaAroundPlayer)
            {
                list.DamageReplayTime = EditorGUILayout.FloatField("Damage Replay Time", list.DamageReplayTime);
            }
            else
            {
                list.Cooldown = EditorGUILayout.FloatField("Cooldown", list.Cooldown);
            }
            if (_weaponAttributes.Type == WeaponScriptableObjectType.ListWeapons)
                list.Probability = EditorGUILayout.Slider("Probability", list.Probability, 0, 1);
        }

        private void ShowProjectileAttribute(WeaponScriptableObject.WeaponAttributes list)
        {
            if (list.WeaponType == WeaponType.None)
                return;

            EditorGUI.indentLevel += 1;
            list.Projectile = (GameObject)EditorGUILayout.ObjectField("Projectile", list.Projectile, typeof(GameObject),
                true);
            list.HitImpact = (GameObject)EditorGUILayout.ObjectField("HitImpact", list.HitImpact, typeof(GameObject),
                true);

            if (list.WeaponType == WeaponType.AreaAroundPlayer)
                return;

            if (list.WeaponType == WeaponType.ScatteredProjectile)
            {
                list.ProjectileSpeed = EditorGUILayout.FloatField("Speed", list.ProjectileSpeed);
                list.ProjectileMaxCount = EditorGUILayout.IntField("Max Count", list.ProjectileMaxCount);
                list.ProjectileDuration = EditorGUILayout.FloatField("Duration", list.ProjectileDuration);
                list.ProjectilePiercingMaxEnemies = EditorGUILayout.IntField("Max Damage Enemy",
                    list.ProjectilePiercingMaxEnemies);
            }

            if (list.WeaponType == WeaponType.RandomDamage || list.WeaponType == WeaponType.SinergyRandomDamage)
            {
                list.ProjectileInterval = EditorGUILayout.FloatField("Interval", list.ProjectileInterval);
                list.ProjectileMaxCount = EditorGUILayout.IntField("Max Count", list.ProjectileMaxCount);
                list.ProjectileDuration = EditorGUILayout.FloatField("Duration", list.ProjectileDuration);
            }

            if (list.WeaponType == WeaponType.RotatingProjectiles || list.WeaponType == WeaponType.WhipProjectiles)
            {
                list.ProjectileSpeed = EditorGUILayout.FloatField("Speed", list.ProjectileSpeed);
                list.ProjectileInterval = EditorGUILayout.FloatField("Interval", list.ProjectileInterval);
                list.ProjectileMaxCount = EditorGUILayout.IntField("Max Count", list.ProjectileMaxCount);
                list.ProjectileDuration = EditorGUILayout.FloatField("Duration", list.ProjectileDuration);
            }

            if (list.WeaponType == WeaponType.ClosestEnemyProjectiles || list.WeaponType == WeaponType.
                DirectionalProjectiles)
            {
                list.ProjectileSpeed = EditorGUILayout.FloatField("Speed", list.ProjectileSpeed);
                list.ProjectileInterval = EditorGUILayout.FloatField("Interval", list.ProjectileInterval);
                list.ProjectileMaxCount = EditorGUILayout.IntField("Max Count", list.ProjectileMaxCount);
                list.ProjectileDuration = EditorGUILayout.FloatField("Duration", list.ProjectileDuration);
                list.ProjectilePiercingMaxEnemies = EditorGUILayout.IntField("Max Damage Enemy",
                    list.ProjectilePiercingMaxEnemies);
            }

            if (list.WeaponType == WeaponType.BounceProjectiles || list.WeaponType == WeaponType.SinergyBounceProjectiles)
            {
                list.ProjectileSpeed = EditorGUILayout.FloatField("Speed", list.ProjectileSpeed);
                list.ProjectileInterval = EditorGUILayout.FloatField("Interval", list.ProjectileInterval);
                list.ProjectileMaxCount = EditorGUILayout.IntField("Max Count", list.ProjectileMaxCount);
                list.ProjectileDuration = EditorGUILayout.FloatField("Duration", list.ProjectileDuration);
                list.ProjectileMaxBounceCount = EditorGUILayout.IntField("Max Bounce", list.ProjectileMaxBounceCount);
            }

            list.IsKnockback = EditorGUILayout.Toggle("Is Knockback", list.IsKnockback);
            EditorGUI.indentLevel -= 1;
        }

        private void GetStyles()
        {
            _styleTextArea = EditorStyles.textArea;
            _styleHeaderYellow.fontStyle = FontStyle.Bold;
            _styleHeaderYellow.normal.textColor = Color.yellow;
            _styleHeaderGreen.fontStyle = FontStyle.Bold;
            _styleHeaderGreen.normal.textColor = Color.green;
            _styleHeaderBoldWhite.fontStyle = FontStyle.Bold;
            _styleHeaderBoldWhite.normal.textColor = Color.red;
            _styleTextArea.wordWrap = true;
            _styleTextArea.normal.textColor = Color.white;
        }

        private void DeleteWeapon(int index)
        {
            if (GUILayout.Button("Delete Weapon"))
            {
                if (_weaponAttributes.Items.Count > 0)
                {
                    if (EditorUtility.DisplayDialog($"Delete Weapon", "Are you sure you want to remove the weapon \n" +
                        $"\"{_weaponAttributes.Items[index].DisplayName}\"?", "Yes", "No"))
                    {
                        _weaponAttributes.Items.
                            RemoveAt(index);
                    }
                }
            }
        }

        private void AddWeapon()
        {
            if (_weaponAttributes.Type == WeaponScriptableObjectType.SinergyWeapon)
            {
                if (_weaponAttributes.Items.Count > 0)
                    return;
            }

            if (GUILayout.Button("Add Weapon"))
            {
                _weaponAttributes.Items.Add(new WeaponScriptableObject.WeaponAttributes());
                _weaponAttributes.Items[_weaponAttributes.Items.Count - 1].DisplayName = "New Weapon";
                _weaponAttributes.Items[_weaponAttributes.Items.Count - 1].Name = WeaponsName.None;
            }
        }

        private string NameWeapon(int index)
        {
            string text = "";
            if (_weaponAttributes.Type == WeaponScriptableObjectType.LevelUpConfig)
            {
                text = $" - LVL {index + 2}";
            }

            if (_weaponAttributes.Items[index].DisplayName == "")
            {
                return $"{_weaponAttributes.Items[index].Name.ToString()}{text}";
            }

            else
            {
                return $"{_weaponAttributes.Items[index].DisplayName}{text}";
            }
        }
    }
}

#endif