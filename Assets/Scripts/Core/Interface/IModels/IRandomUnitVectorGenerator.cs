using UnityEngine;

namespace Core.Interface.IModels
{
    internal interface IRandomUnitVectorGenerator
    {
        void Execute();
        Vector3 CreateUnitVector();
    }
}