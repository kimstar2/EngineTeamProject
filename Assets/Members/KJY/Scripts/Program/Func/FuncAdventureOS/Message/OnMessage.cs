using System;
using System.Collections.Generic;
using _TevLib.CoreLib.EventSystem;
using _TevLib.SoundSystem.Runtime;
using Members.KJY.Scripts.Player;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class OnMessage : GameEvent
    {
        public readonly AnimatorController AnimCon;
        public readonly MessageListSO MessageGroup;
        public readonly Vector2 Position;
        public readonly Dictionary<MessageEventSO,List<Action<ProgramController>>> EventDict;
        public readonly List<ProgramSystems> Systems;
        public readonly SoundClipSO[] SoundClips;
        
        public OnMessage(AnimatorController animCon,MessageListSO msg, Vector2 pos, Dictionary<MessageEventSO,List<Action<ProgramController>>> eventDict, SoundClipSO[] soundClips)
        {
            AnimCon = animCon;
            MessageGroup = msg;
            Position = pos;
            EventDict = eventDict;
            SoundClips = soundClips;
        }
        
        public OnMessage(AnimatorController animCon,MessageListSO msg, Vector2 pos, Dictionary<MessageEventSO,List<Action<ProgramController>>> eventDict,List<ProgramSystems> systems, SoundClipSO[] soundClips)
        {
            AnimCon = animCon;
            MessageGroup = msg;
            Position = pos;
            EventDict = eventDict;
            Systems = systems;
            SoundClips = soundClips;
        }

    }
}