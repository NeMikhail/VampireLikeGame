using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IExperiences;

namespace Configs.PickingExperienceLists
{
    [CreateAssetMenu(fileName = nameof(PickingExperienceList), menuName = "Configs/PickingExperienceList/" +
        nameof(PickingExperienceList))]
    internal sealed class PickingExperienceList : ScriptableObject
    {
        public List<IExperience> experiences = new List<IExperience>();
    }
}