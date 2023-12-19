using UnityEngine;

namespace Core.Interface
{
    public interface IPositionMaker
    {
        Vector3 GetPoint();
        Vector3[] GetPoints(int count);
    }
}