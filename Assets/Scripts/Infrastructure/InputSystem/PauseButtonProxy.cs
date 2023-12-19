using UnityEngine;
using Core.Interface.IInputs;


namespace Infrastructure.InputSystem
{
    internal class PauseButtonProxy : IUserButtonInputProxy
    {
        public bool GetButtonClick()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}