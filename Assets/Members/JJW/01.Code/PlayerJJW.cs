using System;
using UnityEngine;

namespace Members.JJW._01.Code
{
    public class PlayerJJW : MonoBehaviour
    {
        private PlayerInputJJW _playerInputJjw;
        private PlayerMoveJJW _playerMoveJjw;

        private void Awake()
        {
            _playerInputJjw = GetComponent<PlayerInputJJW>();
            _playerMoveJjw = GetComponent<PlayerMoveJJW>();

            if (_playerInputJjw == null || _playerMoveJjw == null)
            {
                return;
            }
        }
        
        
        
        
    }

}

