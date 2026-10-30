using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Utils
{
    public class Toggler : MonoBehaviour , IToggleable
    {
        [SerializeField] private bool defaultToggle;
        [Header("Event Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private bool usingEvent;
        [ShowIf("usingEvent")]
        public UnityEvent onToggled , onUnToggled;

        public bool Toggled { get; private set; }

        private void Awake()
        {
            Toggled = defaultToggle;
        }

        public virtual void Toggle()
        {
            Toggled = !Toggled;

            if (!usingEvent) return;
            if (Toggled)
                onToggled?.Invoke();
            else
                onUnToggled?.Invoke();
        }
    }
}
