using UnityEngine;

namespace Test
{
    public class KeyboardInput : IInputService
    {
        public Vector2 GetInput()
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                return Vector2.zero;

            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
    }
}