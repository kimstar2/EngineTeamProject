using UnityEngine;
using UnityEngine.InputSystem;


namespace Members.JJW._01.Code
{
    public class PlayerInputJJW : MonoBehaviour
    {
        public Vector2 MoveDir { get; private set; }

        public void OnMove(InputValue value)
        {
            MoveDir = value.Get<Vector2>();
        }

    }
}
