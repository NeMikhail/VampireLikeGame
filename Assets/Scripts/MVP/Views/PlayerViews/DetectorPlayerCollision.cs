using System;
using UnityEngine;
using Configs.PickingExperienceLists;
using Core.Interface.IExperiences;
using Core.ResourceLoader;
using Structs;

namespace MVP.Views.PlayerViews
{
    internal sealed class DetectorPlayerCollision : MonoBehaviour, IExperiencePickingUp, IExperiencePickedUp
    {
        public event Action<TypeOfExperience> OnPickingUpExperience;
        public event Action<Transform> OnPickedSphere;

        private PickingExperienceList _experienceList;


        private void Awake()
        {
            _experienceList = ResourceLoadManager.GetConfig<PickingExperienceList>();
        }

        private void OnTriggerEnter(Collider other)
        {
            bool success = other.gameObject.
                TryGetComponent<IExperience>(out var experience);

            if (success)
            {
                _experienceList.experiences.Add(experience);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            bool success = other.gameObject.
                TryGetComponent<IExperience>(out var experience);

            if (success)
            {
                experience.Rigidbody.velocity = Vector3.zero;
            }
        }

        public void ActivatePickUpActions(IExperience experience)
        {
            OnPickingUpExperience?.Invoke(experience.TypeOfExperience);
            OnPickedSphere?.Invoke(experience.ExperienceObject.transform);
        }
    }
}