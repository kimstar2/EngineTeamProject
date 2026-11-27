using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class WaitSequencer : MonoBehaviour
    {
        [SerializeField] private WaitSequenceGroup[] group;
        private CancellationTokenSource _cts;
        
        public void Play()
        {
            KillTask();
            _cts = new CancellationTokenSource();
            PlayAsync(_cts.Token).Forget();
        }
        
        private async UniTaskVoid PlayAsync(CancellationToken ct)
        {
            foreach (WaitSequenceGroup wsg in group)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(wsg.time), cancellationToken: ct);
                wsg.action?.Invoke();
            }
        }
        
        private void KillTask()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}