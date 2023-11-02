using UnityEngine;

namespace Test.Input
{
    public class KeyboardInputService : IInputService
    {
        public Vector2 GetInput()
        {
            if (UnityEngine.Input.GetAxis("Horizontal") == 0 && UnityEngine.Input.GetAxis("Vertical") == 0)
                return Vector2.zero;

            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical")).normalized;
        }
    }
}