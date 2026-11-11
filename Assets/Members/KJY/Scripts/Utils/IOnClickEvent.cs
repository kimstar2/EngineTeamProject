using System;

namespace Members.KJY.Scripts.Utils
{
    public interface IOnClickEvent
    {
        event Action<IOnClickEvent> OnClick;
    }
}