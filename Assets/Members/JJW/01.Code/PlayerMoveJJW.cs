using System;
using UnityEngine;

namespace Members.JJW._01.Code
{
    public class PlayerMoveJJW : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        [SerializeField] private float _moveSpeed = 5f;
        
        private Vector2 _xyMove;
        private PlayerWindowBoundsJJW _windowBounds;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
            _windowBounds = GetComponent<PlayerWindowBoundsJJW>();
        }

        public void SetMove(Vector2 xyMove)
        {
            _xyMove = xyMove;
        }

        private void FixedUpdate()
        {
            PlayerMove();
        }

        private void PlayerMove()
        {
            Vector2 velocity = _xyMove * _moveSpeed;

            if (_windowBounds != null)
            {
                velocity = _windowBounds.LimitVelocity(velocity, Time.fixedDeltaTime);
            }

            RbCompo.linearVelocity = velocity;
        }
    }
}
