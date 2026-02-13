using System;
using System.Reflection;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x0200001F RID: 31
	[Obfuscation(Exclude = true)]
	internal class SelfChatComponent : MonoBehaviour
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00008644 File Offset: 0x00006844
		private void Start()
		{
			if (CheatThread._misc.autoChatDelay == 0)
			{
				CheatThread._misc.autoChatDelay = 3100;
			}
			this.lastMessageTime = DateTime.Now - TimeSpan.FromMilliseconds((double)(CheatThread._misc.autoChatDelay + 1));
			Debug.Log("SelfChatComponent initialized.");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008698 File Offset: 0x00006898
		private void Update()
		{
			if (CheatThread._misc.autoChatEnable && DateTime.Now - this.lastMessageTime >= TimeSpan.FromMilliseconds((double)CheatThread._misc.autoChatDelay) && !string.IsNullOrEmpty(CheatThread._misc.autoChatMessage) && CheatThread._misc.autoChatMessage.Trim().Length > 0)
			{
				ChatUtility.SendServerMessage(CheatThread._misc.autoChatMessage);
				this.lastMessageTime = DateTime.Now;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000871A File Offset: 0x0000691A
		public SelfChatComponent()
		{
		}

		// Token: 0x04000088 RID: 136
		private DateTime lastMessageTime;
	}
}
