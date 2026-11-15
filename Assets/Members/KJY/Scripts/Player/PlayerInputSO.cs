using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Player
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "KJY/Player Input", order = 0)]
    public class PlayerInputSO : ScriptableObject , Controls.IPlayerActions
    {
        private Controls _controls;
        
        public void SetEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }
        
        public void SetDisable() => _controls?.Player.Disable();
        
        public event Action OnAnyKeyPressed;
        public event Action OnAttackKeyPressed;
        public event Action OnAnyKeyUnPressed;
        public void OnMove(InputAction.CallbackContext context) { }

        public void OnLook(InputAction.CallbackContext context) { }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackKeyPressed?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context) { }

        public void OnCrouch(InputAction.CallbackContext context) { }

        public void OnJump(InputAction.CallbackContext context) { }

        public void OnPrevious(InputAction.CallbackContext context) { }

        public void OnNext(InputAction.CallbackContext context) { }

        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnAnykey(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAnyKeyPressed?.Invoke();
            if (context.canceled)
                OnAnyKeyUnPressed?.Invoke();
        }
    }
}