using System;
using UnityEngine;
using Core.Interface;
using Core.Interface.IExperiences;
using Structs;

namespace MVP.Views.ExperienceViews
{
    internal sealed class ExperienceView : MonoBehaviour, IExperience, IPoolObject<ExperienceView>
    {
        [SerializeField] private TypeOfExperience _typeOfExperience;
        [SerializeField] private Rigidbody _rigidbody;

        public bool IsActive { get; set; }

        public event Action<ExperienceView> OnSpawnObject;
        public event Action<ExperienceView> OnDeSpawnObject;

        public TypeOfExperience TypeOfExperience => _typeOfExperience;
        public Rigidbody Rigidbody => _rigidbody;
        public GameObject ExperienceObject => gameObject;


        public void OnEnable()
        {
            OnSpawnObject?.Invoke(this);
        }

        public void OnDisable()
        {
            OnDeSpawnObject?.Invoke(this);
        }
    }
}