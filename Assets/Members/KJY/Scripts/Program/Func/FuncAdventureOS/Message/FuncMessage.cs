using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class FuncMessage : ProgramFunc
    {
        [SerializeField] private MonoImage characterIcon;
        [SerializeField] private MonoTMP_UGUI characterName;
        [SerializeField] private MonoTMP_UGUI messageTmp;

        public void SetData(MessageEntry entry)
        {
            characterIcon.SetImage(entry.PcData.CharacterSprite);
            characterName.SetText(entry.PcData.CharacterName);
            messageTmp.SetText(entry.Message);
        }
    }
}