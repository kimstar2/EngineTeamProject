using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Utils.LifeCycle
{
    public class OnAwake : MonoBehaviour
    {
        [field:SerializeField] public UnityEvent OnEvent { get; private set; }

        private void Awake()
        {
            OnEvent?.Invoke();
        }
    }
}