using Core.Interface.IInputs;
using UnityEngine;


namespace Infrastructure.InputSystem
{
    internal class ZAxisProxy : IUserInputProxy
    {
        public float GetAxis()
        {
            return Input.GetAxis("Vertical");
        }
    }
}
