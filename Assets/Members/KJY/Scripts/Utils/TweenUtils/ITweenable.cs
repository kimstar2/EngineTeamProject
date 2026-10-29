using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Members.KJY.Scripts.Utils.TweenUtils
{
    public interface ITweenable
    {
        Tween CurrentTween { get; }
        void ExecuteTween();
    }
}
