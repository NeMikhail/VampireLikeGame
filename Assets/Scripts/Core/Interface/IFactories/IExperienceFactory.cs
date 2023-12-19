using Core.Interface.IFeatures;
using UnityEngine;

namespace Core.Interface.IFactories
{
    internal interface IExperienceFactory 
    {
        IRespawnable Respawn(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}
