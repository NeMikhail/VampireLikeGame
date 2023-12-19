using System;
using UnityEngine;

namespace Core.Interface.IExperiences
{
    public interface IExperiencePickedUp
    {
        public event Action<Transform> OnPickedSphere;
    }
}