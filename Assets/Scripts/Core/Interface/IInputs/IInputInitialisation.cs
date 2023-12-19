using System;
using UnityEngine;

namespace Core.Interface.IInputs
{
    internal interface IInputInitialisation
    {
        event Action OnPauseActivated;
        void CheckPauseActivation();
        Vector3 GetInput();
    }
}