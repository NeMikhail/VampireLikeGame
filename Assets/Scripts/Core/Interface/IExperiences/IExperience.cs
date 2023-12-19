using UnityEngine;
using Structs;

namespace Core.Interface.IExperiences
{
    public interface IExperience
    {
        public TypeOfExperience TypeOfExperience { get; }
        public Rigidbody Rigidbody { get; }
        public GameObject ExperienceObject { get; }
    }
}