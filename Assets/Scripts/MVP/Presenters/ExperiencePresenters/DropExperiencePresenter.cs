using System.Collections.Generic;
using UnityEngine;
using Audio;
using Audio.Service;
using Core;
using Core.Interface.IFeatures;
using Core.ResourceLoader;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.ExperienceViews;
using Structs;

namespace MVP.Presenters.ExperiencePresenters
{
    internal sealed class DropExperiencePresenter
    {
        private List<ExperienceView> _experienceViewList;
        private GamePoolPreset _experiencePoolPreset;

        public DropExperiencePresenter()
        {
            _experienceViewList = new List<ExperienceView>();
            _experiencePoolPreset = ResourceLoadManager.GetConfig<GamePoolPreset>(ConstantsProvider.
                EXPERIENCE_CONFIG_NAME);

            foreach (GamePoolPreset.GamePoolItem poolItem in _experiencePoolPreset.PoolItems)
            {
                var experienceView = poolItem.Prefab.GetComponent<ExperienceView>();
                _experienceViewList.Add(experienceView);
            }
        }


        public void DropExperience(IRespawnable respawnable)
        {
            switch (respawnable.TypeOfExperience)
            {
                case TypeOfExperience.Small:
                    DropSmallExperience(respawnable);
                    break;
                case TypeOfExperience.Medium:
                    DropMediumExperience(respawnable);
                    break;
                case TypeOfExperience.Large:
                    DropLargeExperience(respawnable);
                    break;
                default:
                    Debug.Log("No experience");
                    break;
            }
        }

        public void DropSmallExperience(IRespawnable respawnable)
        {
            ExperienceView exp = GamePool.Experience.Pool.Spawn(_experienceViewList[0].ExperienceObject);
            float objectHeight = exp.GetComponent<Renderer>().bounds.size.y;
            exp.transform.SetPositionAndRotation(respawnable.RespawnableTransform.position + new Vector3(0,
                objectHeight / 2f, 0), exp.transform.rotation);
            respawnable.OnDie -= DropSmallExperience;
        }

        public void DropMediumExperience(IRespawnable respawnable)
        {
            ExperienceView exp = GamePool.Experience.Pool.Spawn(_experienceViewList[1].ExperienceObject);
            float objectHeight = exp.GetComponent<Renderer>().bounds.size.y;
            exp.transform.SetPositionAndRotation(respawnable.RespawnableTransform.position + new Vector3(0,
                objectHeight / 2f, 0), exp.transform.rotation);
            respawnable.OnDie -= DropMediumExperience;
        }

        public void DropLargeExperience(IRespawnable respawnable)
        {
            ExperienceView exp = GamePool.Experience.Pool.Spawn(_experienceViewList[2].ExperienceObject);
            float objectHeight = exp.GetComponent<Renderer>().bounds.size.y;
            exp.transform.SetPositionAndRotation(respawnable.RespawnableTransform.position + new Vector3(0,
                objectHeight / 2f, 0), exp.transform.rotation);
            respawnable.OnDie -= DropLargeExperience;
        }

        public void PickupExperience(Transform transform)
        {
            AudioService.Instance.PlayAudioOneShot(AudioClipNames.Experience_Collected);
            GamePool.Experience.Pool.DeSpawn(transform);
        }
    }
}