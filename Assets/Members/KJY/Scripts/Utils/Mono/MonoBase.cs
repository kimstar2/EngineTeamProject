using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Utils.Mono
{
    public abstract class MonoBase<T> : MonoC
    {
        [field: SerializeField] protected bool CacheInAwake { get; private set; } = true;

        [field: HideIf("CacheInAwake")]
        [field: SerializeField] protected T compo;

        private void Awake()
        {
            MonoInit();
        }

        public virtual void MonoInit()
        {
            if (!CacheInAwake) return;
            compo = GetComponent<T>();
        }
    }
}
