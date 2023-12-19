using UnityEngine;
using Core.Interface;

namespace Core.LocationObjectsSystem
{
    public class RandomFloatPositionMaker : IPositionMaker
    {
        private readonly Vector3 _minPosition;
        private readonly Vector3 _maxPosition;

        public RandomFloatPositionMaker(Vector3 minPosition, Vector3 maxPosition)
        {
            _minPosition = minPosition;
            _maxPosition = maxPosition;
        }
        

        public Vector3 GetPoint()
        {
            float posX = Random.Range(_minPosition.x, _maxPosition.x);
            float posZ = Random.Range(_minPosition.z, _maxPosition.z);
            return new Vector3(posX, 0, posZ);
        }

        public Vector3[] GetPoints(int count)
        {
            var positions = new Vector3[count];
            for(var i = 0; i < count; i++)
                positions[i] = GetPoint();
            
            return positions;
        }
    }
}