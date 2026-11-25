using Members.JJW._01.Code;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Members.KJY.Scripts.World
{
    public class WorldMap : MonoBehaviour
    {
        [field:SerializeField] public Camera MapCamera {get; private set;} 
        [field:SerializeField] public MapRoadArea[] Loads { get; private set; }
        [field:SerializeField] public Collider2D[] MapColliders { get; private set; }
        public RenderTexture CameraTexture { get; private set; }
        public bool IsSetTexture { get; private set; }
        public bool IsActive { get; private set; }
        
        [ContextMenu("Set Fields")]
        public void SetField()
        {
            MapCamera = GetComponentInChildren<Camera>();
            Loads = GetComponentsInChildren<MapRoadArea>();
            MapColliders = GetComponentsInChildren<Collider2D>();
            EditorUtility.SetDirty(this);
        }
        
        public void SetTexture(RenderTexture texture)
        {
            CameraTexture = texture;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}