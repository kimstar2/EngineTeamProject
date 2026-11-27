using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class Instantiator : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;

        public void Instantiate()
        {
            prefab = Instantiate(prefab, parent);
        }
    }
}