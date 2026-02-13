using System;
using Assets.Sources.Utils.Weapon;
using Entitas;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x0200001A RID: 26
	internal class misc_tiggerbot
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00006F94 File Offset: 0x00005194
		private static int AimOrNot()
		{
			Vector3 vector = positionHelper.World2EntityPos(Camera.main.transform.forward);
			return FireUtility.BulletTraceNormal(Contexts.sharedInstance.battleRoom.pyEngine.PyEngine, updateData.own, 10000000f, new Vector3(vector.x, vector.y, vector.z), new float[3], new float[3]).EntityId;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007004 File Offset: 0x00005204
		internal static void Execute()
		{
			try
			{
				int num = misc_tiggerbot.AimOrNot();
				if (num > 0)
				{
					foreach (IEntity entity in updateData.EntityList)
					{
						PlayerEntity playerEntity = (PlayerEntity)entity;
						if (CheatThread._misc.tiggerBot && playerEntity.GetId() == num)
						{
							if (playerEntity.GetTeam() == updateData.own.GetTeam() || playerEntity.IsDead() || !(DateTime.Now - misc_tiggerbot.LastTime >= TimeSpan.FromMilliseconds((double)CheatThread._misc.tiggerBotDelayedTime)))
							{
								break;
							}
							if (!misc_tiggerbot.isDelaying)
							{
								misc_tiggerbot.isDelaying = true;
								misc_tiggerbot.LastTime = DateTime.Now;
								break;
							}
							if (misc_tiggerbot.isDelaying)
							{
								FakeInput.ForceMouse(0, FakeInput.InputST.TrueOnce);
								misc_tiggerbot.LastTime = DateTime.Now;
								misc_tiggerbot.isDelaying = false;
								break;
							}
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000710C File Offset: 0x0000530C
		public misc_tiggerbot()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00007114 File Offset: 0x00005314
		// Note: this type is marked as 'beforefieldinit'.
		static misc_tiggerbot()
		{
		}

		// Token: 0x04000076 RID: 118
		private static bool isDelaying = false;

		// Token: 0x04000077 RID: 119
		private static DateTime LastTime = DateTime.Now;
	}
}
