using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyList), menuName = "Configs/Enemy/" + nameof(EnemyList))]
    internal class EnemyList : ScriptableObject
    {
        [Serializable]
        public class Enemy
        {
            public EnemyType EnemyType;
            public GameObject Prefab;
            public EnemyScriptableObject EnemyScriptableObject;
        }

        [FormerlySerializedAs("enemyList")]
        public List<Enemy> EnemyListConfigs = new List<Enemy>();

        public GameObject GetPrefab(EnemyType enemyType)
        {
            var length = EnemyListConfigs.Count;
            for (int i = 0; i < length; i++)
            {
                if (EnemyListConfigs[i].EnemyType == enemyType)
                {
                    return EnemyListConfigs[i].Prefab;
                }
            }

            throw new Exception("enemy type \"" + enemyType + "\" not found");
        }

        public EnemyScriptableObject GetEnemyScriptableObject(EnemyType enemyType)
        {
            var length = EnemyListConfigs.Count;
            for (int i = 0; i < length; i++)
            {
                if (EnemyListConfigs[i].EnemyType == enemyType)
                {
                    return EnemyListConfigs[i].EnemyScriptableObject;
                }
            }

            throw new Exception("enemy type \"" + enemyType + "\" not found");
        }
    }
}