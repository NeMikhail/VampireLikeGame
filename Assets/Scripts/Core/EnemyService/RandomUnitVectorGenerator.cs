using Core.Interface.IModels;
using UnityEngine;

namespace Core.EnemyService
{
    public class RandomUnitVectorGenerator : IRandomUnitVectorGenerator
    {
        public Vector3 CreateUnitVector()
        {
            Vector3 vertical = Random.insideUnitCircle.normalized;
            Vector3 horizontal = new Vector3(vertical.x,vertical.z, vertical.y );
            return horizontal;
        }

        public void Execute()
        {
        }
    }
}