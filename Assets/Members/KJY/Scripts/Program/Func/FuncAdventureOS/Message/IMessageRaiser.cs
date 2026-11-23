using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public interface IMessageRaiser
    {
        public void Raise(PCData data, string message, Vector2 pos);
    }
}