using UnityEngine;

namespace Members.JJW._01.Code
{
    public class MapRoadArea : MonoBehaviour
    {
        public BoxCollider2D AreaCollider { get; private set; }

        public Bounds WorldBounds => AreaCollider.bounds;

        private void Awake()
        {
            AreaCollider = GetComponent<BoxCollider2D>();
        }
    }
}

