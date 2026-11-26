using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Members.KJY.Scripts.Program.Entry
{
    public enum ProgramRenderOrder
    {
        BackProgramTask,
        FrontProgramTask
    }
    public abstract class ProgramEntry : ScriptableObject
    {
        [field: SerializeField] public ProgramType BaseProgram { get; protected set; }
        [field: SerializeField] public string ProgramName { get; protected set; }
        [field: SerializeField] public bool AwakeOpen { get; protected set; }
        [field: SerializeField] public bool UseMyPos { get; private set; }
        [field: ShowIf("UseMyPos")]
        [field: SerializeField] public Vector2 SpawnPos { get; private set; }

        [field: SerializeField] public ProgramRenderOrder RenderOrder { get; protected set; } = ProgramRenderOrder.BackProgramTask;

        [SerializeField] private bool overrideSize;
        [ShowIf(nameof(overrideSize)), SerializeField] private Vector2 startSize = new(400f, 300f);
        [ShowIf(nameof(overrideSize)), SerializeField] private Vector2 minSize = new(400f, 300f);
        [ShowIf(nameof(overrideSize)), SerializeField] private Vector2 maxSize = new(800f, 500f);

        [field:SerializeField] public bool IsSystemOverride { get; private set; }
        [ShowIf(nameof(IsSystemOverride)), SerializeField] private List<ProgramSystems> programSystems = new();

        [SerializeField] private bool overrideInstanceLimit;
        [ShowIf(nameof(overrideInstanceLimit)), SerializeField] private bool onlyOneProgram;

        public string GetProgramName(ProgramInfoSO info) => string.IsNullOrEmpty(ProgramName) ? info.ProgramName : ProgramName;
        public Vector2 GetStartSize(ProgramInfoSO info) => overrideSize ? startSize : info.StartSize;
        public Vector2 GetMinSize(ProgramInfoSO info) => overrideSize ? minSize : info.MinSize;
        public Vector2 GetMaxSize(ProgramInfoSO info) => overrideSize ? maxSize : info.MaxSize;
        public List<ProgramSystems> GetSystems(ProgramInfoSO info) => IsSystemOverride ? programSystems : info.ProgramSystems;

        public Object GetInstanceLimitKey(ProgramInfoSO info)
        {
            if (overrideInstanceLimit)
                return onlyOneProgram ? this : null;

            return info.OnlyOneProgram ? info : null;
        }

        public void SetSpawnPos(Vector2 position)
        {
            UseMyPos = true;
            SpawnPos = position;
        }
    }
}
