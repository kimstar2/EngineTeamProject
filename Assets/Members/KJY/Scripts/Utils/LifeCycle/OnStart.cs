using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Utils.LifeCycle
{
    public class OnStart : MonoBehaviour
    {
        [field:SerializeField] public UnityEvent OnEvent { get; private set; }

        private void Start()
        {
            OnEvent?.Invoke();
        }
    }
}