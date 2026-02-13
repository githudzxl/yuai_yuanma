using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Sources.Chat;
using Assets.Sources.Framework;
using Assets.Sources.Framework.System;
using Assets.Sources.Modules.Ui.Chat;
using EyeRecall.Helper;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
    // Token: 0x02000016 RID: 22
    internal static class ChatUtility
    {
        // Token: 0x06000078 RID: 120 RVA: 0x00006B24 File Offset: 0x00004D24
        // 发送服务器消息
        public static void SendServerMessage(string messageContent)
        {
            IPlaybackSystem chatJobSystem = ChatUtility.GetChatJobSystem();
            if (chatJobSystem == null)
            {
                Debug.LogWarning("ChatUtility: 无法获取 ChatJobSystem。可能无法发送消息。");
                return;
            }
            ChatInputData chatInputData = new ChatInputData
            {
                SenderInputContent = messageContent,
                SenderType = "battle_all",
                ReceiverName = string.Empty,
                ReceiverCid = string.Empty
            };
            chatJobSystem.ReflectInvokeMethod("SendChatInfo", new object[] { chatInputData });
            Debug.Log("ChatUtility: 已发送消息: \"" + messageContent + "\"");
        }

        // Token: 0x06000079 RID: 121 RVA: 0x00006BAC File Offset: 0x00004DAC
        // 获取聊天系统实例
        private static ChatJobSystem GetChatJobSystem()
        {
            GameModuleFeature instance = GameModuleFeature.Instance;
            if (instance == null)
            {
                return null;
            }
            // 使用显式类型参数调用ReflectField
            PlaybackSystem playbackSystem = instance.ReflectField<PlaybackSystem>("_playbackSystem");
            if (playbackSystem == null)
            {
                return null;
            }
            // 使用显式类型参数调用ReflectField
            List<IPlaybackSystem> list = playbackSystem.ReflectField<List<IPlaybackSystem>>("_systems");
            if (list == null)
            {
                return null;
            }
            return list.FirstOrDefault((IPlaybackSystem system) => system.GetType() == typeof(ChatJobSystem)) as ChatJobSystem;
        }

        // Token: 0x0200003F RID: 63
        // 编译器生成的辅助类，用于Lambda表达式
        [CompilerGenerated]
        [Serializable]
        private sealed class CompilerGeneratedClass
        {
            // Token: 0x060000EC RID: 236 RVA: 0x00009E53 File Offset: 0x00008053
            // Note: this type is marked as 'beforefieldinit'.
            static CompilerGeneratedClass()
            {
            }

            // Token: 0x060000ED RID: 237 RVA: 0x00009E5F File Offset: 0x0000805F
            public CompilerGeneratedClass()
            {
            }

            // Token: 0x060000EE RID: 238 RVA: 0x00009E67 File Offset: 0x00008067
            internal bool GetChatJobSystem_Lambda1(IPlaybackSystem system)
            {
                return system.GetType() == typeof(ChatJobSystem);
            }

            // Token: 0x040000FC RID: 252
            public static readonly ChatUtility.CompilerGeneratedClass Instance = new ChatUtility.CompilerGeneratedClass();

            // Token: 0x040000FD RID: 253
            public static Func<IPlaybackSystem, bool> GetChatJobSystem_Func1;
        }
    }
}

