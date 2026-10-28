using System;
using UnityEngine;

namespace Members.JJW._01.Code
{
    public class PlayerMoveJJW : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        [SerializeField] private float _moveSpeed = 5f;
        
        private Vector2 _xyMove;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
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
            Vector2 Velocity = _xyMove * _moveSpeed;
            RbCompo.linearVelocity = Velocity;
        }
    }
}
