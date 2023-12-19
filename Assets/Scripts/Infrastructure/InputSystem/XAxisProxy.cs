using Core.Interface.IInputs;
using UnityEngine;

namespace Infrastructure.InputSystem
{
    internal class XAxisProxy : IUserInputProxy
    {
        public float GetAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}