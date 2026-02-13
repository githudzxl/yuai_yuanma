using System;
using System.Collections.Generic;
using EyeRecall.Helper.Player;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x0200001E RID: 30
	internal class resolver
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00008472 File Offset: 0x00006672
		private static float getfakepitch(float pitch)
		{
			if (pitch <= -32f && pitch != -88.8f)
			{
				return 88.8f;
			}
			if (pitch < 32f || pitch == 88.8f)
			{
				return pitch;
			}
			return -88.8f;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000084A4 File Offset: 0x000066A4
		public static void run(PlayerEntity target)
		{
			if (target.IsMySelf() || updateData.own.GetTeam() == target.GetTeam() || target.IsDead() || target.isMyPlayer)
			{
				return;
			}
			if (CheatThread._rageBot.Resolver && (target == rage_silentbot.a_silent_target || target == rage_silentbot.h_silent_target))
			{
				float viewPitch = target.basicInfo.Current.ViewPitch;
				if (CheatThread._rageBot.a_resolovertype == 0)
				{
					target.basicInfo.Current.ViewPitch = resolver.getfakepitch(viewPitch);
					return;
				}
				if (CheatThread._rageBot.a_resolovertype == 1)
				{
					if (Input.GetKey(KeyCode.Mouse4) && viewPitch >= 32f)
					{
						target.basicInfo.Current.ViewPitch = -88.8f;
					}
					if (Input.GetKey(KeyCode.Mouse3) && viewPitch <= -32f)
					{
						target.basicInfo.Current.ViewPitch = 88.8f;
						return;
					}
				}
				else if (CheatThread._rageBot.a_resolovertype == 2)
				{
					int id = target.GetId();
					float viewPitch2 = target.basicInfo.Current.ViewPitch;
					float time = Time.time;
					float num = 0.01f;
					float num2;
					if (!resolver._lastExecutionTimes.TryGetValue(id, out num2) || time - num2 >= num)
					{
						resolver._lastExecutionTimes[id] = time;
						if (viewPitch2 >= 30f || viewPitch2 <= -30f)
						{
							target.basicInfo.Current.ViewPitch = -target.basicInfo.Current.ViewPitch;
						}
					}
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000862E File Offset: 0x0000682E
		public resolver()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008636 File Offset: 0x00006836
		// Note: this type is marked as 'beforefieldinit'.
		static resolver()
		{
		}

		// Token: 0x04000087 RID: 135
		public static Dictionary<int, float> _lastExecutionTimes = new Dictionary<int, float>();
	}
}
