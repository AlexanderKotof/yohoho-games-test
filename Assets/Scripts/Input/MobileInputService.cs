using Test.UI;
using UnityEngine;

namespace Test.Input
{
    public class MobileInputService : IInputService
    {
        public UIManager _manager;

        public MobileInputService(UIManager manager)
        {
            _manager = manager;
        }

        public Vector2 GetInput()
        {
            return _manager.Joystick.Input;
        }
    }
}