using Structs;
using System;
using UnityEngine;

namespace Core.Interface.IExperiences
{
    public interface IExperiencePickingUp
    {
        public event Action<TypeOfExperience> OnPickingUpExperience;
        public event Action<Transform> OnPickedSphere;
    }
}