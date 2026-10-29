using System;
using UnityEngine;

namespace Members.JJW._01.Code
{
    public class PlayerJJW : MonoBehaviour
    {
        public PlayerMoveJJW MoveCompo { get; private set; }
        public PlayerInputJJW InputCompo { get; private set; }

        private void Awake()
        {
            InputCompo = GetComponent<PlayerInputJJW>();
            MoveCompo = GetComponent<PlayerMoveJJW>();
        }

        private void Update()
        {
            MoveCompo.SetMove(InputCompo.MoveDir);
        }
    }

}

