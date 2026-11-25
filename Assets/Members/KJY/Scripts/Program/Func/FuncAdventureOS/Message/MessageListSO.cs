using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    [CreateAssetMenu(fileName = "Message List", menuName = "KJY/Adventure OS/Message List", order = 0)]
    public class MessageListSO : ScriptableObject
    {
        [field:SerializeField] public MessageGroup[] MessageList { get; private set; }
        
        public void SetMessageList(MessageGroup[] msgList)
        {
            MessageList = msgList;
        }
    }
}